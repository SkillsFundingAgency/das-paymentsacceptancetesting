using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules
{
    public abstract class DataLockRuleBase
    {
        public abstract void AssertDataLockEvents(DataLockContext context, LearnerResults[] results);


        protected DataLockEventResult[] GetEventsForPriceEpisode(LearnerResults[] results, string priceEpisodeIdentifier)
        {
            var actual = results.SelectMany(l => l.DataLockEvents).Where(e => e.PriceEpisodeIdentifier == priceEpisodeIdentifier).ToArray();

            if (actual == null || actual.Length == 0)
            {
                throw new Exception($"Cannot find data lock event for price episode {priceEpisodeIdentifier}");
            }

            return actual;
        }
    }
}