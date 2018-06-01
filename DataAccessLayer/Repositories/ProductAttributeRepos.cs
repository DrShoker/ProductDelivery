using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ProductAttributeRepos : Repository<ProductAttribute>
    {
        public ProductAttributeRepos(LayerContext context)
            : base(context)
        {
            db = context;
        }
    }
}
