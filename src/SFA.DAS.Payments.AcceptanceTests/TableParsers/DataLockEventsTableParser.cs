using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.TableParsers
{
    internal class DataLockEventsTableParser
    {
        internal static void ParseDataLockEventsIntoContext(DataLockContext context, Table dataLockEvents, LookupContext lookupContext)
        {
            if (dataLockEvents.Rows.Count < 1)
            {
                throw new ArgumentOutOfRangeException("Data lock events table must have at least 1 row");
            }

            var structure = ParseDataLockEventsTableStructure(dataLockEvents);
            foreach (var row in dataLockEvents.Rows)
            {
                context.DataLockEvents.Add(ParseDataLockEventsRow(row, structure, lookupContext));
            }
        }

        private static DataLockEventsTableColumnStructure ParseDataLockEventsTableStructure(Table dataLockEvents)
        {
            var structure = new DataLockEventsTableColumnStructure();

            for (var c = 0; c < dataLockEvents.Header.Count; c++)
            {
                var header = dataLockEvents.Header.ElementAt(c);
                switch (header)
                {
                    case "Price Episode identifier":
                        structure.PriceEpisodeIdentifierIndex = c;
                        break;
                    case "Apprenticeship Id":
                        structure.ApprenticeshipIdIndex = c;
                        break;
                    case "ULN":
                        structure.UlnIndex = c;
                        break;
                    case "ILR Start Date":
                        structure.IlrStartDateIndex = c;
                        break;
                    case "ILR Training Price":
                        structure.IlrTrainingPriceIndex = c;
                        break;
                    case "ILR End point assessment price":
                        structure.IlrEndpointAssementPriceIndex = c;
                        break;
                }
            }

            return structure;
        }
        private static DataLockEventReferenceData ParseDataLockEventsRow(TableRow row, DataLockEventsTableColumnStructure structure, LookupContext lookupContext)
        {
            var learnerId = row.ReadRowColumnValue<string>(structure.UlnIndex, "ULN");
            return new DataLockEventReferenceData
            {
                PriceEpisodeIdentifier = row.ReadRowColumnValue<string>(structure.PriceEpisodeIdentifierIndex, "Price Episode identifier"),
                ApprenticeshipId = row.ReadRowColumnValue<int>(structure.ApprenticeshipIdIndex, "Apprenticeship Id"),
                Uln = lookupContext.AddOrGetUln(learnerId),
                IlrStartDate = row.ReadRowColumnValue<DateTime>(structure.IlrStartDateIndex, "ILR Start Date"),
                IlrTrainingPrice = row.ReadRowColumnValue<decimal>(structure.IlrTrainingPriceIndex, "ILR Training Price"),
                IlrEndpointAssementPrice = row.ReadRowColumnValue<decimal>(structure.IlrEndpointAssementPriceIndex, "ILR End point assessment price")
            };
        }


        private class DataLockEventsTableColumnStructure
        {
            public int PriceEpisodeIdentifierIndex { get; set; } = -1;
            public int ApprenticeshipIdIndex { get; set; } = -1;
            public int UlnIndex { get; set; } = -1;
            public int IlrStartDateIndex { get; set; } = -1;
            public int IlrTrainingPriceIndex { get; set; } = -1;
            public int IlrEndpointAssementPriceIndex { get; set; } = -1;
        }
    }
}