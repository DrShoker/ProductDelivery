using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Courier : User
    {
        public int DeliveryCounter { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public ICollection<Delivery> Deliveries { get; set; }
    }
}
