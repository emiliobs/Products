using Products.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products.Api.Models
{
    public class ProductRequest : Product
    {
        public byte[] ImageArray { get; set; }
    }
}