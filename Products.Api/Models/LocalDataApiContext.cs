namespace Products.Api.Models
{
    using Products.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class LocalDataApiContext : DataContext
    {
        public System.Data.Entity.DbSet<Products.Domain.Product> Products { get; set; }

        public System.Data.Entity.DbSet<Products.Domain.Category> Categories { get; set; }
    }
}