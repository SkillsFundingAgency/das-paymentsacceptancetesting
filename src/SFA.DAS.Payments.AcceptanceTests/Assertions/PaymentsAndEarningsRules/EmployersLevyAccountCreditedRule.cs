using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using System.Collections.Generic;
using System.Linq;


namespace SFA.DAS.Payments.AcceptanceTests.Assertions.PaymentsAndEarningsRules
{
    public class EmployersLevyAccountCreditedRule : EmployersLevyAccountDebitedRule
    {

       
        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            var employerPeriod = (EmployerAccountPeriodValue)period;
            var specPeriod = period.PeriodName.ToPeriodDateTime().AddMonths(1).ToPeriodName();

            return $"Expected Employer {employerPeriod.EmployerAccountId} levy budget to be credited {period.Value} in {specPeriod} but was actually credited {actualPaymentInPeriod}";
        }
    }
}