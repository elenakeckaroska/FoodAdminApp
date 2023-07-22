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
using System.Net.Http;
using System.Text;

namespace FoodAdmin.Controllers
{
    public class CookingClassesController : Controller
    {


        public IActionResult Index()
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

            foreach(var coookingClass in cookingClasses)
            {
                foreach(var classUser in cookingClassesUsers)
                {
                    if(coookingClass.id == classUser.cookingClassesID)
                    {
                        coookingClass.CookingClassesUser.Add(classUser);
                    }
                }
            }
            return View(cookingClasses);
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


    }
}
