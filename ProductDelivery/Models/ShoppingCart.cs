using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductDelivery.Models
{
    public class ShoppingCart
    {
        public Dictionary<int,int> products { get; set; }

        public void Add(int productId)
        {
            if(products.Keys.Contains(productId))
            {
                products.Add(productId, 1);
            }
            else
            {
                products[productId]++;
            }
        }

        public void Remove(int productId)
        {
            if(products.Keys.Contains(productId))
            {
                products.Remove(productId);
            }
        }
    }
}
