using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities
{
    public class LayerContext : DbContext
    {
        public DbSet<Product> Products {set;get;}

        public LayerContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProductDeliveryDB;Trusted_Connection=True;");
        }
    }
}
