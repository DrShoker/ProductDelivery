using DataAccessLayer.Entities.Products;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{

    public class Product
    {
        public int Id { get; set; }
        public Enums.Departments Department { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public int Count { get; set; }

        string ImgPath { get; set; }

        public ICollection<ProductAttribute> Attributes { get; set; }
        public ICollection<DeliveryProduct> DeliveryAndProduct { get; set; }
    }
}
