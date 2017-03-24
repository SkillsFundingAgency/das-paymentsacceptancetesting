using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.TableParsers
{
    internal static class EarningAndPaymentTableParser
    {
        internal static void ParseEarningsAndPaymentsTableIntoContext(EarningsAndPaymentsBreakdown breakdown, Table earningAndPayments)
        {
            if (earningAndPayments.Rows.Count < 1)
            {
                throw new ArgumentException("Earnings and payments table must have at least 1 row");
            }

            var periodNames = ParseEarningAndPaymentsHeaders(earningAndPayments);
            ParseEarningAndPaymentsRows(breakdown, earningAndPayments, periodNames);
        }


        private static string[] ParseEarningAndPaymentsHeaders(Table earningAndPayments)
        {
            var headers = earningAndPayments.Header.ToArray();
            if (headers[0] != "Type")
            {
                throw new ArgumentException("Earnings and payments table must have Type as first column");
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
        private static void ParseEarningAndPaymentsRows(EarningsAndPaymentsBreakdown breakdown, Table earningAndPayments, string[] periodNames)
        {
            foreach (var row in earningAndPayments.Rows)
            {
                Match match;
                if (row[0] == "Provider Earned Total")
                {
                    ParseNonEmployerRow(row, periodNames, breakdown.ProviderEarnedTotal);
                }
                else if (row[0] == "Provider Earned from SFA")
                {
                    ParseNonEmployerRow(row, periodNames, breakdown.ProviderEarnedFromSfa);
                }
                else if (row[0] == "Provider Earned from Employer")
                {
                    ParseEmployerRow(Defaults.EmployerAccountId.ToString(), row, periodNames, breakdown.ProviderEarnedFromEmployers);
                }
                else if ((match = Regex.Match(row[0], "Provider Earned from Employer ([0-9]{1,})", RegexOptions.IgnoreCase)).Success)
                {
                    ParseEmployerRow(match.Groups[1].Value, row, periodNames, breakdown.ProviderEarnedFromEmployers);
                }
                else if (row[0] == "Provider Paid by SFA")
                {
                    ParseNonEmployerRow(row, periodNames, breakdown.ProviderPaidBySfa);
                }
                else if (row[0] == "Payment due from Employer")
                {
                    ParseEmployerRow(Defaults.EmployerAccountId.ToString(), row, periodNames, breakdown.PaymentDueFromEmployers);
                }
                else if ((match = Regex.Match(row[0], "Payment due from Employer ([0-9]{1,})", RegexOptions.IgnoreCase)).Success)
                {
                    ParseEmployerRow(match.Groups[1].Value, row, periodNames, breakdown.PaymentDueFromEmployers);
                }
                else if (row[0] == "Levy account debited")
                {
                    ParseEmployerRow(Defaults.EmployerAccountId.ToString(), row, periodNames, breakdown.EmployersLevyAccountDebited);
                }
                else if ((match = Regex.Match(row[0], "employer ([0-9]{1,}) Levy account debited", RegexOptions.IgnoreCase)).Success)
                {
                    ParseEmployerRow(match.Groups[1].Value, row, periodNames, breakdown.EmployersLevyAccountDebited);
                }
                else if (row[0] == "SFA Levy employer budget")
                {
                    ParseNonEmployerRow(row, periodNames, breakdown.SfaLevyBudget);
                }
                else if (row[0] == "SFA Levy co-funding budget")
                {
                    ParseNonEmployerRow(row, periodNames, breakdown.SfaLevyCoFundBudget);
                }
                else if (row[0] == "SFA non-Levy co-funding budget")
                {
                    ParseNonEmployerRow(row, periodNames, breakdown.SfaNonLevyCoFundBudget);
                }
                else if (row[0] == "SFA Levy additional payments budget")
                {
                    ParseNonEmployerRow(row, periodNames, breakdown.SfaLevyAdditionalPayments);
                }
                else if (row[0] == "SFA non-Levy additional payments budget")
                {
                    ParseNonEmployerRow(row, periodNames, breakdown.SfaNonLevyAdditionalPayments);
                }
                else
                {
                    throw new ArgumentException($"Unexpected earning and payments row type of '{row[0]}'");
                }
            }
        }
        private static void ParseNonEmployerRow(TableRow row, string[] periodNames, List<PeriodValue> contextList)
        {
            ParseRowValues(row, periodNames, contextList, (periodName, value) => new PeriodValue
            {
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
