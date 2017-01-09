using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class PaymentsDataHelper
    {
        internal static PaymentEntity[] GetAccountPaymentsForPeriod(long ukprn, long accountId, long? uln, int year, int month, FundingSource fundingSource, ContractType contractType, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = @"SELECT p.* 
                                FROM Payments.Payments p
                                    JOIN PaymentsDue.RequiredPayments rp ON rp.Id = p.RequiredPaymentId 
                                WHERE rp.UKPRN = @ukprn 
                                    AND p.CollectionPeriodMonth = @month 
                                    AND p.CollectionPeriodYear = @year 
                                    AND p.FundingSource = @fundingSource
                                    AND rp.AccountId = @accountId
                                    AND rp.ApprenticeshipContractType = @contractType";

                query = uln.HasValue ? query + " AND rp.Uln = @uln" : query;
                return connection.Query<PaymentEntity>(query, new { ukprn, month, year, accountId, fundingSource, uln, contractType }).ToArray();
            }
        }

        internal static PaymentEntity[] GetPaymentsForPeriod(long ukprn, long? uln, int year, int month, FundingSource fundingSource, ContractType contractType, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = @"SELECT p.* 
                                    FROM Payments.Payments p 
                                        JOIN PaymentsDue.RequiredPayments rp ON rp.Id = p.RequiredPaymentId 
                                    WHERE rp.UKPRN = @ukprn 
                                        AND p.CollectionPeriodMonth = @month 
                                        AND p.CollectionPeriodYear = @year 
                                        AND p.FundingSource = @fundingSource
                                        AND rp.ApprenticeshipContractType = @contractType";

                query = uln.HasValue ? query + " AND rp.Uln = @uln" : query;
                return connection.Query<PaymentEntity>(query, new { ukprn, month, year, fundingSource, uln, contractType }).ToArray();
            }
        }
    }
}
