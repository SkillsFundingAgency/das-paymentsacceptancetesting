using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

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
                decimal currentBalance = 0m;
                if (account.IsRunningBalance)
                {
                    var existingBalance = connection.Query<decimal?>("SELECT BALANCE FROM dbo.DasAccounts WHERE AccountId = @AccountId", new { AccountId = account.Id }).SingleOrDefault();
                    currentBalance = existingBalance != null && existingBalance.HasValue ? existingBalance.Value : 0m;
                }
                var rowsAffected = connection.Execute("UPDATE dbo.DasAccounts " +
                                                      "SET Balance = @Balance " +
                                                      "WHERE AccountId = @AccountId",
                                                      new
                                                      {
                                                          AccountId = account.Id,
                                                          Balance = account.Balance  + currentBalance,
                                                      });
                if (rowsAffected < 1)
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
        }

        internal static void UpdateAccountBalancesForPeriod(IEnumerable<EmployerAccountReferenceData> accounts, string period)
        {
            foreach(var account in accounts)
            {
                UpdateAccountBalanceForPeriod(account, period);
            }
        }

        internal static void UpdateAccountBalancesForPeriod(IEnumerable<EmployerAccountReferenceData> accounts, string period, IEnumerable<LearnerResults> results)
        {
            foreach (var account in accounts)
            {
                UpdateAccountBalanceForPeriod(account, period, results.SingleOrDefault(x=> x.LevyAccountBalanceResults.Count()>0));
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
                decimal currentBalance = 0m;
                if (account.IsRunningBalance)
                {
                    var existingBalance = connection.Query<decimal?>("SELECT BALANCE FROM dbo.DasAccounts WHERE AccountId = @AccountId", new { AccountId = account.Id }).SingleOrDefault();
                    currentBalance = existingBalance != null && existingBalance.HasValue ? existingBalance.Value : 0m;
                }
                connection.Execute("UPDATE dbo.DasAccounts " +
                                   "SET Balance = @Balance " +
                                   "WHERE AccountId = @AccountId ",
                                   new
                                   {
                                       AccountId = account.Id,
                                       Balance = periodBalance.Value + currentBalance
                                   });
            }
        }

        internal static void UpdateAccountBalanceForPeriod(EmployerAccountReferenceData account, string period, LearnerResults result)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }

            if (account.IsRunningBalance == false || result == null)
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
                var paymentsMade = result.LevyAccountBalanceResults.SingleOrDefault(x => x.CalculationPeriod == period);

                decimal currentBalance = 0m;
                if (account.IsRunningBalance)
                {
                    var existingBalance = connection.Query<decimal?>("SELECT BALANCE FROM dbo.DasAccounts WHERE AccountId = @AccountId", new { AccountId = account.Id }).SingleOrDefault();
                    currentBalance = existingBalance != null && existingBalance.HasValue ? existingBalance.Value : 0m;
                }

                connection.Execute("UPDATE dbo.DasAccounts " +
                                   "SET Balance = @Balance " +
                                   "WHERE AccountId = @AccountId ",
                                   new
                                   {
                                       AccountId = account.Id,
                                       Balance = currentBalance -  (paymentsMade == null ? 0 : paymentsMade.Amount * -1)
                                   });
            }
        }
    }
}
