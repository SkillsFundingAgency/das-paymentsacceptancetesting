using System;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class DataLockMatch
    {
        public long CommitmentId { get; set; }
        public string PriceEpisodeId { get; set; }
        public DateTime PriceEpisodeStartDate { get; set; }
    }
}