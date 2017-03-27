using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.TransactionTypeRules
{
    public class ProviderEarnedForFrameworkUpliftOnCompletionRule : ProviderTransationTypeRuleBase
    {
        protected override IEnumerable<PaymentResult> FilterPeriodPayments(IEnumerable<PaymentResult> periodPayments)
        {
            return periodPayments.Where(p => p.TransactionType == TransactionType.Completion16To18FrameworkUplift);
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            var providerPeriod = (ProviderEarnedPeriodValue)period;
            var specPeriod = providerPeriod.PeriodName.ToPeriodDateTime().AddMonths(-1).ToPeriodName();
            return $"Expected {providerPeriod.ProviderId} to be paid {period.Value} in {specPeriod} for completion framework uplift but was actually paid {actualPaymentInPeriod}";
        }
    }
}
