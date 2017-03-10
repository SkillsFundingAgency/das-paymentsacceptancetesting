using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.PaymentsAndEarningsRules
{
    public class ProviderPaidBySfaRule : EarningsAndPaymentsRuleBase
    {
        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, SubmissionContext submissionContext)
        {
            var allPayments = GetPaymentsForBreakdown(breakdown, submissionContext)
                .Where(p => p.FundingSource != Enums.FundingSource.CoInvestedEmployer)
                .ToArray();
            foreach (var period in breakdown.ProviderPaidBySfa)
            {
                AssertResultsForPeriod(period, allPayments);
            }
        }

        private PaymentResult[] GetPaymentsForBreakdown(EarningsAndPaymentsBreakdown breakdown, SubmissionContext submissionContext)
        {
            var payments = submissionContext.SubmissionResults.Where(r => r.ProviderId.Equals(breakdown.ProviderId, StringComparison.CurrentCultureIgnoreCase));
            if (breakdown is LearnerEarningsAndPaymentsBreakdown)
            {
                payments = payments.Where(r => r.LearnerId.Equals(((LearnerEarningsAndPaymentsBreakdown)breakdown).LearnerId, StringComparison.CurrentCultureIgnoreCase));
            }
            return payments.SelectMany(r => r.Payments).ToArray();
        }
        private void AssertResultsForPeriod(PeriodValue period, PaymentResult[] allPayments)
        {
            var prevPeriodDate = new DateTime(int.Parse(period.PeriodName.Substring(3, 2)) + 2000, int.Parse(period.PeriodName.Substring(0, 2)), 1).AddMonths(-1);
            var prevPeriodName = $"{prevPeriodDate.Month:00}/{prevPeriodDate.Year - 2000:00}";
            var paidInPeriod = allPayments.Where(p => p.CalculationPeriod == prevPeriodName).Sum(p => p.Amount);
            if (period.Value != paidInPeriod)
            {
                throw new Exception($"Expected provider to be paid {period.Value} by SFA in {period.PeriodName} but actually paid {paidInPeriod}");
            }
        }
    }
}