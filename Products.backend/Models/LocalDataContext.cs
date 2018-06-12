namespace Products.backend.Models
{
    using Products.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class LocalDataContext: DataContext
    {
        public System.Data.Entity.DbSet<Products.Domain.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<Products.Domain.Ubication> Ubications { get; set; }
    }
}