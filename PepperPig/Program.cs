using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PepperPig
{
    class Program
    {
        static void Main(string[] args)
        {
            var track = ConfigurationManager.AppSettings["Track"];
            var endpointName = ConfigurationManager.AppSettings["EndpointName"];

            var consumerKeyName = "IVORY_XYLOPHONE_TWITTER_CONSUMER_KEY";
            var consumerSecretKeyName = "IVORY_XYLOPHONE_TWITTER_CONSUMER_SECRET";
            var accessTokenSecretKeyName = "IVORY_XYLOPHONE_TWITTER_ACCESS_TOKEN_SECRET";
            var accessTokenKeyName = "IVORY_XYLOPHONE_TWITTER_ACCESS_TOKEN";

            var consumerKey = Environment.GetEnvironmentVariable(consumerKeyName);
            if (consumerKey == null)
            {
                throw new Exception($"{consumerKeyName} enviroment variable is not set.");
            }

            var consumerSecret = Environment.GetEnvironmentVariable(consumerSecretKeyName);
            if (consumerSecret == null)
            {
                throw new Exception($"{consumerSecretKeyName} enviroment variable is not set.");
            }

            var accessToken = Environment.GetEnvironmentVariable(accessTokenKeyName);
            if (accessToken == null)
            {
                throw new Exception($"{accessTokenKeyName} enviroment variable is not set.");
            }

            var accessTokenSecret = Environment.GetEnvironmentVariable(accessTokenSecretKeyName);
            if (accessTokenSecret == null)
            {
                throw new Exception($"{accessTokenSecretKeyName} enviroment variable is not set.");
            }

            App.RunAsync(
                    track,
                    consumerKey,
                    consumerSecret,
                    accessToken,
                    accessTokenSecret,
                    endpointName)
                .GetAwaiter().GetResult();
        }
    }
}
