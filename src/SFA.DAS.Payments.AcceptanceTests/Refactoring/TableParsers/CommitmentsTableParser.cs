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
    public static class CommitmentsTableParser
    {

        public static void ParseCommitmentsIntoContext(CommitmentsContext context, Table commitments, LookupContext lookupContext)
        {
            if (commitments.Rows.Count < 1)
            {
                throw new ArgumentOutOfRangeException("commitments table must have at least 1 row");
            }

            var structure = ParseCommitmentsTableStructure(commitments);
            foreach (var row in commitments.Rows)
            {
                context.Commitments.Add(ParseCommitmentsTableRow(row, structure, context.Commitments.Count, lookupContext));
            }
        }



        private static CommitmentsTableColumnStructure ParseCommitmentsTableStructure(Table commitments)
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
                    case "commitment Id":
                        structure.CommitmentIdIndex = c;
                        break;
                    case "version Id":
                        structure.VersionIdIndex = c;
                        break;
                    case "start date":
                        structure.StartDateIndex = c;
                        break;
                    case "end date":
                        structure.EndDateIndex = c;
                        break;
                    case "status":
                        structure.StatusIndex = c;
                        break;
                    case "effective from":
                        structure.EffectiveFromIndex = c;
                        break;
                    case "effective to":
                        structure.EffectiveToIndex = c;
                        break;
                    case "standard code":
                        // TODO
                        break;
                    case "framework code":
                        // TODO
                        break;
                    case "programme type":
                        // TODO
                        break;
                    case "pathway code":
                        // TODO
                        break;
                    default:
                        throw new ArgumentException($"Unexpected column in commitments table: {header}");
                }
            }
            if (structure.UlnIndex == -1)
            {
                throw new ArgumentException("Commitments table is missing ULN column");
            }
            if (structure.StartDateIndex == -1)
            {
                throw new ArgumentException("Commitments table is missing start date column");
            }
            if (structure.EndDateIndex == -1)
            {
                throw new ArgumentException("Commitments table is missing end date column");
            }

            return structure;
        }

        private static CommitmentReferenceData ParseCommitmentsTableRow(TableRow row, CommitmentsTableColumnStructure structure, int rowIndex, LookupContext lookupContext)
        {
            var learnerId = row[structure.UlnIndex];
            var uln = 20000L + rowIndex;
            var providerId = structure.ProviderIndex > -1 ? row[structure.ProviderIndex] : Defaults.ProviderId;
            var ukprn = lookupContext.AddOrGetUkprn(providerId);
            var status = structure.StatusIndex > -1 ? row[structure.StatusIndex] : Defaults.CommitmentStatus;
            var standardCode = Defaults.StandardCode;

            int priority = Defaults.CommitmentPriority;
            if (structure.PriorityIndex > -1 && !int.TryParse(row[structure.PriorityIndex], out priority))
            {
                throw new ArgumentException($"'{row[structure.PriorityIndex]}' is not a valid priority");
            }

            var employerAccountId = Defaults.EmployerAccountId;
            if (structure.EmployerIndex > -1 && (row[structure.EmployerIndex].Length < 10 || !int.TryParse(row[structure.EmployerIndex].Substring(9), out employerAccountId)))
            {
                throw new ArgumentException($"'{row[structure.EmployerIndex]}' is not a valid employer reference");
            }

            var price = Defaults.AgreePrice;
            if (structure.PriceIndex > -1 && !int.TryParse(row[structure.PriceIndex], out price))
            {
                throw new ArgumentException($"'{row[structure.PriceIndex]}' is not a valid agreed price");
            }

            var commitmentId = rowIndex + 1;
            if (structure.CommitmentIdIndex > -1 && !int.TryParse(row[structure.CommitmentIdIndex], out commitmentId))
            {
                throw new ArgumentException($"'{row[structure.CommitmentIdIndex]}' is not a valid commitment id");
            }

            var versionId = Defaults.CommitmentVersionId;
            if (structure.VersionIdIndex > -1 && !int.TryParse(row[structure.VersionIdIndex], out versionId))
            {
                throw new ArgumentException($"'{row[structure.VersionIdIndex]}' is not a valid version id");
            }

            DateTime startDate;
            if (!DateTime.TryParse(row[structure.StartDateIndex], out startDate))
            {
                throw new ArgumentException($"'{row[structure.StartDateIndex]}' is not a valid start date");
            }

            DateTime endDate;
            if (!DateTime.TryParse(row[structure.EndDateIndex], out endDate))
            {
                throw new ArgumentException($"'{row[structure.EndDateIndex]}' is not a valid end date");
            }

            DateTime? effectiveFrom = null;
            if (structure.EffectiveFromIndex > -1 && !TryParseNullableDateTime(row[structure.EffectiveFromIndex], out effectiveFrom))
            {
                throw new ArgumentException($"'{row[structure.EffectiveFromIndex]}' is not a valid effective from");
            }

            DateTime? effectiveTo = null;
            if (structure.EffectiveToIndex > -1 && string.IsNullOrEmpty(row[structure.EffectiveToIndex]))
            {
                effectiveTo = DateTime.MaxValue;
            }
            else if (structure.EffectiveToIndex > -1 && !TryParseNullableDateTime(row[structure.EffectiveToIndex], out effectiveTo))
            {
                throw new ArgumentException($"'{row[structure.EffectiveToIndex]}' is not a valid effective to");
            }

            if (effectiveFrom == null)
            {
                effectiveFrom = startDate;
            }
            if (effectiveTo == null)
            {
                effectiveTo = endDate;
            }

            return new CommitmentReferenceData
            {
                EmployerAccountId = employerAccountId,
                LearnerId = learnerId,
                Uln = uln,
                Priority = priority,
                ProviderId = providerId,
                Ukprn = ukprn,
                AgreedPrice = price,
                CommitmentId = commitmentId,
                VersionId = versionId,
                StartDate = startDate,
                EndDate = endDate,
                EffectiveFrom = effectiveFrom.Value,
                EffectiveTo = effectiveTo.Value,
                Status = status,
                StandardCode = standardCode
            };
        }

        private static bool TryParseNullableDateTime(string value, out DateTime? dateTime)
        {
            DateTime temp;
            if (DateTime.TryParse(value, out temp))
            {
                dateTime = temp;
                return true;
            }

            dateTime = null;
            return false;
        }



        private class CommitmentsTableColumnStructure
        {
            public int CommitmentIdIndex { get; set; } = -1;
            public int VersionIdIndex { get; set; } = -1;
            public int UlnIndex { get; set; } = -1;
            public int PriorityIndex { get; set; } = -1;
            public int EmployerIndex { get; set; } = -1;
            public int ProviderIndex { get; set; } = -1;
            public int PriceIndex { get; set; } = -1;
            public int StartDateIndex { get; set; } = -1;
            public int EndDateIndex { get; set; } = -1;
            public int StatusIndex { get; set; } = -1;
            public int EffectiveFromIndex { get; set; } = -1;
            public int EffectiveToIndex { get; set; } = -1;
        }

    }
}
