using NServiceBus.GitTransport;

namespace PepperPig
{
    using System.Threading.Tasks;
    using NServiceBus;

    class App
    {
        public static async Task RunAsync(
            string track,
            string consumerKey,
            string consumerSecret,
            string accessToken,
            string accessTokenSecret,
            string endpointName)
        {
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName(endpointName);
//            busConfiguration.UseTransport<Git>().ConnectionString("bla");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.LimitMessageProcessingConcurrencyTo(1);
            busConfiguration.SendFailedMessagesTo("error");

            await Piggy.StartAsync(
                await Endpoint.Start(busConfiguration),
                track,
                consumerKey,
                consumerSecret,
                accessToken,
                accessTokenSecret,
                endpointName);
        }
    }
}
