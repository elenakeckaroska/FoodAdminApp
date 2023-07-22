using FoodAdmin.Models;
using FoodApp.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace FoodAdmin.Controllers
{
    public class RecipesController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:44327/recipe/GetAll";

            HttpResponseMessage response = client.GetAsync(URL).Result;


            var result = response.Content.ReadAsStringAsync().Result;

            List<Recipe> recipes = System.Text.Json.JsonSerializer.Deserialize<List<Recipe>>(result);

            //ingridients

            HttpClient client1 = new HttpClient();
            string URL1 = "https://localhost:44327/recipe/GetAllIngredients";

            HttpResponseMessage response1 = client1.GetAsync(URL1).Result;


            var result1 = response1.Content.ReadAsStringAsync().Result;

            List<Ingredient> ingredients = System.Text.Json.JsonSerializer.Deserialize<List<Ingredient>>(result1);



            //Cooking Classes

            HttpClient client3 = new HttpClient();

            string URL3 = "https://localhost:44327/CookingClasses/GetAllForAdmin";

            HttpResponseMessage response3 = client.GetAsync(URL3).Result;


            var result3 = response3.Content.ReadAsStringAsync().Result;

            List<CookingClasses> cookingClasses = System.Text.Json.JsonSerializer.Deserialize<List<CookingClasses>>(result3);


            foreach (var recipe in recipes)
            {
                foreach(var ingredient in ingredients)
                {
                    if(recipe.id == ingredient.recipeId)
                    {
                        recipe.Ingridients.Add(ingredient);
                    }

                    if (cookingClasses.Select(c => c.recipeId)
                        .Contains(recipe.id))
                    {
                        recipe.isScheduledClassFor = true;
                    }
                    else
                    {
                        recipe.isScheduledClassFor = false;
                    }

                }
            }




            return View(recipes);
        }
    }
}
