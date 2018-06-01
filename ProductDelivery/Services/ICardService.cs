using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using ProductDelivery.Models;

namespace ProductDelivery.Services
{
    public interface ICardService
    {
        int Length { get; }

        void Add(string userId,int productId);

        ShoppingCart GetCard(string userId);

        void Remove(string userId, int productId);

        void CheckOut();
    }
}
