using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class SanPham
    {
        public int masanpham { get; set; }
        public string tensanpham { get; set; }
        public int loaisanpham { get; set; }
        public int soluong { get; set; }
        public decimal dongia { get; set; }
        public int khuyenmai { get; set; }
        public string hinhanh { get; set; }
        public string mota { get; set; }
        public string chitiet { get; set; }
        public string ghichu { get; set; }

        public List<ImageProduct> dshinhanh { get; set; }

        public List<string> Detailss { get; set; }

        public IEnumerable<Product> splienquan { get; set; }

        public IEnumerable<Vote> danhgia { get; set; }

        public void loaddata()
        {
            var dt = chitiet.Split(';');
            Detailss = dt.ToList();

            TTPHONEEntities tt = new TTPHONEEntities();
            int typeproductid = tt.Products.Find(masanpham).TypeProductID;
            splienquan = tt.Products.Where(p => p.TypeProductID == typeproductid).Take(8).ToList();

            dshinhanh = tt.ImageProducts.Where(ip => ip.ProductID == masanpham).OrderByDescending(ip=>ip.ImageID).Take(4).ToList();

            danhgia = tt.Votes.Where(v => v.ProductID == masanpham).OrderByDescending(v => v.VoteID).Take(10).ToList();
        }
    }
}