using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public Task<Delivery> FirstOrDefaultAsync(CancellationToken predicate)
        {
            return db.Deliveries.FirstOrDefaultAsync(predicate);
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
