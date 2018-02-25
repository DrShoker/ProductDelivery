using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;

namespace DataAccessLayer.Repositories
{
    public class ProductAttributeRepos : IRepository<ProductAttribute>
    {
        private LayerContext db;

        public ProductAttributeRepos(LayerContext context)
        {
            db = context;
        }

        public void Create(ProductAttribute item)
        {
            db.ProductAttributes.Add(item);
        }

        public void Delete(int id)
        {
            var pa = db.ProductAttributes.Find(id);
            if (pa != null)
                db.ProductAttributes.Remove(pa);
        }

        public IEnumerable<ProductAttribute> Find(Func<ProductAttribute, bool> predicate)
        {
            return db.ProductAttributes.Where(predicate);
        }

        public ProductAttribute Get(int id)
        {
            return db.ProductAttributes.Find(id);
        }

        public IEnumerable<ProductAttribute> GetAll()
        {
            return db.ProductAttributes;
        }

        public void Update(ProductAttribute item)
        {
            db.ProductAttributes.Update(item);
        }
    }
}
