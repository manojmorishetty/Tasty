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

namespace Tasty.Controllers
{
    public class PaymentsController : Controller
    {
        private TastyEntities db = new TastyEntities();


        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderId");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PaymentId,UserId,OrderId,CardNumber,CardName,Cvv,ExpiryDate")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderId", payment.OrderId);
            return View(payment);
        }
    }
}
