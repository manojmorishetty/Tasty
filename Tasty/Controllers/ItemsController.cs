using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tasty;
using System.IO;
using System.Web.Services;
using Newtonsoft.Json;

namespace Tasty.Controllers
{
    public class ItemsController : Controller
    {
        private TastyEntities db = new TastyEntities();

        // GET: Items
        public async Task<ActionResult> Index()
        {
            return View(await db.Items.ToListAsync());
        }


        // GET: Items/Create
        public ActionResult Create()
        {
            return base.View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ItemId,ItemName,Quantity,BasePrice,img_src,ImageFile")] Item item)
        {
            if (ModelState.IsValid)
            {
                string fileName = item.ItemName.ToString();
                string extension = Path.GetExtension(item.img_src);
                fileName += extension;
                item.img_src = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                item.ImageFile.SaveAs(fileName);
                db.Items.Add(item);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ModelState.Clear();
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ItemId,ItemName,Quantity,BasePrice,img_src")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            Item item = await db.Items.FindAsync(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string fullPath = Request.MapPath("~/Content/Images/" + item.ItemName + ".png");
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Item item = await db.Items.FindAsync(id);
            db.Items.Remove(item);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public new async Task<ActionResult> View()
        {
            return View(await db.Items.ToListAsync());
        }

        [HttpPost]
        [WebMethod]
        public JsonResult Cart(string JsonLocalStorageObj)
        {
            var Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonLocalStorageObj);
            List<String> keys = new List<string>();
            List<String> values = new List<string>();
            foreach (var x in Data.Keys.ToList())
            {
                keys.Add(x);
                values.Add(Data[x]);
            }
            List<Item> items = new List<Item>();
            for(int i = 0; i < keys.Count(); i++)
            {
                int itemId = Convert.ToInt32(keys.ElementAt(i));
                Item item = db.Items.Where(e => e.ItemId == itemId).FirstOrDefault();
                item.Quantity= Convert.ToInt32(values.ElementAt(i));
                items.Add(item);
            }
            return Json(items);
        }

            public async Task<ActionResult> Vie()
        {
            return View(await db.Items.ToListAsync());
        }
    }
}
