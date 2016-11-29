using System;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities
{
    public class PaymentEntity
    {
        public Guid PaymentId { get; set; }
        public Guid RequiredPaymentId { get; set; }
        public string CommitmentId { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public long Ukprn { get; set; }
        public int DeliveryMonth { get; set; }
        public int DeliveryYear { get; set; }
        public int CollectionPeriodMonth { get; set; }
        public int CollectionPeriodYear { get; set; }
        public int FundingSource { get; set; }
        public int TransactionType { get; set; }
        public decimal Amount { get; set; }
    }
}
