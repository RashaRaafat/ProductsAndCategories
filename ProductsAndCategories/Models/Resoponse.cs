using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndCategories.Models
{
    public class Resoponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public object result { get; set; }
    }
}
