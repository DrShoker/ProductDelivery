using DataAccessLayer.Entities;

namespace ServerPD.Interfaces
{
    public interface IBodyBuilder
    {
        string CreateBody(Delivery delievery);
    }
}
