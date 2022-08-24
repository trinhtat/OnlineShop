using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class WebRegisterController : Controller
    {
        TTPHONEEntities db = new TTPHONEEntities();

        // GET: WebRegister
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register()
        {
            string accountname = Request.Form["accountname"].ToString();
            string password = Request.Form["password"].ToString();
            string customername = Request.Form["customername"].ToString();
            string sbirthday = Request.Form["birthday"].ToString();
            string sgender = Request.Form["gender"].ToString();
            string address = Request.Form["address"].ToString();
            string email = Request.Form["email"].ToString();
            string phonenumber = Request.Form["phonenumber"].ToString();
            int gender=2;
            if (sgender == "nam")
            {
                gender = 1;
            }
            DateTime datebirth = DateTime.Parse(sbirthday);
            if (db.Accounts.Any(a => a.AccountName == accountname))
            {
                ViewBag.thongbaoaccount = "account da ton tai";
                return View();
            }
            Account account = new Account()
            {
                AccountName = accountname,
                Password = password,
                Role = 3,
                Note = "tài khoản khách hàng"
            };
            db.Accounts.Add(account);
            db.SaveChanges();
            Customer customer=new Customer()
            {
                CustomerName=customername,
                AccountID=db.Accounts.OrderByDescending(a=>a.AccountID).FirstOrDefault().AccountID,
                DateBirth=datebirth,
                Gender=gender,
                Address=address,
                Email=email,
                PhoneNumber=phonenumber
            };
            db.Customers.Add(customer);
            db.SaveChanges();

            return RedirectToAction("Index", "WebLogin");
        }
    }
}