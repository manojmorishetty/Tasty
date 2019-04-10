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


        public async Task<ActionResult> Create()
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Customers");
            }
            return View("Create");
        }
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Createpost(Payment payment)
        {
            //var Payment = db.Set<Payment>();
            //var LastOrderId = db.Orders.Max(x => x.OrderId);
            //Payment.Add(new Payment { OrderId=LastOrderId,UserId= 10,CardName=payment.CardName,CardNumber=payment.CardNumber,Cvv=payment.Cvv,ExpiryDate=payment.ExpiryDate });
            //db.SaveChanges();
            return View("View");
        }
    }
}
