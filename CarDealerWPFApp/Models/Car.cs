using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerWPFApp.Models
{
    class Car
    {      
        public string vin { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public int year { get; set; }
        public decimal price { get; set; }
    }
}
