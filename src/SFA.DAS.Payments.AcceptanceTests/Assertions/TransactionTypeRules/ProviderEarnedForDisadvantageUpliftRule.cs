using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.TransactionTypeRules
{
    public class ProviderEarnedForDisadvantageUpliftRule : ProviderTransationTypeRuleBase
    {
        protected override IEnumerable<PaymentResult> FilterPeriodPayments(IEnumerable<PaymentResult> periodPayments)
        {
            return periodPayments.Where(p => p.TransactionType == TransactionType.FirstDisadvantagePayment
                                          || p.TransactionType == TransactionType.SecondDisadvantagePayment);
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            var providerPeriod = (ProviderEarnedPeriodValue)period;
            var specPeriod = providerPeriod.PeriodName.ToPeriodDateTime().AddMonths(-1).ToPeriodName();
            return $"Expected {providerPeriod.ProviderId} to be paid {period.Value} in {specPeriod} for disadvantaged incentive but was actually paid {actualPaymentInPeriod}";
        }
    }
}
