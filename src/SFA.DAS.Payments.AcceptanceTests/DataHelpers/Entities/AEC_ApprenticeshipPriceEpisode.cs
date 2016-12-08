using System;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities
{
    public class AEC_ApprenticeshipPriceEpisode
    {
        public long UKPRN { get; set; }
       
        public DateTime EpisodeStartDate { get; set; }
        public DateTime PriceEpisodePlannedEndDate { get; set; }
        public DateTime? PriceEpisodeActualEndDate { get; set; }
        public CompletionStatus CompletionStatus { get; set; }
        public decimal PriceEpisodeTotalTNPPrice { get; set; }
        public decimal PriceEpisodeInstalmentValue { get; set; }
        public decimal PriceEpisodeCompletionElement { get; set; }

    }
}
