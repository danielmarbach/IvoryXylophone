using System;
using NServiceBus;

namespace Anything.Contracts
{
    public class MessageConvertingDone : IEvent
    {
        public Guid MessageId { get; set; }
        public string Message { get; set; }
    }
}