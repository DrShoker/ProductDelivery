﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using ProductDelivery.Models;

namespace ProductDelivery.Services
{
    public class CardService : ICardService
    {
        Dictionary<string, ShoppingCart> card = new Dictionary<string, ShoppingCart>();

        public int Length => card.Keys.Count;

        public void Add(string userId,int productId)
        {
            if(!card.Keys.Contains(userId))
            {
                ShoppingCart userCard = new ShoppingCart();
                card.Add(userId, userCard);
            }
            card[userId].Add(productId);
        }

        public void CheckOut()
        {
            throw new NotImplementedException();
        }

        public void Remove(string userId,int productId)
        {
            if(card.Keys.Contains(userId))
            { 
                    card[userId].Remove(productId);
            }
        }

        public ShoppingCart GetCard(string userId)
        {
            if (!card.Keys.Contains(userId))
                return null;
            return card[userId];
        }
    }
}
