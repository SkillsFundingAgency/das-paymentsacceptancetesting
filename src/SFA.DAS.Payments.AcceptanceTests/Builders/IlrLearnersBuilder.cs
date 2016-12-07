using System.Linq;
using IlrGenerator;

namespace SFA.DAS.Payments.AcceptanceTests.Builders
{
    public class IlrLearnersBuilder : IlrBuilder
    {
        internal Learner[] Learners;

        internal IlrLearnersBuilder(IlrBuilder parentBuilder)
            : base(parentBuilder.Submission)
        {
            Learners = new[]
            {
                new Learner
                {
                    Uln = Defaults.FirstUln + parentBuilder.Submission.Learners.Length,
                    LearningDeliveries = new LearningDelivery[0]
                }
            };

            parentBuilder.Submission.Learners = Learners;
        }

        public IlrLearnersBuilder WithLearners(Entities.Learner[] learners)
        {

            Learners = learners.Select(l =>
            {
                var tnp1 = l.LearningDelivery.PriceEpisodeTotalTNPPrice * 0.8m;
                var tnp2 = l.LearningDelivery.PriceEpisodeTotalTNPPrice - tnp1;

                if (l.LearningDelivery.StandardCode == 0)
                {
                    tnp1 = tnp1 + tnp2;
                    tnp2 = 0;
                }
                return new Learner
                {
                    Uln = l.Uln,
                    LearningDeliveries = new[]
                    {
                        new LearningDelivery
                        {
                            ActFamCodeValue = (short) l.LearningDelivery.LearnerType,
                            ActualStartDate = l.LearningDelivery.EpisodeStartDate,
                            PlannedEndDate = l.LearningDelivery.PriceEpisodePlannedEndDate,
                            ActualEndDate = l.LearningDelivery.PriceEpisodeActualEndDate,

                            StandardCode = l.LearningDelivery.StandardCode,
                            ProgrammeType = l.LearningDelivery.ProgrammeType,
                            FrameworkCode = l.LearningDelivery.FrameworkCode,
                            PathwayCode = l.LearningDelivery.PathwayCode,

                            TrainingCost = tnp1,
                            EndpointAssesmentCost = tnp2
                        }
                    }
                };
            }).ToArray();

            Submission.Learners = Learners;

            return this;
        }
    }
}