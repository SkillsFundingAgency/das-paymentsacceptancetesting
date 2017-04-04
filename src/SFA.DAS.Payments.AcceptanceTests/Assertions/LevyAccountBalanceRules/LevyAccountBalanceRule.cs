using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.LevyAccountBalanceRules
{
    public class LevyAccountBalanceRule
    {

        public  void AssertBreakdown(PeriodValue breakdown, LearnerResults result)
        {
            AssertResultsForPeriod(breakdown, result.LevyAccountBalanceResults.ToArray());
        }

        protected  void AssertResultsForPeriod(PeriodValue period, LevyAccountBalanceResult[] balances)
        {
            var balanceInPeriod = balances.SingleOrDefault(p => p.CalculationPeriod == period.PeriodName);
            if (balanceInPeriod == null || period.Value != balanceInPeriod.Amount)
            {
                throw new Exception(FormatAssertionFailureMessage(period, balanceInPeriod.Amount));
            }
        }

        protected string FormatAssertionFailureMessage(PeriodValue period, decimal actualbalance)
        {
            return $"Expected {period.Value} levy balance in {period.PeriodName} but was actually {actualbalance}";
        }
    }
}
