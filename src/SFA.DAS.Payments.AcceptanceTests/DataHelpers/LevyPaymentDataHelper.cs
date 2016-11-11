using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class LevyPaymentDataHelper
    {
        internal static LevyPaymentEntity[] GetLevyPaymentsForPeriod(long ukprn, int year, int month, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = "SELECT * FROM Payments.Payments WHERE UKPRN = @ukprn AND CollectionPeriodMonth = @month AND CollectionPeriodYear = @year AND FundingSource = 1";
                return connection.Query<LevyPaymentEntity>(query, new { ukprn, month, year }).ToArray();
            }
        }

        internal static LevyPaymentEntity[] GetAccountLevyPaymentsForPeriod(long ukprn, long accountId, int year, int month, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = @"SELECT * 
                                FROM Payments.Payments 
                                WHERE UKPRN = @ukprn 
                                    AND CollectionPeriodMonth = @month 
                                    AND CollectionPeriodYear = @year 
                                    AND FundingSource = 1 
                                    AND CommitmentId IN (SELECT CommitmentId FROM dbo.DasCommitments WHERE AccountId = @accountId)";
                return connection.Query<LevyPaymentEntity>(query, new { ukprn, month, year, accountId }).ToArray();
            }
        }
    }
}
