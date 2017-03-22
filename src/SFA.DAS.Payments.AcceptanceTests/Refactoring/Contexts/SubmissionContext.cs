using System;
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
            EmploymentStatus = new List<EmploymentStatusReferenceData>();
            LearningSupportStatus = new List<LearningSupportReferenceData>();
        }

        public bool HaveSubmissionsBeenDone { get; set; }
        public List<IlrLearnerReferenceData> IlrLearnerDetails { get; set; }
        public List<LearnerResults> SubmissionResults { get; set; }
        public List<ContractTypeReferenceData> ContractTypes { get; set; }
        public List<EmploymentStatusReferenceData> EmploymentStatus { get; set; }
        public List<LearningSupportReferenceData> LearningSupportStatus { get; set; }
        public DateTime? FirstSubmissionDate { get; set; }
    }
}
