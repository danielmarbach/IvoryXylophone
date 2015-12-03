using System;
using NServiceBus;

namespace Anything
{
    public class StartSaga : ICommand
    {
        public Guid MessageId { get; set; }
    }
}