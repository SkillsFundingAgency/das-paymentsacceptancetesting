using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions.TransactionTypeRules
{
    public abstract class TransactionTypeRuleBase
    {
        public virtual void AssertPeriodValues(IEnumerable<PeriodValue> periodValues, LearnerResults[] submissionResults, EmployerAccountContext employerAccountContext)
        {
            foreach (var period in periodValues)
            {
                var payments = FilterPayments(period, submissionResults);
                var paidInPeriod = payments.Sum(p => p.Amount);

                if (period.Value != paidInPeriod)
                {
                    throw new Exception(FormatAssertionFailureMessage(period, paidInPeriod));
                }
            }
        }

        protected abstract IEnumerable<PaymentResult> FilterPayments(PeriodValue period, IEnumerable<LearnerResults> submissionResults);
        protected abstract string FormatAssertionFailureMessage(PeriodValue period, decimal actualPaymentInPeriod);
    }
}
