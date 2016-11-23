using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.Entities;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class PaymentsDataHelper
    {



        internal static PaymentEntity[] GetAccountPaymentsForPeriod(long ukprn, long accountId, int year, int month, FundingSource fundingSource, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = @"SELECT * 
                                FROM Payments.Payments 
                                WHERE UKPRN = @ukprn 
                                    AND CollectionPeriodMonth = @month 
                                    AND CollectionPeriodYear = @year 
                                    AND FundingSource =@fundingSource
                                    AND CommitmentId IN (SELECT CommitmentId FROM dbo.DasCommitments WHERE AccountId = @accountId)";
                return connection.Query<PaymentEntity>(query, new { ukprn, month, year, accountId,fundingSource }).ToArray();
            }
        }

        internal static PaymentEntity[] GetPaymentsForPeriod(long ukprn, int year, int month, FundingSource fundingSource, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = "SELECT * FROM Payments.Payments WHERE UKPRN = @ukprn AND CollectionPeriodMonth = @month AND CollectionPeriodYear = @year AND FundingSource = @fundingSource";
                return connection.Query<PaymentEntity>(query, new { ukprn, month, year, fundingSource }).ToArray();
            }
        }

    }
}
