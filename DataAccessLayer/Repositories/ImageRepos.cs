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
    public class ImageRepos : Repository<Image>
    {
        public ImageRepos(LayerContext context)
            : base(context)
        {
            set = db.Images;
        }
    }
}
