using System.Data.SqlClient;
using Dapper;
using ProviderPayments.TestStack.Core;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class AccountDataHelper
    {
        internal static void CreateAccount(long accountId, string accountName, decimal levyBalance, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var existingAccountId = connection.QuerySingleOrDefault<long?>("SELECT AccountId FROM DasAccounts WHERE AccountId = @accountId", new { accountId });

                if (!existingAccountId.HasValue)
                {
                    connection.Execute("INSERT INTO DasAccounts (AccountId,AccountHashId,AccountName,Balance,VersionId) VALUES (@accountId,@accountId,@accountName,@levyBalance,CONVERT(varchar, GETDATE(), 126))",
                        new { accountId, accountName, levyBalance });
                }
            }
        }

        internal static void UpdateAccountBalance(long accountId, decimal levyBalance, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                connection.Execute("UPDATE DasAccounts SET Balance = @levyBalance WHERE AccountId = @accountId",
                    new { accountId, levyBalance });
            }
        }

        internal static void UpdateAudit(EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                connection.Execute("INSERT INTO DasAccountsAudit (ReadDateTime,AccountsRead,CompletedSuccessfully) SELECT GETDATE(), COUNT(AccountId),1 FROM dbo.DasAccounts");
            }
        }

        internal static void ClearAccounts(EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                connection.Execute("DELETE FROM dbo.DasAccounts");
                connection.Execute("DELETE FROM dbo.DasAccountsAudit");
            }
        }
    }
}
