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
                var prevPeriodDate = PeriodNameToDate(period.PeriodName).AddMonths(-1);
                period.PeriodName = DateToPeriodName(prevPeriodDate);

                AssertResultsForPeriod(period, employerPayments);
            }
        }

        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            var employerPeriod = (EmployerAccountPeriodValue)period;
            var specPeriod = DateToPeriodName(PeriodNameToDate(period.PeriodName).AddMonths(1));

            return $"Expected Employer {employerPeriod.EmployerAccountId} levy budget to be debited {period.Value} in {specPeriod} but was actually debited {actualPaymentInPeriod}";
        }


        private DateTime PeriodNameToDate(string name)
        {
            return new DateTime(int.Parse(name.Substring(3, 2)) + 2000, int.Parse(name.Substring(0, 2)), 1);
        }
        private string DateToPeriodName(DateTime date)
        {
            return $"{date.Month:00}/{date.Year - 2000:00}";
        }
    }
}