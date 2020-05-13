using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework
{
    public class DeliveryPoint
    {
        private readonly string _name;
        private readonly IEnumerable<Transporter> _transports;
        private readonly IDictionary<DeliveryPoint, int> _deliveryPoints;

        public DeliveryPoint(string name,
            IEnumerable<Transporter> transports = null,
            IDictionary<DeliveryPoint, int> deliveryPoints = null)
        {
            _name = name;
            _transports = transports ?? new List<Transporter> { };
            _deliveryPoints = deliveryPoints ?? new Dictionary<DeliveryPoint, int> { };
        }

        private List<Package> _packageQueue = new List<Package> { };
        public void Receive(Package package, string deliverer)
        {
            package.RemoveFirstDeliveryPoint();

            if (package.NextDeliveryPoint == null)
            {
                Console.WriteLine($"Package {package.Id} delivered to {_name} by {deliverer}");
                package.Delivered();
                return;
            }

            Console.WriteLine($"Package {package.Id} received at {_name} by {deliverer}");
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
                    freeTransports.First().Load(package, _deliveryPoints[package.NextDeliveryPoint]);
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