using System;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class Learner
    {
        public string Name { get; set; }
        public long Uln { get; set; }

        public string LearnRefNumber { get; set; }
            
        public LearningDelivery LearningDelivery { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}