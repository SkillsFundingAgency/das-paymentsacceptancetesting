using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class EarningsDataHelper
    {
        internal static PeriodisedValuesEntity[] GetPeriodisedValuesForUkprn(long ukprn, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = "SELECT * FROM Rulebase.AE_LearningDelivery_PeriodisedValues WHERE UKPRN = @ukprn";
                return connection.Query<PeriodisedValuesEntity>(query, new { ukprn }).ToArray();
            }
        }
    }
}
