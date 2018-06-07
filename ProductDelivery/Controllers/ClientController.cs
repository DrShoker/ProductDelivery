using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProductDelivery.Services;

namespace ProductDelivery.Controllers
{
    public class ClientController : Controller
    {
        HttpClient client;

        public ClientController(IConfiguration config)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(config["server"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Route("Client/{Id}")]
        public IActionResult Profile(int Id)
        {

            return View();
        }
    }
}