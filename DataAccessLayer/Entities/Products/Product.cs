using DataAccessLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{

    public class Product
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int Price { get; set; }
        public int Name { get; set; }
        public string Type { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public int Count { get; set; }

        public List<ProductAttribute> Attributes { get; set; }
        public List<DeliveryProduct> DeliveryAndProduct { get; set; }
    }
}
