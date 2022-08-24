using ShopOnline.Models;
using ShopOnline.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class WebHomeController : Controller
    {

        // GET: chitietsanpham
        public ActionResult Index()
        {
            var result = new WebHomeRepository();
            
            return View(result);
        }
    }
}