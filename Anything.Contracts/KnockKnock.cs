using NServiceBus;

namespace Anything.Contracts
{
    public class KnockKnock: IEvent
    {
        public string Message { get; set; }
    }
}
