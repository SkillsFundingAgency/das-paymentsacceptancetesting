using SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions
{
    public static class DataLockAssertions
    {
        private static readonly DataLockRuleBase[] Rules =
        {
            new DataLockEventsRule(),
            new DataLockErrorsRule(),
            new DataLockPeriodsRule(),
            new DataLockCommitmentVersionRule(),
        };

        public static void AssertDataLockOutput(DataLockContext context, LearnerResults[] results)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }

            foreach (var rule in Rules)
            {
                rule.AssertDataLockEvents(context, results);
            }
        }
    }
}
