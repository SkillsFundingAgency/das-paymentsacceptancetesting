using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.PaymentsAndEarningsRules
{
    public class SfaLevyAdditionalPaymentsRule : PaymentsRuleBase
    {
        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, IEnumerable<LearnerResults> submissionResults, EmployerAccountContext employerAccountContext)
        {
            var payments = GetPaymentsForBreakdown(breakdown, submissionResults)
                .Where(p => p.FundingSource == FundingSource.FullyFundedSfa && p.ContractType == ContractType.ContractWithEmployer)
                .ToArray();
            foreach (var period in breakdown.SfaLevyAdditionalPayments)
            {
                AssertResultsForPeriod(period, payments);
            }
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            return $"Expected SFA Levy additional payment of {period.Value} in {period.PeriodName} but was actually {actualPaymentInPeriod}";
        }
    }
}
