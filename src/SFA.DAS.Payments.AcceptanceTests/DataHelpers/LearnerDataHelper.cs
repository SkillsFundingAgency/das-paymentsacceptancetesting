using ProviderPayments.TestStack.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.Payments.AcceptanceTests.Builders;

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
                    new { ukprn, learnRefNumber, uln});

            }

        }

        internal static void SaveLearningDelivery(long ukprn, 
                                                string learnRefNumber,
                                                int progType, 
                                                DateTime learnStartDate,
                                                DateTime learnPlanEndDate, 
                                                EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {

                connection.Execute("INSERT INTO [Valid].[LearningDelivery]" + 
                "  ([UKPRN],[LearnRefNumber],[LearnAimRef],[AimType],[AimSeqNumber],[LearnStartDate],[LearnPlanEndDate]," +
                "[FundModel],[ProgType],[StdCode])" +
                " VALUES (@ukprn,@learnRefNumber,'ZPROG001',1,1,@learnStartDate,@learnPlanEndDate,36,@progType,@stdCode)",
                    new { ukprn, learnRefNumber, learnStartDate,learnPlanEndDate,progType, stdCode = IlrBuilder.Defaults.StandardCode });

            }

           
        }

        internal static void SaveLearningDeliveryFAM(long ukprn,
                                               string learnRefNumber,
                                               string famType,
                                               int famCode,
                                               EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {

                connection.Execute("INSERT INTO [Valid].[LearningDeliveryFAM]" +
                            " ([UKPRN],[LearnRefNumber],[AimSeqNumber],[LearnDelFAMType],[LearnDelFAMCode])" +
                " VALUES (@ukprn,@learnRefNumber,1,@famType,@famCode)",
                    new { ukprn, learnRefNumber, famType,famCode});

            }
        }


        internal static void SaveTrailblazerApprenticeshipFinancialRecord(long ukprn,
                                                                   string learnRefNumber,
                                                                   string tbFinType,
                                                                   int tbFinCode,
                                                                   decimal tbFinAmount,
                                                                   EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {

                connection.Execute("INSERT INTO [Valid].[TrailblazerApprenticeshipFinancialRecord]" +
                            " ([UKPRN],[LearnRefNumber],[AimSeqNumber],[TBFinType],[TBFinCode],[TBFinAmount])" +
                " VALUES (@ukprn,@learnRefNumber,1,@tbFinType,@tbFinCode,@tbFinAmount)",
                    new { ukprn, learnRefNumber, tbFinType,tbFinCode,tbFinAmount});

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
    }
}
