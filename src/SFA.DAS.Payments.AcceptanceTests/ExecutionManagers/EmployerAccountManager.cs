using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.ExecutionManagers
{
    internal class EmployerAccountManager
    {
        internal static void AddOrUpdateAccount(EmployerAccountReferenceData account)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }

            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                var rowsAffected = connection.Execute("UPDATE dbo.DasAccounts " +
                                                      "SET Balance = @Balance " +
                                                      "WHERE AccountId = @AccountId",
                                                      new
                                                      {
                                                          AccountId = account.Id,
                                                          Balance = account.Balance,
                                                      });
                if (rowsAffected < 1)
                {
                    connection.Execute("INSERT INTO dbo.DasAccounts (AccountId, AccountHashId, AccountName, Balance, VersionId,IsLevyPayer) " +
                                       "VALUES (@AccountId, @AccountHashId, @AccountName, @Balance, @VersionId,@IsLevyPayer)",
                        new
                        {
                            AccountId = account.Id,
                            AccountHashId = account.Id.ToString(),
                            AccountName = $"Employer {account.Id}",
                            Balance = account.Balance,
                            VersionId = 1,
                            IsLevyPayer = account.IsLevyPayer
                        });
                }
            }
        }

        internal static void UpdateAccountBalancesForPeriod(IEnumerable<EmployerAccountReferenceData> accounts, string period)
        {
            foreach (var account in accounts)
            {
                UpdateAccountBalanceForPeriod(account, period);
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
                                   "SET Balance = @Balance " +
                                   "WHERE AccountId = @AccountId ",
                                   new
                                   {
                                       AccountId = account.Id,
                                       Balance = periodBalance.Value
                                   });
            }
        }
    }
}
