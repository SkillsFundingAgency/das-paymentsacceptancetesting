using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.ResultsDataModels
{
    public class SubmissionDataLockResult
    {
        public long CommitmentId { get; set; }
        public string CommitmentVersion { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
