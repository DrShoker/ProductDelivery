using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using System;

namespace DataAccessLayer.Repositories
{
    public class DeliveryProductRepos
        : IRepository<DeliveryProduct>
    {
        LayerContext db;

        public DeliveryProductRepos(LayerContext context)
        {
            db = context;
        }

        public void Create(DeliveryProduct item)
        {
            db.DeliveriesProducts.Add(item);
        }

        public void Delete(int id)
        {
            var dp = db.DeliveriesProducts.Find(id);
            if (dp!=null)
                db.DeliveriesProducts.Remove(dp);
        }

        public IEnumerable<DeliveryProduct> Find(Func<DeliveryProduct, bool> predicate)
        {
            return db.DeliveriesProducts.Where(predicate);
        }

        public DeliveryProduct Get(int id)
        {
            return db.DeliveriesProducts.Find(id);
        }

        public IEnumerable<DeliveryProduct> GetAll()
        {
            return db.DeliveriesProducts;
        }

        public void Update(DeliveryProduct item)
        {
            db.DeliveriesProducts.Update(item);
        }
    }
}
