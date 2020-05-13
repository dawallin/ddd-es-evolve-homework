using System.Collections.Generic;

namespace Homework
{
    public class Map
    {
        public IList<DeliveryPoint> DeliveryPoints { get; set; }
        public IList<Transporter> Transports { get; set; }

        public Map()
        {
            DeliveryPoints = new List<DeliveryPoint>();
            Transports = new List<Transporter>();

            A = new DeliveryPoint("A");
            DeliveryPoints.Add(A);

            B = new DeliveryPoint("B");
            DeliveryPoints.Add(B);

            var boat = new Transporter("Boat");
            Transports.Add(boat);

            Port = new DeliveryPoint("Port",
                new List<Transporter> { boat },
                new Dictionary<DeliveryPoint, int> { { A, 4 } });
            DeliveryPoints.Add(Port);

            var truck1 = new Transporter("Truck_1");
            Transports.Add(truck1);

            var truck2 = new Transporter("Truck_2");
            Transports.Add(truck2);

            Factory = new DeliveryPoint("Factory",
                new List<Transporter> { truck1, truck2 },
                new Dictionary<DeliveryPoint, int> { { Port, 1 }, { B, 5 } });
            DeliveryPoints.Add(Factory);
        }

        public DeliveryPoint A { get; set; }

        public DeliveryPoint B { get; set; }
        public DeliveryPoint Port { get; set; }

        public DeliveryPoint Factory { get; set; }
    }
}