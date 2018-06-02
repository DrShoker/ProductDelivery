using System;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductDelivery.Models;
using ProductDelivery.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProductDelivery.Controllers
{
    public class UserController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            return Content(role);
        }

        [HttpGet]
        public IEnumerable<Client> GetClients()
        {
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/User/").Result;

            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                ViewBag.Result = JsonConvert.DeserializeObject<IEnumerable<Client>>(data);
            }

            else
                ViewBag.Result = "Error";
            return View();
        }

        [HttpGet]
        public IActionResult GetClientById(int id)
        {
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"api/User/getclientbyid/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                ViewBag.Result = JsonConvert.DeserializeObject<Client>(data);
            }

            else
                ViewBag.Result = "Error";
            return View();
        }

        [HttpGet]
        public IEnumerable<Client> GetClientByName(string name)
        {
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"api/User/getclientbyid/{name}").Result;

            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                ViewBag.Result = JsonConvert.DeserializeObject<IEnumerable<Client>>(data);
            }

            else
                ViewBag.Result = "Error";
            return View();
        }

        [HttpPost]
        public IActionResult CreateClient(Client client)
        {
            var clientJson = JsonConvert.SerializeObject(client);
            string data;
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(UrlContacts.BaseUrl);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.PostAsync($"api/Product/", new StringContent(clientJson, Encoding.UTF8, "application/json")).Result;

            data = response.Content.ReadAsStringAsync().Result;
            IEnumerable<Client> clients = JsonConvert.DeserializeObject<IEnumerable<Client>>(data);
            //ViewBag.Result = products;
            return RedirectToAction("GetClients");
        }

        [HttpDelete]
        public IActionResult DeleteClient(int id)
        {
            string data;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlContacts.BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.DeleteAsync($"api/User/deleteclient/{id}").Result;

            return RedirectToAction("GetClients");
        }

        [HttpPut]
        public IActionResult EditClient(Client client)
        {
            var clientJson = JsonConvert.SerializeObject(client);
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(UrlContacts.BaseUrl);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.PutAsync("api/User/", new StringContent(clientJson, Encoding.UTF8, "application/json")).Result;

            return RedirectToAction("GetClients");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                using (EFUnitOfWork db = new EFUnitOfWork())
                {
                    var role = await GetUserRole(model.Email, model.Password);
                    if (role != null)
                    {
                        await Authenticate(model.Email, role);

                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            return View(model);
        }

        Task<string> GetUserRole(string Email,string Pas)
        {
            return Task.Run(() =>
            {
                string role = null;
                Task<object>[] tasks = new Task<object>[3]
                {
                    new Task<object>(()=>
                    {
                        using (EFUnitOfWork db = new EFUnitOfWork())
                        {
                            return db.Couriers.FirstOrDefault((u)=> u.Email==Email && u.Password==Pas);
                        }
                    }),
                    new Task<object>(()=>
                    {
                        using (EFUnitOfWork db = new EFUnitOfWork())
                        {
                            return db.Clients.FirstOrDefault((u)=> u.Email==Email && u.Password==Pas);
                        }
                    }),
                    new Task<object>(()=>
                    {
                        using (EFUnitOfWork db = new EFUnitOfWork())
                        {
                            return db.Admins.FirstOrDefault((u)=> u.Email==Email && u.Password==Pas);
                        }
                    })
                };
                foreach (var t in tasks)
                    t.Start();
                //Task.WaitAll(tasks);
                for (int i = 0; i < tasks.Length; i++)
                {
                    if (tasks[i].Result != null)
                    {
                        switch (i)
                        {
                            case 0:
                                role = "courier";
                                break;
                            case 1:
                                role = "client";
                                break;
                            case 2:
                                role = "admin";
                                break;
                        }
                        break;
                    }
                }
                return role;
            }
            );
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            using (EFUnitOfWork db = new EFUnitOfWork())
            {
                if (ModelState.IsValid)
                {
                    Client client = await db.Clients.GetAll().ToAsyncEnumerable().FirstOrDefault();
                    if (client == null)
                    {
                        db.Clients.Create(new Client { Email = model.Email, Password = model.Password });
                        await db.SaveChangesAsync();

                        await Authenticate(model.Email,"client");

                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("", "Некорректные логин и (или) пароль");
                }
                return View(model);
            }
        }

        private async Task Authenticate(string userName,string role)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimTypes.Role, role)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity identity = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.
                SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
                );
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }
    }
}