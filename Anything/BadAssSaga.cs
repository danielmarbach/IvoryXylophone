using System;
using System.Collections.Generic;
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
            public List<string> MorseList { get; set; }
            public string OrigMessageText { get; set; }
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
            Data.OrigMessageText = message.Text;
            var toMorse = MorseConverter.ToMorse(message.Text);
            foreach (var s in toMorse)
            {
                await context.SendLocal(new AddMorseChar { MessageId = message.MessageId, Morse = s });
            }
        }

        public Task Handle(AddMorseChar message, IMessageHandlerContext context)
        {
            if (Data.MorseList == null) Data.MorseList = new List<string>();

            Data.MorseList.Add(message.Morse);
            if (Data.OrigMessageText.Length == Data.MorseList.Count)
            {
                context.SendLocal(new SendConvertedMessage { MessageId = message.MessageId, Message = MorseConverter.FromMorse(Data.MorseList.ToArray()) });
            }
            return Task.FromResult(0);
        }
    }

    public class AddMorseChar : ICommand
    {
        public Guid MessageId { get; set; }
        public string Morse { get; set; }
    }
}
