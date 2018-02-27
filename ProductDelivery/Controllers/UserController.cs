using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ProductDelivery.Models;
using ProductDelivery.Models.ViewModels; // пространство имен моделей RegisterModel и LoginModel
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace ProductDelivery.Controllers
{
    public class UserController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return Content(User.Identity.Name);
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
            using (EFUnitOfWork db = new EFUnitOfWork())
            {
                var client = await db.Clients.GetAll().ToAsyncEnumerable().
                    FirstOrDefault(c => c.Email == model.Email);
                if (client != null)
                {
                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
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

                        await Authenticate(model.Email);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("", "Некорректные логин и (или) пароль");
                }
                return View(model);
            }
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
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