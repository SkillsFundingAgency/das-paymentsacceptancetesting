using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.TransactionTypeRules
{
    public class ProviderEarnedForLearningSupportRule : ProviderTransationTypeRuleBase
    {
        protected override IEnumerable<PaymentResult> FilterPeriodPayments(IEnumerable<PaymentResult> periodPayments)
        {
            return periodPayments.Where(p => p.TransactionType == TransactionType.LearningSupport);
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            var providerPeriod = (ProviderEarnedPeriodValue)period;
            return $"Expected {providerPeriod.ProviderId} to be paid {period.Value} in {period.PeriodName} for learning support but was actually paid {actualPaymentInPeriod}";
        }
    }
}
