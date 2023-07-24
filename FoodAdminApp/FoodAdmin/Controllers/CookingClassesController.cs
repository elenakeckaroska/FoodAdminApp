using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using FoodAdmin.Models;
using FoodApp.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace FoodAdmin.Controllers
{
    public class CookingClassesController : Controller
    {


        public IActionResult Index()
        {
            List<CookingClasses> cookingClasses = this.GetAllCookingClasses();
            return View(cookingClasses);
        }

        public List<CookingClasses> GetAllCookingClasses()
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44327/CookingClasses/GetAllForAdmin";

            HttpResponseMessage response = client.GetAsync(URL).Result;


            var result = response.Content.ReadAsStringAsync().Result;

            List<CookingClasses> cookingClasses = System.Text.Json.JsonSerializer.Deserialize<List<CookingClasses>>(result);


            HttpClient client1 = new HttpClient();

            string URL1 = "https://localhost:44327/CookingClasses/GetAllCookingClassesUser";

            HttpResponseMessage response1 = client.GetAsync(URL1).Result;


            var result1 = response1.Content.ReadAsStringAsync().Result;

            List<CookingClassesUser> cookingClassesUsers = System.Text.Json.JsonSerializer.Deserialize<List<CookingClassesUser>>(result1);

            foreach (var coookingClass in cookingClasses)
            {
                foreach (var classUser in cookingClassesUsers)
                {
                    if (coookingClass.id == classUser.cookingClassesID)
                    {
                        coookingClass.CookingClassesUser.Add(classUser);
                    }
                }
            }

            return cookingClasses;
        }
        public IActionResult Create(Guid id, string recipeTitle)
        {
            CookingClasses model = new CookingClasses();
            model.recipeId = id;
            model.recipeTitle = recipeTitle;
            
         
            return View(model);
        }

        // POST: CookingClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,link,price,dateTime,recipeId,maxParticipants, recipeTitle")] CookingClasses cookingClasses)
        {

            HttpClient client = new HttpClient();
            string URL = "https://localhost:44327/CookingClasses/Create";

            string jsonContent = JsonConvert.SerializeObject(cookingClasses);


            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            //cookingClassesService.Create(cookingClasses);

            //List<Recipe> recipes = _context.Recipes
            //    .Include(r=>r.CookingClass)
            //    .Where(r => r.CookingClass == null).ToList();

            //ViewData["RecipeId"] = new SelectList(recipes, "Id", "Title", cookingClasses.RecipeId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("id,link,price,dateTime,recipeId,maxParticipants, recipeTitle")] CookingClasses cookingClasses)
        {

            HttpClient client = new HttpClient();
            string URL = "https://localhost:44327/CookingClasses/Edit";

            string jsonContent = JsonConvert.SerializeObject(cookingClasses);


            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            //cookingClassesService.Create(cookingClasses);

            //List<Recipe> recipes = _context.Recipes
            //    .Include(r=>r.CookingClass)
            //    .Where(r => r.CookingClass == null).ToList();

            //ViewData["RecipeId"] = new SelectList(recipes, "Id", "Title", cookingClasses.RecipeId);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id, string recipeTitle)
        {

            HttpClient client = new HttpClient();

            string URL = "https://localhost:44327/CookingClasses/Details/"+id;

            HttpResponseMessage response = client.GetAsync(URL).Result;


            var result = response.Content.ReadAsStringAsync().Result;

            CookingClassesEditDto cookingClasses = System.Text.Json.JsonSerializer.Deserialize<CookingClassesEditDto>(result);

            
       

            return View(cookingClasses);
        }

        public IActionResult Details(Guid id, string recipeTitle)
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44327/CookingClasses/Details/" + id;

            HttpResponseMessage response = client.GetAsync(URL).Result;


            var result = response.Content.ReadAsStringAsync().Result;

            CookingClassesEditDto cookingClasses = System.Text.Json.JsonSerializer.Deserialize<CookingClassesEditDto>(result);
            return View(cookingClasses);
        }

        public IActionResult Delete(Guid id)
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44327/CookingClasses/Delete/" + id;

            HttpResponseMessage response = client.GetAsync(URL).Result;


            var result = response.Content.ReadAsStringAsync().Result;

            return RedirectToAction("Index");
        }


        public IActionResult GetAllOrders()
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44327/Order/GetOrders";

            HttpResponseMessage response = client.GetAsync(URL).Result;


            var result = response.Content.ReadAsStringAsync().Result;

            List<Order> orders = System.Text.Json.JsonSerializer.Deserialize<List<Order>>(result);


            List<CookingClasses> cookingClasses = this.GetAllCookingClasses();

            



            HttpClient client1 = new HttpClient();

            string URL1 = "https://localhost:44327/CookingClasses/GetAllCookingClassesInOrderAdmin";

            HttpResponseMessage response1 = client.GetAsync(URL1).Result;


            var result1 = response1.Content.ReadAsStringAsync().Result;

            List<CookingClassInOrder> classInOrder = System.Text.Json.JsonSerializer.Deserialize<List<CookingClassInOrder>>(result1);

            foreach (var order in orders)
            {
                foreach (var classOrder in classInOrder)
                {
                    if (order.id == classOrder.orderId)
                    {
                        order.classesInOrder.Add(classOrder);
                    }
                }
            }

            var resultOrders = from order in orders
                         from classOrder in order.classesInOrder
                         join cookingClass in cookingClasses on classOrder.classId equals cookingClass.id
                         select new
                         {
                             order.userId,
                             order.username,
                             CookingClassId = cookingClass.id,
                             cookingClass.dateTime,
                             cookingClass.price
                         };


            List<CookingClassViewModel> viewModel = resultOrders.Select(item => new CookingClassViewModel
            {
                Username = item.username,
                Price = item.price,
                RecipeTitle = GetRecipeTitle(item.CookingClassId) // Replace this with your logic to get the recipe title
            }).ToList();

            return View(viewModel);
        }
        private string GetRecipeTitle(Guid cookingClassId)
        {
            // Replace this with your logic to get the recipe title based on the cookingClassId
            // You may need to fetch it from your existing collections or data sources
            // For demonstration purposes, let's assume a simple dictionary lookup
            List<CookingClasses> cookingClasses = this.GetAllCookingClasses();


            return cookingClasses.Where(c => c.id == cookingClassId)
                .Select(c => c.recipeTitle)
                .FirstOrDefault();

        }

    }
}
