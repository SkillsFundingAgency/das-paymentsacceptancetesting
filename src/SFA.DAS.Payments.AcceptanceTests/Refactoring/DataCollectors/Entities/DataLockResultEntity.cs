namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.DataCollectors.Entities
{
    public class DataLockResultEntity
    {
        public long Ukprn { get; set; }
        public long Uln { get; set; }
        public int Period { get; set; }
        public long CommitmentId { get; set; }
        public long CommitmentVersion { get; set; }
        public int TransactionType { get; set; }
    }
}