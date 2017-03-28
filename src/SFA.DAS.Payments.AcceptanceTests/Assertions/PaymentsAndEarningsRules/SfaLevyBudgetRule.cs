using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.PaymentsAndEarningsRules
{
    public class SfaLevyBudgetRule : PaymentsRuleBase
    {
        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, IEnumerable<LearnerResults> submissionResults, EmployerAccountContext employerAccountContext)
        {
            var payments = GetPaymentsForBreakdown(breakdown, submissionResults)
                .Where(p => p.FundingSource == FundingSource.Levy)
                .ToArray();
            foreach (var period in breakdown.SfaLevyBudget)
            {
                AssertResultsForPeriod(period, payments);
            }
        }
        protected new void AssertResultsForPeriod(PeriodValue period, PaymentResult[] allPayments)
        {
            var paidInPeriod = allPayments.Where(p => p.DeliveryPeriod == period.PeriodName && p.Amount>=0).Sum(p => p.Amount);
            if (!AreValuesEqual(period.Value, paidInPeriod))
            {
                throw new Exception(FormatAssertionFailureMessage(period, paidInPeriod));
            }
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            return $"Expected SFA Levy budget to be debited {period.Value} in {period.PeriodName} but was actually debited {actualPaymentInPeriod}";
        }
    }
}
