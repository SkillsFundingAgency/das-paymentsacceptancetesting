namespace SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels
{
    public class DataLockEventPeriodReferenceData
    {
        public string PriceEpisodeIdentifier { get; set; }
        public string Period { get; set; }
        public bool PayableFlag { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
