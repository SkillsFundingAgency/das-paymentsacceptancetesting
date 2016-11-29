using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.Entities;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class PaymentsDueDataHelper
    {
        internal static RequiredPaymentEntity[] GetPaymentsDueForPeriod(long ukprn, int year, int month, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = "SELECT * FROM PaymentsDue.RequiredPayments WHERE UKPRN = @ukprn AND CollectionPeriodMonth = @month AND CollectionPeriodYear = @year";
                return connection.Query<RequiredPaymentEntity>(query, new { ukprn, month, year }).ToArray();
            }
        }
    }
}