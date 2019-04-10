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


        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create2([Bind(Include = "PaymentId,UserId,OrderId,CardNumber,CardName,Cvv,ExpiryDate")] Payment payment)
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

        public async Task<ActionResult> Create(Payment payment)
        {
            var Payment = db.Set<Payment>();
            var LastOrderId = db.Orders.Max(x => x.OrderId);
            if (Session["email"]==null)
            {
                return RedirectToAction("Login", "Customers");
            }
            Payment.Add(new Payment { OrderId=LastOrderId,UserId= Convert.ToInt32(Session["userid"]),CardName=payment.CardName,CardNumber=payment.CardNumber,Cvv=payment.Cvv,ExpiryDate=payment.ExpiryDate });
            db.SaveChanges();
            return RedirectToAction("View", "Items");
        }
    }
}
