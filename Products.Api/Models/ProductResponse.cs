﻿namespace Products.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class ProductResponse
    {

        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastPurchase { get; set; }

        public double Stock { get; set; }

        public string Remarks { get; set; }



    }
}