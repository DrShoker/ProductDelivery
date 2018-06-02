using DataAccessLayer.Entities;
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
            return RedirectToAction("GetCouriers");
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
            return RedirectToAction("GetCouriers");
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

            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                ViewBag.Result = JsonConvert.DeserializeObject<IEnumerable<Courier>>(data);
            }

            else
                ViewBag.Result = "Error";
            return RedirectToAction("GetCouriers");
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

        [HttpPut]
        public IActionResult EditCourier(Courier courier)
        {
            var courierJson = JsonConvert.SerializeObject(courier);
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(UrlContacts.BaseUrl);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.PutAsync("api/Courier/", new StringContent(courierJson, Encoding.UTF8, "application/json")).Result;

            return RedirectToAction("GetCouriers");
        }
    }
}