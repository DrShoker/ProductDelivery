using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using System;

namespace DataAccessLayer.Repositories
{
    public class DeliveryRepos : IRepository<Delivery>
    {
        LayerContext db;

        public DeliveryRepos(LayerContext context)
        {
            db = context;
        }

        public void Create(Delivery item)
        {
            db.Deliveries.Add(item);
        }

        public void Delete(int id)
        {
            var d = db.Deliveries.Find(id);
            if (d != null)
                db.Deliveries.Remove(d);
        }

        public IEnumerable<Delivery> Find(Func<Delivery, bool> predicate)
        {
            return db.Deliveries.Where(predicate);
        }

        public Delivery Get(int id)
        {
            return db.Deliveries.Find(id);
        }

        public IEnumerable<Delivery> GetAll()
        {
            return db.Deliveries;
        }

        public void Update(Delivery item)
        {
            db.Deliveries.Update(item);
        }
    }
}
