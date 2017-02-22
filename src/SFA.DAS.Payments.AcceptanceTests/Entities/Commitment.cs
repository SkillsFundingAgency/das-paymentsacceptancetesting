using System;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class Commitment
    {
        public Commitment()
        {
            Status = CommitmentPaymentStatus.Active;
        }

        public long Id { get; set; }
        public long VersionId { get; set; }
        public int Priority { get; set; }
        public string Learner { get; set; }
        public string Employer { get; set; }
        public string Provider { get; set; }

        public decimal? AgreedPrice { get; set; }

        public CommitmentPaymentStatus Status { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long StandardCode { get; set; }

        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public int FrameworkCode { get; set; }
        public int ProgrammeType { get; set; }
        public int PathwayCode { get; set; }
    }
}