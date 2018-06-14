using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductDelivery.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ProductDelivery.Controllers
{
    public class CourierController : Controller
    {
        EFUnitOfWork db = new EFUnitOfWork();

        [HttpGet]
        public IActionResult GetCouriers()
        {
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Courier/").Result;

            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                ViewBag.Result = JsonConvert.DeserializeObject<IEnumerable<Courier>>(data);
            }

            else
                ViewBag.Result = "Error";
            return View();
        }
        
        [HttpGet]
        public IActionResult CourierPersonalArea()
        {
            string email = User.Identity.Name;
            Courier courier = db.Couriers.FirstOrDefault(c => c.Email == email);
            ViewBag.Result = courier;

            return View();
        }

        [HttpGet]
        public IActionResult GetCourierById(int id)
        {
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"api/Courier/getcourierbyid/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                ViewBag.Result = JsonConvert.DeserializeObject<Courier>(data);
            }

            else
                ViewBag.Result = "Error";
            return View();
        }

        [HttpGet]
        public IActionResult GetCourierByName(string name)
        {
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"api/Courier/getcourierbyname/{name}").Result;

            data = response.Content.ReadAsStringAsync().Result;
            IEnumerable<Courier> couriers = JsonConvert.DeserializeObject<IEnumerable<Courier>>(data);
            //ViewBag.Result = products;

            return View(couriers);
        }

        [HttpPost]
        public IActionResult CreateCourier(Courier courier)
        {
            var courierJson = JsonConvert.SerializeObject(courier);
            string data;
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(UrlContacts.BaseUrl);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.PostAsync($"api/Courier/", new StringContent(courierJson, Encoding.UTF8, "application/json")).Result;

            data = response.Content.ReadAsStringAsync().Result;
            IEnumerable<Courier> couriers = JsonConvert.DeserializeObject<IEnumerable<Courier>>(data);
            //ViewBag.Result = products;
            return RedirectToAction("GetCouriers");
        }

        [HttpDelete]
        public IActionResult DeleteCourier(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.DeleteAsync($"api/Courier/deletecourier/{id}").Result;

            return RedirectToAction("GetCouriers");
        }

        [HttpPost]
        public IActionResult EditCourier(Courier courier)
        {
            var courierJson = JsonConvert.SerializeObject(courier);
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(UrlContacts.BaseUrl);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.PutAsync("api/Courier/", new StringContent(courierJson, Encoding.UTF8, "application/json")).Result;

            return RedirectToAction("CourierPersonalArea");
        }

        [HttpGet]
        public IActionResult GetDeliveriesByCurrentCourier(Courier courier)
        {
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"api/Delivery/getcourierdeliveries/{courier}").Result;

            data = response.Content.ReadAsStringAsync().Result;
            IEnumerable<Delivery> deliveries = JsonConvert.DeserializeObject<IEnumerable<Delivery>>(data);
            //ViewBag.Result = products;

            return View(deliveries);
        }
    }
}