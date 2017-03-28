using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.TransactionTypeRules
{
    public abstract class ProviderTransationTypeRuleBase : TransactionTypeRuleBase
    {
        protected override IEnumerable<PaymentResult> FilterPayments(PeriodValue period, IEnumerable<LearnerResults> submissionResults, EmployerAccountContext employerAccountContext)
        {
            var providerPeriod = (ProviderEarnedPeriodValue)period;
            var earnedPeriod = GetPaymentFilterPeriodName(providerPeriod);

            var providerPaymentsInPeriod = submissionResults.Where(r => r.ProviderId.Equals(providerPeriod.ProviderId, StringComparison.CurrentCultureIgnoreCase))
                                                            .SelectMany(r => r.Payments)
                                                            .Where(p => p.DeliveryPeriod == earnedPeriod
                                                                     && p.FundingSource != FundingSource.CoInvestedEmployer);
            return FilterPeriodPayments(providerPaymentsInPeriod);
        }

        protected abstract IEnumerable<PaymentResult> FilterPeriodPayments(IEnumerable<PaymentResult> periodPayments);

        protected virtual string GetPaymentFilterPeriodName(ProviderEarnedPeriodValue providerPeriod)
        {
            return providerPeriod.PeriodName.ToPeriodDateTime().AddMonths(-1).ToPeriodName();
        }
    }
}
