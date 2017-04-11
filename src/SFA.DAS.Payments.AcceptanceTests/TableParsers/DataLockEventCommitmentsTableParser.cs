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
    internal static class DataLockEventCommitmentsTableParser
    {
        internal static void ParseDataLockEventCommitmentsIntoContext(DataLockContext context, Table dataLockEventCommitments, LookupContext lookupContext)
        {
            if (dataLockEventCommitments.Rows.Count < 1)
            {
                throw new ArgumentOutOfRangeException("Data lock event commitments table must have at least 1 row");
            }

            var structure = ParseDataLockEventCommitmentsTableStructure(dataLockEventCommitments);
            foreach (var row in dataLockEventCommitments.Rows)
            {
                context.DataLockEventCommitments.Add(ParseDataLockEventsRow(row, structure, lookupContext));
            }
        }

        private static DataLockEventCommitmentsTableColumnStructure ParseDataLockEventCommitmentsTableStructure(Table dataLockEventErrors)
        {
            var structure = new DataLockEventCommitmentsTableColumnStructure();

            for (var c = 0; c < dataLockEventErrors.Header.Count; c++)
            {
                var header = dataLockEventErrors.Header.ElementAt(c);
                switch (header)
                {
                    case "Price Episode identifier":
                        structure.PriceEpisodeIdentifierIndex = c;
                        break;
                    case "Apprentice Version":
                        structure.ApprenticeshipVersionIndex = c;
                        break;
                    case "Start Date":
                        structure.StartDateIndex = c;
                        break;
                    case "framework code":
                        structure.FrameworkCodeIndex = c;
                        break;
                    case "programme type":
                        structure.ProgrammeTypeIndex = c;
                        break;
                    case "pathway code":
                        structure.PathwayCodeIndex = c;
                        break;
                    case "standard code":
                        structure.StandardCodeIndex = c;
                        break;
                    case "Negotiated Price":
                        structure.NegotiatedPriceIndex = c;
                        break;
                    case "Effective Date":
                        structure.EffectiveDateIndex = c;
                        break;
                }
            }

            if (structure.PriceEpisodeIdentifierIndex == -1)
            {
                throw new ArgumentException("Data lock event commitments table is missing Price Episode identifier column");
            }

            return structure;
        }
        private static DataLockEventCommitmentReferenceData ParseDataLockEventsRow(TableRow row, DataLockEventCommitmentsTableColumnStructure structure, LookupContext lookupContext)
        {
            return new DataLockEventCommitmentReferenceData
            {
                PriceEpisodeIdentifier = row.ReadRowColumnValue<string>(structure.PriceEpisodeIdentifierIndex, "Price Episode identifier"),
                ApprenticeshipVersion = row.ReadRowColumnValue<int>(structure.ApprenticeshipVersionIndex, "Apprentice Version"),
                StartDate = row.ReadRowColumnValue<DateTime>(structure.StartDateIndex, "Start Date"),
                FrameworkCode = row.ReadRowColumnValue<int>(structure.FrameworkCodeIndex, "framework code"),
                ProgrammeType = row.ReadRowColumnValue<int>(structure.ProgrammeTypeIndex, "programme type"),
                PathwayCode = row.ReadRowColumnValue<int>(structure.PathwayCodeIndex, "pathway code"),
                StandardCode = row.ReadRowColumnValue<long>(structure.StandardCodeIndex, "standard code"),
                NegotiatedPrice = row.ReadRowColumnValue<int>(structure.NegotiatedPriceIndex, "Negotiated Price"),
                EffectiveDate = row.ReadRowColumnValue<DateTime>(structure.EffectiveDateIndex, "Effective Date"),
            };
        }


        private class DataLockEventCommitmentsTableColumnStructure
        {
            public int PriceEpisodeIdentifierIndex { get; set; } = -1;
            public int ApprenticeshipVersionIndex { get; set; } = -1;
            public int StartDateIndex { get; set; } = -1;
            public int FrameworkCodeIndex { get; set; } = -1;
            public int ProgrammeTypeIndex { get; set; } = -1;
            public int PathwayCodeIndex { get; set; } = -1;
            public int StandardCodeIndex { get; set; } = -1;
            public int NegotiatedPriceIndex { get; set; } = -1;
            public int EffectiveDateIndex { get; set; } = -1;
        }
    }
}
