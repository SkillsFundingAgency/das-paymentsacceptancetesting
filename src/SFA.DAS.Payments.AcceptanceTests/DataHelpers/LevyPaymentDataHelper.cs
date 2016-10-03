using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal class LevyPaymentDataHelper
    {
        internal static LevyPaymentEntity[] GetLevyPaymentsForPeriod(long ukprn, int year, int month, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = "SELECT * FROM LevyPayments.Payments WHERE UKPRN = @ukprn AND CollectionPeriodMonth = @month AND CollectionPeriodYear = @year";
                return connection.Query<LevyPaymentEntity>(query, new { ukprn, month, year }).ToArray();
            }
        }
    }
}
