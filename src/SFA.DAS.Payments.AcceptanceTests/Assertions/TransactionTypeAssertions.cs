using SFA.DAS.Payments.AcceptanceTests.Assertions.TransactionTypeRules;
using SFA.DAS.Payments.AcceptanceTests.Contexts;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions
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
            new ProviderEarnedForCompletionRule().AssertPeriodValues(earningsAndPaymentsContext.ProviderEarnedForCompletion, submissionResults, employerAccountContext);
            new ProviderEarnedForBalancingRule().AssertPeriodValues(earningsAndPaymentsContext.ProviderEarnedForBalancing, submissionResults, employerAccountContext);
            new ProviderEarnedFor16To18IncentiveRule().AssertPeriodValues(earningsAndPaymentsContext.ProviderEarnedFor16To18Incentive, submissionResults, employerAccountContext);
            new ProviderEarnedForEnglishAndMathOnProgrammeRule().AssertPeriodValues(earningsAndPaymentsContext.ProviderEarnedForEnglishAndMathOnProgramme, submissionResults, employerAccountContext);
            new ProviderEarnedForEnglishAndMathBalancing().AssertPeriodValues(earningsAndPaymentsContext.ProviderEarnedForEnglishAndMathBalancing, submissionResults, employerAccountContext);
            new ProviderEarnedForFrameworkUpliftOnProgrammeRule().AssertPeriodValues(earningsAndPaymentsContext.ProviderEarnedForFrameworkUpliftOnProgramme, submissionResults, employerAccountContext);
            new ProviderEarnedForFrameworkUpliftOnCompletionRule().AssertPeriodValues(earningsAndPaymentsContext.ProviderEarnedForFrameworkUpliftOnCompletion, submissionResults, employerAccountContext);
            new ProviderEarnedForFrameworkUpliftOnBalancingRule().AssertPeriodValues(earningsAndPaymentsContext.ProviderEarnedForFrameworkUpliftOnBalancing, submissionResults, employerAccountContext);
            new ProviderEarnedForLearningSupportRule().AssertPeriodValues(earningsAndPaymentsContext.ProviderEarnedForLearningSupport, submissionResults, employerAccountContext);
            new ProviderEarnedForDisadvantageUpliftRule().AssertPeriodValues(earningsAndPaymentsContext.ProviderEarnedForDisadvantageUplift, submissionResults, employerAccountContext);
        }
    }
}
