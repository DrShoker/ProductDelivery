using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace ProductDelivery.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public IActionResult OpenProducts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ShowProductInfo()
        {
            return View("");
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