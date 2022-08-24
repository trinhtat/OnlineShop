using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.admin.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: admin/AdminHome
        public ActionResult Index()
        {
            string trangthai = Session["trangthaiadmin"].ToString();
            if(trangthai=="Đăng Nhập")
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            return View();
        }
    }
}