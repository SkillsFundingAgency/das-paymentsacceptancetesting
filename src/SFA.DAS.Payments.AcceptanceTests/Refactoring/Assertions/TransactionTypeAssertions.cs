using SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.TransactionTypeRules;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions
{
    public static class TransactionTypeAssertions
    {
        public static void AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext earningsAndPaymentsContext, SubmissionContext submissionContext, EmployerAccountContext employerAccountContext)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }

            var submissionResults = submissionContext.SubmissionResults.ToArray();
            new EmployerEarnedFor16To18IncentiveRule().AssertPeriodValues(earningsAndPaymentsContext.EmployerEarnedFor16To18Incentive, submissionResults, employerAccountContext);
            new ProviderEarnedForOnProgrammeRule().AssertPeriodValues(earningsAndPaymentsContext.ProviderEarnedForOnProgramme, submissionResults, employerAccountContext);
        }
    }
}
