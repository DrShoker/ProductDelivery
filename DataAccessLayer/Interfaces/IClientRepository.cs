using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Client GetWithDeliveries(int id);
    }
}
