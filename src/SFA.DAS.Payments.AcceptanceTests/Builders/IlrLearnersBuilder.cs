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

        public IlrLearnersBuilder WithLearners(Contexts.Learner[] learners)
        {

            Learners = learners.Select(l =>
            {
                var tnp1 = l.LearningDelivery.AgreedPrice * 0.8m;
                var tnp2 = l.LearningDelivery.AgreedPrice - tnp1;
                return new Learner
                {
                    Uln = l.Uln,
                    LearningDeliveries = new[]
                    {
                        new LearningDelivery
                        {
                            ActFamCodeValue = (short) l.LearningDelivery.LearnerType,
                            ActualStartDate = l.LearningDelivery.StartDate,
                            PlannedEndDate = l.LearningDelivery.PlannedEndDate,
                            ActualEndDate = l.LearningDelivery.ActualEndDate,
                            StandardCode = Defaults.StandardCode,
                            ProgrammeType = Defaults.ProgrammeType,
                            FrameworkCode = Defaults.FrameworkCode,
                            PathwayCode = Defaults.PathwayCode,
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