using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class WebTaiKhoanController : Controller
    {
        private TTPHONEEntities db = new TTPHONEEntities();

        // GET: WebTaiKhoan
        public ActionResult Index()
        {
            string trangthai = Session["trangthaiweb"].ToString();
            if (trangthai == "Đăng Nhập")
            {
                return RedirectToAction("Index", "WebLogin");
            }
            int accountid = int.Parse(Session["accountid"].ToString());
            Customer customer = db.Customers.FirstOrDefault(c => c.AccountID == accountid);
            return View(customer);
        }

        public ActionResult CapNhatThongTinKhachHang()
        {
            string trangthai = Session["trangthaiweb"].ToString();
            if (trangthai == "Đăng Nhập")
            {
                return RedirectToAction("Index", "WebLogin");
            }
            int accountid = int.Parse(Session["accountid"].ToString());
            Customer customer = db.Customers.FirstOrDefault(c => c.AccountID == accountid);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatThongTin()
        {
            string trangthai = Session["trangthaiweb"].ToString();
            if (trangthai == "Đăng Nhập")
            {
                return RedirectToAction("Index", "WebLogin");
            }

            int accountid = int.Parse(Session["accountid"].ToString());
            var customer = db.Customers.FirstOrDefault(c => c.AccountID == accountid);

            string hoten = Request.Form["hoten"].ToString();
            string sngaysinh = Request.Form["ngaysinh"].ToString();
            DateTime.TryParseExact(sngaysinh, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngaysinh);
            string sgioitinh = Request.Form["gioitinh"].ToString();
            int gioitinh;
            if (sgioitinh=="1")
            {
                gioitinh = 1;
            }
            else
            {
                gioitinh = 2;
            }
            string diachi = Request.Form["diachi"].ToString();
            string email = Request.Form["email"].ToString();
            string sodienthoai = Request.Form["sodienthoai"].ToString();
            string ghichu = Request.Form["ghichu"].ToString();
            if (!string.IsNullOrEmpty(hoten))
            {
                customer.CustomerName = hoten;
            }
            if (!string.IsNullOrEmpty(sngaysinh))
            {
                customer.DateBirth = ngaysinh;
            }
            if (!string.IsNullOrEmpty(diachi))
            {
                customer.Address = diachi;
            }
            if (!string.IsNullOrEmpty(email))
            {
                customer.Email = email;
            }
            if (!string.IsNullOrEmpty(email))
            {
                customer.Email = email;
            }
            if (!string.IsNullOrEmpty(sgioitinh))
            {
                customer.Gender = gioitinh;
            }
            if (!string.IsNullOrEmpty(ghichu))
            {
                customer.Note = ghichu;
            }
            db.Customers.AddOrUpdate(customer);
            db.SaveChanges();
            return View(customer);
        }

        public ActionResult DoiMatKhau()
        {
            string trangthai = Session["trangthaiweb"].ToString();
            if (trangthai == "Đăng Nhập")
            {
                return RedirectToAction("Index", "WebLogin");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoiMK()
        {
            string trangthai = Session["trangthaiweb"].ToString();
            if (trangthai == "Đăng Nhập")
            {
                return RedirectToAction("Index", "WebLogin");
            }

            int accountid = int.Parse(Session["accountid"].ToString());
            Account account = db.Accounts.Find(accountid);

            string matkhaucu = Request.Form["matkhaucu"].ToString();
            string matkhaumoi = Request.Form["matkhaumoi"].ToString();
            string xacnhan = Request.Form["xacnhan"].ToString();

            if (account.Password != matkhaucu)
            {
                ViewBag.thongbaopass = "Mật khẩu sai.";
                return View();
            }
            if (matkhaumoi != xacnhan)
            {
                ViewBag.thongbaopass = "Mật khẩu không trùng nhau.";
                return View();
            }
            account.Password = matkhaumoi;
            db.Accounts.AddOrUpdate(account);
            db.SaveChanges();
            return View();
        }

        public ActionResult QuanLyDonHang()
        {
            string trangthai = Session["trangthaiweb"].ToString();
            if (trangthai == "Đăng Nhập")
            {
                return RedirectToAction("Index", "WebLogin");
            }

            int accountid = int.Parse(Session["accountid"].ToString());
            List<Order> orders = db.Orders.Where(o => o.AccountID == accountid).OrderByDescending(o=>o.DateOrder).ToList();

            return View(orders);
        }

        public ActionResult HuyDonHang(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.Status = 3;
            db.Orders.AddOrUpdate(order);
            db.SaveChanges();
            return RedirectToAction("QuanLyDonHang", "WebTaiKhoan");
        }

        public ActionResult ChiTietDonHang(int id)
        {
            var orderDetails = db.OrderDetails.Where(od => od.OrderID == id).ToList();
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            return View(orderDetails);
        }
    }
}