namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels
{
    public class DataLockPeriodMatch
    {
        public string LearnerId { get; set; }
        public string PeriodName { get; set; }
        public int? CommitmentId { get; set; }
        public int? CommitmentVersion { get; set; }
    }
}