using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels
{
    public class PaymentResult
    {
        public string CalculationPeriod { get; set; }
        public string DeliveryPeriod { get; set; }
        public FundingSource FundingSource { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public ContractType ContractType { get; set; }
    }
}