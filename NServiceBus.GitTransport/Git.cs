using System;
using System.Collections;
using System.Collections.Generic;
using NServiceBus.Settings;
using NServiceBus.Support;
using NServiceBus.Transports;

namespace NServiceBus.GitTransport
{
    public class Git : TransportDefinition
    {
        protected override void ConfigureForReceiving(TransportReceivingConfigurationContext context)
        {
            context.SetQueueCreatorFactory(() => new QueueCreator());
            context.SetMessagePumpFactory(c => new PushMessages(context.Settings.EndpointInstanceName().EndpointName));
        }

        protected override void ConfigureForSending(TransportSendingConfigurationContext context)
        {
            context.SetDispatcherFactory(() => new Dispatcher(context.GlobalSettings.EndpointName()));
        }

        public override IEnumerable<Type> GetSupportedDeliveryConstraints()
        {
            return new List<Type>();
        }

        public override TransactionSupport GetTransactionSupport()
        {
            return TransactionSupport.None;
        }

        public override IManageSubscriptions GetSubscriptionManager()
        {
            throw new NotSupportedException();
        }

        public override string GetDiscriminatorForThisEndpointInstance()
        {
            return RuntimeEnvironment.MachineName;
        }

        public override string ToTransportAddress(LogicalAddress logicalAddress)
        {
            return logicalAddress.ToString();
        }

        public override OutboundRoutingPolicy GetOutboundRoutingPolicy(ReadOnlySettings settings)
        {
            return new OutboundRoutingPolicy(OutboundRoutingType.DirectSend, OutboundRoutingType.DirectSend, OutboundRoutingType.DirectSend);
        }

        public override string ExampleConnectionStringForErrorMessage { get; }
    }
}