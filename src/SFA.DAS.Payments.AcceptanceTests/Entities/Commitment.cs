using System;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class Commitment
    {
        public Commitment()
        {
            Status = CommitmentPaymentStatus.Active;
            //VersionId = 1;
        }

        public long Id { get; set; }
        public long VersionId { get; set; }
        public int Priority { get; set; }
        public string Learner { get; set; }
        public string Employer { get; set; }
        public string Provider { get; set; }

        public decimal? AgreedPrice { get; set; }

        public CommitmentPaymentStatus Status { get; set; }
        public string StopPeriod { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public long? StandardCode { get; set; }

        public DateTime? StopPeriodCensusDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(StopPeriod) && !ActualEndDate.HasValue)
                {
                    return null;
                }
                if (!string.IsNullOrWhiteSpace(StopPeriod))
                {
                    return new DateTime(int.Parse(StopPeriod.Substring(3)) + 2000, int.Parse(StopPeriod.Substring(0, 2)), 1)
                        .NextCensusDate();
                }
                else
                {
                    return ActualEndDate.Value.AddMonths(1).NextCensusDate();
                }
            }
        }

        public string CommitmentIdenifier { get; set; }

        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }
}