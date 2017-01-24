using System;
using System.Linq;
using IlrGenerator;
using PriceEpisode = SFA.DAS.Payments.AcceptanceTests.Entities.PriceEpisode;
using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.Enums;
using CompletionStatus = IlrGenerator.CompletionStatus;

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
                var learner = new Learner
                {
                    Uln = l.Uln,
                    LearnRefNumber = l.LearnRefNumber,
                    DateOfBirth = l.DateOfBirth
                };

                var learningDeliveries = new List<LearningDelivery>();

                foreach(var ld in l.LearningDeliveries)
                {
                    learningDeliveries.Add(
                        new LearningDelivery
                        {
                            ActFamCodeValue = GetActFamCode(l.LearningDelivery.LearnerType),
                            ActualStartDate = ld.StartDate,
                            PlannedEndDate = ld.PlannedEndDate,
                            ActualEndDate = ld.ActualEndDate,

                            StandardCode = ld.StandardCode,
                            ProgrammeType = ld.ProgrammeType,
                            FrameworkCode = ld.FrameworkCode,
                            PathwayCode = ld.PathwayCode,
                            CompletionStatus= TransformCompletionStatus(ld.CompletionStatus),
                            FinancialRecords = GetLearningDeliveryFinancialRecords(ld),
                            FamRecords = TransformFamRecords(ld.LearningDeliveryFams)
                        }
                    );
                }

                learner.LearningDeliveries = learningDeliveries.ToArray();
                return learner;
            }).ToArray();

            Submission.Learners = Learners;

            return this;
        }

        private IlrGenerator.CompletionStatus TransformCompletionStatus(Enums.CompletionStatus status)
        {
            if (status == Enums.CompletionStatus.Completed)
                return CompletionStatus.Completed;
            else if (status == Enums.CompletionStatus.Transferred)
                return CompletionStatus.Transferred;
            else if (status == Enums.CompletionStatus.PlannedBreak)
                return CompletionStatus.PlannedBreak;
           else
                return CompletionStatus.Continuing;
        }

        private short GetActFamCode(LearnerType learnerType)
        {
            short result = 1;
            switch (learnerType)
            {
                case LearnerType.ProgrammeOnlyDas:
                case LearnerType.ProgrammeOnlyDas16To18:
                    result = 1;
                    break;
                default:
                    result = 2;
                    break;
            }
            return result;
        }
        private LearningDeliveryActFamRecord[] TransformFamRecords(Entities.LearningDeliveryFam[] learningDeliveryFams)
        {
            if (learningDeliveryFams == null)
                return null;

            var result = new List<LearningDeliveryActFamRecord>();
            learningDeliveryFams.ToList().ForEach(
                x => result.Add(new LearningDeliveryActFamRecord() {
                    Code = x.FamCode,
                    From = x.StartDate,
                    To = x.EndDate
                }));

            return result.ToArray();
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

            var financialRecords = new List<FinancialRecord>();

            if (episode.Tnp1 != null)
            {
                financialRecords.Add(GetFinancialRecord(1, (int)episode.Tnp1, episode.StartDate));
            }

            if (episode.Tnp2 != null)
            {
                financialRecords.Add(GetFinancialRecord(2, (int)episode.Tnp2, episode.StartDate));
            }

            if (episode.Tnp3 != null)
            {
                financialRecords.Add(GetFinancialRecord(3, (int)episode.Tnp3, episode.StartDate));
            }

            if (episode.Tnp4 != null)
            {
                financialRecords.Add(GetFinancialRecord(4, (int)episode.Tnp4, episode.StartDate));
            }

            return financialRecords.ToArray();
        }

        private FinancialRecord GetFinancialRecord(int code, int amount, DateTime date)
        {
            return new FinancialRecord
            {
                Type = "TNP",
                Code = code,
                Date = date,
                Amount = amount
            };
        }
    }
}