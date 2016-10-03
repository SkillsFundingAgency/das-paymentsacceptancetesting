using System;
using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;

namespace SFA.DAS.Payments.AcceptanceTests.Contexts
{
    public class EarningAndPaymentsContext
    {

        public EarningAndPaymentsContext(ReferenceDataContext referenceDataContext)
        {
            ReferenceDataContext = referenceDataContext;
        }

        public ReferenceDataContext ReferenceDataContext { get; set; }

        public DateTime IlrStartDate { get; set; }
        public DateTime IlrPlannedEndDate { get; set; }
        public DateTime? IlrActualEndDate { get; set; }
        public CompletionStatus IlrCompletionStatus { get; set; }

        public Dictionary<string, decimal> EarnedByPeriod { get; set; }
        public int Ukprn { get; set; }
    }
}
