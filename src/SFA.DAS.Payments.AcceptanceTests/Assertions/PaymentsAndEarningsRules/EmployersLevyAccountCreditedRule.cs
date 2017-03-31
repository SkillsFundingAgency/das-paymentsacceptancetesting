using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;
using System;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.PaymentsAndEarningsRules
{
    public class EmployersLevyAccountCreditedRule : PaymentsRuleBase
    {
        public override void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, IEnumerable<LearnerResults> submissionResults, EmployerAccountContext employerAccountContext)
        {
            var payments = GetPaymentsForBreakdown(breakdown, submissionResults)
                .Where(p => p.FundingSource == FundingSource.Levy && 
                p.ContractType == ContractType.ContractWithEmployer && p.Amount <= 0)
                .ToArray();
            foreach (var period in breakdown.EmployersLevyAccountCredited)
            {
                var employerPayments = payments.Where(p => p.EmployerAccountId == period.EmployerAccountId).ToList();

                var positivePayments = employerPayments.Select(x =>
                new PaymentResult
                {
                    Amount =x.Amount*-1,
                    CalculationPeriod = x.CalculationPeriod,
                    ContractType = x.ContractType,
                    DeliveryPeriod = x.DeliveryPeriod,
                    EmployerAccountId = x.EmployerAccountId,
                    FundingSource = x.FundingSource,
                    TransactionType = x.TransactionType
                });

                var prevPeriod = new EmployerAccountPeriodValue
                {
                    EmployerAccountId = period.EmployerAccountId,
                    PeriodName = period.PeriodName.ToPeriodDateTime().AddMonths(-1).ToPeriodName(),
                    Value = period.Value
                };

                AssertResultsForPeriod(prevPeriod, positivePayments.ToArray());
            }
        }

        
        protected override string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod)
        {
            var employerPeriod = (EmployerAccountPeriodValue)period;
            var specPeriod = period.PeriodName.ToPeriodDateTime().AddMonths(1).ToPeriodName();

            return $"Expected Employer {employerPeriod.EmployerAccountId} levy to be credited {period.Value} in {specPeriod} but was actually credited {actualPaymentInPeriod}";
        }
    }
}