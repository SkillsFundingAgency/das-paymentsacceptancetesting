using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.DataCollectors.Entities;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.DataCollectors
{
    public static class PaymentsDataCollector
    {
        public static void CollectForPeriod(List<LearnerResults> results, LookupContext lookupContext)
        {
            var paymentsData = ReadPaymentsFromDeds();
            foreach (var data in paymentsData)
            {
                var learner = GetOrCreateLearner(data.Ukprn, data.Uln, results, lookupContext);

                learner.Payments.Add(new PaymentResult
                {
                    EmployerAccountId = int.Parse(data.AccountId),
                    Amount = data.Amount,
                    CalculationPeriod = $"{data.CollectionPeriodMonth:00}/{(data.CollectionPeriodYear - 2000):00}",
                    DeliveryPeriod = $"{data.DeliveryMonth:00}/{(data.DeliveryYear - 2000):00}",
                    FundingSource = (FundingSource)data.FundingSource,
                    TransactionType = (TransactionType)data.TransactionType,
                    ContractType = (ContractType)data.ApprenticeshipContractType
                });
            }
        }

        private static PaymentResultEntity[] ReadPaymentsFromDeds()
        {
            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                var query = @"SELECT rp.Ukprn, rp.Uln, p.DeliveryMonth, p.DeliveryYear, 
                                     p.CollectionPeriodMonth, p.CollectionPeriodYear, 
                                     p.FundingSource, p.TransactionType, p.Amount ,
                                     rp.ApprenticeshipContractType, rp.AccountId
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
