namespace SFA.DAS.Payments.AcceptanceTests.DataCollectors.Entities
{
    public class PaymentResultEntity
    {
        public long Ukprn { get; set; }
        public long Uln { get; set; }
        public int DeliveryMonth { get; set; }
        public int DeliveryYear { get; set; }
        public int CollectionPeriodMonth { get; set; }
        public int CollectionPeriodYear { get; set; }
        public int FundingSource { get; set; }
        public int TransactionType { get; set; }
        public decimal Amount { get; set; }

        public int ApprenticeshipContractType { get; set; }

        public string AccountId { get; set; }
    }
}
