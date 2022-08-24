using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopOnline.Repositories
{
    public class WebDanhMucRepository
    {
        TTPHONEEntities db = new TTPHONEEntities();

        public IEnumerable<Product> spoppo { get; set; }
        public IEnumerable<Product> spxiaomi { get; set; }
        public IEnumerable<Product> spapple { get; set; }
        public IEnumerable<Product> spnokia { get; set; }
        public IEnumerable<Product> spsamsung { get; set; }

        public WebDanhMucRepository()
        {
            spoppo = sanphamoppo();
            spxiaomi = sanphamxiaomi();
            spapple = sanphamapple();
            spnokia = sanphamnokia();
            spsamsung = sanphamsamsung();
        }

        public IEnumerable<Product> sanphamoppo()
        {
            var products = db.Products.Where(p => p.TypeProduct.TypeProductName == "Oppo").OrderByDescending(p => p.ProductID).Take(4).Include(p => p.Sale).Include(p => p.TypeProduct);
            return products.ToList();
        }
        public IEnumerable<Product> sanphamxiaomi()
        {
            var products = db.Products.Where(p => p.TypeProduct.TypeProductName == "Xiaomi").OrderByDescending(p => p.ProductID).Take(4).Include(p => p.Sale).Include(p => p.TypeProduct);
            return products.ToList();
        }
        public IEnumerable<Product> sanphamapple()
        {
            var products = db.Products.Where(p => p.TypeProduct.TypeProductName == "Apple").OrderByDescending(p => p.ProductID).Take(4).Include(p => p.Sale).Include(p => p.TypeProduct);
            return products.ToList();
        }
        public IEnumerable<Product> sanphamnokia()
        {
            var products = db.Products.Where(p => p.TypeProduct.TypeProductName == "Nokia").OrderByDescending(p => p.ProductID).Take(4).Include(p => p.Sale).Include(p => p.TypeProduct);
            return products.ToList();
        }
        public IEnumerable<Product> sanphamsamsung()
        {
            var products = db.Products.Where(p => p.TypeProduct.TypeProductName == "Samsung").OrderByDescending(p => p.ProductID).Take(4).Include(p => p.Sale).Include(p => p.TypeProduct);
            return products.ToList();
        }
    }
}