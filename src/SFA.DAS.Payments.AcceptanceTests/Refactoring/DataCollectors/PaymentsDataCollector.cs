using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.DataCollectors.Entities;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.DataCollectors
{
    public static class PaymentsDataCollector
    {
        public static void CollectForPeriod(List<LearnerResults> results, LookupContext lookupContext)
        {
            var paymentsData = ReadPaymentsFromDeds();
            foreach(var data in paymentsData )
            {
                var learner = GetOrCreateLearner(data.Ukprn, data.Uln, results, lookupContext);

                learner.Payments.Add(new PaymentResult
                {
                    Amount = data.Amount,
                    CalculationPeriod=data.CalculationPeriod,
                    DeliveryPeriod = data.DeliveryPeriod,
                    FundingSource = (Enums.FundingSource) data.FundingSource,
                    TransactionType = (Enums.TransactionType)data.TransactionType,
                    ContractType = (Enums.ContractType)data.ContractType
                });
            }
        }

        private static PaymentResultEntity[] ReadPaymentsFromDeds()
        {
            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                var query = @"SELECT p.*,rp.Ukprn,rp.Uln 
                                FROM Payments.Payments p
                                    JOIN PaymentsDue.RequiredPayments rp ON rp.Id = p.RequiredPaymentId ";
                            
                return connection.Query<PaymentResultEntity>(query).ToArray();
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
