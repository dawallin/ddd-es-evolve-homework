using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Homework
{
    public class Package
    {
        public readonly string Id;
        private IEnumerable<DeliveryPoint> _deliveryPoints;
        public bool IsDelivered;
        public DeliveryPoint Origin { get; set; }
        public DeliveryPoint Destination { get; set; }

        public Package(string id, IEnumerable<DeliveryPoint> deliveryPoints)
        {
            Id = id;
            _deliveryPoints = deliveryPoints;
            IsDelivered = false;
            Origin = deliveryPoints.First();
            Destination = deliveryPoints.Last();
        }

        public DeliveryPoint NextDeliveryPoint => _deliveryPoints.FirstOrDefault();
        public void RemoveFirstDeliveryPoint() => _deliveryPoints = _deliveryPoints.Skip(1);
        public void Delivered() => IsDelivered = true;
    }
}