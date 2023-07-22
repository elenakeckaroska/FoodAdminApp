using System.Collections.Generic;
using System;
using FoodApp.Models.Models;

namespace FoodAdmin.Models
{
    public class Recipe
    {
        public Guid id { get; set; }

        public string title { get; set; }

        public string preparationDescription { get; set; }
        public string category { get; set; }


        public virtual ICollection<Ingredient> Ingridients { get; set; }

        public string ownerOfRecipeId { get; set; }

        public bool isScheduledClassFor { get; set; }

        public Recipe()
        {
            this.Ingridients = new List<Ingredient>();
        }


        //public virtual CookingClasses CookingClass { get; set; }


    }
}
