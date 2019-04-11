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
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Tasty.Controllers
{
    public class OrdersController : Controller
    {
        private TastyEntities db = new TastyEntities();

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            var orders = db.Orders.Include(o => o.Customer);
            return View(await orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[WebMethod]
        public async Task<ActionResult> Create(string hiddentype)
        {
            dynamic Data = JsonConvert.DeserializeObject(hiddentype);
            Data = JsonConvert.DeserializeObject(Data);
            float totalprice=0;
            JArray items = (JArray)Data;
            int length = items.Count;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(Data[i].BasePrice); totalprice = 800;
                //totalprice = totalprice+ float.Parse(Data[i].BasePrice, CultureInfo.InvariantCulture);
            }
            
            var Order = db.Set<Order>();
            if (Session["userid"] == null)
            {
                //Convert.ToInt32(Session["userid"])
            }
            Order.Add(new Order { UserId = Convert.ToInt32(Session["userid"].ToString()), OrderPrice = totalprice });
            db.SaveChanges();
            for (int i=0;i<length;i++)
            {
                var Orderdetail = db.Set<OrderDetail>();
                var LastOrderId = db.Orders.Max(x => x.OrderId);
                Orderdetail.Add(new OrderDetail { OrderId= LastOrderId ,ItemId=Data[i].ItemId,Quantity= Data[i].Quantity });
                db.SaveChanges();
            }
            return RedirectToAction("Create", "Payments");
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Customers, "UserId", "FirstName", order.UserId);
            return View();
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderId,UserId,OrderPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Customers, "UserId", "FirstName", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
