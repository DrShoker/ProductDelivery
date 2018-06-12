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
        [Route("Client/{Id}")]
        public IActionResult Profile(int Id)


        {

            string adress = "/api/Client/getclientbyid/" + Id;
            HttpResponseMessage response = client.GetAsync(adress).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                Client client = JsonConvert.DeserializeObject<Client>(data);
            }
            return View(client);
        }
    }
}