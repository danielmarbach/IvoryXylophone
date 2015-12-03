using System;
using System.Threading.Tasks;
using Anything.Contracts;
using NServiceBus;

namespace CookieMonster
{
    public class KnockKnockWhosThereHandler: IHandleMessages<KnockKnock>
    {
        public Task Handle(KnockKnock message, IMessageHandlerContext context)
        {
            var authToken = Environment.GetEnvironmentVariable("IvoryXylophone.AuthToken");
            var authTokenSecret = Environment.GetEnvironmentVariable("IvoryXylophone.AuthTokenSecret");

            var service = new TweetService("70Q2snrryqQEjU1MOFcL8jRGo", "USDkARHUa7DGRmipwVoVkCUevLfku3ltqWXbj7W1Ks3itBOZGO", 
                authToken, authTokenSecret);

            service.Publish(message.Message);
            return Task.FromResult(0);
        }
    }
}
