using System.Collections.Generic;
using System.Linq;

namespace Homework
{
    public class Package
    {
        public readonly string Id;
        private IEnumerable<DeliveryPoint> _deliveryPoints;
        public bool IsDelivered;

        public Package(string id, IEnumerable<DeliveryPoint> deliveryPoints)
        {
            Id = id;
            _deliveryPoints = deliveryPoints;
            IsDelivered = false;
        }

        public DeliveryPoint NextDeliveryPoint => _deliveryPoints.FirstOrDefault();
        public void RemoveFirstDeliveryPoint() => _deliveryPoints = _deliveryPoints.Skip(1);
        public void Delivered() => IsDelivered = true;
    }
}