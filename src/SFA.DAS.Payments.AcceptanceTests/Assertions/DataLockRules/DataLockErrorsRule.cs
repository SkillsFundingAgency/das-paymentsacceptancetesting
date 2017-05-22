using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules
{
    public class DataLockErrorsRule : DataLockRuleBase
    {
        public override void AssertDataLockEvents(DataLockContext context, LearnerResults[] results)
        {
            foreach (var expected in context.DataLockEventErrors)
            {
                var actualEvent = GetEventsForPriceEpisode(results, expected.PriceEpisodeIdentifier).FirstOrDefault();
                var actual = actualEvent.Errors.FirstOrDefault(e => e.ErrorCode == expected.ErrorCode);
                if (actual == null)
                {
                    throw new Exception($"Event for price episode {expected.PriceEpisodeIdentifier} does not contain error with code {expected.ErrorCode}");
                }

                if (expected.ErrorDescription != actual.SystemDescription)
                {
                    throw new Exception($"Expected error description '{expected.ErrorDescription}' but actually '{actual.SystemDescription}'");
                }
            }
        }
    }
}
