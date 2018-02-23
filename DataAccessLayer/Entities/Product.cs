using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public abstract class Product
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
    }
}
