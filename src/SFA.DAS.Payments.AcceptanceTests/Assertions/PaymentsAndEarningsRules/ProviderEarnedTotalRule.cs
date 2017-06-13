using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.PaymentsAndEarningsRules
{
    public class ProviderEarnedTotalRule : EarningsAndPaymentsRuleBase
    {
        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, IEnumerable<LearnerResults> submissionResults, EmployerAccountContext employerAccountContext)
        {
            var allEarnings = GetEarningsForBreakdown(breakdown, submissionResults);
            foreach (var period in breakdown.ProviderEarnedTotal)
            {
                AssertResultsForPeriod(period, allEarnings);
            }
        }

        private LearnerEarningsResult[] GetEarningsForBreakdown(EarningsAndPaymentsBreakdown breakdown, IEnumerable<LearnerResults> submissionResults)
        {
            var filteredResults = submissionResults.Where(r => r.ProviderId.Equals(breakdown.ProviderId, StringComparison.CurrentCultureIgnoreCase));
            if (breakdown is LearnerEarningsAndPaymentsBreakdown)
            {
                filteredResults = filteredResults.Where(r => r.LearnerId.Equals(((LearnerEarningsAndPaymentsBreakdown)breakdown).LearnerId, StringComparison.CurrentCultureIgnoreCase));
            }
            return filteredResults.Select(r => r.Earnings.Select(e => new LearnerEarningsResult
            {
                LearnerId = r.LearnerId,
                DeliveryPeriod = e.DeliveryPeriod,
                CalculationPeriod = e.CalculationPeriod,
                Value = e.Value
            })).SelectMany(e => e)
            .OrderBy(e => e.DeliveryPeriod)
            .ThenBy(e => e.LearnerId)
            .ToArray();
        }
        private void AssertResultsForPeriod(PeriodValue period, LearnerEarningsResult[] allEarnings)
        {
            // This is not picking correct earning when price changed mid-year and the change period it the one we care about.
            var earnedInPeriod = (from e in allEarnings
                                  where e.DeliveryPeriod == period.PeriodName
                                  && ComparePeriods(e.CalculationPeriod, period.PeriodName) >= 0
                                  group e by e.LearnerId into g
                                  select g.First()).Sum(x => x.Value);
            if (!AreValuesEqual(period.Value, earnedInPeriod))
            {
                throw new Exception($"Expected provider to earn {Math.Round(period.Value, 2)} in {period.PeriodName} but actually earned {Math.Round(earnedInPeriod, 2)}");
            }
        }

        private int ComparePeriods(string x, string y)
        {
            var xDate = x.ToPeriodDateTime();
            var yDate = y.ToPeriodDateTime();
            return xDate.CompareTo(yDate);
        }

        private class LearnerEarningsResult : EarningsResult
        {
            public string LearnerId { get; set; }
        }
    }
}
