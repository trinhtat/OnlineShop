using ShopOnline.Models;
using ShopOnline.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class WebDanhMucController : Controller
    {
        TTPHONEEntities db = new TTPHONEEntities();

        // GET: WebDanhMuc
        public ActionResult Index()
        {
            var result = new WebDanhMucRepository();
            return View(result);
        }

        public ActionResult GetAll(int id)
        {
            var products = db.Products.Where(p => p.TypeProductID==id).Include(p => p.Sale).Include(p => p.TypeProduct);
            return View(products.ToList());
        }
    }
}