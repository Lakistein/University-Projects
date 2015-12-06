using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GAPT.Models
{
    public class ShoppingCart
    {
        public const string CartSessionKey = "CartId";

        private readonly GAPTContext _db;
        private readonly string _cartId;

        private ShoppingCart(GAPTContext db, string cartId)
        {
            _db = db;
            _cartId = cartId;
        }

        public static ShoppingCart GetCart(GAPTContext storeContext, Controller controller)
        {
            return new ShoppingCart(storeContext, GetCartId(controller.HttpContext));
        }

        private static string GetCartId(HttpContextBase context)
        {
            if(context.Session[CartSessionKey] == null)
            {
                var username = context.User.Identity.Name;

                context.Session[CartSessionKey] = !string.IsNullOrWhiteSpace(username)
                    ? username
                    : Guid.NewGuid().ToString();
            }

            return context.Session[CartSessionKey].ToString();
        }

        public async Task AddToCart(ShoppingCartItem item)
        {
            //TODO: proveri da li u cart ima neki item koji je isti i ima sve iteme, ako nema dodaj nov item u korpu, ako ima samo mu povecaj broj

            var cartItems = _db.Carts.Where(
                c => c.CartId == _cartId && c.ShoppingCartItemId == item.Id);

            Cart cartItem = null;

            List<int> o = (from n in _db.OutfitItem
                                  where n.OutfitId == item.ItemId
                                  select n.Id).ToList();

            foreach (var c in cartItems)
            {
                List<int> newItem = (from n in _db.ShoppingCartOutfitItems
                                     where n.CartRecordId == c.RecordId
                                     select n.Id).ToList();

                if (o.Count == newItem.Count)
                {
                    cartItem = c;
                    break;
                }
            }

            if(cartItem == null)
            {
                cartItem = new Cart
                {
                    ShoppingCartItemId = item.Id,
                    ItemId = item.ItemId,
                    CartId = _cartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                _db.Carts.Add(cartItem);
                _db.SaveChanges();
                int i = (from n in _db.Carts
                         where n.CartId == _cartId
                         orderby n.RecordId descending
                         select n.RecordId).FirstOrDefault();
                item.RecordId = i;
                _db.ShoppingCartItems.AddOrUpdate(item);
                _db.SaveChanges();
                var cartItemNew = await _db.Carts.SingleOrDefaultAsync(
                        c => c.CartId == _cartId && c.RecordId == i);
                var s =
                    _db.ShoppingCartItems.SingleOrDefault(
                        x => x.ItemId == item.ItemId && x.IsOutfit == item.IsOutfit);
                
                List<int> itemIds = (from n in _db.OutfitItem
                                     where n.OutfitId == item.ItemId
                                     select n.ItemId).ToList();

                foreach(var ids in itemIds)
                {
                    item.Items.Add(_db.Items.Find(ids));
                    _db.ShoppingCartOutfitItems.Add(new ShoppingCartOutfitItems()
                    {
                        ItemId = ids,
                        ShoppingCartItemId = s.Id,
                        CartRecordId = cartItemNew.RecordId
                    });
                }


                //TODO: ask profesor for this one, what happens if customer deletes one item from outfit, and add same outfit to the cart

            }
            else
            {
                List<int> newItem = (from n in _db.ShoppingCartOutfitItems
                                     where n.CartRecordId == cartItem.RecordId
                                     select n.Id).ToList();

                List<int> original = (from n in _db.OutfitItem
                                      where n.OutfitId == cartItem.ItemId
                                      select n.Id).ToList();
                if(newItem.Count != original.Count)
                {
                    cartItem = new Cart
                    {
                        ShoppingCartItemId = item.Id,
                        ItemId = item.ItemId,
                        CartId = _cartId,
                        Count = 1,
                        DateCreated = DateTime.Now
                    };

                    _db.Carts.Add(cartItem);
                    _db.SaveChanges();
                    int i = (from n in _db.Carts
                             where n.CartId == _cartId
                             orderby n.RecordId descending
                             select n.RecordId).FirstOrDefault();
                    item.RecordId = i;
                    _db.ShoppingCartItems.AddOrUpdate(item);
                    _db.SaveChanges();
                    var cartItemNew = await _db.Carts.SingleOrDefaultAsync(
                            c => c.CartId == _cartId && c.RecordId == i);
                    var s =
                        _db.ShoppingCartItems.SingleOrDefault(
                            x => x.ItemId == item.ItemId && x.IsOutfit == item.IsOutfit);

                    List<int> itemIds = (from n in _db.OutfitItem
                                         where n.OutfitId == item.ItemId
                                         select n.ItemId).ToList();

                    foreach(var ids in itemIds)
                    {
                        item.Items.Add(_db.Items.Find(ids));
                        _db.ShoppingCartOutfitItems.Add(new ShoppingCartOutfitItems()
                        {
                            ItemId = ids,
                            ShoppingCartItemId = s.Id,
                            CartRecordId = cartItemNew.RecordId
                        });
                    }
                }
                else
                    cartItem.Count++;

                _db.SaveChanges();
            }
        }

        public async Task<int> RemoveFromCart(int id)
        {
            var cartItem = await _db.Carts.SingleOrDefaultAsync(
                c => c.CartId == _cartId && c.RecordId == id);

            if(cartItem != null)
            {
                if(cartItem.Count > 1)
                {
                    return --cartItem.Count;
                }

                if(cartItem.ShoppingCartItem.IsOutfit)
                {
                    var items =
                        _db.ShoppingCartOutfitItems.Where(x => x.CartRecordId == cartItem.RecordId);
                    _db.ShoppingCartOutfitItems.RemoveRange(items);
                }
                _db.Carts.Remove(cartItem);
            }

            return 0;
        }

        public List<Cart> GetCartItems()
        {
            return _db.Carts.Where(c => c.CartId == _cartId).ToList();
        }

        public Task<decimal> GetTotal()
        {
            return _db.Carts
                .Where(c => c.CartId == _cartId)
                .Select(c => c.Count * c.ShoppingCartItem.Price)
                .DefaultIfEmpty()
                .SumAsync();
        }

        public Task<int> GetCount()
        {
            return _db.Carts
                .Where(c => c.CartId == _cartId)
                .Select(c => c.Count)
                .DefaultIfEmpty()
                .SumAsync();
        }

        private async Task EmptyCart()
        {
            foreach(var cartItem in await _db.Carts.Where(
                c => c.CartId == _cartId).ToListAsync())
            {
                _db.Carts.Remove(cartItem);
            }
        }

        public async Task MigrateCart(string userName)
        {
            var carts = await _db.Carts.Where(c => c.CartId == _cartId).ToListAsync();

            foreach(var item in carts)
            {
                item.CartId = userName;
            }

            await _db.SaveChangesAsync();
        }
    }
}