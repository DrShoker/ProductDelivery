using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace ServerPD.Services
{
    public interface ITrackService
    {
        void AddDelivery(int deliveryId, double x, double y);

        DoublePoint GetDeliveryCorrds(int deliveryId);

        void UpdateCoords(int deliveryId, double x, double y);

        void EndDelivery(int deliveryId);
    }
}
