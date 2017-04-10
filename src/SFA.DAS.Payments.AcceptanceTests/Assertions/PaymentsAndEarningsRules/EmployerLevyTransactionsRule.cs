using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.PaymentsAndEarningsRules
{
    public class EmployerLevyTransactionsRule : PaymentsRuleBase
    {

        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, IEnumerable<LearnerResults> submissionResults, EmployerAccountContext employerAccountContext)
        {
            var payments = submissionResults.SelectMany(r => r.Payments)
                        .Where(r=> r.FundingSource == FundingSource.Levy &&
                        r.ContractType == ContractType.ContractWithEmployer);
            
            foreach (var period in breakdown.EmployerLevyTransactions)
            {
                var employerPayments = payments.Where(p => p.EmployerAccountId == period.EmployerAccountId).ToList();
                AssertResultsForPeriod(period, employerPayments.ToArray());
            }
        }


        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            var employerPeriod = (EmployerAccountPeriodValue)period;
            var specPeriod = period.PeriodName.ToPeriodDateTime().AddMonths(1).ToPeriodName();

            return $"Expected Employer transactions net for {employerPeriod.EmployerAccountId} expected to be {period.Value} in {specPeriod} but was actually {actualPaymentInPeriod}";
        }
    }
}
