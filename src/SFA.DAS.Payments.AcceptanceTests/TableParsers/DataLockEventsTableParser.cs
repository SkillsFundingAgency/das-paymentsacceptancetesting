using System;
using System.Linq;
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
                var header = dataLockEvents.Header.ElementAt(c).ToLowerInvariant();
                switch (header)
                {
                    case "price episode identifier":
                        structure.PriceEpisodeIdentifierIndex = c;
                        break;
                    case "apprenticeship id":
                        structure.ApprenticeshipIdIndex = c;
                        break;
                    case "uln":
                        structure.UlnIndex = c;
                        break;
                    case "ilr start date":
                        structure.IlrStartDateIndex = c;
                        break;
                    case "ilr training price":
                        structure.IlrTrainingPriceIndex = c;
                        break;
                    case "ilr ed point assessment price":
                        structure.IlrEndpointAssementPriceIndex = c;
                        break;
                    case "ilr effective from":
                        structure.IlrEffectiveFromIndex = c;
                        break;
                    case "ilr effective to":
                        structure.IlrEffectiveToIndex = c;
                        break;
                    default:
                        throw new ArgumentException($"Unexpected column in data lock events table: {header}");
                }
            }

            if (structure.PriceEpisodeIdentifierIndex == -1)
            {
                throw new ArgumentException("Data lock events table is missing Price Episode identifier column");
            }

            return structure;
        }
        private static DataLockEventReferenceData ParseDataLockEventsRow(TableRow row, DataLockEventsTableColumnStructure structure, LookupContext lookupContext)
        {
            var learnerId = row.ReadRowColumnValue<string>(structure.UlnIndex, "ULN");
            var startDate = row.ReadRowColumnValue<DateTime>(structure.IlrStartDateIndex, "ILR Start Date");
            var effectiveFrom = row.ReadRowColumnValue<DateTime>(structure.IlrEffectiveFromIndex, "ILR Effective from", startDate);
            return new DataLockEventReferenceData
            {
                PriceEpisodeIdentifier = row.ReadRowColumnValue<string>(structure.PriceEpisodeIdentifierIndex, "Price Episode identifier"),
                ApprenticeshipId = row.ReadRowColumnValue<int>(structure.ApprenticeshipIdIndex, "Apprenticeship Id"),
                Uln = lookupContext.AddOrGetUln(learnerId),
                IlrStartDate = startDate,
                IlrTrainingPrice = row.ReadRowColumnValue<decimal>(structure.IlrTrainingPriceIndex, "ILR Training Price"),
                IlrEndpointAssementPrice = row.ReadRowColumnValue<decimal>(structure.IlrEndpointAssementPriceIndex, "ILR End point assessment price"),
                ILrEffectiveFrom = effectiveFrom,
                ILrEffectiveTo = row.ReadRowColumnValue<DateTime?>(structure.IlrEffectiveToIndex, "ILR Effective to"),
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
            public int IlrEffectiveFromIndex { get; set; } = -1;
            public int IlrEffectiveToIndex { get; set; } = -1;
        }
    }
}