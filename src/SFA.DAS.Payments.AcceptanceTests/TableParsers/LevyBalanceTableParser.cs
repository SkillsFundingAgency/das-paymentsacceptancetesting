using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using TechTalk.SpecFlow;
using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.TableParsers
{
    public static class LevyBalanceTableParser
    {
        

        internal static List<PeriodValue> ParseLevyAccountBalanceTable( Table employerBalancesTable)
        {
          
            if (employerBalancesTable.RowCount > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(employerBalancesTable), "Balances table can only contain a single row");
            }

            var periodBalances = new List<PeriodValue>();
            for (var c = 0; c < employerBalancesTable.Header.Count; c++)
            {
                var periodName = employerBalancesTable.Header.ElementAt(c);
                if (periodName == "...")
                {
                    continue;
                }
                if (!Validations.IsValidPeriodName(periodName))
                {
                    throw new ArgumentException($"'{periodName}' is not a valid period name format. Expected MM/YY");
                }

                int periodBalance;
                if (!int.TryParse(employerBalancesTable.Rows[0][c], out periodBalance))
                {
                    throw new ArgumentException($"Balance '{employerBalancesTable.Rows[0][c]}' is not a value balance");
                }

                periodBalances.Add(new PeriodValue
                {
                    PeriodName = periodName,
                    Value = periodBalance
                });
            }

            return periodBalances;
        }
     
    }
}
