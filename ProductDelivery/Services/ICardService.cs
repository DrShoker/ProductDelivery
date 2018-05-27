using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace ProductDelivery.Services
{
    public interface ICardService
    {

        void Add(string userId,int productId);

        IEnumerable<Product> GetProducts(string userId);

        void Remove(string userId, int productId);

        void CheckOut();
    }
}
