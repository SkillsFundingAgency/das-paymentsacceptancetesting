﻿using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.TableParsers
{
    public static class LearningSupportTableParser
    {
        public static void ParseLearningSupportIntoContext(SubmissionContext submissionContext, Table learningSupportStatus)
        {
            if (learningSupportStatus.Rows.Count < 1)
            {
                throw new ArgumentOutOfRangeException("Learning support table must have at least 1 row");
            }

            var structure = ParseContractTypesTableStructure(learningSupportStatus);
            foreach (var row in learningSupportStatus.Rows)
            {
                submissionContext.LearningSupportStatus.Add(ParseLearningSupportTableRow(row, structure));
            }
        }
        
        private static LearningSupportTableColumnStructure ParseContractTypesTableStructure(Table contractTypes)
        {
            var structure = new LearningSupportTableColumnStructure();

            for (var c = 0; c < contractTypes.Header.Count; c++)
            {
                var header = contractTypes.Header.ElementAt(c).ToLowerInvariant();
                switch (header)
                {
                    case "learning support code":
                        structure.LearningSupportCodeIndex = c;
                        break;
                    case "date from":
                        structure.DateFromIndex = c;
                        break;
                    case "date to":
                        structure.DateToIndex = c;
                        break;
                    default:
                        throw new ArgumentException($"Unexpected column in learning support table: {header}");
                }
            }

            return structure;
        }
        private static LearningSupportReferenceData ParseLearningSupportTableRow(TableRow row, LearningSupportTableColumnStructure structure)
        {
            return new LearningSupportReferenceData
            {
                LearningSupportCode = row.ReadRowColumnValue<int>(structure.LearningSupportCodeIndex, "Learning support code"),
                DateFrom = row.ReadRowColumnValue<DateTime>(structure.DateFromIndex, "date from"),
                DateTo = row.ReadRowColumnValue<DateTime>(structure.DateToIndex, "date to")
            };
        }
        
        private class LearningSupportTableColumnStructure
        {
            public int LearningSupportCodeIndex { get; set; } = -1;
            public int DateFromIndex { get; set; } = -1;
            public int DateToIndex { get; set; } = -1;
        }
    }
}
