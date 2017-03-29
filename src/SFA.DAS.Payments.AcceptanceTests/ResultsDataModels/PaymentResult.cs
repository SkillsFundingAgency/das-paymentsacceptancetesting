using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.ResultsDataModels
{
    public class PaymentResult
    {
        public int EmployerAccountId { get; set; }
        public string CalculationPeriod { get; set; }
        public string DeliveryPeriod { get; set; }
        public FundingSource FundingSource { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public ContractType ContractType { get; set; }
    }
}