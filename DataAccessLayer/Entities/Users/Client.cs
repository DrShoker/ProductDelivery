using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Client : User
    {
        public int Bonuses { get; set; }
        public bool IsBlocked { get; set; }
        public int Misses { get; set; }

        public List<Delivery> Deliveries { get; set; }
    }
}
