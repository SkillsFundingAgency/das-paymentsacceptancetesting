//using System;
//using System.Linq;
//using SFA.DAS.Payments.AcceptanceTests.Assertions.PaymentsAndEarningsRules;
//using SFA.DAS.Payments.AcceptanceTests.Contexts;
//using SFA.DAS.Payments.AcceptanceTests.Assertions.LevyAccountBalanceRules;
//using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
//using System.Collections.Generic;
//using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

//namespace SFA.DAS.Payments.AcceptanceTests.Assertions
//{
//    public static class LevyAccountBalanceAssertions
//    {
//        private static readonly LevyAccountBalanceRule[] Rules =
//        {
//            new LevyAccountBalanceRule()
//        };

//        public static void AssertLevyAccountBalanceResults(SubmissionContext submissionContext, List<PeriodValue> periodBalances)
//        {
//            if (TestEnvironment.ValidateSpecsOnly)
//            {
//                return;
//            }
//            var balances = submissionContext.SubmissionResults.Where(x => x.LevyAccountBalanceResults.Count() > 0).SingleOrDefault();
//            if (balances == null)
//            {
//                return;
//            }
//            foreach (var period in periodBalances)
//            {
//                VaidateAccountBalance(balances, periodBalances);

//            }

//        }

//        private static void VaidateAccountBalance(LearnerResults results, List<PeriodValue> periodBalances)
//        {
//            foreach (var breakdown in periodBalances)
//            {
//                foreach (var rule in Rules)
//                {
//                    rule.AssertBreakdown(breakdown, results);
//                }
//            }
//        }
//    }
//}
