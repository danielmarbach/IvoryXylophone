using System;
using NServiceBus;

namespace Anything
{
    public class BadAssSaga : Saga<BadAssSaga.SagaData>
    {
        public class SagaData : ContainSagaData
        {
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            throw new NotImplementedException();
        }
    }
}
