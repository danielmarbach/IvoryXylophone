using System;
using Anything.Contracts;
using NServiceBus;

namespace Anything
{
    class Program
    {
        public static void Main(string[] args)
        {
            var busConfiguration = new BusConfiguration();

            busConfiguration.EndpointName("Anything");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.LimitMessageProcessingConcurrencyTo(1);
            busConfiguration.SendFailedMessagesTo("error");

            var endpoint = Endpoint.Start(busConfiguration).GetAwaiter().GetResult();

            var context = endpoint.CreateBusContext();
            context.SendLocal(new StartSaga { MessageId = Guid.NewGuid(), Text = "test" });

            //DrawStartup.IvoryXylophone();
            Console.ReadLine();
        }
    }
}
