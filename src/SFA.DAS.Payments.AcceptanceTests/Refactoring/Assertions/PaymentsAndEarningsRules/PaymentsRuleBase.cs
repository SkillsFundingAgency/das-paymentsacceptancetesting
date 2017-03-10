using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.PaymentsAndEarningsRules
{
    public abstract class PaymentsRuleBase : EarningsAndPaymentsRuleBase
    {
        protected abstract string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod);

        protected PaymentResult[] GetPaymentsForBreakdown(EarningsAndPaymentsBreakdown breakdown, SubmissionContext submissionContext)
        {
            var payments = submissionContext.SubmissionResults.Where(r => r.ProviderId.Equals(breakdown.ProviderId, StringComparison.CurrentCultureIgnoreCase));
            if (breakdown is LearnerEarningsAndPaymentsBreakdown)
            {
                payments = payments.Where(r => r.LearnerId.Equals(((LearnerEarningsAndPaymentsBreakdown)breakdown).LearnerId, StringComparison.CurrentCultureIgnoreCase));
            }
            return payments.SelectMany(r => r.Payments).ToArray();
        }
      
        protected void AssertResultsForPeriod(PeriodValue period, PaymentResult[] allPayments)
        {
            var paidInPeriod = allPayments.Where(p => p.CalculationPeriod == period.PeriodName).Sum(p => p.Amount);
            if (period.Value != paidInPeriod)
            {
                throw new Exception(FormatAssertionFailureMessage(period, paidInPeriod));
            }
        }
    }
}