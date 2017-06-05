using System;

namespace SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels
{
    public class DataLockEventReferenceData
    {
        public string PriceEpisodeIdentifier { get; set; }
        public int ApprenticeshipId { get; set; }
        public long Uln { get; set; }
        public DateTime IlrStartDate { get; set; }
        public decimal IlrTrainingPrice { get; set; }
        public decimal IlrEndpointAssementPrice { get; set; }
        public DateTime ILrEffectiveFrom { get; set; }
        public DateTime? ILrEffectiveTo { get; set; }
    }
}
