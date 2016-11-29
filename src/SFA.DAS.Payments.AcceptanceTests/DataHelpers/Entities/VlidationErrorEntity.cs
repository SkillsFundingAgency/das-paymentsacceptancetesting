namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities
{
    public class VlidationErrorEntity
    {
        public long Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long AimSeqNumber { get; set; }
        public string RuleId { get; set; }
        public string CollectionPeriodName { get; set; }
        public int CollectionPeriodMonth { get; set; }
        public int CollectionPeriodYear { get; set; }
        
    }
}
