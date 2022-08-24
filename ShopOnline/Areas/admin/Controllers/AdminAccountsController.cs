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
    public class AdminAccountsController : Controller
    {
        private TTPHONEEntities db = new TTPHONEEntities();

        // GET: admin/AdminAccounts
        public ActionResult Index()
        {
            string trangthai = Session["trangthaiadmin"].ToString();
            if (trangthai == "Đăng Nhập")
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            string admin = Session["accountadmin"].ToString();
            if (admin == "")
            {
                return RedirectToAction("Index", "AdminHome");
            }
            return View(db.Accounts.ToList());
        }

        // GET: admin/AdminAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: admin/AdminAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/AdminAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountID,AccountName,Password,Role,Note")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: admin/AdminAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: admin/AdminAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,AccountName,Password,Role,Note")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: admin/AdminAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: admin/AdminAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            var customers = db.Customers.Where(c => c.AccountID == id);
            db.Customers.RemoveRange(customers);
            db.SaveChanges();
            db.Accounts.Remove(account);
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

        // POST: admin/AdminAccounts/Search
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResultSearch1(Account account)
        {
            if (account.AccountName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listAccount = db.Accounts.Where(tp => tp.AccountName.Contains(account.AccountName)).ToList();
            if (listAccount == null)
            {
                return HttpNotFound();
            }
            return View(listAccount);
        }

        public ActionResult ResultSearch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listAccount = db.Accounts.Where(tp => tp.AccountID == id).Include(i => i.Customers).ToList();
            if (listAccount == null)
            {
                return HttpNotFound();
            }
            return View(listAccount);
        }
        [HttpPost, ActionName("ResultSearch")]
        [ValidateAntiForgeryToken]
        public ActionResult ResultSearch(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listAccount = db.Accounts.Where(tp => tp.AccountID == id).Include(i => i.Customers).ToList();
            if (listAccount == null)
            {
                return HttpNotFound();
            }
            return View(listAccount);
        }

        
        public ActionResult ResultSearchFromOrder(int id)
        {
            var listAccount = db.Accounts.Where(tp => tp.AccountID == id).Include(i => i.Customers).ToList();
            if (listAccount == null)
            {
                return HttpNotFound();
            }
            return View(listAccount);
        }
    }
}
