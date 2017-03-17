using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.TransactionTypeRules
{
    public abstract class ProviderTransationTypeRuleBase : TransactionTypeRuleBase
    {
        protected override IEnumerable<PaymentResult> FilterPayments(PeriodValue period, IEnumerable<LearnerResults> submissionResults)
        {
            var providerPeriod = (ProviderEarnedPeriodValue)period;
            var earnedPeriod = GetPaymentFilterPeriodName(providerPeriod);

            var providerPaymentsInPeriod = submissionResults.Where(r => r.ProviderId.Equals(providerPeriod.ProviderId, StringComparison.CurrentCultureIgnoreCase))
                                                            .SelectMany(r => r.Payments)
                                                            .Where(p => p.DeliveryPeriod == earnedPeriod);
            return FilterPeriodPayments(providerPaymentsInPeriod);
        }

        protected abstract IEnumerable<PaymentResult> FilterPeriodPayments(IEnumerable<PaymentResult> periodPayments);

        protected virtual string GetPaymentFilterPeriodName(ProviderEarnedPeriodValue providerPeriod)
        {
            return providerPeriod.PeriodName;
        }
    }
}
