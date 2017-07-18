namespace SFA.DAS.Payments.AcceptanceTests.DataCollectors.Entities
{
    public class SubmissionDataLockResultEntity
    {
        public long Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int CollectionPeriodMonth { get; set; }
        public int CollectionPeriodYear { get; set; }
        
        public long CommitmentId { get; set; }
        public string CommitmentVersion { get; set; }
        public int TransactionType { get; set; }
    }
}