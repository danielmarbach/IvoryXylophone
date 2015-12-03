using System;
using System.Collections;
using System.Collections.Generic;
using NServiceBus.Settings;
using NServiceBus.Transports;

namespace NServiceBus.GitTransport
{
    public class GitTransport : TransportDefinition
    {
        protected override void ConfigureForReceiving(TransportReceivingConfigurationContext context)
        {
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
            return null;
        }

        public override string GetDiscriminatorForThisEndpointInstance()
        {
            return null;
        }

        public override string ToTransportAddress(LogicalAddress logicalAddress)
        {
            return null;
        }

        public override OutboundRoutingPolicy GetOutboundRoutingPolicy(ReadOnlySettings settings)
        {
            return null;
        }

        public override string ExampleConnectionStringForErrorMessage { get; }
    }
}