using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class PizzaElement
    {
        public int id { get; set; }

        public string name { get; set; }

        public double price { get; set; }

        public string imgURL { get; set; }

        public string type { get; set; }

    }
}