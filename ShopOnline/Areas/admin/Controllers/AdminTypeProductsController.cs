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
    public class AdminTypeProductsController : Controller
    {
        private TTPHONEEntities db = new TTPHONEEntities();

        // GET: admin/AdminTypeProducts
        public ActionResult Index()
        {
            string trangthai = Session["trangthaiadmin"].ToString();
            if (trangthai == "Đăng Nhập")
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            return View(db.TypeProducts.ToList());
        }

        // GET: admin/AdminTypeProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeProduct typeProduct = db.TypeProducts.Find(id);
            if (typeProduct == null)
            {
                return HttpNotFound();
            }
            return View(typeProduct);
        }

        // GET: admin/AdminTypeProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/AdminTypeProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TypeProductID,TypeProductName,ImageURL,Note")] TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {
                db.TypeProducts.Add(typeProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeProduct);
        }

        // GET: admin/AdminTypeProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeProduct typeProduct = db.TypeProducts.Find(id);
            if (typeProduct == null)
            {
                return HttpNotFound();
            }
            return View(typeProduct);
        }

        // POST: admin/AdminTypeProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TypeProductID,TypeProductName,ImageURL,Note")] TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeProduct);
        }

        // GET: admin/AdminTypeProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeProduct typeProduct = db.TypeProducts.Find(id);
            if (typeProduct == null)
            {
                return HttpNotFound();
            }
            return View(typeProduct);
        }

        // POST: admin/AdminTypeProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeProduct typeProduct = db.TypeProducts.Find(id);
            db.TypeProducts.Remove(typeProduct);
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
        public ActionResult ResultSearch(TypeProduct typeproduct)
        {
            if (typeproduct.TypeProductName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listtypeProduct = db.TypeProducts.Where(tp => tp.TypeProductName.Contains(typeproduct.TypeProductName)).ToList();
            if (listtypeProduct == null)
            {
                return HttpNotFound();
            }
            return View(listtypeProduct);
        }
    }
}
