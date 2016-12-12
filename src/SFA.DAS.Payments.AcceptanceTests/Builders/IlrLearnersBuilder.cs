using System.Linq;
using IlrGenerator;
using PriceEpisode = SFA.DAS.Payments.AcceptanceTests.Entities.PriceEpisode;
using System.Collections.Generic;

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
                    LearnRefNumber=  Defaults.FirstUln + parentBuilder.Submission.Learners.Length.ToString(),
                    LearningDeliveries = new LearningDelivery[0]
                }
            };

            parentBuilder.Submission.Learners = Learners;
        }

        public IlrLearnersBuilder WithLearners(Entities.Learner[] learners)
        {

            Learners = learners.Select(l =>
            {
                //var tnp1 = l.LearningDelivery.PriceEpisodes[0].TotalPrice * 0.8m;
                //var tnp2 = l.LearningDelivery.PriceEpisodes[0].TotalPrice - tnp1;

                //if (l.LearningDelivery.StandardCode == 0)
                //{
                //    tnp1 = tnp1 + tnp2;
                //    tnp2 = 0;
                //}

                return new Learner
                {
                    Uln = l.Uln,
                    LearnRefNumber=l.LearnRefNumber,
                    LearningDeliveries = new[]
                    {
                        new LearningDelivery
                        {
                            ActFamCodeValue = (short) l.LearningDelivery.LearnerType,
                            ActualStartDate = l.LearningDelivery.StartDate,
                            PlannedEndDate = l.LearningDelivery.PlannedEndDate,
                            ActualEndDate = l.LearningDelivery.ActualEndDate,

                            StandardCode = l.LearningDelivery.StandardCode,
                            ProgrammeType = l.LearningDelivery.ProgrammeType,
                            FrameworkCode = l.LearningDelivery.FrameworkCode,
                            PathwayCode = l.LearningDelivery.PathwayCode,

                            //TrainingCost = tnp1,
                            //EndpointAssesmentCost = tnp2,

                            FinancialRecords = GetLearningDeliveryFinancialRecords(l.LearningDelivery)
                        }
                    }
                };
            }).ToArray();

            Submission.Learners = Learners;

            return this;
        }

        private FinancialRecord[] GetLearningDeliveryFinancialRecords(Entities.LearningDelivery learningDelivery)
        {
            var financialRecords = new List<FinancialRecord>();

            foreach (var priceEpisode in learningDelivery.PriceEpisodes)
            {
                financialRecords.AddRange(GetPriceEpisodeFinancialRecords(priceEpisode));
            }

            return financialRecords.ToArray();
        }

        private FinancialRecord[] GetPriceEpisodeFinancialRecords(PriceEpisode episode)
        {
            if (episode == null)
            {
                return new FinancialRecord[0];
            }

            var financialRecords = new FinancialRecord[]
            {
                new FinancialRecord
                {
                    Type = "TNP",
                    Code = episode.Tnp1 != null ? 1 : 3,
                    Date = episode.StartDate,
                    Amount = episode.Tnp1 != null ? (int)episode.Tnp1 : (int)episode.Tnp3
                },
                new FinancialRecord
                {
                    Type = "TNP",
                    Code = episode.Tnp2 != null ? 2 : 4,
                    Date = episode.StartDate,
                    Amount = episode.Tnp2 != null ? (int)episode.Tnp2 : (int)episode.Tnp4
                }
            };

            return financialRecords;
        }
    }
}