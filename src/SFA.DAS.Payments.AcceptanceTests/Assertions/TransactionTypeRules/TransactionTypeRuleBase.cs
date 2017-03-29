using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.TransactionTypeRules
{
    public abstract class TransactionTypeRuleBase
    {
        public virtual void AssertPeriodValues(IEnumerable<PeriodValue> periodValues, LearnerResults[] submissionResults, EmployerAccountContext employerAccountContext)
        {
            foreach (var period in periodValues)
            {
                var payments = FilterPayments(period, submissionResults, employerAccountContext);
                var paidInPeriod = payments.Sum(p => p.Amount);

                if(Math.Round(paidInPeriod, 2) != Math.Round(period.Value, 2))
                {
                    throw new Exception(FormatAssertionFailureMessage(period, paidInPeriod));
                }
            }
        }

        protected abstract IEnumerable<PaymentResult> FilterPayments(PeriodValue period, IEnumerable<LearnerResults> submissionResults, EmployerAccountContext employerAccountContext);
        protected abstract string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod);
    }
}
