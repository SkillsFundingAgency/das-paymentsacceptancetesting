using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.TransactionTypeRules
{
    public class ProviderEarnedForBalancingRule : ProviderTransationTypeRuleBase
    {
        protected override IEnumerable<PaymentResult> FilterPeriodPayments(IEnumerable<PaymentResult> periodPayments)
        {
            return periodPayments.Where(p => p.TransactionType == TransactionType.Balancing);
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            var employerPeriod = (ProviderEarnedPeriodValue)period;
            return $"Expected provider {employerPeriod.ProviderId} to earn {period.Value} in {period.PeriodName} for balancing but was actually earned {actualPaymentInPeriod}";
        }
    }
}
