using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class WebTimController : Controller
    {
        TTPHONEEntities db = new TTPHONEEntities();

        // GET: WebTim
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TimTuKhoa()
        {
            string tukhoa = Request.Form["tukhoa"].ToString();
            var result = db.Products.Where(p => p.ProductName.Contains(tukhoa));
            return View(result);
        }
    }
}