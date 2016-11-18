using ProviderPayments.TestStack.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.Payments.AcceptanceTests.Builders;
using SFA.DAS.Payments.AcceptanceTests.Entities;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class LearnerDataHelper
    {
        internal static void SaveLearner(long ukprn, 
                                        long uln, 
                                        EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {

                connection.Execute("INSERT INTO [Valid].[Learner]" +
                "([UKPRN],[LearnRefNumber],[ULN],[Ethnicity],[Sex],[LLDDHealthProb])" +
                "VALUES (@ukprn,'1',@uln,98,'M',2)",
                    new { ukprn, uln});

            }

        }

        internal static void SaveLearningDelivery(long ukprn, 
                                                DateTime learnStartDate,
                                                DateTime learnPlanEndDate, 
                                                EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {

                connection.Execute("INSERT INTO [Valid].[LearningDelivery]" + 
                "  ([UKPRN],[LearnRefNumber],[LearnAimRef],[AimType],[AimSeqNumber],[LearnStartDate],[LearnPlanEndDate]," +
                "[FundModel],[ProgType],[StdCode])" +
                " VALUES (@ukprn,'1','ZPROG001',1,1,@learnStartDate,@learnPlanEndDate,36,25,98765)",
                    new { ukprn,  learnStartDate,learnPlanEndDate });

            }

           
        }

        internal static void SaveLearningDeliveryFAM(long ukprn,
                                               EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {

                connection.Execute("INSERT INTO [Valid].[LearningDeliveryFAM]" +
                            " ([UKPRN],[LearnRefNumber],[AimSeqNumber],[LearnDelFAMType],[LearnDelFAMCode])" +
                " VALUES (@ukprn,'1',1,'ACT',1)",
                    new { ukprn});

            }
        }


        internal static void SaveTrailblazerApprenticeshipFinancialRecord(long ukprn,
                                                                   int tbFinCode,
                                                                   decimal tbFinAmount,
                                                                   EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {

                connection.Execute("INSERT INTO [Valid].[TrailblazerApprenticeshipFinancialRecord]" +
                            " ([UKPRN],[LearnRefNumber],[AimSeqNumber],[TBFinType],[TBFinCode],[TBFinAmount])" +
                " VALUES (@ukprn,'1',1,'TNP',@tbFinCode,@tbFinAmount)",
                    new { ukprn,tbFinCode,tbFinAmount});

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

        internal static AE_LearningDelivery GetAELearningDelivery(long ukprn,
                                                                long uln,
                                                                 DateTime learnStartDate,
                                                                DateTime learnPlanEndDate,
                                                                EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = "SELECT * FROM [Rulebase].[AE_LearningDelivery] WHERE UKPRN = @ukprn AND ULN = @uln AND LearnStartDate= @learnStartDate AND LearnPlanEndDate =@learnPlanEndDate ";
                return connection.QuerySingleOrDefault<AE_LearningDelivery>(query, new { ukprn, uln, learnStartDate,learnPlanEndDate});
            }
        }
    }
}
