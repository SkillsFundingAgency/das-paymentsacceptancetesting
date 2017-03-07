namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels
{
    public class PaymentResult
    {
        public string CalculationPeriod { get; set; }
        public string DeliveryPeriod { get; set; }
        public int FundingSource { get; set; }
        public int TransactionType { get; set; }
        public decimal Amount { get; set; }
    }
}