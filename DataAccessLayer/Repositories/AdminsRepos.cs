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
    public class AdminsRepos : IRepository<Admin>
    {
        private LayerContext db;

        public AdminsRepos(LayerContext context)
        {
            db = context;
        }

        public void Create(Admin item)
        {
            db.Admins.Add(item);
        }

        public void Delete(int id)
        {
            Admin a = db.Admins.Find(id);
            if (a != null)
                db.Admins.Remove(a);
        }

        public IEnumerable<Admin> Find(Func<Admin, bool> predicate)
        {
            return db.Admins.Where(predicate);
        }

        public Task<Admin> FirstOrDefaultAsync(CancellationToken predicate)
        {
            return db.Admins.FirstOrDefaultAsync(predicate);
        }

        public Admin Get(int id)
        {
            return db.Admins.Find(id);
        }

        public IEnumerable<Admin> GetAll()
        {
            return db.Admins;
        }

        public void Update(Admin item)
        {
            db.Admins.Update(item);
        }
    }
}
