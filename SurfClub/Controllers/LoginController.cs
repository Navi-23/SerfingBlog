using Microsoft.AspNetCore.Mvc;
using SurfClub.Models;
using SurfClub.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SurfClub.Controllers
{
    public class LoginController : Controller
    {

        private readonly SurfClubDBContext dbContext;
        public LoginController(SurfClubDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
}
[HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ///проверять, что пользователь есть в БД
                var user = dbContext.Users.FirstOrDefault(c =>
                c.Nickname == model.Nickname && c.Password == model.Password);

                if(user == null){
                    ModelState.AddModelError(string.Empty, "Неверный псевдоним или пароль");
                    return View("Index", model);
                }
                else
                {

                    // авторизовать на сайте, что-то сделать

                    var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString())
            };
                    var authProp = new AuthenticationProperties();

                    if (model.RememberMe)
                    {
                        authProp.IsPersistent = true;
                        authProp.ExpiresUtc = DateTime.UtcNow.AddHours(2);
                    }

                    // создаем объект ClaimsIdentity
                    ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    // установка аутентификационных куки
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(id), authProp);

                    HttpContext.Session.SetString("NickName", user.Nickname);
                    HttpContext.Session.SetString("Photo", user.Photo.ToString());
                    HttpContext.Session.SetInt32("UserId", user.Id);

                    return RedirectToAction("Index", "Feed");
                }


            }
            return View("Index", model);
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Feed");
        }


    }
}
