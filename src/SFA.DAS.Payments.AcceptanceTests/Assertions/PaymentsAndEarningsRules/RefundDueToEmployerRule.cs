using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.PaymentsAndEarningsRules
{
    public class RefundDueToEmployerRule : PaymentsRuleBase
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
                var isDasEmployer = employerAccount == null ? false : employerAccount.IsDasEmployer;
                var paymentsForEmployer = allPayments.Where(p => p.EmployerAccountId == period.EmployerAccountId || (!isDasEmployer && p.EmployerAccountId == 0)).ToArray();
                var prevPeriod = new EmployerAccountPeriodValue
                {
                    EmployerAccountId = period.EmployerAccountId,
                    PeriodName = period.PeriodName.ToPeriodDateTime().AddMonths(-1).ToPeriodName(),
                    Value = period.Value
                };

                AssertResultsForPeriod(prevPeriod, paymentsForEmployer);
            }
        }

        protected new void AssertResultsForPeriod(PeriodValue period, PaymentResult[] allPayments)
        {
            var paidInPeriod = allPayments
                            .Where(p => p.CalculationPeriod == period.PeriodName)
                            .Sum(p => p.Amount);

            if (paidInPeriod >= 0 && !AreValuesEqual(period.Value, paidInPeriod))
            {
                throw new Exception(FormatAssertionFailureMessage(period, paidInPeriod));
            }
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            var specPeriod = period.PeriodName.ToPeriodDateTime().AddMonths(1).ToPeriodName();

            return $"Expected employer to be refunded {period.Value} in {specPeriod} but actually refunded {actualPaymentInPeriod}";
        }
    }
}
