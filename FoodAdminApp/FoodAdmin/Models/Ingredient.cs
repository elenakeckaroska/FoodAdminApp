using FoodAdmin.Models;
using System;
using System.Collections.Generic;

namespace FoodApp.Models.Models
{
    public class Ingredient
    {
        public Guid id { get; set; }

        public string name { get; set; }
        public double quantity { get; set; }

        public string unitOfMeasurement { get; set; }

        //public virtual Recipe Recipe { get; set; }

        public Guid recipeId { get; set; }
    }
}
