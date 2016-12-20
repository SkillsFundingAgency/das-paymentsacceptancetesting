using System;
using System.Collections.Generic;
using System.Linq;
using IlrBuilder = SFA.DAS.Payments.AcceptanceTests.Builders.IlrBuilder;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class LearningDelivery 
    {
        public LearningDelivery()
        {
            StandardCode = IlrBuilder.Defaults.StandardCode;
            FrameworkCode = IlrBuilder.Defaults.FrameworkCode;
            PathwayCode = IlrBuilder.Defaults.PathwayCode;
            ProgrammeType = IlrBuilder.Defaults.ProgrammeType;
           
        }

        public LearnerType LearnerType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public CompletionStatus CompletionStatus { get; set; }
        public long StandardCode { get; set; }
        public int FrameworkCode { get; set; }
        public int ProgrammeType { get; set; }
        public int PathwayCode { get; set; }

        public PriceEpisode[] PriceEpisodes { get; set; }

        public LearningDeliveryFam[] LearningDeliveryFams { get; set; }

        public void AddPriceEpisode(PriceEpisode priceEpisode)
        {
            var priceEpisodes = PriceEpisodes?.ToList() ?? new List<PriceEpisode>();

            priceEpisodes.Add(priceEpisode);

            PriceEpisodes = priceEpisodes.ToArray();
        }
    }
}