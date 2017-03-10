using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using System;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.PaymentsAndEarningsRules
{
    public class PaymentDueFromEmployersRule : PaymentsRuleBase
    {
        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, SubmissionContext submissionContext)
        {
            var allPayments = GetPaymentsForBreakdown(breakdown, submissionContext)
                .Where(p => p.FundingSource == FundingSource.CoInvestedEmployer)
                .ToArray();
            foreach (var period in breakdown.PaymentDueFromEmployers)
            {
                var paymentsForEmployer = allPayments.Where(p => p.EmployerAccountId == period.EmployerAccountId).ToArray();

                var prevPeriodDate = new DateTime(int.Parse(period.PeriodName.Substring(3, 2)) + 2000, int.Parse(period.PeriodName.Substring(0, 2)), 1).AddMonths(-1);
                var prevPeriodName = $"{prevPeriodDate.Month:00}/{prevPeriodDate.Year - 2000:00}";
                period.PeriodName = prevPeriodName;

                AssertResultsForPeriod(period, paymentsForEmployer);
            }
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            return $"Expected provider to be paid {period.Value} by SFA in {period.PeriodName} but actually paid {actualPaymentInPeriod}";
        }
    }
}