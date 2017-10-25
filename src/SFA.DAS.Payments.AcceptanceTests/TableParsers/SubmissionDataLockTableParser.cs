﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.TableParsers
{
    internal class SubmissionDataLockTableParser
    {
        internal static void ParseDataLockStatusTableIntoContext(SubmissionDataLockContext dataLockContext, string learnerId, Table dataLockStatusTable)
        {
            if (dataLockStatusTable.Rows.Count < 1)
            {
                throw new ArgumentException("Data Lock status table must have at least 1 row");
            }

            var periodNames = ParseDataLockStatusHeaders(dataLockStatusTable);
            ParseDataLockStatusRows(dataLockContext, learnerId, dataLockStatusTable, periodNames);
        }

        private static string[] ParseDataLockStatusHeaders(Table dataLockStatusTable)
        {
            var headers = dataLockStatusTable.Header.ToArray();
            if (!headers[0].Equals("Payment type", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException("Data Lock status table must have Payment type as first column");
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

        private static void ParseDataLockStatusRows(SubmissionDataLockContext context, string learnerId, Table dataLockStatusTable, string[] periodNames)
        {
            foreach (var row in dataLockStatusTable.Rows)
            {
                if (row[0].Equals("On-program", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseRow(learnerId, row, periodNames, context.DataLockStatusForOnProgramme);
                }
                else if (row[0].Equals("Completion", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseRow(learnerId, row, periodNames, context.DataLockStatusForCompletion);
                }
                else if (row[0].Equals("Balancing", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseRow(learnerId, row, periodNames, context.DataLockStatusForBalancing);
                }
                else if (row[0].Equals("Employer 16-18 incentive", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseRow(learnerId, row, periodNames, context.DataLockStatusForEmployer16To18Incentive);
                }
                else if (row[0].Equals("Provider 16-18 incentive", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseRow(learnerId, row, periodNames, context.DataLockStatusForProvider16To18Incentive);
                }
                else if (row[0].Equals("English and maths on programme", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseRow(learnerId, row, periodNames, context.DataLockStatusForEnglishAndMathOnProgramme);
                }
                else if (row[0].Equals("English and maths Balancing", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseRow(learnerId, row, periodNames, context.DataLockStatusForEnglishAndMathBalancing);
                }
                else if (row[0].Equals("Provider disadvantage uplift", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseRow(learnerId, row, periodNames, context.DataLockStatusForDisadvantageUplift);
                }
                else if (row[0].Equals("Framework uplift on-program", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseRow(learnerId, row, periodNames, context.DataLockStatusForFrameworkUpliftOnProgramme);
                }
                else if (row[0].Equals("Framework uplift completion", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseRow(learnerId, row, periodNames, context.DataLockStatusForFrameworkUpliftCompletion);
                }
                else if (row[0].Equals("Framework uplift balancing", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseRow(learnerId, row, periodNames, context.DataLockStatusForFrameworkUpliftBalancing);
                }
                else if (row[0].Equals("Provider learning support", StringComparison.InvariantCultureIgnoreCase))
                {
                    ParseRow(learnerId, row, periodNames, context.DataLockStatusForLearningSupport);
                }
                else
                {
                    throw new ArgumentException($"Unexpected data lock row type of '{row[0]}'");
                }
            }
        }

        private static void ParseRow(string learnerId, TableRow row, string[] periodNames, List<SubmissionDataLockPeriodMatch> contextList)
        {
            ParseRowValues(row, periodNames, contextList, (periodName, commitmentId, commitmentVersion) => new SubmissionDataLockPeriodMatch
            {
                LearnerId = learnerId,
                PeriodName = periodName,
                CommitmentId = commitmentId,
                CommitmentVersion = commitmentVersion
            });
        }

        private static void ParseRowValues<T>(TableRow row, string[] periodNames, List<T> contextList, Func<string, int?, string, T> valueCreator)
        {
            for (var i = 1; i < periodNames.Length; i++)
            {
                var periodName = periodNames[i];
                if (string.IsNullOrEmpty(periodName))
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(row[i]))
                {
                    continue;
                }

                Match match;

                if ((match = Regex.Match(row[i], "commitment ([0-9]{1,}) v([0-9]{1}-[0-9]{3})", RegexOptions.IgnoreCase)).Success)
                {
                    var commitmentId = int.Parse(match.Groups[1].Value);
                    var commitmentVersion = match.Groups[2].Value;

                    contextList.Add(valueCreator(periodName, commitmentId, commitmentVersion));
                }
                else
                {
                    throw new ArgumentException($"Value '{row[i]}' is not a valid enter in the data lock status table for {row[0]} in period {periodName}");
                }
            }
        }
    }
}