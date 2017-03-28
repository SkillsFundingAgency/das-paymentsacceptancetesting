using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.PaymentsAndEarningsRules
{
    public class SfaLevyCoFundBudgetRule : PaymentsRuleBase
    {
        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, IEnumerable<LearnerResults> submissionResults, EmployerAccountContext employerAccountContext)
        {
            var payments = GetPaymentsForBreakdown(breakdown, submissionResults)
                .Where(p => p.FundingSource == FundingSource.CoInvestedSfa && p.ContractType == ContractType.ContractWithEmployer)
                .ToArray();
            foreach (var period in breakdown.SfaLevyCoFundBudget)
            {
                AssertResultsForPeriod(period, payments);
            }
        }
        protected new void AssertResultsForPeriod(PeriodValue period, PaymentResult[] allPayments)
        {
            var paidInPeriod = allPayments.Where(p => p.DeliveryPeriod == period.PeriodName).Sum(p => p.Amount);
            if (!AreValuesEqual(period.Value, paidInPeriod))
            {
                throw new Exception(FormatAssertionFailureMessage(period, paidInPeriod));
            }
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            return $"Expected SFA Levy co funded budget to be debited {period.Value} in {period.PeriodName} but was actually debited {actualPaymentInPeriod}";
        }
    }
}