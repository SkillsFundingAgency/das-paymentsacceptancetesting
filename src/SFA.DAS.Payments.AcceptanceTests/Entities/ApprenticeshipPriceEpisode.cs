using System;
using IlrBuilder = SFA.DAS.Payments.AcceptanceTests.Builders.IlrBuilder;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class ApprenticeshipPriceEpisode 
    {
        public ApprenticeshipPriceEpisode()
        {
            StandardCode = IlrBuilder.Defaults.StandardCode;
            FrameworkCode = IlrBuilder.Defaults.FrameworkCode;
            PathwayCode = IlrBuilder.Defaults.PathwayCode;
            ProgrammeType = IlrBuilder.Defaults.ProgrammeType;


        }

        public string PriceEpisodeIdentifier { get; set; }
        public decimal PriceEpisodeTotalTNPPrice { get; set; }
        public LearnerType LearnerType { get; set; }
        public DateTime EpisodeStartDate { get; set; }
        public DateTime PriceEpisodePlannedEndDate { get; set; }
        public DateTime? PriceEpisodeActualEndDate { get; set; }
        public CompletionStatus CompletionStatus { get; set; }

        /***************************/
        /* REMOVE THESE ************/
        /***************************/
        public long StandardCode { get; set; }
        public int FrameworkCode { get; set; }
        public int ProgrammeType { get; set; }
        public int PathwayCode { get; set; }

        /***************************/
        /* REMOVE THESE ************/
        /***************************/

        public decimal PriceEpisodeInstalmentValue { get; set; }
        public decimal PriceEpisodeCompletionElement { get; set; }

        public decimal? TNP1 { get; set; }
        public decimal? TNP2 { get; set; }
        public decimal? TNP3 { get; set; }
        public decimal? TNP4 { get; set; }


    }
}