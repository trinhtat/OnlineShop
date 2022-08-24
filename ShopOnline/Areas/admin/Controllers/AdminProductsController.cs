using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Models;

namespace ShopOnline.Areas.admin.Controllers
{
    public class AdminProductsController : Controller
    {
        private TTPHONEEntities db = new TTPHONEEntities();

        // GET: admin/AdminProducts
        public ActionResult Index()
        {
            string trangthai = Session["trangthaiadmin"].ToString();
            if (trangthai == "Đăng Nhập")
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            var products = db.Products.OrderByDescending(o=>o.ProductID).Include(p => p.Sale).Include(p => p.TypeProduct);
            return View(products.ToList());
        }

        // GET: admin/AdminProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: admin/AdminProducts/Create
        public ActionResult Create()
        {
            ViewBag.SaleID = new SelectList(db.Sales, "SaleID", "Discount");
            ViewBag.TypeProductID = new SelectList(db.TypeProducts, "TypeProductID", "TypeProductName");
            return View();
        }

        // POST: admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,TypeProductID,Quantity,UnitPrice,SaleID,ImageURL,Description,Detail,Note")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SaleID = new SelectList(db.Sales, "SaleID", "Discount", product.SaleID);
            ViewBag.TypeProductID = new SelectList(db.TypeProducts, "TypeProductID", "TypeProductName", product.TypeProductID);
            return View(product);
        }

        // GET: admin/AdminProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.SaleID = new SelectList(db.Sales, "SaleID", "Note", product.SaleID);
            ViewBag.TypeProductID = new SelectList(db.TypeProducts, "TypeProductID", "TypeProductName", product.TypeProductID);
            return View(product);
        }

        // POST: admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,TypeProductID,Quantity,UnitPrice,SaleID,ImageURL,Description,Detail,Note")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SaleID = new SelectList(db.Sales, "SaleID", "Note", product.SaleID);
            ViewBag.TypeProductID = new SelectList(db.TypeProducts, "TypeProductID", "TypeProductName", product.TypeProductID);
            return View(product);
        }

        // GET: admin/AdminProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: admin/AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Search()
        {
            return View();
        }

        // POST: admin/AdminTypeProducts/Search
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResultSearch(Product product)
        {
            if (product.ProductName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listProduct = db.Products.Where(tp => tp.ProductName.Contains(product.ProductName)).ToList();
            if (listProduct == null)
            {
                return HttpNotFound();
            }
            return View(listProduct);
        }
    }
}
