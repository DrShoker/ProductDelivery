using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ProductDelivery.Models;
using ProductDelivery.Models.ViewModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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
                    db.Clients.Create(new Client { Email = model.Email, Password = model.Password });
                    await db.SaveChangesAsync();
                    await Authenticate(model.Email, "client");
                    return RedirectToAction("Index", "Home");
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