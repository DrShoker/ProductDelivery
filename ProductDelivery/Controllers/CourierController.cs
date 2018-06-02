using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProductDelivery.Controllers
{
    public class CourierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}