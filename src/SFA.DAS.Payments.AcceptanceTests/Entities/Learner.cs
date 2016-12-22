using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class Learner
    {
        public Learner()
        {
            LearningDeliveries = new List<LearningDelivery>();
        }
        public string Name { get; set; }
        public long Uln { get; set; }

        public string LearnRefNumber { get; set; }
            
        public List<LearningDelivery> LearningDeliveries { get; set; }

        public LearningDelivery LearningDelivery
        {
            get {
                return LearningDeliveries[0];
            }
        }
    }
}