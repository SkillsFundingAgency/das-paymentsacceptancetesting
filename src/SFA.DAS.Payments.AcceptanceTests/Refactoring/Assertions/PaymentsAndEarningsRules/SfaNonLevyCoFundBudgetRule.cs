using System;
using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.PaymentsAndEarningsRules
{
    public class SfaNonLevyCoFundBudgetRule : PaymentsRuleBase
    {

        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, IEnumerable<LearnerResults> submissionResults, EmployerAccountContext employerAccountContext)
        {
            var payments = GetPaymentsForBreakdown(breakdown, submissionResults)
                .Where(p => p.FundingSource == FundingSource.CoInvestedSfa && p.ContractType == ContractType.ContractWithSfa)
                .ToArray();
            foreach (var period in breakdown.SfaNonLevyCoFundBudget)
            {
                AssertResultsForPeriod(period, payments);
            }
        }
        protected new void AssertResultsForPeriod(PeriodValue period, PaymentResult[] allPayments)
        {
            var paidInPeriod = allPayments.Where(p => p.DeliveryPeriod == period.PeriodName).Sum(p => p.Amount);
            if (period.Value != paidInPeriod)
            {
                throw new Exception(FormatAssertionFailureMessage(period, paidInPeriod));
            }
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            return $"Expected SFA Non Levy co funded budget to be debited {period.Value} in {period.PeriodName} but was actually debited {actualPaymentInPeriod}";
        }
    }
}