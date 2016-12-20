using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.Entities;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class DataLockDataHelper
    {
        internal static DataLockMatch[] GetDataLockMatchesForUkprn(long ukprn, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = "SELECT " +
                                "CommitmentId, " +
                                "PriceEpisodeIdentifier AS PriceEpisodeId " +
                            "FROM DataLock.DasLearnerCommitment " +
                            "WHERE Ukprn = @ukprn";
                return connection.Query<DataLockMatch>(query, new { ukprn }).ToArray();
            }
        }
    }
}