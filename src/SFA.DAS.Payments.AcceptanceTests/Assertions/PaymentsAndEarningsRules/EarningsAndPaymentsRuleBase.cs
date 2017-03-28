using System;
using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.PaymentsAndEarningsRules
{
    public abstract class EarningsAndPaymentsRuleBase
    {
        public abstract void AssertBreakdown(EarningsAndPaymentsBreakdown breakdown, IEnumerable<LearnerResults> submissionResults, EmployerAccountContext employerAccountContext);

        protected bool AreValuesEqual(decimal expected, decimal actual)
        {
            return Math.Round(actual, 2) == Math.Round(expected, 2);
        }
    }
}
