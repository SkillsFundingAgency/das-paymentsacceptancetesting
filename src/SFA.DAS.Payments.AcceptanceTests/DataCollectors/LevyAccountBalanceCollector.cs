using Dapper;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.DataCollectors.Entities;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Payments.AcceptanceTests.DataCollectors
{
    public static class LevyAccountBalanceCollector
    {
        public static void CollectForPeriod(string period, List<LearnerResults> results, LookupContext lookupContext)
        {
            var collectionPeriodDate = new DateTime(2000 + int.Parse(period.Substring(3, 2)), int.Parse(period.Substring(0, 2)), 1);

            var balance = ReadAccountBalanceFromDeds(collectionPeriodDate.Month,collectionPeriodDate.Year);

            var learner = GetOrCreateLearner(lookupContext.Providers.First().Value, lookupContext.Learners.First().Value, results, lookupContext);

            learner.LevyAccountBalanceResults.Add(new LevyAccountBalanceResult
            {
                Amount = balance?? 0,
                CalculationPeriod = period,
                DeliveryPeriod = $"{collectionPeriodDate.Month:00}/{(collectionPeriodDate.Year - 2000):00}"
            });

        }

        internal static decimal? ReadAccountBalanceFromDeds(int calculationPeriodMonth, int calculationPeriodYear)
        {
            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                var command = "SELECT Sum(p.Amount) " + 
                            " FROM Payments.Payments p " + 
                            " JOIN PaymentsDue.RequiredPayments rp " +
                                "  ON rp.Id = p.RequiredPaymentId " +
                            " WHERE p.FundingSource = 1 " +
                            " AND p.CollectionPeriodMonth = @calculationPeriodMonth AND p.CollectionPeriodYear = @calculationPeriodYear";
                return connection.Query<decimal?>(command,new { calculationPeriodMonth, calculationPeriodYear }).Single();
            }
        }
        private static LearnerResults GetOrCreateLearner(long ukprn, long uln, List<LearnerResults> results, LookupContext lookupContext)
        {
            var providerId = lookupContext.GetProviderId(ukprn);
            var learnerId = lookupContext.GetLearnerId(uln);
            var learner = results.SingleOrDefault(l => l.ProviderId == providerId && l.LearnerId == learnerId);
            if (learner == null)
            {
                learner = new LearnerResults
                {
                    ProviderId = providerId,
                    LearnerId = learnerId
                };
                results.Add(learner);
            }
            return learner;
        }
       

    }
}
