using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace ServerPD.Services
{
    public interface ITrackService
    {
        void AddDelivery(int deliveryId, int x, int y);

        Point GetDeliveryCorrds(int deliveryId);

        void UpdateCoords(int deliveryId,int x, int y);

        void EndDelivery(int deliveryId);
    }
}
