using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductDelivery.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Newtonsoft.Json;

namespace ProductDelivery.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult GetAdmins()
        {
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Admin/").Result;

            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                ViewBag.Result = JsonConvert.DeserializeObject<IEnumerable<Admin>>(data);
            }

            else
                ViewBag.Result = "Error";
            return RedirectToAction("GetAdmins");
        }

        [HttpGet]
        public IActionResult GetAdminById(int id)
        {
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"api/Admin/getadminbyid/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                ViewBag.Result = JsonConvert.DeserializeObject<Admin>(data);
            }

            else
                ViewBag.Result = "Error";
            return RedirectToAction("GetAdmins");
        }

        [HttpGet]
        public IActionResult GetAdminByName(string name)
        {
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"api/Admin/getadminbyname/{name}").Result;

            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                ViewBag.Result = JsonConvert.DeserializeObject<IEnumerable<Client>>(data);
            }

            else
                ViewBag.Result = "Error";
            return RedirectToAction("GetAdmins");
        }

        [HttpPost]
        public IActionResult CreateAdmin(Admin admin)
        {
            var clientJson = JsonConvert.SerializeObject(admin);
            string data;
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(UrlContacts.BaseUrl);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.PostAsync($"api/Admin/", new StringContent(clientJson, Encoding.UTF8, "application/json")).Result;

            data = response.Content.ReadAsStringAsync().Result;
            IEnumerable<Admin> admins = JsonConvert.DeserializeObject<IEnumerable<Admin>>(data);
            //ViewBag.Result = products;
            return RedirectToAction("GetAdmins");
        }

        [HttpDelete]
        public IActionResult DeleteAdmin(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.DeleteAsync($"api/Admin/deleteadmin/{id}").Result;

            return RedirectToAction("GetAdmins");
        }

        [HttpPut]
        public IActionResult EditAdmin(Admin admin)
        {
            var adminJson = JsonConvert.SerializeObject(admin);
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(UrlContacts.BaseUrl);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.PutAsync("api/Admin/", new StringContent(adminJson, Encoding.UTF8, "application/json")).Result;

            return RedirectToAction("GetAdmins");
        }
    }
}