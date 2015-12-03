using System.Threading.Tasks;
using Anything.Contracts;
using NServiceBus;

namespace CookieMonster
{
    public class KnockKnockWhosThereHandler: IHandleMessages<KnockKnock>
    {
        public Task Handle(KnockKnock message, IMessageHandlerContext context)
        {
            var service = new TweetService("", "", "", "");
            service.Publish(message.Message);
            return Task.FromResult(0);
        }
    }
}
