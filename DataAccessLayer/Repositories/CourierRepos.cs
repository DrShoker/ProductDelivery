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
    public class CourierRepos : Repository<Courier>
    {
        public CourierRepos(LayerContext context)
            : base(context)
        {
            set = db.Couriers;
        }
    }
}
