using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.PaymentsAndEarningsRules
{
    public class ProviderEarnedTotalRule : EarningsAndPaymentsRuleBase
    {
        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, SubmissionContext submissionContext)
        {
            var allEarnings = GetEarningsForBreakdown(breakdown, submissionContext);
            foreach (var period in breakdown.ProviderEarnedTotal)
            {
                AssertResultsForPeriod(period, allEarnings);
            }
        }

        private EarningsResult[] GetEarningsForBreakdown(EarningsAndPaymentsBreakdown breakdown, SubmissionContext submissionContext)
        {
            var earnings = submissionContext.SubmissionResults.Where(r => r.ProviderId.Equals(breakdown.ProviderId, StringComparison.CurrentCultureIgnoreCase));
            if (breakdown is LearnerEarningsAndPaymentsBreakdown)
            {
                earnings = earnings.Where(r => r.LearnerId.Equals(((LearnerEarningsAndPaymentsBreakdown)breakdown).LearnerId, StringComparison.CurrentCultureIgnoreCase));
            }
            return earnings.SelectMany(r => r.Earnings).ToArray();
        }
        private void AssertResultsForPeriod(PeriodValue period, EarningsResult[] allEarnings)
        {
            var earnedInPeriod = allEarnings.Where(r => r.CalculationPeriod == period.PeriodName && r.DeliveryPeriod == period.PeriodName).Sum(r => r.Value);
            if (period.Value != earnedInPeriod)
            {
                throw new Exception($"Expected provider to earn {period.Value} in {period.PeriodName} but actually earned {earnedInPeriod}");
            }
        }
    }
}
