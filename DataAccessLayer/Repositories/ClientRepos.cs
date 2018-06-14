using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace DataAccessLayer.Repositories
{
    public class ClientRepos : Repository<Client>, IClientRepository
    {
        public ClientRepos(LayerContext context)
            : base(context)
        {
            set = db.Clients;
        }

        public Client GetWithDeliveries(int id)
        {
            var client = db.Clients.Find(id);
            if (client == null)
                return null;
            client.Deliveries = db.Deliveries
                .Where(d=>d.ClientId==id)
                .ToList();
            //include Delivery And Products
            client.Deliveries.ToList()
                .ForEach(d =>
                {
                    d.DeliveryAndProducts = db.DeliveriesProducts.Where(dp => dp.DeliveryId == d.Id).ToList();
                    d.Client = null;
                    });
            client.Deliveries
                .Select(d => d.DeliveryAndProducts).ToList()
                .ForEach(dps => dps.ToList().ForEach(dp => {
                    dp.Product = db.Products.First(p => p.Id == dp.ProductId);
                    dp.Product.DeliveryAndProduct = null;
                    dp.Delivery = null;
                }));
            return client;
        }
    }
}
