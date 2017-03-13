using System;
using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class Provider
    {
        public string Name { get; set; }
        public long Ukprn { get; set; }
        public DateTime IlrStartDate
        {
            get { return Learners.Min(l => l.LearningDelivery.StartDate); }
        }
        public DateTime IlrEndDate
        {
            get
            {
                var maxActualEndDate = Learners.Max(l => l.LearningDeliveries.Max(ld => ld.ActualEndDate)) ?? DateTime.MinValue;
                var maxEndDate = Learners.Max(l => l.LearningDeliveries.Max(ld => ld.PlannedEndDate));

                return maxActualEndDate > maxEndDate
                    ? maxActualEndDate
                    : maxEndDate;
            }
        }
        public Learner[] Learners { get; set; }
        public Dictionary<string, decimal> EarnedByPeriod { get; set; }

        public Dictionary<long, Dictionary<string, decimal>> EarnedByPeriodByUln { get; set; }

        public Dictionary<string, DataLockMatch[]> DataLockMatchesByPeriod { get; set; } 

        public Provider()
        {
            EarnedByPeriod = new Dictionary<string, decimal>();
            DataLockMatchesByPeriod = new Dictionary<string, DataLockMatch[]>();
            EarnedByPeriodByUln = new Dictionary<long, Dictionary<string, decimal>>();
        }

        public PriceEpisode GetPriceEpisode(string key)
        {
            foreach (var learner in Learners)
            {
                foreach (var learningDelivery in learner.LearningDeliveries)
                {
                    if (learningDelivery.PriceEpisodes.Any(pe => pe.DataLockMatchKey == key))
                    {
                        return learningDelivery.PriceEpisodes.First(pe => pe.DataLockMatchKey == key);
                    }
                }
            }

            return null;
        }
    }
}