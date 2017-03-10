using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ExecutionManagers
{
    internal class EmployerAccountManager
    {
        internal static void AddAccount(EmployerAccountReferenceData account)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }

            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                connection.Execute("INSERT INTO dbo.DasAccounts (AccountId, AccountHashId, AccountName, Balance, VersionId) " +
                                    "VALUES (@AccountId, @AccountHashId, @AccountName, @Balance, @VersionId)",
                                    new
                                    {
                                        AccountId = account.Id,
                                        AccountHashId = account.Id.ToString(),
                                        AccountName = $"Employer {account.Id}",
                                        Balance = account.Balance,
                                        VersionId = 1
                                    });
            }
        }

        internal static void UpdateAccountBalanceForPeriod(EmployerAccountReferenceData account, string period)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }

            var periodBalance = account.PeriodBalances?.SingleOrDefault(x => x.PeriodName.Equals(period, System.StringComparison.CurrentCultureIgnoreCase));
            if (periodBalance == null)
            {
                return;
            }

            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                connection.Execute("UPDATE dbo.DasAccounts " +
                                   "SET Balance = @Balance" +
                                   "WHERE AccountId = @AccountId",
                                   new
                                   {
                                       AccountId = account.Id,
                                       Balance = account.Balance
                                   });
            }
        }
    }
}
