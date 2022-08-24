using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class WebLoginController : Controller
    {
        TTPHONEEntities db = new TTPHONEEntities();

        // GET: WebLogin
        public ActionResult Index()
        {
            Session["trangthaiweb"] = "Đăng Nhập";
            Session["accountid"] = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login()
        {
            string accountname = Request.Form["accountname"].ToString();
            string password = Request.Form["password"].ToString();
            var account = db.Accounts.FirstOrDefault(a => a.AccountName == accountname && a.Password == password);
            if (account == null)
            {
                ViewBag.tb = "Tài khoản hoặc mật khẩu sai";
                return View();
            }
            else
            {
                Session["trangthaiweb"] = "Đăng Xuất";
                Session["accountid"] = account.AccountID;
                return RedirectToAction("Index", "WebHome");
            }
        }
    }
}