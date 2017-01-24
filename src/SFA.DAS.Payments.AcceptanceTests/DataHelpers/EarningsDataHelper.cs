using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ProviderPayments.TestStack.Core;
using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.Entities;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class EarningsDataHelper
    {
        internal static PeriodisedValuesEntity[] GetPeriodisedValuesForUkprnSummary(long ukprn, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = "SELECT " +
                                "Ukprn, " +
                                "SUM(Period_1) AS Period_1, " +
                                "SUM(Period_2) AS Period_2, " +
                                "SUM(Period_3) AS Period_3, " +
                                "SUM(Period_4) AS Period_4, " +
                                "SUM(Period_5) AS Period_5, " +
                                "SUM(Period_6) AS Period_6, " +
                                "SUM(Period_7) AS Period_7, " +
                                "SUM(Period_8) AS Period_8, " +
                                "SUM(Period_9) AS Period_9, " +
                                "SUM(Period_10) AS Period_10, " +
                                "SUM(Period_11) AS Period_11, " +
                                "SUM(Period_12) AS Period_12 " +
                            "FROM Rulebase.AEC_ApprenticeshipPriceEpisode_PeriodisedValues " +
                            "WHERE UKPRN = @ukprn " +
                            "AND AttributeName IN ('PriceEpisodeOnProgPayment', 'PriceEpisodeCompletionPayment', 'PriceEpisodeBalancePayment', " +
                            "'PriceEpisodeFirstEmp1618Pay','PriceEpisodeFirstProv1618Pay','PriceEpisodeSecondEmp1618Pay','PriceEpisodeSecondProv1618Pay') " +
                            " GROUP BY UKPRN";
                return connection.Query<PeriodisedValuesEntity>(query, new { ukprn }).ToArray();
            }
        }

        internal static PeriodisedValuesEntity[] GetPeriodisedValuesForUkprn(long ukprn, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = "SELECT " +
                                "pv.Ukprn, " +
                                "l.ULN, " +
                                "SUM(Period_1) AS Period_1, " +
                                "SUM(Period_2) AS Period_2, " +
                                "SUM(Period_3) AS Period_3, " +
                                "SUM(Period_4) AS Period_4, " +
                                "SUM(Period_5) AS Period_5, " +
                                "SUM(Period_6) AS Period_6, " +
                                "SUM(Period_7) AS Period_7, " +
                                "SUM(Period_8) AS Period_8, " +
                                "SUM(Period_9) AS Period_9, " +
                                "SUM(Period_10) AS Period_10, " +
                                "SUM(Period_11) AS Period_11, " +
                                "SUM(Period_12) AS Period_12 " +
                            "FROM Rulebase.AEC_ApprenticeshipPriceEpisode_PeriodisedValues pv " +
                            " Join Valid.Learner l " +
                            " ON l.UKPRN = pv.UKPRN AND pv.LearnRefNumber = l.LearnRefNumber " +
                            " WHERE pv.UKPRN = @ukprn " +
                            " AND AttributeName IN ('PriceEpisodeOnProgPayment', 'PriceEpisodeCompletionPayment', 'PriceEpisodeBalancePayment'," +
                            " 'PriceEpisodeFirstEmp1618Pay','PriceEpisodeFirstProv1618Pay','PriceEpisodeSecondEmp1618Pay','PriceEpisodeSecondProv1618Pay') " +
                            " Group By pv.UKPRN, l.ULN";
                           
                return connection.Query<PeriodisedValuesEntity>(query, new { ukprn }).ToArray();
            }
        }

        internal static decimal GetBalancingPaymentForUkprn(long ukprn,string periodName, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = $"SELECT {periodName} " +
                            "FROM Rulebase.AEC_ApprenticeshipPriceEpisode_PeriodisedValues " +
                            "WHERE UKPRN = @ukprn AND AttributeName='PriceEpisodeBalancePayment' ";
                           
                return connection.Query<decimal>(query, new { ukprn }).FirstOrDefault();
            }
        }

        internal static void SavePeriodisedValuesForUkprn(long ukprn,
                                                            string learnRefNumber,
                                                            Dictionary<int,decimal> periods,
                                                            string priceEpisodeIdentifier,
                                                            EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                
                var periodValues = new System.Text.StringBuilder();

                foreach (var period in periods.Keys)
                {
                    var periodAmount = periods[period];

                    connection.Execute("INSERT INTO [Rulebase].[AEC_ApprenticeshipPriceEpisode_Period] " +
                                       "(Ukprn,LearnRefNumber,PriceEpisodeIdentifier,Period,PriceEpisodeOnProgPayment) " +
                                       "VALUES " +
                                       "(@ukprn,@learnRefNumber,@PriceEpisodeIdentifier,@Period, @periodAmount)",
                        new { ukprn, learnRefNumber, priceEpisodeIdentifier, period, periodAmount });
                }

                //populate all period values, default to 0 if none found
                for (var i = 1; i <= 12; i++)
                {
                    periodValues.Append($"{periods.Values.ElementAtOrDefault(i - 1)},");
                }

                var columnValues = periodValues.ToString();
                columnValues = columnValues.Remove(columnValues.Length - 1, 1);

                connection.Execute("INSERT INTO [Rulebase].[AEC_ApprenticeshipPriceEpisode_PeriodisedValues] " +
                                   "(Ukprn,LearnRefNumber,PriceEpisodeIdentifier,AttributeName, " +
                                   "Period_1,Period_2,Period_3,Period_4,Period_5,Period_6,Period_7,Period_8,Period_9,Period_10,Period_11,Period_12)" +
                                   "VALUES " +
                                   $"(@ukprn,@learnRefNumber,@PriceEpisodeIdentifier, 'PriceEpisodeOnProgPayment', {columnValues})",
                    new { ukprn, learnRefNumber, priceEpisodeIdentifier });
            }
        }

        
        internal static void SaveLearningDeliveryValuesForUkprn(long ukprn, 
                                                                string learnRefNumber,
                                                                LearningDelivery learningDelivery,
                                                                EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                foreach (var priceEpisode in learningDelivery.PriceEpisodes)
                {

                    connection.Execute("INSERT INTO [Rulebase].[AEC_ApprenticeshipPriceEpisode] " +
                                           "(Ukprn,LearnRefNumber,PriceEpisodeAimSeqNumber,PriceEpisodeIdentifier,PriceEpisodeTotalTNPPrice," +
                                           " EpisodeEffectiveTNPStartDate,PriceEpisodePlannedEndDate," +
                                           "PriceEpisodeInstalmentValue,PriceEpisodeCompletionElement," +
                                           "TNP1,TNP2,TNP3,TNP4) " +
                                           "VALUES " +
                                           "(@ukprn,@learnRefNumber, 1, " +
                                           " @priceEpisodeIdentifier," +
                                           " @priceEpisodeTotalTNPPrice," +
                                           " @episodeStartDate," +
                                           " @episodeEndDate," +
                                           " @monthlyPayment," +
                                           " @completionPayment," +
                                           " @tnp1," +
                                           " @tnp2," +
                                           " @tnp3," +
                                           " @tnp4)",
                            new
                            {
                                ukprn = ukprn,
                                learnRefNumber = learnRefNumber,
                                priceEpisodeIdentifier = priceEpisode.Id,
                                priceEpisodeTotalTNPPrice = priceEpisode.TotalPrice,
                                episodeStartDate = priceEpisode.StartDate,
                                episodeEndDate = priceEpisode.EndDate ?? learningDelivery.ActualEndDate ?? learningDelivery.PlannedEndDate,
                                monthlyPayment = priceEpisode.MonthlyPayment,
                                completionPayment = priceEpisode.CompletionPayment,
                                tnp1 = priceEpisode.Tnp1,
                                tnp2 = priceEpisode.Tnp2,
                                tnp3 = priceEpisode.Tnp3,
                                tnp4 = priceEpisode.Tnp4,
                            });

                }
            }
        }

        internal static void SaveEarnedAmount(long ukprn,
                                            long commitmentId,
                                            long accountId,
                                            Learner learner,
                                            string collectionPeriodName,
                                            int collectionPeriodMonth,
                                            int collectionPeriodYear,
                                            int transactionType,
                                            decimal amountDue,
                                            EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {

                var accountVersionId = IdentifierGenerator.GenerateIdentifier(8, false);
                var commitmentVersionId = IdentifierGenerator.GenerateIdentifier(8, false);

                connection.Execute("INSERT INTO PaymentsDue.RequiredPayments (" +
                                       "CommitmentId," +
                                       "CommitmentVersionId," +
                                       "AccountId," +
                                       "AccountVersionId," +
                                       "uln," +
                                       "LearnRefNumber," +
                                       "AimSeqNumber," +
                                       "Ukprn," +
                                       "DeliveryMonth," +
                                       "DeliveryYear," +
                                       "CollectionPeriodName," +
                                       "CollectionPeriodMonth," +
                                       "CollectionPeriodYear," +
                                       "TransactionType," +
                                       "AmountDue," +
                                       "StandardCode," +
                                       "ProgrammeType," +
                                       "FrameworkCode," +
                                       "PathwayCode" +
                                   ") VALUES (" +
                                       "@commitmentId," +
                                       "@commitmentVersionId," +
                                       "@accountId," +
                                       "@accountVersionId," +
                                       "@uln," +
                                       "@learnRefNumber," +
                                       "1," +
                                       "@ukprn," +
                                       "@collectionPeriodMonth," +
                                       "@collectionPeriodYear," +
                                       "@collectionPeriodName," +
                                       "@collectionPeriodMonth," +
                                       "@collectionPeriodYear," +
                                       "@transactionType," +
                                       "@amountDue," +
                                       "@standardCode," +
                                       "@programmeType," +
                                       "@frameworkCode," +
                                       "@pathwayCode" +
                                   ")",
                    new
                    {
                        commitmentId,
                        commitmentVersionId,
                        accountId,
                        accountVersionId,
                        uln = learner.Uln,
                        learnRefNumber = learner.LearnRefNumber,
                        ukprn,
                        collectionPeriodName,
                        collectionPeriodMonth,
                        collectionPeriodYear,
                        transactionType,
                        amountDue,
                        standardCode = learner.LearningDelivery.StandardCode == 0 ? (long?)null : learner.LearningDelivery.StandardCode,
                        programmeType = learner.LearningDelivery.ProgrammeType == 0 ? (int?)null : learner.LearningDelivery.ProgrammeType,
                        frameworkCode = learner.LearningDelivery.FrameworkCode == 0 ? (int?)null : learner.LearningDelivery.FrameworkCode,
                        pathwayCode = learner.LearningDelivery.PathwayCode == 0 ? (int?)null : learner.LearningDelivery.PathwayCode
                    });
            }
        }
    }
}
