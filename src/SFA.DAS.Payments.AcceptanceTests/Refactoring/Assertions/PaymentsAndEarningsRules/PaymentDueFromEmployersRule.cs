using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.PaymentsAndEarningsRules
{
    public class PaymentDueFromEmployersRule : PaymentsRuleBase
    {
        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, IEnumerable<LearnerResults> submissionResults, EmployerAccountContext employerAccountContext)
        {
            var allPayments = GetPaymentsForBreakdown(breakdown, submissionResults)
                .Where(p => p.FundingSource == FundingSource.CoInvestedEmployer)
                .ToArray();
            foreach (var period in breakdown.PaymentDueFromEmployers)
            {
                // Currently have to assume there is only 1 non-levy employer in spec as there is no way to tell employer if there is no commitment.
                var employerAccount = employerAccountContext.EmployerAccounts.SingleOrDefault(a => a.Id == period.EmployerAccountId);
                var isLevyPayingEmployer = employerAccount == null ? true : employerAccount.IsLevyPayer;
                var paymentsForEmployer = allPayments.Where(p => p.EmployerAccountId == period.EmployerAccountId || (!isLevyPayingEmployer && p.EmployerAccountId == 0)).ToArray();
                var prevPeriod = new PeriodValue
                {
                    PeriodName = period.PeriodName.ToPeriodDateTime().AddMonths(-1).ToPeriodName(),
                    Value = period.Value
                };

                AssertResultsForPeriod(prevPeriod, paymentsForEmployer);
            }
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            var employerPeriod = (EmployerAccountPeriodValue)period;
            var specPeriod = period.PeriodName.ToPeriodDateTime().AddMonths(1).ToPeriodName();

            return $"Expected provider to be paid {period.Value} by employer {employerPeriod.EmployerAccountId} in {specPeriod} but actually paid {actualPaymentInPeriod}";
        }
    }
}