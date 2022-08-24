using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace ShopOnline.Repositories
{
    public class WebChiTietSanPhamRepository
    {
        TTPHONEEntities db = new TTPHONEEntities();

        public Product sanpham { get; set; }
        public IEnumerable<Product> splienquan { get; set; }

        public WebChiTietSanPhamRepository()
        {

        }
        public WebChiTietSanPhamRepository(int id)
        {
            sanpham = db.Products.Find(id);
            splienquan= db.Products.Where(p=>p.TypeProductID==sanpham.TypeProductID).Take(4).Include(p => p.Sale).Include(p => p.TypeProduct);
        }


    }
}