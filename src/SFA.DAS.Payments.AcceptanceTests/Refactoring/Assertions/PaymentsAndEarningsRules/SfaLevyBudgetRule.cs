using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.PaymentsAndEarningsRules
{
    public class SfaLevyBudgetRule : EarningsAndPaymentsRuleBase
    {
        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, SubmissionContext submissionContext)
        {
            var payments = GetEarningsForBreakdown(breakdown, submissionContext);
            foreach (var period in breakdown.SfaLevyBudget)
            {
                AssertResultsForPeriod(period, payments);
            }
        }

        private PaymentResult[] GetEarningsForBreakdown(EarningsAndPaymentsBreakdown breakdown, SubmissionContext submissionContext)
        {
            var payments = submissionContext.SubmissionResults.Where(r => r.ProviderId.Equals(breakdown.ProviderId, StringComparison.CurrentCultureIgnoreCase));
            if (breakdown is LearnerEarningsAndPaymentsBreakdown)
            {
                payments = payments.Where(r => r.LearnerId.Equals(((LearnerEarningsAndPaymentsBreakdown)breakdown).LearnerId, StringComparison.CurrentCultureIgnoreCase));
            }
            return payments.SelectMany(r => r.Payments).ToArray();
        }
        private void AssertResultsForPeriod(PeriodValue period, PaymentResult[] payments)
        {
            var budgetValue = payments.Where(r => r.CalculationPeriod == period.PeriodName && 
                                            r.DeliveryPeriod == period.PeriodName
                                            && r.FundingSource == Enums.FundingSource.CoInvestedSfa 
                                            && r.ContractType == Enums.ContractType.ContractWithEmployer)
                                            .Sum(r => r.Amount);
            if (period.Value != budgetValue)
            {
                throw new Exception($"Expected sfa levy budget to be {period.Value} in {period.PeriodName} but actually was {budgetValue}");
            }
        }
    }
}
