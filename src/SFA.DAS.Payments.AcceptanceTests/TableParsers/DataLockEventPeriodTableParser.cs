using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.TableParsers
{
    internal class DataLockEventPeriodTableParser
    {
        internal static void ParseDataLockEventPeriodsIntoContext(DataLockContext context, Table dataLockEventCommitments, LookupContext lookupContext)
        {
            if (dataLockEventCommitments.Rows.Count < 1)
            {
                throw new ArgumentOutOfRangeException("Data lock event periods table must have at least 1 row");
            }

            var structure = ParseDataLockEventPeriodsTableStructure(dataLockEventCommitments);
            foreach (var row in dataLockEventCommitments.Rows)
            {
                context.DataLockEventPeriods.Add(ParseDataLockEventPeriodsRow(row, structure, lookupContext));
            }
        }

        private static DataLockEventPeriodsTableColumnStructure ParseDataLockEventPeriodsTableStructure(Table dataLockEventErrors)
        {
            var structure = new DataLockEventPeriodsTableColumnStructure();

            for (var c = 0; c < dataLockEventErrors.Header.Count; c++)
            {
                var header = dataLockEventErrors.Header.ElementAt(c);
                switch (header)
                {
                    case "Price Episode identifier":
                        structure.PriceEpisodeIdentifierIndex = c;
                        break;
                    case "Period":
                        structure.PeriodIndex = c;
                        break;
                    case "Payable Flag":
                        structure.PayableFlagIndex = c;
                        break;
                    case "Transaction Type":
                        structure.TransactionTypeIndex = c;
                        break;
                }
            }

            if (structure.PriceEpisodeIdentifierIndex == -1)
            {
                throw new ArgumentException("Data lock event periods table is missing Price Episode identifier column");
            }

            return structure;
        }
        private static DataLockEventPeriodReferenceData ParseDataLockEventPeriodsRow(TableRow row, DataLockEventPeriodsTableColumnStructure structure, LookupContext lookupContext)
        {
            return new DataLockEventPeriodReferenceData
            {
                PriceEpisodeIdentifier = row.ReadRowColumnValue<string>(structure.PriceEpisodeIdentifierIndex, "Price Episode identifier"),
                Period = row.ReadRowColumnValue<string>(structure.PeriodIndex, "Period"),
                PayableFlag = row.ReadRowColumnValue<bool>(structure.PayableFlagIndex, "Payable Flag"),
                TransactionType = (TransactionType)row.ReadRowColumnValue<string>(structure.TransactionTypeIndex, "Transaction Type").ToEnumByDescription(typeof(TransactionType)),
            };
        }


        private class DataLockEventPeriodsTableColumnStructure
        {
            public int PriceEpisodeIdentifierIndex { get; set; } = -1;
            public int PeriodIndex { get; set; } = -1;
            public int PayableFlagIndex { get; set; } = -1;
            public int TransactionTypeIndex { get; set; } = -1;
        }
    }
}
