using System;
using NServiceBus;

namespace Anything
{
    class Program
    {
        public static void Main(string[] args)
        {
            // TODO remove when message is re
            var busConfiguration = new BusConfiguration();

            Endpoint.Start(busConfiguration).Start();
            

            DrawStartup.IvoryXylophone();
            Console.ReadLine();
        }
    }
}
