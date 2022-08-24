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
    public class AdminImageNewsController : Controller
    {
        private TTPHONEEntities db = new TTPHONEEntities();

        // GET: admin/AdminImageNews
        public ActionResult Index()
        {
            var imageNews = db.ImageNews.Include(i => i.News);
            return View(imageNews.ToList());
        }

        // GET: admin/AdminImageNews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageNew imageNew = db.ImageNews.Find(id);
            if (imageNew == null)
            {
                return HttpNotFound();
            }
            return View(imageNew);
        }

        // GET: admin/AdminImageNews/Create
        public ActionResult Create()
        {
            ViewBag.NewID = new SelectList(db.News, "NewID", "ImageURL");
            return View();
        }

        // POST: admin/AdminImageNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageID,ImageName,NewID,ImageURL,Note")] ImageNew imageNew)
        {
            if (ModelState.IsValid)
            {
                db.ImageNews.Add(imageNew);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NewID = new SelectList(db.News, "NewID", "ImageURL", imageNew.NewID);
            return View(imageNew);
        }

        // GET: admin/AdminImageNews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageNew imageNew = db.ImageNews.Find(id);
            if (imageNew == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewID = new SelectList(db.News, "NewID", "ImageURL", imageNew.NewID);
            return View(imageNew);
        }

        // POST: admin/AdminImageNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageID,ImageName,NewID,ImageURL,Note")] ImageNew imageNew)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imageNew).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NewID = new SelectList(db.News, "NewID", "ImageURL", imageNew.NewID);
            return View(imageNew);
        }

        // GET: admin/AdminImageNews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageNew imageNew = db.ImageNews.Find(id);
            if (imageNew == null)
            {
                return HttpNotFound();
            }
            return View(imageNew);
        }

        // POST: admin/AdminImageNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImageNew imageNew = db.ImageNews.Find(id);
            db.ImageNews.Remove(imageNew);
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

        //
        public ActionResult ResultSearch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listImageNew = db.ImageNews.Where(tp => tp.NewID == id).Include(i => i.News).ToList();
            if (listImageNew == null)
            {
                return HttpNotFound();
            }
            return View(listImageNew);
        }
        [HttpPost, ActionName("ResultSearch")]
        [ValidateAntiForgeryToken]
        public ActionResult ResultSearch(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listImageNew = db.ImageNews.Where(tp => tp.NewID == id).Include(i => i.News).ToList();
            if (listImageNew == null)
            {
                return HttpNotFound();
            }
            return View(listImageNew);
        }
    }
}
