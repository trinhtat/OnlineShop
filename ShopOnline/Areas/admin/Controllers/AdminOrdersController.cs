using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Models;

namespace ShopOnline.Areas.admin.Controllers
{
    public class AdminOrdersController : Controller
    {
        private TTPHONEEntities db = new TTPHONEEntities();

        // GET: admin/AdminOrders
        public ActionResult Index()
        {
            string trangthai = Session["trangthaiadmin"].ToString();
            if (trangthai == "Đăng Nhập")
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            var orders = db.Orders.Include(o => o.Account);
            foreach (var item in orders)
            {
                if ((DateTime.Now - item.DateOrder).Value.Days > 7)
                {
                    ViewBag.thongbao3days = "Có đơn hàng quá 3 ngày chưa được xử lý.";
                    return View(orders.ToList());
                }
                else
                {
                    ViewBag.hongbao3days = "";
                    return View(orders.ToList());
                }
            }
            return View(orders.ToList());
        }

        // GET: admin/AdminOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: admin/AdminOrders/Create
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "AccountName");
            return View();
        }

        // POST: admin/AdminOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,AccountID,DateOrder,Status,Note")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "AccountName", order.AccountID);
            return View(order);
        }

        // GET: admin/AdminOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "AccountName", order.AccountID);
            return View(order);
        }

        // POST: admin/AdminOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,AccountID,DateOrder,Status,Note")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "AccountName", order.AccountID);
            return View(order);
        }

        // GET: admin/AdminOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: admin/AdminOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<OrderDetail> orderDetails = db.OrderDetails.Where(od => od.OrderID == id).ToList();
            db.OrderDetails.RemoveRange(orderDetails);
            db.SaveChanges();
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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

        
        [HttpPost, ActionName("ChangeStatus")]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus()
        {
            int id = int.Parse(Request.Form["orderid"].ToString());
            int status = int.Parse(Request.Form["status"].ToString());
            if (status == 0 || status == 1)
            {
                return RedirectToAction("Index");
            }
            Order order = db.Orders.Find(id);
            order.Status = status;
            db.Orders.AddOrUpdate(order);
            var orderDetail = db.OrderDetails.Where(od => od.OrderID == id);
            if (status == 3)
            {
                foreach (var item in orderDetail)
                {
                    Product product = db.Products.Find(item.ProductID);
                    product.Quantity += item.Quantity;
                    db.Products.AddOrUpdate(product);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("OrderStatus")]
        [ValidateAntiForgeryToken]
        public ActionResult OrderStatus()
        {
            string trangthaiorder = Session["orderstatus"].ToString();
            int orderstatus = int.Parse(Request.Form["status"].ToString());
            string sdate = Request.Form["dateorder"].ToString();
            if (orderstatus == 0)
            {
                if(trangthaiorder=="Tất cả")
                {
                    orderstatus = 4;
                }
                else if(trangthaiorder=="Chờ xử lý")
                {
                    orderstatus = 1;
                }
                else if(trangthaiorder=="Thành công")
                {
                    orderstatus = 2;
                }
                else if(trangthaiorder=="Thất bại")
                {
                    orderstatus = 3;
                }
            }
            var orders = db.Orders.Include(o => o.Account).ToList();
            if (orderstatus > 3)
            {
                Session["orderstatus"] = "Tất cả";
            }
            else
            {
                orders = db.Orders.Where(o => o.Status == orderstatus).Include(o => o.Account).ToList();
            }

            if (orderstatus == 1)
            {
                Session["orderstatus"] = "Chờ xử lý";
            }
            else if (orderstatus == 2)
            {
                Session["orderstatus"] = "Thành công";
            }
            else if (orderstatus == 3)
            {
                Session["orderstatus"] = "Thất bại";
            }
            
            if (sdate != "")
            {
                Session["orderdate"] = sdate;
                DateTime.TryParseExact(sdate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime orderdate);
                foreach (var item in orders)
                {
                    var a = item.DateOrder.Value.Year;
                    var b = item.DateOrder.Value.Month;
                    var c = item.DateOrder.Value.Day;
                    var a1 = orderdate.Year;
                    var b1 = orderdate.Month;
                    var c1 = orderdate.Day;
                }
                orders = orders.Where(o => o.DateOrder.Value.Year == orderdate.Year && o.DateOrder.Value.Month == orderdate.Month && o.DateOrder.Value.Day == orderdate.Day).ToList();
            }
            else
            {
                Session["orderdate"] = "";
            }
            return View(orders);
        }

        public ActionResult OrderOver3Days()
        {
            var orders = db.Orders.Where(o => o.Status==1).Include(o => o.Account).ToList();
            orders = orders.Where(o => (DateTime.Now - o.DateOrder).Value.Days > 3).ToList();
            return View(orders);
        }

        public ActionResult PrintOrderPDF(int id)
        {
            Order order = db.Orders.Find(id);
            return View(order);
        }

        public ActionResult PrintOrderPDFDone(int id)
        {
            FilePDF filePDF = new FilePDF();
            filePDF.PrintPDF(id);
            Order order = db.Orders.Find(id);
            return View(order);
        }
    }
}
