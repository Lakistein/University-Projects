using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using GAPT.Models;
using GAPT.ViewModel;

namespace GAPT.Controllers
{
    public class ShoppingCartController : Controller
    {
        private GAPTContext _db = new GAPTContext();

        public async Task<ActionResult> Index()
        {
            var cart = ShoppingCart.GetCart(_db, this);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = await cart.GetTotal(),
                Ids = new List<List<int>> ()
            };
            viewModel.Items = new List<List<Item>>(viewModel.CartItems.Count);
            for (int i = 0; i < viewModel.Items.Capacity; i++)
            {
                viewModel.Items.Add(new List<Item>());
                viewModel.Ids.Add(new List<int>());
            }

            int count = -1;
            foreach (Cart t in viewModel.CartItems)
            {
                count++;
                if(!t.ShoppingCartItem.IsOutfit)
                    continue;

                var itemIds = (from n in _db.ShoppingCartOutfitItems
                    where n.CartRecordId == t.RecordId
                    select new { Id = n.Id, ItemId = n.ItemId}).ToList();

                foreach(var id in itemIds)
                {
                    viewModel.Items[count].Add(_db.Items.Find(id.ItemId));
                    viewModel.Ids[count].Add(id.Id);
                }
            }
            return View(viewModel);
        }

        public async Task<ActionResult> RemoveItemFromOutfit(int id)
        {
            ShoppingCartOutfitItems s = _db.ShoppingCartOutfitItems.Find(id);
            s.ShoppingCartItem.ApplyDiscount = false;
            _db.ShoppingCartOutfitItems.Remove(s);
            _db.SaveChanges();

            if(_db.ShoppingCartOutfitItems.Where(x => x.CartRecordId == s.CartRecordId).ToList().Count == 0)
            {
                _db.Carts.Remove(_db.Carts.Find(s.CartRecordId));
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddProduct(int id, string type)
        {
            ShoppingCartItem product = new ShoppingCartItem();

            bool isOutfit = type.Equals("Outfit");
            var cart = ShoppingCart.GetCart(_db, this);
            if(!_db.ShoppingCartItems.Any(x => x.ItemId == id && x.IsOutfit == isOutfit))
            {
                if(isOutfit)
                {
                    Outfit o = await _db.Outfits.SingleOrDefaultAsync(a => a.Id == id);
                    if(o == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    product.Name = o.Name;
                    product.Price = o.Price;
                    product.IsOutfit = true;
                    product.ApplyDiscount = true;
                    product.Discount = (decimal)o.Discount;
                }
                else
                {
                    Item o = await _db.Items.SingleOrDefaultAsync(a => a.Id == id);
                    if(o == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    product.Name = o.Name;
                    product.Price = o.Price;
                }

                product.ItemId = id;
                _db.ShoppingCartItems.Add(product);
                await _db.SaveChangesAsync();

                await cart.AddToCart(product);
            }
            else
            {
                await cart.AddToCart(_db.ShoppingCartItems.SingleOrDefault(x => x.ItemId == id && x.IsOutfit == isOutfit));
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // AJAX: /ShoppingCart/RemoveProduct/5
        [HttpPost]
        public async Task<ActionResult> RemoveProduct(int id)
        {
            var cart = ShoppingCart.GetCart(_db, this);

            var productName = await _db.Carts
                .Where(i => i.RecordId == id)
                .Select(i => i.ShoppingCartItem.Name)
                .SingleOrDefaultAsync();

            var itemCount = await cart.RemoveFromCart(id);

            await _db.SaveChangesAsync();

            var removed = (itemCount > 0) ? " 1 copy of " : string.Empty;

            var results = new ShoppingCartRemoveViewModel
            {
                Message = removed + productName + " has been removed from your shopping cart.",
                CartTotal = await cart.GetTotal(),
                CartCount = await cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(_db, this);

            var cartItems = cart.GetCartItems()
                .Select(a => a.ShoppingCartItem.Name)
                .OrderBy(x => x)
                .ToList();

            ViewBag.CartCount = cartItems.Count();
            ViewBag.CartSummary = string.Join("\n", cartItems.Distinct());

            return PartialView("CartSummary");
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}