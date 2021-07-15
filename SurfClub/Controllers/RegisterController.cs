using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurfClub.Helpers;
using SurfClub.Models;
using SurfClub.Models.dbModels;
using SurfClub.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SurfClub.Controllers
{
    public class RegisterController : Controller
    {
        private readonly SurfClubDBContext dbContext;
        public RegisterController(SurfClubDBContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User model, IFormFile imageData)
        {

            if (ModelState.IsValid)
            {
                ///проверять, что пользователь есть в БД
                var user = dbContext.Users.FirstOrDefault(c =>
                c.Nickname == model.Nickname);

                //Прописать условие
                if (user != null)
                {
                    ModelState.AddModelError(string.Empty, "Такой пользователь уже зарегестрирован");
                    return View("Index", model);
                }
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Пароли не совпадают");
                    return View("Index", model);
                }
                else
                {
                    model.Photo = ImageHelper.UploadImage(imageData);

                    // авторизовать на сайте, что-то сделать
                    dbContext.Users.Add(model);
                    dbContext.SaveChanges();



                    var claims = new List<Claim>
            {



                new Claim(ClaimsIdentity.DefaultNameClaimType, model.Id.ToString())
            };
                    var authProp = new AuthenticationProperties();

                 

                    // создаем объект ClaimsIdentity
                    ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    // установка аутентификационных куки
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(id), authProp);

                    HttpContext.Session.SetString("NickName", model.Nickname);
                    HttpContext.Session.SetString("Photo", model.Photo.ToString());
                    HttpContext.Session.SetInt32("UserId", model.Id);

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
