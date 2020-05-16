using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Homework
{
    public class Transporter
    {
        private readonly EventPublisher _eventPublisher;
        public DeliveryPoint HomeDeliveryPoint;

        private enum Direction
        {
            Forward,
            Back,
        }

        public string Name { get; private set; }

        private int _deliverTime;

        public Package Package { get; set; }
        public int Position { get; set; }
        public bool IsFree { get; set; }

        private Direction _direction { get; set; }

        public Transporter(string name, EventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
            Name = name;
            Position = 0;
            IsFree = true;
        }

        public void Load(Package package, int deliveryTime)
        {
            _eventPublisher.Publish(new Event()
            {
                Destination = package.NextDeliveryPoint.Name,
                EventName = EventType.DEPART,
                Location = HomeDeliveryPoint.Name,
                TransportId = Name,
                Cargo = Cargo.Map(package),
            });
            Package = package;
            Position = 0;
            _direction = Direction.Forward;
            IsFree = false;
            _deliverTime = deliveryTime;
        }

        public void Move()
        {
            if (IsFree)
                return;

            switch (_direction)
            {
                case Direction.Forward:
                    MoveForward();
                    break;
                case Direction.Back when Position > 0:
                    MoveBackward();
                    break;
            }
        }

        private void MoveBackward()
        {
            Position = Position - 1;
            if (Position == 0)
            {
                _eventPublisher.Publish(new Event()
                {
                    EventName = EventType.ARRIVE,
                    Location = HomeDeliveryPoint.Name,
                    TransportId = Name,
                });
                IsFree = true;
            }
        }

        private void MoveForward()
        {
            Position = Position + 1;
            if (Position == _deliverTime)
                Unload();
        }

        private void Unload()
        {
            var deliveryPoint = Package.NextDeliveryPoint;
            deliveryPoint.Receive(Package, Name);
            _eventPublisher.Publish(new Event()
            {
                EventName = EventType.ARRIVE,
                Location = HomeDeliveryPoint.Name,
                TransportId = Name,
                Cargo = Cargo.Map(Package),
            });
            _direction = Direction.Back;
        }
    }

    public class Event
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EventType EventName { get; set; }
        public int Time { get; set; }
        public string TransportId { get; set; }
        public string Location { get; set; }
        public string Destination { get; set; }
        public Cargo Cargo { get; set; }
    }

    public class Cargo
    {
        public string Id { get; set; }
        public string Destination { get; set; }
        public string Origin { get; set; }

        public static Cargo Map(Package package)
        {
            return new Cargo
            {
                Id = package.Id,
                Origin = package.Origin.Name,
                Destination = package.Destination.Name,
            };
        }
    }

    public enum EventType
    {
        Unknown,
        DEPART,
        ARRIVE,
    }

    public class EventPublisher
    {
        public int Time { get; set; }

        private JsonSerializerSettings _jsonSettings;

        public EventPublisher()
        {
            Time = 0;
            _jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public void Publish(Event @event)
        {
            @event.Time = Time;
            var json = JsonConvert.SerializeObject(@event, Formatting.None, _jsonSettings);
            Console.WriteLine(json);
        }
    }
}