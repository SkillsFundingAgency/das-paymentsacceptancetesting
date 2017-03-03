using Dapper;
using ProviderPayments.TestStack.Core;
using System.Data.SqlClient;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    public class ReferenceDataHelper
    {

        internal static void SavePostCodeDisadvantageUplift(string value, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                connection.Execute("INSERT INTO [AT].[ReferenceData] ([Key],[Value],[Type])" +
                            " VALUES ('OX17 1EZ',@value,'PostCode')",
                             new { value });

            }
        }
    }
}
