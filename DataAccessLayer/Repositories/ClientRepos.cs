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
    public class ClientRepos : Repository<Client>
    {
        public ClientRepos(LayerContext context)
            : base(context)
        {
            set = db.Clients;
        }
    }
}
