using DataAccessLayer.Entities;
using ServerPD.Interfaces;

namespace ServerPD.Helpers
{
    public class BodyBuilder : IBodyBuilder
    {
        public string CreateBody(Delivery delievery)
        {
            return $"Dear client, {delievery.Client.Name} " +
                   "\n" +
                   $"Your delievery {delievery.Id} " +
                   "\n" +
                   $"which includes: {delievery.DeliveryAndProducts} " +
                   "\n" +
                   $"has successefuly completed by {delievery.Courier.Name} " +
                   $"on {delievery.Date} " +
                   "\n" +
                   $"Have a nice day! " +
                   "\n" +
                   $"Best wishes," +
                   "\n" +
                   $"Product Delievery Team";

        }
    }
}
