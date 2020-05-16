using System.Collections.Generic;
using System.Linq;

namespace Homework
{
    public class DeliveryPoint
    {
        public string Name { get; private set; }
        private readonly IEnumerable<Transporter> _transports;
        private readonly IDictionary<DeliveryPoint, int> _deliveryPoints;

        public DeliveryPoint(string name,
            IEnumerable<Transporter> transports = null,
            IDictionary<DeliveryPoint, int> deliveryPoints = null)
        {
            Name = name;
            _transports = transports ?? new List<Transporter> { };
            _deliveryPoints = deliveryPoints ?? new Dictionary<DeliveryPoint, int> { };
        }

        private List<Package> _packageQueue = new List<Package> { };

        public void Receive(Package package, string deliverer)
        {
            package.RemoveFirstDeliveryPoint();

            if (package.NextDeliveryPoint == null)
            {
                package.Delivered();
                return;
            }

            _packageQueue.Add(package);
        }

        public void Load()
        {
            LoadPackages();
        }

        private void LoadPackages()
        {
            while(true)
            {
                if (_packageQueue.Count == 0)
                    break;

                var freeTransports = _transports
                    .Where(t => t.IsFree)
                    .ToArray();

                if (freeTransports.Any())
                {
                    var package = _packageQueue.First();
                    var transport = freeTransports.First();

                    transport.Load(package, _deliveryPoints[package.NextDeliveryPoint]);
                    _packageQueue = _packageQueue.Skip(1).ToList();
                }
                else
                {
                    break;
                }
            };
        }
    }
}