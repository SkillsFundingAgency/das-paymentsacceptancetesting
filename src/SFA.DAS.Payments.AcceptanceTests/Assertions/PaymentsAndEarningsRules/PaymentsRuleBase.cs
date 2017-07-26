using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.PaymentsAndEarningsRules
{
    public abstract class PaymentsRuleBase : EarningsAndPaymentsRuleBase
    {
        protected abstract string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod);

        protected PaymentResult[] GetPaymentsForBreakdown(EarningsAndPaymentsBreakdown breakdown, IEnumerable<LearnerResults> submissionResults)
        {
            var payments = submissionResults.Where(r => r.ProviderId.Equals(breakdown.ProviderId, StringComparison.CurrentCultureIgnoreCase));
            if (breakdown is LearnerEarningsAndPaymentsBreakdown)
            {
                payments = payments.Where(r => r.LearnerReferenceNumber.Equals(((LearnerEarningsAndPaymentsBreakdown)breakdown).LearnerReferenceNumber, StringComparison.CurrentCultureIgnoreCase));
            }
            return payments.SelectMany(r => r.Payments).ToArray();
        }

        protected void AssertResultsForPeriod(PeriodValue period, PaymentResult[] allPayments)
        {
            var paidInPeriod = allPayments.Where(p => p.CalculationPeriod == period.PeriodName).Sum(p => p.Amount);
            if (!AreValuesEqual(period.Value, paidInPeriod))
            {
                throw new Exception(FormatAssertionFailureMessage(period, paidInPeriod));
            }
        }
    }
}