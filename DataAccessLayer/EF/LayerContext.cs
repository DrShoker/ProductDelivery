using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities
{
    public class LayerContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<DeliveryProduct> DeliveriesProducts { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Image> Images { get; set; }

        public LayerContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS01;Database=ProductDeliveryDB21;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Product)
                .WithOne(p => p.Image)
                .HasForeignKey<Product>();
            modelBuilder.Entity<Product>()
                .HasOne(p=>p.Image)
                .WithOne(p => p.Product)
                .HasForeignKey<Image>();
        }
    }
}
