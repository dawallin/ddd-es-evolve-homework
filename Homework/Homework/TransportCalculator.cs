using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework
{
    public class TransportCalculator
    {
        private readonly char[] _destinations;
        private int totalTime = 0;
        private Map _map;

        public TransportCalculator(string destinations)
        {
            _destinations = destinations.ToCharArray();
            _map = new Map();
        }

        public int Deliver()
        {
            var packages = _destinations.Select((d, index) =>
            {
                var id = (index + 1).ToString();

                switch (d)
                {
                    case 'A':
                        return new Package(id, new List<DeliveryPoint>
                        {
                            _map.Factory,
                            _map.Port,
                            _map.A,
                        });
                    case 'B':
                        return new Package(id, new List<DeliveryPoint>
                        {
                            _map.Factory,
                            _map.B,
                        });
                    default:
                        throw new ArgumentException("Invalid destination");
                }
            }).ToList();

            foreach (var package in packages)
            {
                _map.Factory.Receive(package, "factory");
            }

            while (packages.Any(p => !p.IsDelivered))
            {
                Load();

                totalTime = totalTime + 1;
                Console.WriteLine();
                Console.WriteLine($"TimeTick {totalTime}");

                Move();
            }

            return totalTime;
        }

        private void Move()
        {
            foreach (var transport in _map.Transports)
            {
                transport.Move();
            }
        }

        private void Load()
        {
            foreach (var deliveryPoint in _map.DeliveryPoints)
            {
                deliveryPoint.Load();
            }
        }
    }
}
