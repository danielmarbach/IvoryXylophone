using System;
using System.Threading;
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

            var endpoint = Endpoint.Start(busConfiguration).GetAwaiter().GetResult();
            var context = endpoint.CreateBusContext();

            Console.ReadLine();
            DrawStartup.IvoryXylophone();

            //Thread.Sleep(100);
            //context.SendLocal(new StartSaga { MessageId = Guid.NewGuid(), Text = "Da BAD ASS TEst" });

            Console.ReadLine();
        }
    }
}
