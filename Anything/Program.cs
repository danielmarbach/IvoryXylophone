using System;
using NServiceBus;

namespace Anything
{
    class Program
    {
        public static void Main(string[] args)
        {
            var busConfiguration = new BusConfiguration();

            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.SendFailedMessagesTo("error");
            busConfiguration.LimitMessageProcessingConcurrencyTo(1);
            var endpoint = Endpoint.Start(busConfiguration).GetAwaiter().GetResult();

            var context = endpoint.CreateBusContext();
            context.SendLocal(new StartSaga { MessageId = Guid.NewGuid() });

            //DrawStartup.IvoryXylophone();
            Console.ReadLine();
        }
    }
}
