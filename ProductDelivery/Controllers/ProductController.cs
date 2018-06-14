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
using System.Text;

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

        [HttpGet]
        public IActionResult GetProduct(int id)
        {
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"api/Product/getproduct/{id}").Result;


            data = response.Content.ReadAsStringAsync().Result;
            IEnumerable<Product> products = JsonConvert.DeserializeObject<IEnumerable<Product>>(data);
            //ViewBag.Result = products;

            return View(products);

        }

        [HttpGet]
        public IActionResult GetProductsForName(string name)
        {
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"api/Product/getproductsforname/{name}").Result;


            data = response.Content.ReadAsStringAsync().Result;
            IEnumerable<Product> products = JsonConvert.DeserializeObject<IEnumerable<Product>>(data);
            //ViewBag.Result = products;

            return View(products);

        }

        [HttpGet]
        public ActionResult CreateProduct(int id = 0)
        {

            return View(new Product());
        }


        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            var productJson = JsonConvert.SerializeObject(product);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsync($"api/Product/", new StringContent(productJson, Encoding.UTF8, "application/json")).Result;
            //ViewBag.Result = products;

            return RedirectToAction("GetProducts");

        }

            [HttpPost]
            public IActionResult DeleteProduct(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.DeleteAsync($"api/Product/deleteproduct/{id}").Result;
            //ViewBag.Result = products;

            return RedirectToAction("GetProducts");
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            var productJson = JsonConvert.SerializeObject(product);
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PutAsync($"api/Product/", new StringContent(productJson, Encoding.UTF8, "application/json")).Result;
            //ViewBag.Result = products;

            return RedirectToAction("GetProducts");

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
