using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.TransactionTypeRules
{
    public class EmployerEarnedFor16To18IncentiveRule : TransactionTypeRuleBase
    {
        protected override IEnumerable<PaymentResult> FilterPayments(PeriodValue period, IEnumerable<LearnerResults> submissionResults, EmployerAccountContext employerAccountContext)
        {
            var employerPeriod = (EmployerAccountProviderPeriodValue)period;
            var employer = employerAccountContext.EmployerAccounts.SingleOrDefault(a => a.Id == employerPeriod.EmployerAccountId);
            var isLevyPayer = employer?.IsLevyPayer ?? false;
            var earningPeriod = employerPeriod.PeriodName.ToPeriodDateTime().AddMonths(-1).ToPeriodName();
            return submissionResults.Where(l => l.ProviderId.Equals(employerPeriod.ProviderId, System.StringComparison.CurrentCultureIgnoreCase))
                                    .SelectMany(r => r.Payments).Where(p => (p.EmployerAccountId == employerPeriod.EmployerAccountId || !isLevyPayer && p.EmployerAccountId == 0)
                                                                         && p.DeliveryPeriod == earningPeriod
                                                                         && (p.TransactionType == TransactionType.First16To18EmployerIncentive || p.TransactionType == TransactionType.Second16To18EmployerIncentive));
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            var employerPeriod = (EmployerAccountPeriodValue)period;
            return $"Expected Employer {employerPeriod.EmployerAccountId} to be paid {period.Value} in {period.PeriodName} for 16-18 incentive but was actually paid {actualPaymentInPeriod}";
        }
    }
}
