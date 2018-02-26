using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;

namespace DataAccessLayer.Repositories
{
    public class ImageRepos : IRepository<Image>
    {
        LayerContext db;

        public ImageRepos(LayerContext context)
        {
            db = context;
        }

        public void Create(Image item)
        {
            db.Images.Add(item);
        }

        public void Delete(int id)
        {
            var i = db.Images.Find(id);
            if (i != null)
                db.Images.Remove(i);
        }

        public IEnumerable<Image> Find(Func<Image, bool> predicate)
        {
            return db.Images.Where(predicate);
        }

        public Image Get(int id)
        {
            return db.Images.Find(id);
        }

        public IEnumerable<Image> GetAll()
        {
            return db.Images;
        }

        public void Update(Image item)
        {
            db.Images.Update(item);
        }
    }
}
