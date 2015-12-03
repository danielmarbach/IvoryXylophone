using System.Threading.Tasks;

namespace CookieMonster
{
    using System;
    using NServiceBus;
    using NServiceBus.Logging;

    class Program
    {
        static void Main()
        {
            DoWork().GetAwaiter().GetResult();
        }

        static async Task DoWork()
        {
            LogManager.Use<DefaultFactory>().Level(LogLevel.Error);

            var busConfiguration = new BusConfiguration();

            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.SendFailedMessagesTo("error");
            busConfiguration.AuditProcessedMessagesTo("audit");

            busConfiguration.EnableInstallers();

            var endpoint = await Endpoint.Create(busConfiguration);
            await endpoint.Start();

            Console.Out.WriteLine("CookieMonster endpoint is running, please hit any key to exit");
            Console.ReadKey();
        }
    }
}