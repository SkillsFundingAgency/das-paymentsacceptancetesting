using SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.PaymentsAndEarningsRules;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions
{
    public static class PaymentsAndEarningsAssestions
    {
        private static readonly EarningsAndPaymentsRuleBase[] Rules =
        {
            new ProviderEarnedTotalRule(),
            new ProviderPaidBySfaRule(),
            new PaymentDueFromEmployersRule(),
            new EmployersLevyAccountDebitedRule(),
            new SfaLevyBudgetRule(),
            new SfaLevyCoFundBudgetRule(),
            new SfaNonLevyCoFundBudgetRule(),
            new SfaLevyAdditionalPaymentsRule(),
            new SfaNonLevyAdditionalPaymentsRule()
        };

        public static void AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext earningsAndPaymentsContext, SubmissionContext submissionContext, EmployerAccountContext employerAccountContext)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }

            ValidateOverallEarningsAndPayments(earningsAndPaymentsContext, submissionContext, employerAccountContext);
        }

        private static void ValidateOverallEarningsAndPayments(EarningsAndPaymentsContext earningsAndPaymentsContext, SubmissionContext submissionContext, EmployerAccountContext employerAccountContext)
        {
            foreach (var breakdown in earningsAndPaymentsContext.OverallEarningsAndPayments)
            {
                foreach (var rule in Rules)
                {
                    rule.AssertBreakdown(breakdown, submissionContext, employerAccountContext);
                }
            }
        }
    }
}
