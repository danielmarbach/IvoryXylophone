

using System.Threading.Tasks;
using NServiceBus.GitTransport;

namespace CookieMonster
{
    using System;
    using NServiceBus;
    using NServiceBus.Logging;

    class Program
    {
        public static string ConsumerKey { get; private set; }
        public static string ConsumerSecret { get; private set; }
        public static string AccessToken { get; private set; }
        public static string AccessTokenSecret { get; private set; }

        static void Main()
        {
            var consumerKeyName = "IVORY_XYLOPHONE_TWITTER_CONSUMER_KEY";
            var consumerSecretKeyName = "IVORY_XYLOPHONE_TWITTER_CONSUMER_SECRET";
            var accessTokenSecretKeyName = "IVORY_XYLOPHONE_TWITTER_ACCESS_TOKEN_SECRET";
            var accessTokenKeyName = "IVORY_XYLOPHONE_TWITTER_ACCESS_TOKEN";

            ConsumerKey = Environment.GetEnvironmentVariable(consumerKeyName);
            if (ConsumerKey == null)
            {
                throw new Exception($"{consumerKeyName} enviroment variable is not set.");
            }

            ConsumerSecret = Environment.GetEnvironmentVariable(consumerSecretKeyName);
            if (ConsumerSecret == null)
            {
                throw new Exception($"{consumerSecretKeyName} enviroment variable is not set.");
            }

            AccessToken = Environment.GetEnvironmentVariable(accessTokenKeyName);
            if (AccessToken == null)
            {
                throw new Exception($"{accessTokenKeyName} enviroment variable is not set.");
            }

            AccessTokenSecret = Environment.GetEnvironmentVariable(accessTokenSecretKeyName);
            if (AccessTokenSecret == null)
            {
                throw new Exception($"{accessTokenSecretKeyName} enviroment variable is not set.");
            }

            DoWork().GetAwaiter().GetResult();
        }

        static async Task DoWork()
        {
            LogManager.Use<DefaultFactory>().Level(LogLevel.Error);

            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("CookieMonster");
//            busConfiguration.UseTransport<Git>().ConnectionString("bla");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.LimitMessageProcessingConcurrencyTo(1);
            busConfiguration.SendFailedMessagesTo("error");

            var endpoint = await Endpoint.Create(busConfiguration);
            await endpoint.Start();

            Console.Out.WriteLine("CookieMonster endpoint is running, please hit any key to exit");
            Console.ReadKey();
        }
    }
}