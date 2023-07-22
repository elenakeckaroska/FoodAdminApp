using FoodAdmin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();

        }
        [HttpGet]
        public IActionResult Login()
        {
            UserLoginDto model = new UserLoginDto();
            return View(model);
            
        }

        [HttpPost]
        public IActionResult Login(UserLoginDto model)
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44327/api/admin/index";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");


            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var result = response.Content.ReadAsStringAsync().Result;

            HttpContext.Session.SetString("IsAdmin", result);
            HttpContext.Session.SetString("Username", model.Email);

            if (result == "true")
                return RedirectToAction("Index");
            else
                return RedirectToAction("Login");

        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login");

        }

        [HttpGet]
        public IActionResult probvanje()
        {
            string myVariable = HttpContext.Session.GetString("IsAdmin");
            ViewData["item"] = myVariable;
            return View();
        }

        [HttpGet]
        public IActionResult AddCookingClasses()
        {
            string myVariable = HttpContext.Session.GetString("IsAdmin");
            ViewData["item"] = myVariable;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
