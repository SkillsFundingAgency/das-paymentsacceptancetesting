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
        };

        public static void AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext earningsAndPaymentsContext, SubmissionContext submissionContext)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }

            ValidateOverallEarningsAndPayments(earningsAndPaymentsContext, submissionContext);
        }

        private static void ValidateOverallEarningsAndPayments(EarningsAndPaymentsContext earningsAndPaymentsContext, SubmissionContext submissionContext)
        {
            foreach (var breakdown in earningsAndPaymentsContext.OverallEarningsAndPayments)
            {
                foreach (var rule in Rules)
                {
                    rule.AssertBreakdown(breakdown, submissionContext);
                }
            }
        }
    }
}
