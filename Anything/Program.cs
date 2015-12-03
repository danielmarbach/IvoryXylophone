using System;
using Anything.Contracts;
using NServiceBus;
using NServiceBus.GitTransport;

namespace Anything
{
    class Program
    {
        public static void Main(string[] args)
        {
            var busConfiguration = new BusConfiguration();

            busConfiguration.EndpointName("Anything");
//            busConfiguration.UseTransport<Git>().ConnectionString("bla");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.LimitMessageProcessingConcurrencyTo(1);
            busConfiguration.SendFailedMessagesTo("error");

            Endpoint.Start(busConfiguration).GetAwaiter().GetResult();

            //DrawStartup.IvoryXylophone();
            Console.ReadLine();
        }
    }
}
