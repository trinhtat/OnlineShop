using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopOnline.Repositories
{
    public class WebHomeRepository
    {
        TTPHONEEntities db = new TTPHONEEntities();

        public IEnumerable<Product> spmoi { get; set; }
        public IEnumerable<Product> spbanchay { get; set; }

        public WebHomeRepository()
        {
            this.spmoi = sanphammoi();
            this.spbanchay = sanphambanchay();
        }

        public IEnumerable<Product> sanphammoi()
        {
            var products = db.Products.OrderByDescending(p => p.ProductID).Take(8).Include(p => p.Sale).Include(p => p.TypeProduct);
            return products.ToList();
        }

        public IEnumerable<Product> sanphambanchay()
        {
            var maxproducts = db.OrderDetails.GroupBy(od => od.ProductID).OrderByDescending(od => od.Count()).Take(8).Select(od=>od.Key);
            var products = db.Products.Where(p=>maxproducts.Contains(p.ProductID)).Include(p=>p.Sale).Include(p=>p.TypeProduct).ToList();
            return products;
        }
    }
}