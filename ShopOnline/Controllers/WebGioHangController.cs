using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class WebGioHangController : Controller
    {
        private TTPHONEEntities db = new TTPHONEEntities();

        // GET: WebGioHang
        public ActionResult Index()
        {
            GioHang giohang = (GioHang)Session["giohang"];
            giohang.tinhtien();
            return View(giohang);
        }

        // GET: WebGioHang/themsanpham
        public ActionResult ThemSanPham(int id)
        {
            Product product = db.Products.Find(id);
            DonHang them = new DonHang()
            {
                masanpham = product.ProductID,
                tensanpham = product.ProductName,
                hinhanh=product.ImageURL,
                soluong = 1,
                dongia = product.UnitPrice,
                khuyenmai = product.Sale.Discount
            };
            GioHang giohang = (GioHang)Session["giohang"];
            if (giohang == null)
            {
                giohang = new GioHang();
            }
            if (product.Quantity == 0)
            {
                ViewBag.thongbaosanpham = product.ProductName + " đang hết hàng";
                return View(giohang);
            }
            DonHang hang= giohang.dssanpham.Where(sp => sp.masanpham == product.ProductID).FirstOrDefault();
            if (hang == null)
            {
                giohang.dssanpham.Add(them);
            }
            else
            {
                them.soluong += hang.soluong;
                giohang.dssanpham.Remove(hang);
                giohang.dssanpham.Add(them);
            }
            
            giohang.tinhtien();
            Session["giohang"] = giohang;
            ViewBag.thongbaosanpham = "";
            return View(giohang);
        }

        // GET: WebGioHang/xoasanpham
        public ActionResult XoaSanPham(int id)
        {
            GioHang giohang = (GioHang)Session["giohang"];
            DonHang xoa = giohang.dssanpham.FirstOrDefault(gh => gh.masanpham == id);
            giohang.dssanpham.Remove(xoa);
            Session["giohang"] = giohang;
            return View(giohang);
        }

        // GET: WebGioHang/setsoluong
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetSoLuong()
        {
            string sproductid = Request.Form["productid"];
            string squantity = Request.Form["quantity"];
            int id = int.Parse(sproductid);
            int quantity = int.Parse(squantity);
            GioHang giohang = (GioHang)Session["giohang"];
            DonHang update=giohang.dssanpham.FirstOrDefault(sp => sp.masanpham == id);
            Product product = db.Products.Find(id);
            if(quantity<=0 || product.Quantity < quantity)
            {
                ViewBag.thongbaosanpham = "Số lượng thay đổi của \""+update.tensanpham+"\" không phù hợp";
                return View(giohang);
            }
            giohang.dssanpham.Remove(update);
            update.soluong = quantity;
            giohang.dssanpham.Add(update);
            giohang.tinhtien();
            Session["giohang"] = giohang;
            ViewBag.thongbaosanpham = "";
            return View(giohang);
        }

        // GET: WebGioHang/dathang
        public ActionResult DatHang()
        {
            string trangthai = Session["trangthaiweb"].ToString();
            if (trangthai == "Đăng Nhập")
            {
                return RedirectToAction("Index", "WebLogin");
            }
            GioHang giohang = (GioHang)Session["giohang"];
            if (giohang.dssanpham.Count == 0)
            {
                ViewBag.thongbao = "Đặt hàng thất bại, giỏ hàng không có hàng.";
                return View();
            }
            int accountid = int.Parse(Session["accountid"].ToString());
            Order them = new Order()
            {
                AccountID = accountid,
                DateOrder = DateTime.Now,
                Status = 1
            };
            db.Orders.Add(them);
            db.SaveChanges();
            int orderid = db.Orders.OrderByDescending(o => o.OrderID).FirstOrDefault().OrderID;
            foreach (var item in giohang.dssanpham)
            {
                db.OrderDetails.Add(new OrderDetail { OrderID = orderid, ProductID = item.masanpham, Quantity = item.soluong });
                Product product = db.Products.Find(item.masanpham);
                product.Quantity -= item.soluong;
                db.Products.AddOrUpdate(product);
            }
            db.SaveChanges();
            Session["giohang"] = new GioHang();
            ViewBag.thongbao = "Đặt hàng thành công, cảm ơn quý khách đã mua hàng. Chúng tôi sẽ sớm liên hệ với bạn";
            return View();
        }

    }
}