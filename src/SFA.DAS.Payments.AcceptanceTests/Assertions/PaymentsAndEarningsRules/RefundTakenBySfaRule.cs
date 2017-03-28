using System.Collections.Generic;
using System.Linq;

using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.PaymentsAndEarningsRules
{
    public class RefundTakenBySfaRule : ProviderPaidBySfaRule
    {
        
        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            var specPeriod = period.PeriodName.ToPeriodDateTime().AddMonths(1).ToPeriodName();

            return $"Expected provider to refund taken {period.Value} by SFA in {specPeriod} but actually refunded {actualPaymentInPeriod}";
        }
    }
}