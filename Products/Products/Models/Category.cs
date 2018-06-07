using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }


    }
}
