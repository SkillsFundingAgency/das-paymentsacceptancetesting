using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.TableParsers
{
    internal class TransactionTypeTableParser
    {
        internal static void ParseTransactionTypeTableIntoContext(EarningsAndPaymentsContext earningsAndPaymentsContext, string providerId, Table transactionTypesTable)
        {
            if (transactionTypesTable.Rows.Count < 1)
            {
                throw new ArgumentException("Transaction types table must have at least 1 row");
            }

            var periodNames = ParseEarningAndPaymentsHeaders(transactionTypesTable);
            ParseTransationTypesRows(earningsAndPaymentsContext, providerId, transactionTypesTable, periodNames);
        }


        private static string[] ParseEarningAndPaymentsHeaders(Table earningAndPayments)
        {
            var headers = earningAndPayments.Header.ToArray();
            if (headers[0] != "Payment type" && headers[0] != "Transaction type") //duplicate language
            {
                throw new ArgumentException("Transaction types table must have Payment type as first column");
            }

            var periods = new string[headers.Length];
            for (var c = 1; c < headers.Length; c++)
            {
                var periodName = headers[c];
                if (periodName == "...")
                {
                    continue;
                }
                if (!Validations.IsValidPeriodName(periodName))
                {
                    throw new ArgumentException($"'{periodName}' is not a valid period name format. Expected MM/YY");
                }

                periods[c] = periodName;
            }
            return periods;
        }
        private static void ParseTransationTypesRows(EarningsAndPaymentsContext context, string providerId, Table transactionTypesTable, string[] periodNames)
        {
            foreach (var row in transactionTypesTable.Rows)
            {
                Match match;
                if (row[0] == "On-program")
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForOnProgramme);
                }
                else if (row[0] == "Completion")
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForCompletion);
                }
                else if (row[0] == "Balancing")
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForBalancing);
                }
                else if (row[0] == "Employer 16-18 incentive")
                {
                    ParseEmployerRow(Defaults.EmployerAccountId.ToString(), row, periodNames, context.EmployerEarnedFor16To18Incentive);
                }
                else if ((match = Regex.Match(row[0], "Employer ([0-9]{1,}) 16-18 incentive", RegexOptions.IgnoreCase)).Success)
                {
                    ParseEmployerRow(match.Groups[1].Value, row, periodNames, context.EmployerEarnedFor16To18Incentive);
                }
                else if (row[0] == "Provider 16-18 incentive")
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedFor16To18Incentive);
                }
                else if (row[0] == "English and maths on programme")
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForEnglishAndMathOnProgramme);
                }
                else if (row[0] == "English and maths Balancing")
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForEnglishAndMathBalancing);
                }
                else if (row[0] == "Provider disadvantage uplift")
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForDisadvantageUplift);
                }
                else if (row[0] == "Framework uplift on-program")
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForFrameworkUpliftOnProgramme);
                }
                else if (row[0] == "Framework uplift completion")
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForFrameworkUpliftOnCompletion);
                }
                else if (row[0] == "Framework uplift balancing")
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForFrameworkUpliftOnBalancing);
                }
                else
                {
                    throw new ArgumentException($"Unexpected earning and payments row type of '{row[0]}'");
                }
            }
        }
        private static void ParseProviderRow(string providerId, TableRow row, string[] periodNames, List<ProviderEarnedPeriodValue> contextList)
        {
            ParseRowValues(row, periodNames, contextList, (periodName, value) => new ProviderEarnedPeriodValue
            {
                ProviderId = providerId,
                PeriodName = periodName,
                Value = value
            });
        }
        private static void ParseEmployerRow(string rowAccountId, TableRow row, string[] periodNames, List<EmployerAccountPeriodValue> contextList)
        {
            int employerAccountId;
            if (!int.TryParse(rowAccountId, out employerAccountId))
            {
                throw new ArgumentException($"Employer id '{rowAccountId}' is not valid (Parsing row {row[0]})");
            }


            ParseRowValues(row, periodNames, contextList, (periodName, value) => new EmployerAccountPeriodValue
            {
                EmployerAccountId = employerAccountId,
                PeriodName = periodName,
                Value = value
            });
        }
        private static void ParseRowValues<T>(TableRow row, string[] periodNames, List<T> contextList, Func<string, decimal, T> valueCreator)
        {
            for (var i = 1; i < periodNames.Length; i++)
            {
                var periodName = periodNames[i];
                if (string.IsNullOrEmpty(periodName))
                {
                    continue;
                }

                decimal value;
                if (!decimal.TryParse(row[i], out value))
                {
                    throw new ArgumentException($"Value '{row[i]}' is not a valid enter in the earning and payments table for {row[0]} in period {periodName}");
                }

                contextList.Add(valueCreator(periodName, value));
            }
        }
    }
}
