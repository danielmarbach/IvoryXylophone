using System;
using System.Threading.Tasks;
using Anything.Contracts;
using MorseLib;
using NServiceBus;

namespace Anything
{
    public class BadAssSaga : Saga<BadAssSaga.SagaData>,
        IAmStartedByMessages<StartSaga>,
        IHandleMessages<AddMorseChar>
    {
        public class SagaData : ContainSagaData
        {
            public Guid MessageId { get; set; }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.ConfigureMapping<StartSaga>(x => x.MessageId).ToSaga(s => s.MessageId);
            mapper.ConfigureMapping<AddMorseChar>(x => x.MessageId).ToSaga(s => s.MessageId);
        }

        public async Task Handle(StartSaga message, IMessageHandlerContext context)
        {
            /*
            H	....
            E	.
            L	.-..
            L	.-..
            O	---
            BK, Break	-...-.-
            W	.--
            O	---
            R	.-.
            L	.-..
            D	-..
            Full-stop (period)	.-.-.-
            */
            var toMorse = MorseConverter.ToMorse(message.Text);
            

            await context.SendLocal(new AddMorseChar { MessageId = message.MessageId });
            await context.SendLocal(new AddMorseChar { MessageId = message.MessageId });
            await context.SendLocal(new AddMorseChar { MessageId = message.MessageId });
            await context.SendLocal(new AddMorseChar { MessageId = message.MessageId });
        }

        public Task Handle(AddMorseChar message, IMessageHandlerContext context)
        {

            return Task.FromResult(0);
        }
    }

    public class AddMorseChar : ICommand
    {
        public Guid MessageId { get; set; }
    }
}
