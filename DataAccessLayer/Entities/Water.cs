using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Water: Product
    {
        public string Type { get; set; }
        public double Size { get; set; }
        public string Shell { get; set; }
    }
}
