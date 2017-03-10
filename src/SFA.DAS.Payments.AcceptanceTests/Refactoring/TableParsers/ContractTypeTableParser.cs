using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.TableParsers
{
    public static class ContractTypeTableParser
    {
        public static void ParseContractTypesIntoContext(SubmissionContext submissionContext, Table contractTypes, LookupContext lookupContext)
        {
            if (contractTypes.Rows.Count < 1)
            {
                throw new ArgumentOutOfRangeException("Contract types table must have at least 1 row");
            }

            var structure = ParseContractTypesTableStructure(contractTypes);
            foreach (var row in contractTypes.Rows)
            {
                submissionContext.ContractTypes.Add(ParseContractTypeTableRow(row, structure));
            }
        }


        private static ContractTypesTableColumnStructure ParseContractTypesTableStructure(Table contractTypes)
        {
            var structure = new ContractTypesTableColumnStructure();

            for (var c = 0; c < contractTypes.Header.Count; c++)
            {
                var header = contractTypes.Header.ElementAt(c);
                switch (header)
                {
                    case "contract type":
                        structure.ContractTypeIndex = c;
                        break;
                    case "date from":
                        structure.DateFromIndex = c;
                        break;
                    case "date to":
                        structure.DateToIndex = c;
                        break;
                    default:
                        throw new ArgumentException($"Unexpected column in contract types table: {header}");
                }
            }

            if (structure.ContractTypeIndex == -1)
            {
                throw new ArgumentException("Contract types table is missing contract type column");
            }
            if (structure.ContractTypeIndex == -1)
            {
                throw new ArgumentException("Contract types table is missing date from column");
            }
            if (structure.DateToIndex == -1)
            {
                throw new ArgumentException("Contract types table is missing date to column");
            }

            return structure;
        }
        private static ContractTypeReferenceData ParseContractTypeTableRow(TableRow row, ContractTypesTableColumnStructure structure)
        {
            return new ContractTypeReferenceData
            {
                ContractType = (ContractType)row.ReadRowColumnValue<string>(structure.ContractTypeIndex, "contract type").ToEnumByDescription(typeof(ContractType)),
                DateFrom = row.ReadRowColumnValue<DateTime>(structure.DateFromIndex, "date from"),
                DateTo = row.ReadRowColumnValue<DateTime>(structure.DateToIndex, "date to")
            };
        }


        private class ContractTypesTableColumnStructure
        {
            public int ContractTypeIndex { get; set; } = -1;
            public int DateFromIndex { get; set; } = -1;
            public int DateToIndex { get; set; } = -1;
        }
    }
}
