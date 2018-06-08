using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public bool IsActive { get; set; }

        public string Image { get; set; }

        public double Stock { get; set; }

        public DateTime LastPurchase { get; set; }

        public string Remarks { get; set; }

        public string ImageFullPath
        {
            get => $"http://productsbackend5.azurewebsites.net/{Image.Substring(1)}";
        }

        

    }
}
