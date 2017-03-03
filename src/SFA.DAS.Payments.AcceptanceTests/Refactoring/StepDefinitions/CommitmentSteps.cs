using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.StepDefinitions
{
    [Binding]
    public class CommitmentSteps
    {
        public CommitmentSteps(CommitmentsContext commitmentsContext)
        {
            CommitmentsContext = commitmentsContext;
        }
        public CommitmentsContext CommitmentsContext { get; }

        [Given("the following commitments exist:")]
        public void GivenCommitmentsExistForLearners(Table commitments)
        {
            if (commitments.Rows.Count < 1)
            {
                throw new ArgumentOutOfRangeException("commitments table must have at least 1 row");
            }

            var structure = ParseCommitmentsTableStructure(commitments);
            foreach (var row in commitments.Rows)
            {
                CommitmentsContext.Commitments.Add(ParseCommitmentsTableRow(row, structure));
            }
        }


        private CommitmentsTableColumnStructure ParseCommitmentsTableStructure(Table commitments)
        {
            var structure = new CommitmentsTableColumnStructure();

            for (var c = 0; c < commitments.Header.Count; c++)
            {
                var header = commitments.Header.ElementAt(c);
                switch (header)
                {
                    case "ULN":
                        structure.UlnIndex = c;
                        break;
                    case "priority":
                        structure.PriorityIndex = c;
                        break;
                    case "Employer":
                        structure.EmployerIndex = c;
                        break;
                    case "Provider":
                        structure.ProviderIndex = c;
                        break;
                    case "agreed price":
                        structure.PriceIndex = c;
                        break;
                    default:
                        throw new ArgumentException($"Unexpected column in commitments table: {header}");
                }
            }
            if (structure.UlnIndex == -1)
            {
                throw new ArgumentException("Commitments table is missing ULN column");
            }
            if (structure.PriorityIndex == -1)
            {
                throw new ArgumentException("Commitments table is missing priority column");
            }

            return structure;
        }

        private CommitmentReferenceData ParseCommitmentsTableRow(TableRow row, CommitmentsTableColumnStructure structure)
        {
            int priority;
            if (!int.TryParse(row[structure.PriorityIndex], out priority))
            {
                throw new ArgumentException($"'{row[structure.PriorityIndex]}' is not a valid priority");
            }

            var employerAccountId = 1;
            if (structure.EmployerIndex > -1 && (row[structure.EmployerIndex].Length < 10 || !int.TryParse(row[structure.EmployerIndex].Substring(9), out employerAccountId)))
            {
                throw new ArgumentException($"'{row[structure.EmployerIndex]}' is not a valid employer reference");
            }

            var price = Defaults.AgreePrice;
            if (structure.PriceIndex > -1 && !int.TryParse(row[structure.PriceIndex], out price))
            {
                throw new ArgumentException($"'{row[structure.PriceIndex]}' is not a valid agreed price");
            }

            return new CommitmentReferenceData
            {
                EmployerAccountId = employerAccountId,
                Uln = row[structure.UlnIndex],
                Priority = priority,
                ProviderId = structure.ProviderIndex > -1 ? row[structure.ProviderIndex] : Defaults.ProviderId,
                AgreedPrice = price
            };
        }

        private class CommitmentsTableColumnStructure
        {
            public int UlnIndex { get; set; } = -1;
            public int PriorityIndex { get; set; } = -1;
            public int EmployerIndex { get; set; } = -1;
            public int ProviderIndex { get; set; } = -1;
            public int PriceIndex { get; set; } = -1;
        }
    }
}
