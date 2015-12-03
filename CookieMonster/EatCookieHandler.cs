using System.Threading.Tasks;
using CookieMonster.Contracts;
using NServiceBus;

namespace CookieMonster
{
    public class EatCookieHandler: IHandleMessages<EatYummieCookies>
    {
        public Task Handle(EatYummieCookies message, IMessageHandlerContext context)
        {
            var service = new TweetService(Program.ConsumerKey, 
                Program.ConsumerSecret, 
                Program.AccessToken, 
                Program.AccessTokenSecret);

            service.Publish(message.Message);
            return Task.FromResult(0);
        }
    }
}
