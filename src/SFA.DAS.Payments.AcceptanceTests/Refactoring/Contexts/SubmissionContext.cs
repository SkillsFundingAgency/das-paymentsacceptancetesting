using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts
{
    public class SubmissionContext
    {
        public SubmissionContext()
        {
            HaveSubmissionsBeenDone = false;
            IlrLearnerDetails = new List<IlrLearnerReferenceData>();
            ContractTypes = new List<ContractTypeReferenceData>();
        }

        public bool HaveSubmissionsBeenDone { get; set; }
        public List<IlrLearnerReferenceData> IlrLearnerDetails { get; set; }
        public List<LearnerResults> SubmissionResults { get; set; }
        public List<ContractTypeReferenceData> ContractTypes { get; set; }
    }
}
