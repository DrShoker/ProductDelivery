using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        LayerContext db;
        AdminsRepos adminsRepos;
        ClientRepos clientRepos;
        CourierRepos courierRepos;
        DeliveryRepos deliveryRepos;
        DeliveryProductRepos deliveryProductRepos;
        ProductRepos productRepos;
        ProductAttributeRepos productAttributeRepos;
        ImageRepos ImageRepos;

        public EFUnitOfWork()
        {
            db = new LayerContext();
        }

        public IRepository<Product> Products
        {
            get
            {
                return productRepos ?? new ProductRepos(db);
            }
        }

        public IRepository<ProductAttribute> ProductAttributes
        {
            get
            {
                return productAttributeRepos ?? new ProductAttributeRepos(db);
            }
        }

        public IRepository<Admin> Admins
        {
            get
            {
                return adminsRepos ?? new AdminsRepos(db);
            }
        }

        public IRepository<Client> Clients
        {
            get
            {
                return clientRepos ?? new ClientRepos(db);
            }
        }

        public IRepository<Courier> Couriers
        {
            get
            {
                return courierRepos ?? new CourierRepos(db);
            }
        }

        public IRepository<Delivery> Deliveries
        {
            get
            {
                return deliveryRepos ?? new DeliveryRepos(db);
            }
        }

        public IRepository<DeliveryProduct> DeliveriesProducts
        {
            get
            {
                return DeliveriesProducts ?? new DeliveryProductRepos(db);
            }
        }

        public IRepository<Image> Images
        {
            get
            {
                return ImageRepos ?? new ImageRepos(db);
            }
        }

        bool disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    db.Dispose();
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
