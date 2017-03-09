using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.DataCollectors.Entities;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.DataCollectors
{
    public static class EarningsCollector
    {
        public static void CollectForPeriod(string period, List<LearnerResults> results)
        {
            var periodisedValues = ReadEarningsFromDeds();
        }

        private static PeriodisedValuesEntity[] ReadEarningsFromDeds()
        {
            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                var command = "SELECT "
                            + "    ldpv.Ukprn, "
                            + "	l.ULN, "
                            + "    SUM(Period_1) AS Period1, "
                            + "    SUM(Period_2)AS Period2, "
                            + "    SUM(Period_3) AS Period3, "
                            + "    SUM(Period_4) AS Period4, "
                            + "    SUM(Period_5) AS Period5, "
                            + "    SUM(Period_6) AS Period6, "
                            + "    SUM(Period_7) AS Period7, "
                            + "    SUM(Period_8) AS Period8, "
                            + "    SUM(Period_9) AS Period9, "
                            + "    SUM(Period_10) AS Period10, "
                            + "    SUM(Period_11) AS Period11, "
                            + "    SUM(Period_12) AS Period12 "
                            + "FROM Rulebase.AEC_LearningDelivery_PeriodisedValues ldpv "
                            + "    JOIN Rulebase.AEC_LearningDelivery ld "
                            + "        ON ld.Ukprn = ldpv.Ukprn "
                            + "        AND ld.LearnRefNumber = ldpv.LearnRefNumber "
                            + "        AND ld.AimSeqNumber = ldpv.AimSeqNumber "
                            + "    JOIN Valid.Learner l "
                            + "        ON ld.Ukprn = l.UKPRN "
                            + "        AND ld.LearnRefNumber = l.LearnRefNumber "
                            + "WHERE ldpv.AttributeName IN('DisadvFirstPayment', 'DisadvSecondPayment', 'LDApplic1618FrameworkUpliftBalancingPayment', 'LDApplic1618FrameworkUpliftCompletionPayment', 'LDApplic1618FrameworkUpliftOnProgPayment', 'LearnDelFirstEmp1618Pay', 'LearnDelFirstProv1618Pay', 'LearnDelSecondEmp1618Pay', 'LearnDelSecondProv1618Pay', 'LearnSuppFundCash', 'MathEngBalPayment', 'MathEngOnProgPayment', 'ProgrammeAimBalPayment', 'ProgrammeAimCompletionPayment', 'ProgrammeAimOnProgPayment') "
                            + "GROUP BY ldpv.UKPRN, l.ULN";
                return connection.Query<PeriodisedValuesEntity>(command).ToArray();
            }
        }
    }
}
