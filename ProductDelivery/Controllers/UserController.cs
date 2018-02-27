using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ProductDelivery.Models.ViewModels; // пространство имен моделей RegisterModel и LoginModel
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ProductDelivery.Controllers
{
    public class UserController : Controller
    {
        

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
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
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}