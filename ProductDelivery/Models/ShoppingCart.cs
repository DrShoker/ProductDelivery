using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductDelivery.Models
{
    public class ShoppingCart
    {
        Dictionary<Product, int> products = new Dictionary<Product, int>();

        public int Length {
            get {
                return products.Keys.Count;
            }
        }

        public IEnumerable<Product> Products {
            get
            {
                return products.Keys;
            }
        }

        public Delivery ToDelivery()
        {
            List<DeliveryProduct> pd = products.Keys.Select(p=>new DeliveryProduct(p.Id,products[p])).ToList();
            var delivery = new Delivery(pd);
            return delivery;
        }

        public int this[int prodId]
        {
            get
            {
                var prod = products.Keys.FirstOrDefault(p => p.Id == prodId);
                if (prod == null)
                    return 0;
                return products[prod];
            }
        }

        public void Add(Product product)
        {
            if (!products.Keys.Select(p => p.Id).Contains(product.Id))
            {
                products.Add(product, 1);
            }
            else
            {
                products[products.Keys.First(p=> p.Id == product.Id)]++;
            }
        }

        public void Remove(int productId)
        {
            var prodToDelete = products.Keys.FirstOrDefault(p => p.Id == productId);
            if (prodToDelete!=null)
            {
                if (--products[prodToDelete] == 0)
                    products.Remove(prodToDelete);
            }
        }
    }
}
