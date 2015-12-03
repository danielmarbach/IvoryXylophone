using System;
using NServiceBus;

namespace Anything.Contracts
{
    public class StartSaga : ICommand
    {
        public Guid MessageId { get; set; }
        public string Text { get; set; }
    }
}