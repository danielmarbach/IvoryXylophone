using System.Collections.Generic;
using System.Threading.Tasks;
using LibGit2Sharp;
using NServiceBus.Extensibility;
using NServiceBus.Transports;

namespace NServiceBus.GitTransport
{
    public class Dispatcher : IDispatchMessages
    {
        private EndpointName endpointName;

        public Dispatcher(EndpointName endpointName)
        {
            this.endpointName = endpointName;
        }

        public Task Dispatch(IEnumerable<TransportOperation> outgoingMessages, ContextBag context)
        {
            using (var repo = new Repository($"../{endpointName}", null))
            {
                foreach (var operation in outgoingMessages)
                {
                    
                }
            }
            return Task.FromResult(0);
        }
    }

    public class QueueCreator : ICreateQueues
    {
        public Task CreateQueueIfNecessary(QueueBindings queueBindings, string identity)
        {
            return Task.CompletedTask;
        }
    }
}