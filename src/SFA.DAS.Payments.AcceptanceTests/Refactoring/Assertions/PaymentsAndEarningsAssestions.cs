using System;
using System.Linq;
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
            new SfaNonLevyAdditionalPaymentsRule(),
            new RefundTakenBySfaRule(),
            new EmployersLevyAccountCreditedRule()
        };

        public static void AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext earningsAndPaymentsContext, SubmissionContext submissionContext, EmployerAccountContext employerAccountContext)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }

            ValidateOverallEarningsAndPayments(earningsAndPaymentsContext, submissionContext, employerAccountContext);
            ValidateLearnerSpecificEarningsAndPayments(earningsAndPaymentsContext, submissionContext, employerAccountContext);
        }

        private static void ValidateOverallEarningsAndPayments(EarningsAndPaymentsContext earningsAndPaymentsContext, SubmissionContext submissionContext, EmployerAccountContext employerAccountContext)
        {
            foreach (var breakdown in earningsAndPaymentsContext.OverallEarningsAndPayments)
            {
                foreach (var rule in Rules)
                {
                    rule.AssertBreakdown(breakdown, submissionContext.SubmissionResults, employerAccountContext);
                }
            }
        }
        private static void ValidateLearnerSpecificEarningsAndPayments(EarningsAndPaymentsContext earningsAndPaymentsContext, SubmissionContext submissionContext, EmployerAccountContext employerAccountContext)
        {
            foreach (var breakdown in earningsAndPaymentsContext.LearnerOverallEarningsAndPayments)
            {
                var learnerResults = submissionContext.SubmissionResults.Where(r => r.LearnerId == breakdown.LearnerId).ToArray();
                try
                {
                    foreach (var rule in Rules)
                    {
                        rule.AssertBreakdown(breakdown, learnerResults, employerAccountContext);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message} (learner {breakdown.LearnerId})", ex);
                }
            }
        }
    }
}
