using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductDelivery.Models;
using DataAccessLayer.Repositories;
using System.IO;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ProductDelivery.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            using (EFUnitOfWork db = new EFUnitOfWork())
            {
                List<int> AdminsImg = db.Admins.GetAll().Select(i => i.Id).ToList();
                return View(AdminsImg);
            }
        }
    }
}
