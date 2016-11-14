using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;

namespace SFA.DAS.Payments.AcceptanceTests.Contexts
{
    public class EarningAndPaymentsContext
    {
        public ReferenceDataContext ReferenceDataContext { get; set; }
        public Provider[] Providers { get; private set; }

        public EarningAndPaymentsContext(ReferenceDataContext referenceDataContext)
        {
            ReferenceDataContext = referenceDataContext;
        }

        public void AddProvider(Provider provider)
        {
            var providers = Providers?.ToList() ?? new List<Provider>();
            providers.Add(provider);

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

        public DateTime GetProviderIlrStartDate(long ukprn)
        {
            var provider = Providers.Single(p => p.Ukprn == ukprn);

            return provider.IlrStartDate;
        }

        public DateTime GetProviderIlrEndDate(long ukprn)
        {
            var provider = Providers.Single(p => p.Ukprn == ukprn);

            return provider.IlrEndDate;
        }

        public Learner[] GetProviderIlrLearners(long ukprn)
        {
            var provider = Providers.Single(p => p.Ukprn == ukprn);

            return provider.Learners;
        }

        public void SetProviderIlrLearners(long ukprn, Learner[] learners)
        {
            var provider = Providers.Single(p => p.Ukprn == ukprn);

            provider.Learners = learners;
        }

        public Dictionary<string, decimal> GetProviderEarnedByPeriod(long ukprn)
        {
            var provider = Providers.Single(p => p.Ukprn == ukprn);

            return provider.EarnedByPeriod;
        }

        public void SetProviderEarnedByPeriod(long ukprn, Dictionary<string, decimal> earned)
        {
            var provider = Providers.Single(p => p.Ukprn == ukprn);

            provider.EarnedByPeriod = earned;
        }
    }
}
