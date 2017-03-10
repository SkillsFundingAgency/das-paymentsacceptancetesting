using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using System.Linq;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.PaymentsAndEarningsRules
{
    public class EmployersLevyAccountDebitedRule : PaymentsRuleBase
    {

        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, SubmissionContext submissionContext)
        {
            var payments = GetPaymentsForBreakdown(breakdown, submissionContext)
                .Where(p => p.FundingSource == FundingSource.Levy && p.ContractType == ContractType.ContractWithEmployer)
                .ToArray();
            foreach (var period in breakdown.EmployersLevyAccountDebited)
            {
                AssertResultsForPeriod(period, payments);
            }
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            return $"Expected Employer levy budget to be debited {period.Value} in {period.PeriodName} but was actually debited {actualPaymentInPeriod}";
        }
    }
}