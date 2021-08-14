using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealerAPI
{
    public class Car
    {
        [Key]
        public string VIN { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }      
        public decimal Price { get; set; }     
    }
}
