using SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions
{
    public static class DataLockAssertions
    {
        private static readonly DataLockRuleBase[] Rules = 
        {
            new DataLockEventsRule()
        };

        public static void AssertDataLockOutput(DataLockContext context, LearnerResults[] results)
        {
            foreach (var rule in Rules)
            {
                rule.AssertDataLockEvents(context, results);
            }
        }
    }
}
