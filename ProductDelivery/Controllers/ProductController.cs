using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using DataAccessLayer.Enums;
using System.Net.Http;
using ProductDelivery.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ProductDelivery.Controllers
{
    public class ProductController : Controller
    {
        EFUnitOfWork db = new EFUnitOfWork();

        public IActionResult GetProducts()
        {
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Product/").Result;

            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                ViewBag.Result = JsonConvert.DeserializeObject<IEnumerable<Product>>(data);
            }

            else
                ViewBag.Result = "Error";
            return View();

        }

        [HttpGet]
        public IActionResult OpenProducts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Catalog(Departments dep)
        {
            Departments department = dep;
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"api/Product/getcatalog/{dep}").Result;


                data = response.Content.ReadAsStringAsync().Result;
                IEnumerable<Product> products = JsonConvert.DeserializeObject<IEnumerable<Product>>(data);
                //ViewBag.Result = products;
                ViewBag.Dep = dep.ConvertToString();

            return View(products);

        }
    }

        //[HttpGet]
        //public IActionResult Catalog(Departments dep)
        //{
        //    Departments department = dep;
        //    List<Product> products = db.Products.GetAll().Where(p=>p.Department==department).
        //        OrderBy(p=>p.Type).ToList();
        //    ViewBag.Dep = dep.ConvertToString();
        //    return View(products);
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
