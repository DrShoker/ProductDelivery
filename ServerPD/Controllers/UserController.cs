using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerPD.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace ServerPD.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        EFUnitOfWork db = new EFUnitOfWork();

        [HttpGet]
        public IEnumerable<Client> GetProducts()
        {
            List<Client> clients = db.Clients.GetAll().ToList();
            return clients;
        }

        [HttpGet("getclientbyid/{id}")]
        public IActionResult GetClientDeliveries(int id)
        {
            Client client = db.Clients.Get(id);
            if (client == null)
                return NotFound();
            return new ObjectResult(client);
        }

        [HttpGet("getclientbyname/{name}")]
        public IEnumerable<Client> GetClientByName(string name)
        {
            List<Client> clients = db.Clients.GetAll().Where(c => c.Name.Contains(name)).ToList();
            return clients;
        }

        public IActionResult CreateClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clients.Create(client);
            db.Save();
            return CreatedAtRoute("DefaultApi", new { id = client.Id }, client);
        }

        [HttpDelete("deletepdroduct/{id}")]
        public IActionResult DeleteClient(int id)
        {
            Product product = db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            db.Products.Delete(id);
            db.Save();
            return Ok(product);
        }

        [HttpPut]
        public IActionResult EditClient(int id, Client client)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != client.Id)
                return BadRequest();

            db.Clients.Update(client);
            db.Save();

            return Ok(client);
        }

        [Authorize]
        public IActionResult Index()
        {
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            return Content(role);
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
            if (ModelState.IsValid)
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

        Task<string> GetUserRole(string Email, string Pas)
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

                        await Authenticate(model.Email, "client");

                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("", "Некорректные логин и (или) пароль");
                }
                return View(model);
            }
        }

        private async Task Authenticate(string userName, string role)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.
                SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id)
                );
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }
    }
}