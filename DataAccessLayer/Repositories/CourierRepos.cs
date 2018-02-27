using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class CourierRepos : IRepository<Courier>
    {
        private LayerContext db;

        public CourierRepos(LayerContext context)
        {
            db = context;
        }

        public void Create(Courier item)
        {
            db.Couriers.Add(item);
        }

        public void Delete(int id)
        {
            Courier c = db.Couriers.Find(id);
            if(c!=null)
                db.Couriers.Remove(c);
        }

        public IEnumerable<Courier> Find(Func<Courier, bool> predicate)
        {
            return db.Couriers.Where(predicate);
        }

        public Task<Courier> FirstOrDefaultAsync(CancellationToken predicate)
        {
            return db.Couriers.FirstOrDefaultAsync(predicate);
        }

        public Courier Get(int id)
        {
            return db.Couriers.Find(id);
        }

        public IEnumerable<Courier> GetAll()
        {
            return db.Couriers;
        }

        public void Update(Courier item)
        {
            db.Couriers.Update(item);
        }
    }
}
