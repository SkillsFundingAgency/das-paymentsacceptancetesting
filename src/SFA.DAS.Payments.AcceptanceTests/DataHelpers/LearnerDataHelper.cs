using ProviderPayments.TestStack.Core;
using System;
using System.Data.SqlClient;
using Dapper;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.Entities;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class LearnerDataHelper
    {
        internal static void SaveLearner(long ukprn, 
                                        long uln, 
                                        string learnRefNumber,
                                        EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {

                connection.Execute("INSERT INTO [Valid].[Learner]" +
                "([UKPRN],[LearnRefNumber],[ULN],[Ethnicity],[Sex],[LLDDHealthProb])" +
                "VALUES (@ukprn,@learnRefNumber,@uln,98,'M',2)",
                    new { ukprn, learnRefNumber , uln });

            }

        }

        internal static void SaveLearningDelivery(long ukprn, 
                                                string learnRefNumber,
                                               LearningDelivery learningDelivery,
                                                EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {

                connection.Execute("INSERT INTO [Valid].[LearningDelivery]" + 
                "  ([UKPRN],[LearnRefNumber],[LearnAimRef],[AimType],[AimSeqNumber],[LearnStartDate],[LearnPlanEndDate]," +
                "[FundModel],[ProgType],[StdCode],FWorkCode,PWayCode)" +
                " VALUES (@ukprn,@learnRefNumber,'ZPROG001',1,1,@StartDate,@PlannedEndDate,36,@ProgrammeType,@StandardCode,@FrameworkCode,@PathwayCode)",
                    new { ukprn,
                        learnRefNumber,
                        learningDelivery.StartDate,
                        learningDelivery.PlannedEndDate,
                        ProgrammeType = learningDelivery.ProgrammeType > 0 ? learningDelivery.ProgrammeType : (int?)null,
                        StandardCode = learningDelivery.StandardCode > 0 ? learningDelivery.StandardCode : (long?)null,
                        FrameworkCode = learningDelivery.FrameworkCode > 0 ? learningDelivery.FrameworkCode : (int?)null,
                        PathwayCode = learningDelivery.PathwayCode > 0 ? learningDelivery.PathwayCode : (int?)null
                    });

            }

           
        }

        internal static void SaveLearningDeliveryFAM(long ukprn,
                                                  string learnRefNumber,
                                                  DateTime startDate,
                                                  DateTime? endDate,
                                                EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {

                connection.Execute("INSERT INTO [Valid].[LearningDeliveryFAM]" +
                            " ([UKPRN],[LearnRefNumber],[AimSeqNumber],[LearnDelFAMType],[LearnDelFAMCode],[LearnDelFAMDateFrom],[LearnDelFAMDateTo])" +
                             " VALUES (@ukprn,@learnRefNumber,1,'ACT',1,@startDate,@endDate)",
                    new { ukprn,learnRefNumber,startDate,endDate});

            }
        }


        internal static void SaveTrailblazerApprenticeshipFinancialRecord(long ukprn,
                                                                   int tbFinCode,
                                                                    string learnRefNumber,
                                                                   decimal tbFinAmount,
                                                                   EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {

                connection.Execute("INSERT INTO [Valid].[TrailblazerApprenticeshipFinancialRecord]" +
                            " ([UKPRN],[LearnRefNumber],[AimSeqNumber],[TBFinType],[TBFinCode],[TBFinAmount])" +
                            " VALUES (@ukprn,@learnRefNumber,1,'TNP',@tbFinCode,@tbFinAmount)",
                    new { ukprn,learnRefNumber,tbFinCode,tbFinAmount});

            }
        }

        internal static void SaveLearningProvider(long ukprn, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {

                connection.Execute("INSERT INTO [Valid].[LearningProvider]" +
                            " ([UKPRN])" +
                            " VALUES (@ukprn)",
                    new { ukprn});

            }
        }

        
        internal static void SaveFileDetails(long ukprn, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var fileName = Guid.NewGuid().ToString();
                connection.Execute("INSERT INTO [dbo].[FileDetails]" +
                            " ([UKPRN],[FileName],[SubmittedTime],[Success])" +
                " VALUES (@ukprn,@fileName,getDate(),1)",
                    new { ukprn,fileName });

            }
        }

        internal static AEC_ApprenticeshipPriceEpisode GetAELearningDelivery(long ukprn,
                                                                string learnRefNumber,
                                                                 DateTime learnStartDate,
                                                                DateTime learnPlanEndDate,
                                                                EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = "SELECT * FROM [Rulebase].[AEC_ApprenticeshipPriceEpisode] WHERE UKPRN = @ukprn AND LearnRefNumber = @learnRefNumber AND EpisodeStartDate= @learnStartDate AND PriceEpisodePlannedEndDate=@learnPlanEndDate ";
                return connection.QuerySingleOrDefault<AEC_ApprenticeshipPriceEpisode>(query, new { ukprn, learnRefNumber, learnStartDate,learnPlanEndDate});
            }
        }

    }
}
