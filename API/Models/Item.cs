using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public double ItemPrice { get; set; }

        public string ItemDescription { get; set; }

        public string ItemImageURL { get; set; }

        public string ItemType { get; set; }
    }
}