using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules
{
    public class DataLockPeriodsRule : DataLockRuleBase
    {
        public override void AssertDataLockEvents(DataLockContext context, LearnerResults[] results)
        {
            foreach (var expected in context.DataLockEventPeriods)
            {
                var actualEvent = GetEventForPriceEpisode(results, expected.PriceEpisodeIdentifier);
                var actualPeriods = actualEvent.Periods.Where(e => e.CollectionPeriodName == expected.Period).ToArray();
                if (!actualPeriods.Any())
                {
                    throw new Exception($"Event for price episode {expected.PriceEpisodeIdentifier} does not contain period {expected.Period}");
                }

                var actual = actualPeriods.FirstOrDefault(e => e.TransactionType == (int)expected.TransactionType);
                if (actual == null)
                {
                    var found = actualPeriods.Select(e => e.TransactionType.ToString()).Aggregate((x, y) => $"{x}\n{y}");
                    throw new Exception($"Expected a transaction type of {expected.TransactionType} in period {expected.Period} but none found.\n\nFound:\n{found}");
                }

                if (expected.PayableFlag != actual.IsPayable)
                {
                    throw new Exception($"Expected payable flag of {expected.PayableFlag} but actually {actual.IsPayable}");
                }
            }
        }
    }
}
