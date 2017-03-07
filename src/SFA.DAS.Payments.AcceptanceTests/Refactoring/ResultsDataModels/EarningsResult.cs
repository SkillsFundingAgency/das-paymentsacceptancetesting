namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels
{
    public class EarningsResult
    {
        public string CalculationPeriod { get; set; }
        public string DeliveryPeriod { get; set; }
        public string TransactionType { get; set; }
        public decimal Value { get; set; }
    }
}