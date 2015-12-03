using NServiceBus;

namespace CookieMonster.Contracts
{
    public class EatYummieCookies: ICommand
    {
        public string Message { get; set; }
    }
}
