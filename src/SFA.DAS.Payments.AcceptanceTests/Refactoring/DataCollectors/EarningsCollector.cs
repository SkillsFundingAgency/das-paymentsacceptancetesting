using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.DataCollectors.Entities;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.DataCollectors
{
    public static class EarningsCollector
    {
        public static void CollectForPeriod(string period, List<LearnerResults> results, LookupContext lookupContext)
        {
            var periodisedValues = ReadEarningsFromDeds();
            foreach (var periodisedValue in periodisedValues)
            {
                var learner = GetOrCreateLearner(periodisedValue.Ukprn, periodisedValue.Uln, results, lookupContext);
                learner.Earnings.Add(CreateEarningResultForPeriod(1, periodisedValue.Period1, period));
                learner.Earnings.Add(CreateEarningResultForPeriod(2, periodisedValue.Period2, period));
                learner.Earnings.Add(CreateEarningResultForPeriod(3, periodisedValue.Period3, period));
                learner.Earnings.Add(CreateEarningResultForPeriod(4, periodisedValue.Period4, period));
                learner.Earnings.Add(CreateEarningResultForPeriod(5, periodisedValue.Period5, period));
                learner.Earnings.Add(CreateEarningResultForPeriod(6, periodisedValue.Period6, period));
                learner.Earnings.Add(CreateEarningResultForPeriod(7, periodisedValue.Period7, period));
                learner.Earnings.Add(CreateEarningResultForPeriod(8, periodisedValue.Period8, period));
                learner.Earnings.Add(CreateEarningResultForPeriod(9, periodisedValue.Period9, period));
                learner.Earnings.Add(CreateEarningResultForPeriod(10, periodisedValue.Period10, period));
                learner.Earnings.Add(CreateEarningResultForPeriod(11, periodisedValue.Period11, period));
                learner.Earnings.Add(CreateEarningResultForPeriod(12, periodisedValue.Period12, period));
            }
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
        private static EarningsResult CreateEarningResultForPeriod(int periodNumber, decimal value, string collectionPeriod)
        {
            var collectionPeriodDate = new DateTime(int.Parse(collectionPeriod.Substring(3, 2)), int.Parse(collectionPeriod.Substring(0, 2)), 1);
            var collectionPeriodNumber = collectionPeriodDate.GetPeriodNumber();
            var deliveryPeriodDate = collectionPeriodDate.AddMonths(periodNumber - collectionPeriodNumber);
            
            return new EarningsResult
            {
                CalculationPeriod = collectionPeriod,
                DeliveryPeriod = deliveryPeriodDate.ToString("MM/yy"),
                Value = value
            };
        }
    }
}
