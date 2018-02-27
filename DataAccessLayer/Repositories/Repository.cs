using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public abstract class Repository<T>
        : IRepository<T> where T : class
    {
        protected LayerContext db;
        protected DbSet<T> set;

        public Repository(LayerContext context)
        {
            db = context;
        }

        public void Create(T item)
        {
            set.Add(item);
        }

        public void Delete(int id)
        {
            T obj = set.Find(id);
            if(obj!=null)
                set.Remove(obj);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return set.Where(predicate);
        }

        public Task<T> FirstOrDefaultAsync(CancellationToken predicate)
        {
            return set.FirstOrDefaultAsync();
        }

        public T Get(int id)
        {
            return set.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return set;
        }

        public void Update(T item)
        {
            set.Update(item);
        }
    }
}
