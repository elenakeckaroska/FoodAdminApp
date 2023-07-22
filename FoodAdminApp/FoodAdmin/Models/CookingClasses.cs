using FoodApp.Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FoodAdmin.Models
{
    public class CookingClasses 
    {
        public Guid id { get; set; }
        public string link { get; set; }
        public DateTime dateTime { get; set; }
        public int price { get; set; }
        public Guid recipeId { get; set; }
        public int maxParticipants { get; set; }

        public string recipeTitle { get; set; }

        [JsonIgnore]
        public virtual ICollection<CookingClassesUser> CookingClassesUser { get; set; }

        public CookingClasses()
        {
            this.CookingClassesUser = new List<CookingClassesUser>();
        }
    }

}
