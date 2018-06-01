using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ProductRepos 
        : Repository<Product>
    {
        public ProductRepos(LayerContext context)
            : base(context)
        {
            set = db.Products;
        }
    }
}
