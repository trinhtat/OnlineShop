using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class GioHang
    {
        TTPHONEEntities db = new TTPHONEEntities();
        public decimal TongTien { get; set; }
        public decimal ThanhToan { get; set; }
        public List<DonHang> dssanpham { get; set; }

        public GioHang()
        {
            dssanpham = new List<DonHang>();
        }
        public void tinhtien()
        {
            TongTien = dssanpham.Sum(p => p.dongia * p.soluong);
            ThanhToan = dssanpham.Sum(p => p.dongia * p.soluong * p.khuyenmai / 100);
        }
    }
}