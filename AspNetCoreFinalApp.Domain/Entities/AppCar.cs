using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreFinalApp.Domain.Entities
{
    public class AppCar : BaseEntity
    {
        public string Developer { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }
    }
}
