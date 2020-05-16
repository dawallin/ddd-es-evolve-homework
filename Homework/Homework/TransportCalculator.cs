using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework
{
    public class TransportCalculator
    {
        private readonly char[] _destinations;
        private Map _map;
        private EventPublisher _eventPublisher;

        public TransportCalculator(string destinations)
        {
            _eventPublisher = new EventPublisher();
            _destinations = destinations.ToCharArray();
            _map = new Map(_eventPublisher);
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

                _eventPublisher.Time = _eventPublisher.Time + 1;

                Move();
            }

            return _eventPublisher.Time;
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
