using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.DataCollectors.Entities;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.DataCollectors
{
    public static class DataLockResultCollector
    {
        public static void CollectForPeriod(string period, List<LearnerResults> results, LookupContext lookupContext)
        {
            var dataLockPeriodResults = ReadDataLockResultsFromDeds();

            foreach (var dataLockEntity in dataLockPeriodResults)
            {
                var learner = GetOrCreateLearner(dataLockEntity.Ukprn, dataLockEntity.Uln, results, lookupContext);

                CreateOrUpdateDataLockPeriodResults(learner.DataLockResults, dataLockEntity, period);
            }
        }

        private static DataLockResultEntity[] ReadDataLockResultsFromDeds()
        {
            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                var query = "SELECT "
                            + " pepm.Ukprn, "
                            + " l.Uln, "
                            + " pepm.Period, "
                            + " pepm.CommitmentId, "
                            + " pepm.VersionId AS CommitmentVersion, "
                            + " pepm.TransactionType "
                            + "FROM DataLock.PriceEpisodeMatch pem "
                            + " JOIN DataLock.PriceEpisodePeriodMatch pepm ON pepm.Ukprn = pem.Ukprn "
                            + "  AND pem.PriceEpisodeIdentifier = pepm.PriceEpisodeIdentifier "
                            + "  AND pem.LearnRefNumber = pepm.LearnRefNumber "
                            + "  AND pem.AimSeqNumber = pepm.AimSeqNumber "
                            + " JOIN Valid.Learner l ON l.UKPRN = pepm.Ukprn "
                            + "  AND l.LearnRefNumber = pepm.LearnRefNumber "
                            + "WHERE pem.IsSuccess = 1 "
                            + " AND pepm.Payable = 1";

                return connection.Query<DataLockResultEntity>(query).ToArray();
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

        private static void CreateOrUpdateDataLockPeriodResults(List<DataLockPeriodResults> periodResults, DataLockResultEntity entity, string collectionPeriod)
        {
            var collectionPeriodDate = new DateTime(2000 + int.Parse(collectionPeriod.Substring(3, 2)), int.Parse(collectionPeriod.Substring(0, 2)), 1);
            var collectionPeriodNumber = collectionPeriodDate.GetPeriodNumber();

            var matchPeriod = collectionPeriodDate.AddMonths(entity.Period - collectionPeriodNumber).GetPeriod();

            var existingResults = periodResults.SingleOrDefault(r => r.CalculationPeriod == collectionPeriod && r.MatchPeriod == matchPeriod);

            if (existingResults != null)
            {
                existingResults.Matches.Add(new DataLockResult
                {
                    CommitmentId = entity.CommitmentId,
                    CommitmentVersion = entity.CommitmentVersion,
                    TransactionType = (TransactionType)entity.TransactionType
                });
            }
            else
            {
                periodResults.Add(new DataLockPeriodResults
                {
                    CalculationPeriod = collectionPeriod,
                    MatchPeriod = matchPeriod,
                    Matches = new List<DataLockResult>
                    {
                        new DataLockResult
                        {
                            CommitmentId = entity.CommitmentId,
                            CommitmentVersion = entity.CommitmentVersion,
                            TransactionType = (TransactionType) entity.TransactionType
                        }
                    }
                });
            }
        }
    }
}
