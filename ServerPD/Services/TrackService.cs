using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;

namespace ServerPD.Services
{
    public class TrackService : ITrackService
    {
        Dictionary<int, DoublePoint> deliveries = new Dictionary<int, DoublePoint>();

        public TrackService()
        {
            using (var unitOf = new EFUnitOfWork())
            {
                unitOf
                    .Deliveries
                    .GetAll()
                    .Where(d => d.Status != DataAccessLayer.Enums.DeliveryStatus.Completed)
                    .ToList()
                    .ForEach(d => deliveries.Add(d.Id, new DoublePoint(0, 0)));
            }
        }

        public void AddDelivery(int deliveryId, double x, double y)
        {
            if (!deliveries.Keys.Contains(deliveryId))
                deliveries.Add(deliveryId, new DoublePoint(x, y));
        }

        public void EndDelivery(int deliveryId)
        {
            if (deliveries.Keys.Contains(deliveryId))
                deliveries.Remove(deliveryId);
        }

        public DoublePoint GetDeliveryCorrds(int deliveryId)
        {
            if (deliveries.Keys.Contains(deliveryId))
                return deliveries[deliveryId];
            return new DoublePoint(0, 0);
        }

        public void UpdateCoords(int deliveryId, double x, double y)
        {
            DoublePoint newPoint = new DoublePoint(x, y);
            if (deliveries.Keys.Contains(deliveryId))
            {
                deliveries[deliveryId] = newPoint;
            }
        }
    }
}
