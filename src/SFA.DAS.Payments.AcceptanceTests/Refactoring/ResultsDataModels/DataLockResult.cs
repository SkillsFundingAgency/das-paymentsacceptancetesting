using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels
{
    public class DataLockResult
    {
        public int? CommitmentId { get; set; }
        public int? CommitmentVersion { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}