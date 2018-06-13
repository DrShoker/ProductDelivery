using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace ServerPD.Services
{
    public class TrackService : ITrackService
    {
        Dictionary<int, Point> deliveries;

        public void AddDelivery(int deliveryId, int x, int y)
        {
            if (!deliveries.Keys.Contains(deliveryId))
                deliveries.Add(deliveryId, new Point(x, y));
        }

        public void EndDelivery(int deliveryId)
        {
            if (deliveries.Keys.Contains(deliveryId))
                deliveries.Remove(deliveryId);
        }

        public Point GetDeliveryCorrds(int deliveryId)
        {
            if (deliveries.Keys.Contains(deliveryId))
                return deliveries[deliveryId];
            throw new NullReferenceException();
        }

        public void UpdateCoords(int deliveryId, int x, int y)
        {
            Point newPoint = new Point(x, y);
            if (deliveries.Keys.Contains(deliveryId))
            {
                deliveries[deliveryId] = newPoint;
            }
        }
    }
}
