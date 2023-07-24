using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodAdmin.Models
{
    public class CookingClassInOrder : BaseEntity
    {
        public Guid id { get; set; }
        public Guid classId { get; set; }
        public CookingClasses selectedClass { get; set; }

        public Guid orderId { get; set; }


    }
}
