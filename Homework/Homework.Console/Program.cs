using System;

namespace Homework.Console
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                System.Console.WriteLine("Invalid arguments. Write destinations in format 'ABBBA'");
                return -1;
            }

            var calculator = new TransportCalculator(args[0]);
            
            var result = calculator.Deliver();
            System.Console.WriteLine();
            System.Console.WriteLine($"All packages delivered. Total delivery time: {result}");
            return 0;
        }
    }
}
