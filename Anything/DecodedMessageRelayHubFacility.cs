using System.Threading.Tasks;
using Anything.Contracts;
using CookieMonster.Contracts;
using NServiceBus;

namespace Anything
{
    public class DecodedMessageRelayHubFacility: IHandleMessages<SendConvertedMessage>
    {
        public Task Handle(SendConvertedMessage message, IMessageHandlerContext context)
        {
            context.Send<EatYummieCookies>(e => e.Message = message.Message);

            return Task.FromResult(0);
        }
    }
}
