using System;
using NServiceBus;

namespace Anything.Contracts
{
    public class SendConvertedMessage : ICommand
    {
        public Guid MessageId { get; set; }
        public string Message { get; set; }
    }
}