using Products.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products.backend.Models
{
    public class LocalDataContext: DataContext
    {
        public System.Data.Entity.DbSet<Products.Domain.Category> Categories { get; set; }
    }
}