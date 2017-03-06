using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts
{
    public class SubmissionContext
    {
        public SubmissionContext()
        {
            IlrLearnerDetails = new List<IlrLearnerReferenceData>();
        }
        public List<IlrLearnerReferenceData> IlrLearnerDetails { get; set; }
    }
}
