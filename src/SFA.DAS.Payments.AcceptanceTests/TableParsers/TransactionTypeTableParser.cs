using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.TableParsers
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
            if (!headers[0].Equals("Payment type", StringComparison.InvariantCultureIgnoreCase) && 
                !headers[0].Equals("Transaction type", StringComparison.InvariantCultureIgnoreCase)) //duplicate language
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
                if (row[0].Equals("On-program", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForOnProgramme);
                }
                else if (row[0].Equals("Completion", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForCompletion);
                }
                else if (row[0].Equals("Balancing", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForBalancing);
                }
                else if (row[0].Equals("Employer 16-18 incentive", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseEmployerRow(providerId, Defaults.EmployerAccountId.ToString(), row, periodNames, context.EmployerEarnedFor16To18Incentive);
                }
                else if ((match = Regex.Match(row[0], "Employer ([0-9]{1,}) 16-18 incentive", RegexOptions.IgnoreCase)).Success)
                {
                    ParseEmployerRow(providerId, match.Groups[1].Value, row, periodNames, context.EmployerEarnedFor16To18Incentive);
                }
                else if (row[0].Equals("Provider 16-18 incentive", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedFor16To18Incentive);
                }
                else if (row[0].Equals("English and maths on programme", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForEnglishAndMathOnProgramme);
                }
                else if (row[0].Equals("English and maths Balancing", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForEnglishAndMathBalancing);
                }
                else if (row[0].Equals("Provider disadvantage uplift", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForDisadvantageUplift);
                }
                else if (row[0].Equals("Framework uplift on-program", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForFrameworkUpliftOnProgramme);
                }
                else if (row[0].Equals("Framework uplift completion", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForFrameworkUpliftOnCompletion);
                }
                else if (row[0].Equals("Framework uplift balancing", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForFrameworkUpliftOnBalancing);
                }
                else if (row[0].Equals("Provider learning support", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseProviderRow(providerId, row, periodNames, context.ProviderEarnedForLearningSupport);
                }
                else
                {
                    throw new ArgumentException($"Unexpected transation types row type of '{row[0]}'");
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

        private static void ParseEmployerRow(string providerId, string rowAccountId, TableRow row, string[] periodNames, List<EmployerAccountProviderPeriodValue> contextList)
        {
            int employerAccountId;
            if (!int.TryParse(rowAccountId, out employerAccountId))
            {
                throw new ArgumentException($"Employer id '{rowAccountId}' is not valid (Parsing row {row[0]})");
            }
            
            ParseRowValues(row, periodNames, contextList, (periodName, value) => new EmployerAccountProviderPeriodValue
            {
                ProviderId = providerId,
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
