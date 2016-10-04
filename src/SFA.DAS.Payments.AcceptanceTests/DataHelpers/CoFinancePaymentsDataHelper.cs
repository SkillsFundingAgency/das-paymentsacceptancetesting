using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class CoFinancePaymentsDataHelper
    {
        internal static CoFinancePaymentEntity[] GetLevyPaymentsForPeriod(long ukprn, int year, int month, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = "SELECT * FROM CoInvestedPayments.Payments WHERE UKPRN = @ukprn AND CollectionPeriodMonth = @month AND CollectionPeriodYear = @year";
                return connection.Query<CoFinancePaymentEntity>(query, new { ukprn, month, year }).ToArray();
            }
        }
    }
}
