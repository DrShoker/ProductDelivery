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
    public class DeliveryRepos : Repository<Delivery>
    {
        public DeliveryRepos(LayerContext context)
            : base(context)
        {
            set = db.Deliveries;
        }
    }
}
