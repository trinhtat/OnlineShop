using ShopOnline.Models;
using ShopOnline.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class WebChiTietSpController : Controller
    {
        private TTPHONEEntities db = new TTPHONEEntities();

        // GET: WebChiTietSp
        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult ChiTietsp(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        public ActionResult ChiTietsp(int? id)
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
            var result = new SanPham()
            {
                masanpham = product.ProductID,
                tensanpham = product.ProductName,
                loaisanpham = product.TypeProductID,
                dongia = product.UnitPrice,
                soluong = product.Quantity,
                khuyenmai = product.Sale.Discount,
                hinhanh = product.ImageURL,
                mota = product.Description,
                chitiet = product.Detail,
                ghichu = product.Note
            };
            result.loaddata();
            return View(result);
        }


        //them danh gia
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemDanhGia()
        {
            string trangthai = Session["trangthai"].ToString();
            if (trangthai == "Đăng Nhập")
            {
                return RedirectToAction("Index", "WebLogin");
            }
            string saccountid = Session["accountid"].ToString();
            string textdanhgia = Request.Form["textdanhgia"].ToString();
            string smasanpham = Request.Form["masanpham"].ToString();

            int accountid = int.Parse(saccountid);
            int masanpham = int.Parse(smasanpham);

            Vote tim = db.Votes.FirstOrDefault(v => v.AccountID == accountid && v.ProductID == masanpham);
            Vote them = new Vote()
            {
                AccountID = accountid,
                ProductID = masanpham,
                Comment = textdanhgia
            };
            if (tim == null)
            {
                db.Votes.Add(them);
                db.SaveChanges();
            }
            else
            {
                db.Votes.AddOrUpdate(tim);
                db.SaveChanges();
            }
            return RedirectToAction("ChiTietsp", "WebChiTietSp", new { id = masanpham });
        }
    }
}