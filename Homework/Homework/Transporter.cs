using System;

namespace Homework
{
    public class Transporter
    {
        private enum Direction
        {
            Forward,
            Back,
        }

        private readonly string _name;
        private int _deliverTime;

        public Package Package { get; set; }
        public int Position { get; set; }
        public bool IsFree { get; set; }

        private Direction _direction { get; set; }

        public Transporter(string name)
        {
            _name = name;
            Position = 0;
            IsFree = true;
        }

        public void Load(Package package, int deliveryTime)
        {
            Console.WriteLine($"Package {package.Id} loaded to {_name}");
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
                IsFree = true;
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
            deliveryPoint.Receive(Package, _name);
            _direction = Direction.Back;
        }
    }
}