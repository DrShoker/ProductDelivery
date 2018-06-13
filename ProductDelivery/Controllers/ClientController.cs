using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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

        [HttpGet]
        [Route("Client/{email}")]
        public IActionResult Profile(string email)
        {
            string adress = "/api/Client/getclientbyemail/" + email;
            HttpResponseMessage response = client.GetAsync(adress).Result;
            Client clientModel = null;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                clientModel = JsonConvert.DeserializeObject<Client>(data);
            }
            return View("Profile", clientModel);
        }
    }
}