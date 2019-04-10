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
    public class CustomersController : Controller
    {
        private TastyEntities db = new TastyEntities();

        // GET: Customers/Create
        public ActionResult Login()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customer customer)
        {
            var userExist = db.Customers.Where(e => e.email == customer.email && e.Password == customer.Password).FirstOrDefault();
            if (userExist == null)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View("Login");
            }
            else
            {
                Session["username"] = db.Customers.Where(e => e.email == customer.email && e.Password == customer.Password).FirstOrDefault().FirstName;
                Session["userid"] = customer.UserId;
                Session["email"] = customer.email;
                Session["UserType"] = customer.UserType;
                if (userExist.UserType.Equals("Seller"))
                {
                    return RedirectToAction("Index","Items");
                }
                else
                {
                    return RedirectToAction("View","Items");
                }

            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("View","Items");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Customer customer)
        {
            var userExist = db.Customers.Where(e => e.email == customer.email).FirstOrDefault();
            if (ModelState.IsValid && userExist==null)
            {
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                if (customer.UserType.Equals("Seller"))
                {
                    return RedirectToAction("index", "Items");
                }
                return RedirectToAction("View", "Items");
            }
            ModelState.AddModelError("", "User Exist..Redirected to Login");
            return View("Login");
        }
    }
}
