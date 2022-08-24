using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class WebTintucController : Controller
    {
        TTPHONEEntities db = new TTPHONEEntities();
        public int index { get; set; }
        // GET: WebTintuc
        public ActionResult Index()
        {
            index = 1;
            ViewBag.page = "1";
            var result = db.News.OrderByDescending(n => n.NewID).Skip(8 * (index - 1)).Take(8).Include(n => n.ImageNews).ToList();
            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GotoPage()
        {
            if (Request.Form["page"].ToString() != "")
            {
                index = int.Parse(Request.Form["page"]);
            }
            ViewBag.page = "Page " + index;
            var result = db.News.OrderByDescending(n => n.NewID).Skip(8*(index-1)).Take(8).Include(n => n.ImageNews).ToList();
            return View(result);
        }

        public ActionResult ChiTiettt(int id)
        {
            var result = db.News.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }
    }
}