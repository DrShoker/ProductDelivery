using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductDelivery.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace ProductDelivery.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public IActionResult GetAdmins()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(UrlContacts.BaseUrl);

            return View();
        }
    }
}