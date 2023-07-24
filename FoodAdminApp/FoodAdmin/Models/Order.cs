using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodAdmin.Models
{
    public class Order : BaseEntity
    {
        public string userId { get; set; }
        public string username { get; set; }
        public virtual ICollection<CookingClassInOrder> classesInOrder { get; set; }

        public Order() {
            this.classesInOrder = new List<CookingClassInOrder>();

        }
    }
}
