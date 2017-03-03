namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class DataLockMatch
    {
        public long CommitmentId { get; set; }
        public string PriceEpisodeId { get; set; }
        public decimal Price { get; set; }
    }
}