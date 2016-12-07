namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class Learner
    {
        public string Name { get; set; }
        public long Uln { get; set; }
        public ApprenticeshipPriceEpisode LearningDelivery { get; set; }
    }
}