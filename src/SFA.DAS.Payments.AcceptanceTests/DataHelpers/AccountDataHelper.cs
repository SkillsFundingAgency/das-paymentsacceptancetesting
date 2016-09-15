using System.Data.SqlClient;
using Dapper;
using ProviderPayments.TestStack.Core;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class AccountDataHelper
    {
        internal static void CreateAccount(string accountId, string accountName, decimal levyBalance, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                connection.Execute("INSERT INTO DasAccounts (AccountId,AccountName,LevyBalance) VALUES (@accountId,@accountName,@levyBalance)",
                    new { accountId, accountName, levyBalance });
            }
        }
    }
}
