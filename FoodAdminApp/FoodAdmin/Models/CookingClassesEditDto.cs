using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace FoodAdmin.Models
{
    public class CookingClassesEditDto
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

        public CookingClassesEditDto()
        {
            this.CookingClassesUser = new List<CookingClassesUser>();
        }
    }
}

