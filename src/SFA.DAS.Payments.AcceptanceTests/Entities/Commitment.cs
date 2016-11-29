using System;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class Commitment
    {
        public long Id { get; set; }
        public int Priority { get; set; }
        public string Learner { get; set; }
        public string Employer { get; set; }
        public string Provider { get; set; }
        public CommitmentPaymentStatus Status { get; set; }
        public string StopPeriod { get; set; }

        public DateTime? StopPeriodCensusDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(StopPeriod))
                {
                    return null;
                }

                return
                    new DateTime(int.Parse(StopPeriod.Substring(3)) + 2000, int.Parse(StopPeriod.Substring(0, 2)), 1)
                        .NextCensusDate();
            }
        }
    }
}