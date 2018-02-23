using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Alcohol: Water
    {
        public double Degree { get; set; }
        public int Year{ get; set; }
    }
}
