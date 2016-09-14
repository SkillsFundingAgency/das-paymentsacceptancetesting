using System;

namespace SFA.DAS.Payments.AcceptanceTests.Contexts
{
    public class EarningContext
    {

        public EarningContext(ReferenceDataContext referenceDataContext)
        {
            ReferenceDataContext = referenceDataContext;
        }

        public ReferenceDataContext ReferenceDataContext { get; set; }

        public DateTime IlrStartDate { get; set; }
        public DateTime IlrPlannedEndDate { get; set; }
        public DateTime? IlrActualEndDate { get; set; }
        public CompletionStatus IlrCompletionStatus { get; set; }
    }
}
