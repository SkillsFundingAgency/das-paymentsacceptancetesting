using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using System;
using System.Linq;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.PaymentsAndEarningsRules
{
    public class EmployersLevyAccountDebitedRule : PaymentsRuleBase
    {

        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, SubmissionContext submissionContext, EmployerAccountContext employerAccountContext)
        {
            var payments = GetPaymentsForBreakdown(breakdown, submissionContext)
                .Where(p => p.FundingSource == FundingSource.Levy && p.ContractType == ContractType.ContractWithEmployer)
                .ToArray();
            foreach (var period in breakdown.EmployersLevyAccountDebited)
            {
                var employerPayments = payments.Where(p => p.EmployerAccountId == period.EmployerAccountId).ToArray();
                period.PeriodName = period.PeriodName.ToPeriodDateTime().AddMonths(-1).ToPeriodName();

                AssertResultsForPeriod(period, employerPayments);
            }
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            var employerPeriod = (EmployerAccountPeriodValue)period;
            var specPeriod = period.PeriodName.ToPeriodDateTime().AddMonths(1).ToPeriodName();

            return $"Expected Employer {employerPeriod.EmployerAccountId} levy budget to be debited {period.Value} in {specPeriod} but was actually debited {actualPaymentInPeriod}";
        }
    }
}