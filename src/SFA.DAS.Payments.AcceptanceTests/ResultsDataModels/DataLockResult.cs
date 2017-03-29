using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.ResultsDataModels
{
    public class DataLockResult
    {
        public long CommitmentId { get; set; }
        public long CommitmentVersion { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}