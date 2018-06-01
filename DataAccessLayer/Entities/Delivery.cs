using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Enums;

namespace DataAccessLayer.Entities
{
    public class Delivery
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DeliveryStatus Status { get; set; }
        public Delivery()
        {
            Date = DateTime.Now;
        }

        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int CourierId { get; set; }
        public Courier Courier { get; set; }
        public List<DeliveryProduct> DeliveryAndProducts { get; set; }
    }
}
