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
    public class AdminImageProductsController : Controller
    {
        private TTPHONEEntities db = new TTPHONEEntities();

        // GET: admin/AdminImageProducts
        public ActionResult Index()
        {
            var imageProducts = db.ImageProducts.Include(i => i.Product);
            return View(imageProducts.ToList());
        }

        // GET: admin/AdminImageProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageProduct imageProduct = db.ImageProducts.Find(id);
            if (imageProduct == null)
            {
                return HttpNotFound();
            }
            return View(imageProduct);
        }

        // GET: admin/AdminImageProducts/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            return View();
        }

        // POST: admin/AdminImageProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageID,ImageName,ProductID,ImageURL,Note")] ImageProduct imageProduct)
        {
            if (ModelState.IsValid)
            {
                db.ImageProducts.Add(imageProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", imageProduct.ProductID);
            return View(imageProduct);
        }

        // GET: admin/AdminImageProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageProduct imageProduct = db.ImageProducts.Find(id);
            if (imageProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", imageProduct.ProductID);
            return View(imageProduct);
        }

        // POST: admin/AdminImageProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageID,ImageName,ProductID,ImageURL,Note")] ImageProduct imageProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imageProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", imageProduct.ProductID);
            return View(imageProduct);
        }

        // GET: admin/AdminImageProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageProduct imageProduct = db.ImageProducts.Find(id);
            if (imageProduct == null)
            {
                return HttpNotFound();
            }
            return View(imageProduct);
        }

        // POST: admin/AdminImageProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImageProduct imageProduct = db.ImageProducts.Find(id);
            db.ImageProducts.Remove(imageProduct);
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

        // POST: admin/AdminImageProducts/ResultSearch
        public ActionResult ResultSearch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listImageProduct = db.ImageProducts.Where(tp => tp.ProductID == id).Include(i => i.Product).ToList();
            if (listImageProduct == null)
            {
                return HttpNotFound();
            }
            return View(listImageProduct);
        }
        [HttpPost, ActionName("ResultSearch")]
        [ValidateAntiForgeryToken]
        public ActionResult ResultSearch(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listImageProduct = db.ImageProducts.Where(tp => tp.ProductID==id).Include(i => i.Product).ToList();
            if (listImageProduct == null)
            {
                return HttpNotFound();
            }
            return View(listImageProduct);
        }
    }
}
