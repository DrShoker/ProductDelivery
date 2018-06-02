﻿using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Enums;

namespace DataAccessLayer.Entities
{
    public class DeliveryProduct
    {
        public DeliveryProduct()
        {

        }

        public DeliveryProduct(
            int prodId,
            int count
            )
        {
            ProductId = prodId; 
            Count = count;
        }

        public int Id { get; set; }
        public int Count { get; set; }

        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
