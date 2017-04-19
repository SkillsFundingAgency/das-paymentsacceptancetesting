using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels
{
    public class DataLockEventCommitmentReferenceData
    {
        public string PriceEpisodeIdentifier { get; set; }
        public int ApprenticeshipVersion { get; set; }
        public DateTime StartDate { get; set; }
        public int FrameworkCode { get; set; }
        public int ProgrammeType { get; set; }
        public int PathwayCode { get; set; }
        public long StandardCode { get; set; }
        public int NegotiatedPrice { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
