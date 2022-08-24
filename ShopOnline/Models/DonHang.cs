using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class DonHang
    {
        public int masanpham { get; set; }
        public string tensanpham { get; set; }
        public string hinhanh { get; set; }
        public int soluong { get; set; }
        public decimal dongia { get; set; }
        public int khuyenmai { get; set; }
    }
}