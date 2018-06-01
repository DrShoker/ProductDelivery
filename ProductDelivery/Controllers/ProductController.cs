using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using DataAccessLayer.Enums;

namespace ProductDelivery.Controllers
{
    public class ProductController : Controller
    {
        EFUnitOfWork db = new EFUnitOfWork();

        [HttpGet]
        public IActionResult OpenProducts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Catalog(Departments dep)
        {
            Departments department = (Departments)dep;
            List<Product> products = db.Products.GetAll().Where(p=>p.Department==department).
                OrderBy(p=>p.Type).ToList();
            ViewBag.Dep = dep.ConvertToString();
            return View(products);
        }

        //[Authorize]
        //[HttpPost]
        //public int AddToCart(int id)
        //{

        //    return
        //}


        public IActionResult Index()
        {
            return View();
        }
    }
}