using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ProviderPayments.TestStack.Core;
using System.Collections.Generic;
using System;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using IlrBuilder = SFA.DAS.Payments.AcceptanceTests.Builders.IlrBuilder;
using SFA.DAS.Payments.AcceptanceTests.Entities;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class EarningsDataHelper
    {
        internal static PeriodisedValuesEntity[] GetPeriodisedValuesForUkprn(long ukprn, EnvironmentVariables environmentVariables)
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
                            "GROUP BY UKPRN";
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
                                                            Dictionary<string,decimal> periods,
                                                            DateTime episodeStartDate,
                                                            string priceEpisodeIdentifier,
                                                            EnvironmentVariables environmentVariables)
        {

            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                foreach (var period in periods.Keys)
                {
                    var periodValue = periods[period];
                    connection.Execute("INSERT INTO [Rulebase].[AEC_ApprenticeshipPriceEpisode_PeriodisedValues] " +
                                       $"(Ukprn,LearnRefNumber,AimSeqNumber,EpisodeStartDate,PriceEpisodeIdentifier,AttributeName,{period}) " +
                                       "VALUES " +
                                       "(@ukprn, '1',1,@episodeStartDate,@PriceEpisodeIdentifier, 'PriceEpisodeOnProgPayment', @periodValue)",
                        new { ukprn, periodValue,episodeStartDate,priceEpisodeIdentifier });
                }
                }
        
        }

        
        internal static void SaveLearningDeliveryValuesForUkprn(long ukprn, 
                                                                ApprenticeshipPriceEpisode priceEpisode,
                                                                EnvironmentVariables environmentVariables)
        {

           

            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
               
                connection.Execute("INSERT INTO [Rulebase].[AEC_ApprenticeshipPriceEpisode] " +
                                       "(Ukprn,LearnRefNumber,AimSeqNumber,PriceEpisodeTotalTNPPrice," + 
                                       " EpisodeStartDate,PriceEpisodePlannedEndDate," +
                                       "PriceEpisodeInstalmentValue,PriceEpisodeCompletionElement) " +
                                       "VALUES " +
                                       "(@ukprn,'1', 1, " +
                                       " @PriceEpisodeTotalTNPPrice," +
                                       " @EpisodeStartDate," +
                                       " @PriceEpisodePlannedEndDate," +
                                       " @PriceEpisodeInstalmentValue," +
                                       " @PriceEpisodeCompletionElement," +
                                       " @TNP1," +
                                       " @TNP2," +
                                       " @TNP3,"+
                                       " @TNP4)",
                        new { ukprn,
                                priceEpisode.PriceEpisodeTotalTNPPrice,
                                priceEpisode.EpisodeStartDate,
                                priceEpisode.PriceEpisodePlannedEndDate,
                                priceEpisode.PriceEpisodeInstalmentValue,
                                priceEpisode.PriceEpisodeCompletionElement,
                                priceEpisode.TNP1,
                                priceEpisode.TNP2,
                                priceEpisode.TNP3,
                                priceEpisode.TNP4,
                        });
                
            }

        }

        internal static void SaveEarnedAmount(long ukprn,
                                            long commitmentId,
                                            long accountId,
                                            long uln,
                                            string collectionPeriodName,
                                            int collectionPeriodMonth,
                                            int collectionPeriodYear,
                                            int transactionType,
                                            decimal amountDue,
                                            EnvironmentVariables environmentVariables)
        {

          

            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {

                var accountVersionId = IdentifierGenerator.GenerateIdentifier(8, false).ToString();
                var commitmentVersionId = IdentifierGenerator.GenerateIdentifier(8, false).ToString();

                connection.Execute("INSERT INTO PaymentsDue.RequiredPayments" +
                                       "(CommitmentId,CommitmentVersionId" +
                                       ",AccountId,AccountVersionId,uln,LearnRefNumber" +
                                       ",AimSeqNumber,Ukprn,DeliveryMonth" +
                                       ",DeliveryYear,CollectionPeriodName" +
                                       ",CollectionPeriodMonth,CollectionPeriodYear" +
                                       ",TransactionType,AmountDue) " +
                                       "VALUES " +
                                        "(@commitmentId,@commitmentVersionId" +
                                       ",@accountId,@accountVersionId,@uln,'1'" +
                                       ",1,@ukprn,@collectionPeriodMonth" +
                                       ",@collectionPeriodYear,@collectionPeriodName" +
                                       ",@collectionPeriodMonth,@collectionPeriodYear" +
                                       ",@transactionType,@amountDue) ", 
                        new {
                            commitmentId,
                            commitmentVersionId,
                            accountId,
                            accountVersionId,
                            uln,
                            ukprn,
                            collectionPeriodName,
                            collectionPeriodMonth,
                            collectionPeriodYear,
                            transactionType,
                            amountDue
                        });

            
            }

        }

    }
}
