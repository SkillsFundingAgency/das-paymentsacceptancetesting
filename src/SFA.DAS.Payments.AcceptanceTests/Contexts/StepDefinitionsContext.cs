using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.Entities;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.Contexts
{
    public class StepDefinitionsContext
    {

        public ReferenceDataContext ReferenceDataContext { get; set; }
        public Provider[] Providers { get; private set; }

        public StepDefinitionsContext(ReferenceDataContext referenceDataContext)
        {
            ReferenceDataContext = referenceDataContext;
        }

       
        public void AddProvider(string name,long? ukprn = null)
        {
            var providers = Providers?.ToList() ?? new List<Provider>();

            if (providers.Any(p => p.Name == name))
            {
                return;
            }

            providers.Add(new Provider
            {
                Name = name,
                Ukprn = ukprn.HasValue ? ukprn.Value : long.Parse(IdentifierGenerator.GenerateIdentifier(8, false))
            });

            Providers = providers.ToArray();
        }

        public void SetDefaultProvider()
        {
            var provider = new Provider
            {
                Name = "provider",
                Ukprn = long.Parse(IdentifierGenerator.GenerateIdentifier(8, false))
            };

            Providers = new[] { provider };
        }

        public DateTime GetIlrStartDate()
        {
            return Providers.Min(p => p.IlrStartDate);
        }

        public DateTime GetIlrEndDate()
        {
            return Providers.Max(p => p.IlrEndDate);
        }

        public void AddProviderLearner(string name, Learner learner)
        {
            var provider = Providers.Single(p => p.Name == name);

            AddProviderLearner(provider, learner);
        }

        public void AddProviderLearner(Provider provider, Learner learner)
        {
            var learners = provider.Learners?.ToList() ?? new List<Learner>();
            learners.Add(learner);

            provider.Learners = learners.ToArray();
        }

        public Dictionary<string, decimal> GetProviderEarnedByPeriod(long ukprn, long? uln = null)
        {
            var provider = Providers.Single(p => p.Ukprn == ukprn);
            if (uln == null)
                return provider.EarnedByPeriod;
            else
                return provider.EarnedByPeriodByUln[uln.Value];
        }

        public Provider GetDefaultProvider()
        {
            if (Providers == null || !Providers.Any())
                throw new NullReferenceException("There are no providers set");

            return Providers[0];
        }

        public Learner CreateLearner(decimal agreedPrice,DateTime startDate,DateTime endDate, DateTime? actualEndDate = null)
        {
            var learner = new Learner
            {
                Name = string.Empty,
                Uln = long.Parse(IdentifierGenerator.GenerateIdentifier(10, false)),
                LearnRefNumber= IdentifierGenerator.GenerateIdentifier(10, false),
                LearningDelivery = new LearningDelivery
                {
                    LearnerType = LearnerType.ProgrammeOnlyDas,
                    StartDate = startDate,
                    PlannedEndDate = endDate,
                    ActualEndDate = actualEndDate,
                    CompletionStatus = CompletionStatus.InProgress,
                    PriceEpisodes = new []
                    {
                        new PriceEpisode
                        {
                            Id = IdentifierGenerator.GenerateIdentifier(25),
                            StartDate = startDate,
                            EndDate = actualEndDate.HasValue ? actualEndDate : endDate,
                            TotalPrice = agreedPrice,
                            Tnp1 = agreedPrice * 0.8m,
                            Tnp2 = agreedPrice * 0.2m
                        }
                    }
                }
            };

            return learner;
        }
    }
}
