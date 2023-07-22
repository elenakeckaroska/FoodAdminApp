using System;
using System.Collections.Generic;
using System.Text;

namespace FoodAdmin.Models
{
    public class CookingClassesUser
    {
        public Guid id { get; set; }
        public Guid cookingClassesID { get; set; }
  
        public string username { get; set; }

        public string userId { get; set; }

    }
}
