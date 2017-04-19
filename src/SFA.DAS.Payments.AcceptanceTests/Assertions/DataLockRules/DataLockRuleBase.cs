using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules
{
    public abstract class DataLockRuleBase
    {
        public abstract void AssertDataLockEvents(DataLockContext context, LearnerResults[] results);


        protected DataLockEventResult GetEventForPriceEpisode(LearnerResults[] results, string priceEpisodeIdentifier)
        {
            var actual = results.SelectMany(l => l.DataLockEvents).FirstOrDefault(e => e.PriceEpisodeIdentifier == priceEpisodeIdentifier);

            if (actual == null)
            {
                throw new Exception($"Cannot find data lock event for price episode {priceEpisodeIdentifier}");
            }

            return actual;
        }
    }
}