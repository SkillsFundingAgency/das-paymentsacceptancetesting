using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.TableParsers
{
    public class DataLockEventErrorsTableParser
    {
        internal static void ParseDataLockEventErrorsIntoContext(DataLockContext context, Table dataLockEventErrors, LookupContext lookupContext)
        {
            if (dataLockEventErrors.Rows.Count < 1)
            {
                throw new ArgumentOutOfRangeException("Data lock event errors table must have at least 1 row");
            }

            var structure = ParseDataLockEventsTableStructure(dataLockEventErrors);
            foreach (var row in dataLockEventErrors.Rows)
            {
                context.DataLockEventErrors.Add(ParseDataLockEventsRow(row, structure, lookupContext));
            }
        }

        private static DataLockEventErrorsTableColumnStructure ParseDataLockEventsTableStructure(Table dataLockEventErrors)
        {
            var structure = new DataLockEventErrorsTableColumnStructure();

            for (var c = 0; c < dataLockEventErrors.Header.Count; c++)
            {
                var header = dataLockEventErrors.Header.ElementAt(c);
                switch (header)
                {
                    case "Price Episode identifier":
                        structure.PriceEpisodeIdentifierIndex = c;
                        break;
                    case "Error code":
                        structure.ErrorCodeIndex = c;
                        break;
                    case "Error Description":
                        structure.ErrorDescriptionIndex = c;
                        break;
                }
            }

            if(structure.PriceEpisodeIdentifierIndex == -1)
            {
                throw new ArgumentException("Data lock event errors table is missing Price Episode identifier column");
            }

            return structure;
        }
        private static DataLockEventErrorReferenceData ParseDataLockEventsRow(TableRow row, DataLockEventErrorsTableColumnStructure structure, LookupContext lookupContext)
        {
            return new DataLockEventErrorReferenceData
            {
                PriceEpisodeIdentifier = row.ReadRowColumnValue<string>(structure.PriceEpisodeIdentifierIndex, "Price Episode identifier"),
                ErrorCode = row.ReadRowColumnValue<string>(structure.ErrorCodeIndex, "Error code"),
                ErrorDescription = row.ReadRowColumnValue<string>(structure.ErrorDescriptionIndex, "Error Description")
            };
        }


        private class DataLockEventErrorsTableColumnStructure
        {
            public int PriceEpisodeIdentifierIndex { get; set; } = -1;
            public int ErrorCodeIndex { get; set; } = -1;
            public int ErrorDescriptionIndex { get; set; } = -1;
        }
    }
}
