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

        public LayerContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ProductDeliveryDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAttribute>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Attributes)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<DeliveryProduct>()
                .HasOne(p => p.Product)
                .WithMany(p => p.DeliveryAndProduct)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<DeliveryProduct>()
                .HasOne(p => p.Delivery)
                .WithMany(p => p.DeliveryAndProducts)
                .HasForeignKey(p => p.DeliveryId);

            modelBuilder.Entity<Delivery>()
                .HasOne(p => p.Client)
                .WithMany(p => p.Deliveries)
                .HasForeignKey(p => p.ClientId);

            modelBuilder.Entity<Delivery>()
                .HasOne(p => p.Courier)
                .WithMany(p => p.Deliveries)
                .HasForeignKey(p => p.CourierId);
        }
    }
}
