using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GAPT.Models;

namespace GAPT.Controllers
{
    public class OutfitsController : Controller
    {
        private GAPTContext _db = new GAPTContext();

        // GET: Outfits
        public async Task<ActionResult> Index()
        {
            return View(await _db.Outfits.ToListAsync());
        }

        // GET: Outfits/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outfit outfit = await _db.Outfits.FindAsync(id);

            List<int> oi = (from i in _db.OutfitItem
                            where i.OutfitId == id
                            select i.ItemId).ToList();

            outfit.Items = oi.Select(i => _db.Items.Find(i)).ToList();

            if(outfit == null)
            {
                return HttpNotFound();
            }
            string s = (outfit.IsFemale ? "F," : "M,") + (outfit.IsChild ? "C," : "A,") + (outfit.Items.Aggregate("", (current, item) => current + (item.Type.Name + "," + item.Type.Category.Name + "," + item.ColorHex.ToString() + "~")));
            ViewBag.Arr = s.Remove(s.Length - 1);
            return View(outfit);
        }

        // GET: Outfits/Create
        public ActionResult Create()
        {
            ViewBag.Items = _db.Items.ToList();

            return View();
        }

        // POST: Outfits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Discount,IsFemale,IsChild")] Outfit outfit, IEnumerable<int> items)
        {
            if(ModelState.IsValid)
            {
                decimal price = items.Sum(itemId => _db.Items.Find(itemId).Price);
                outfit.Price = price;
                _db.Outfits.Add(outfit);
                await _db.SaveChangesAsync();
                foreach(var i in items)
                {
                    _db.OutfitItem.Add(new OutfitItem() { OutfitId = outfit.Id, ItemId = i });
                }
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(outfit);
        }

        // GET: Outfits/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outfit outfit = await _db.Outfits.FindAsync(id);

            List<int> oi = (from i in _db.OutfitItem
                            where i.OutfitId == id
                            select i.ItemId).ToList();

            outfit.Items = oi.Select(i => _db.Items.Find(i)).ToList();

            ViewBag.Items = _db.Items.ToList();

            if(outfit == null)
            {
                return HttpNotFound();
            }
            return View(outfit);
        }

        // POST: Outfits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Discount,IsFemale,IsChild")] Outfit outfit, List<int> items)
        {
            if(ModelState.IsValid)
            {
                decimal price = items.Sum(itemId => _db.Items.Find(itemId).Price);
                outfit.Price = price;
                _db.Outfits.Add(outfit);
                _db.Entry(outfit).State = EntityState.Modified;

                var a = _db.OutfitItem.Where(i => i.OutfitId == outfit.Id);
                _db.OutfitItem.RemoveRange(a);
                await _db.SaveChangesAsync();

                if(items != null)
                    foreach(var i in items)
                    {
                        _db.OutfitItem.AddOrUpdate(new OutfitItem() { OutfitId = outfit.Id, ItemId = i });
                    }
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Items = _db.Items.ToList();

            return View(outfit);
        }

        // GET: Outfits/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outfit outfit = await _db.Outfits.FindAsync(id);
            if(outfit == null)
            {
                return HttpNotFound();
            }
            return View(outfit);
        }

        // POST: Outfits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Outfit outfit = await _db.Outfits.FindAsync(id);
            _db.Outfits.Remove(outfit);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
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
