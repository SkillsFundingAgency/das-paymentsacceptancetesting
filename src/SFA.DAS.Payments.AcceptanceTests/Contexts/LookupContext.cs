using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.Contexts
{
    public class LookupContext
    {
        private const long UkprnSeed = 10000;
        private const long UlnSeed = 11000;

        public LookupContext()
        {
            Providers = new Dictionary<string, long>();
            Learners = new Dictionary<string, long>();
        }

        public Dictionary<string, long> Providers { get; }
        public long AddOrGetUkprn(string providerId)
        {
            var ukprn = GetUkprn(providerId);
            if (ukprn == 0)
            {
                ukprn = UkprnSeed + Providers.Count;
                Providers.Add(providerId.ToUpper(), ukprn);
            }
            return ukprn;
        }
        public long GetUkprn(string providerId)
        {
            if (!Providers.ContainsKey(providerId.ToUpper()))
            {
                return 0;
            }
            return Providers[providerId.ToUpper()];
        }
        public string GetProviderId(long ukprn)
        {
            foreach(var providerId in Providers.Keys)
            {
                if(Providers[providerId] == ukprn)
                {
                    return providerId;
                }
            }
            return null;
        }


        public Dictionary<string, long> Learners { get; }
        public long AddOrGetUln(string learnerId)
        {
            var uln = GetUln(learnerId);
            if (uln == 0)
            {
                uln = UlnSeed + Learners.Count;
                Learners.Add(learnerId.ToUpper(), uln);
            }
            return uln;
        }
        public long GetUln(string learnerId)
        {
            if (!Learners.ContainsKey(learnerId.ToUpper()))
            {
                return 0;
            }
            return Learners[learnerId.ToUpper()];
        }
        public string GetLearnerId(long uln)
        {
            foreach (var learnerId in Learners.Keys)
            {
                if (Learners[learnerId] == uln)
                {
                    return learnerId;
                }
            }
            return null;
        }
    }
}
