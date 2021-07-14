using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestProjectAndreev2._0.Models;

namespace TestProjectAndreev2._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyAppDbContext dbContext;


        public HomeController(ILogger<HomeController> logger, MyAppDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            ViewBag.Count = 9;
            ViewBag.Massage = "Привет кураторам!";
            ViewBag.Date = DateTime.Now;

            ViewBag.Names = new[] { "Арина","Даниил","Владимир","Иван" };

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {

            ViewBag.Group = dbContext.Students.ToList();
            return View();
        }

        public IActionResult Student()
        {
         
            var studentModel = dbContext.Students.FirstOrDefault(c=>c.Age>30);
            if (studentModel != null)
            {
                return View(studentModel);
            }
            else
            {
                return View("Error", new ErrorViewModel { });
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
