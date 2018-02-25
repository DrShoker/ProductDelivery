using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class ClientRepos : IRepository<Client>
    {
        LayerContext db;

        public ClientRepos(LayerContext context)
        {
            db = context;
        }

        public void Create(Client item)
        {
            db.Clients.Add(item);
        }

        public void Delete(int id)
        {
            var c = db.Clients.Find(id);
            if (c != null)
                db.Clients.Remove(c);
        }

        public IEnumerable<Client> Find(Func<Client, bool> predicate)
        {
            return db.Clients.Where(predicate);
        }

        public Client Get(int id)
        {
            return db.Clients.Find(id);
        }

        public IEnumerable<Client> GetAll()
        {
            return db.Clients;
        }

        public void Update(Client item)
        {
            db.Clients.Update(item);
        }
    }
}
