using System;
using System.Threading.Tasks;
using ColoredConsole;
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

            ColorConsole.WriteLine(
                            $"{DateTime.UtcNow.ToLocalTime()}".DarkGray(),
                            " ",
                            $"Cookies!!! Yum yum yum yum!".White().OnBlue(),
                            " ",
                            "Cookie says: ".Gray(),
                            " ",
                            $"{message.Message}".White());

            service.Publish(message.Message);
            return Task.FromResult(0);
        }
    }
}
