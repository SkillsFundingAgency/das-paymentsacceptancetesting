using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels
{
    public class DataLockResult
    {
        public long CommitmentId { get; set; }
        public long CommitmentVersion { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}