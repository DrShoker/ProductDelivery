using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Entities.Products.ProductAttribute> ProductAttributes { get; }
        IRepository<Admin> Admins { get; }
        IClientRepository Clients { get; }
        IRepository<Courier> Couriers { get; }
        IRepository<Delivery> Deliveries { get; }
        IRepository<DeliveryProduct> DeliveriesProducts { get; }
        void Save();
    }
}
