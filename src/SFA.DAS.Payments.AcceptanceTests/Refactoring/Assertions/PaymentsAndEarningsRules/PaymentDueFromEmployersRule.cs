using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.PaymentsAndEarningsRules
{
    public class PaymentDueFromEmployersRule : PaymentsRuleBase
    {
        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, SubmissionContext submissionContext)
        {
            var allPayments = GetPaymentsForBreakdown(breakdown, submissionContext)
                .Where(p => p.FundingSource == Enums.FundingSource.CoInvestedEmployer)
                .ToArray();
            foreach (var period in breakdown.PaymentDueFromEmployers)
            {
                var paymentsForEmployer = allPayments.Where(p => p.EmployerAccountId == period.EmployerAccountId).ToArray();
                AssertResultsForPeriod(period, paymentsForEmployer);
            }
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            return $"Expected provider to be paid {period.Value} by SFA in {period.PeriodName} but actually paid {actualPaymentInPeriod}";
        }
    }
}