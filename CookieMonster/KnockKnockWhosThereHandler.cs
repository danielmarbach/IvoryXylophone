using System.Threading.Tasks;
using Anything.Contracts;
using NServiceBus;

namespace CookieMonster
{
    public class KnockKnockWhosThereHandler: IHandleMessages<KnockKnock>
    {
        public Task Handle(KnockKnock message, IMessageHandlerContext context)
        {
            var service = new TweetService("70Q2snrryqQEjU1MOFcL8jRGo", "USDkARHUa7DGRmipwVoVkCUevLfku3ltqWXbj7W1Ks3itBOZGO", "2993705991-8aelrNnfwD6dFD0AOBFHfeU4qWHLn9VH7AkQaB8", "0W16SbLl5A126fLwc3KBoGtAUKNkK5UqlUujbB6yKKsWU");
            service.Publish(message.Message);
            return Task.FromResult(0);
        }
    }
}
