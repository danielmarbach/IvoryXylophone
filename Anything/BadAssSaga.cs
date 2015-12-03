using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Anything
{
    public class BadAssSaga : Saga<BadAssSaga.SagaData>,
        IAmStartedByMessages<StartSaga>
    {
        public class SagaData : ContainSagaData
        {
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            throw new NotImplementedException();
        }

        public Task Handle(StartSaga message, IMessageHandlerContext context)
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
            context.SendLocal(new AddMorseChar());
            return Task.FromResult(0);
        }
    }

    public class AddMorseChar : ICommand
    {
    }

    public class StartSaga : ICommand
    {
    }
}
