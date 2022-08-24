using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.admin.Controllers
{
    public class AdminLoginController : Controller
    {
        private TTPHONEEntities db = new TTPHONEEntities();

        // GET: admin/AdminCheck
        public ActionResult Index()
        {
            Session["trangthaiadmin"] = "Đăng Nhập";
            Session["accountadmin"] = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "AccountID,AccountName,Password,Role,Note")] Account account)
        {
            bool check = false;
            foreach (var item in db.Accounts)
            {
                if(item.AccountName==account.AccountName && item.Password == account.Password)
                {
                    if (item.Role == 1)
                    {
                        Session["accountadmin"] = item.AccountID;
                        check = true;
                    }
                    else if (item.Role == 2)
                    {
                        check = true;
                    }
                }
            }
            if (check)
            {
                ViewBag.result = "login thành công";
                Session["trangthaiadmin"] = "Đăng Xuất";
                return RedirectToAction("Index","AdminHome");
            }
            else
            {
                ViewBag.result = "login thất bại, vui lòng kiểm tra lại tài khoản và mật khẩu";
                return View();
            }
            
        }
    }
}