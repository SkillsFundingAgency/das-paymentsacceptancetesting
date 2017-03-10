using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.PaymentsAndEarningsRules
{
    public class ProviderPaidBySfaRule : PaymentsRuleBase
    {
        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, SubmissionContext submissionContext)
        {
            var allPayments = GetPaymentsForBreakdown(breakdown, submissionContext)
                .Where(p => p.FundingSource != FundingSource.CoInvestedEmployer)
                .ToArray();
            foreach (var period in breakdown.ProviderPaidBySfa)
            {
                AssertResultsForPeriod(period, allPayments);
            }
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            return $"Expected provider to be paid {period.Value} by SFA in {period.PeriodName} but actually paid {actualPaymentInPeriod}";
        }
    }
}