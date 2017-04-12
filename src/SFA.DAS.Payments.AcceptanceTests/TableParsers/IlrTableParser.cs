using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.TableParsers
{
    public class IlrTableParser
    {
        public static void ParseIlrTableIntoContext(SubmissionContext context, Table ilrDetails)
        {
            if (ilrDetails.RowCount < 1)
            {
                throw new ArgumentException("ILR table must have at least 1 row");
            }

            var structure = ParseTableStructure(ilrDetails);
            foreach (var row in ilrDetails.Rows)
            {
                context.IlrLearnerDetails.Add(ParseCommitmentsTableRow(row, structure));
            }
        }


        private static IlrTableStructure ParseTableStructure(Table ilrDetails)
        {
            var structure = new IlrTableStructure();

            for (var c = 0; c < ilrDetails.Header.Count; c++)
            {
                var header = ilrDetails.Header.ElementAt(c);
                switch (header)
                {
                    case "ULN":
                        structure.UlnIndex = c;
                        break;
                    case "agreed price":
                        structure.AgreedPriceIndex = c;
                        break;
                    case "learner type":
                        structure.LearnerTypeIndex = c;
                        break;
                    case "start date":
                        structure.StartDateIndex = c;
                        break;
                    case "planned end date":
                        structure.PlannedEndDateIndex = c;
                        break;
                    case "actual end date":
                        structure.ActualEndDateIndex = c;
                        break;
                    case "completion status":
                        structure.CompletionStatusIndex = c;
                        break;
                    case "Provider":
                        structure.ProviderIndex = c;
                        break;
                    case "Total training price":
                    case "Total training price 1": // duplicate
                        structure.TotalTrainingPrice1Index = c;
                        break;
                    case "Total training price effective date":
                    case "Total training price 1 effective date": // duplicate
                        structure.TotalTrainingPrice1EffectiveDateIndex = c;
                        break;
                    case "Total assessment price":
                    case "Total assessment price 1": // duplicate
                        structure.TotalAssessmentPrice1Index = c;
                        break;
                    case "Total assessment price effective date":
                    case "Total assessment price 1 effective date": // duplicate
                        structure.TotalAssessmentPrice1EffectiveDateIndex = c;
                        break;
                    case "Total training price 2":
                        structure.TotalTrainingPrice2Index = c;
                        break;
                    case "Total training price 2 effective date":
                        structure.TotalTrainingPrice2EffectiveDateIndex = c;
                        break;
                    case "Total assessment price 2":
                        structure.TotalAssessmentPrice2Index = c;
                        break;
                    case "Total assessment price 2 effective date":
                        structure.TotalAssessmentPrice2EffectiveDateIndex = c;
                        break;
                    case "Residual training price":
                    case "Residual training price 1":
                        structure.ResidualTrainingPrice1Index = c;
                        break;
                    case "Residual training price effective date":
                    case "Residual training price 1 effective date":
                        structure.ResidualTrainingPrice1EffectiveDateIndex = c;
                        break;
                    case "Residual assessment price":
                    case "Residual assessment price 1":
                        structure.ResidualAssessmentPrice1Index = c;
                        break;
                    case "Residual assessment price effective date":
                    case "Residual assessment price 1 effective date":
                        structure.ResidualAssessmentPrice1EffectiveDateIndex = c;
                        break;
                    case "Residual training price 2":
                        structure.ResidualTrainingPrice2Index = c;
                        break;
                    case "Residual training price 2 effective date":
                        structure.ResidualTrainingPrice2EffectiveDateIndex = c;
                        break;
                    case "Residual assessment price 2":
                        structure.ResidualAssessmentPrice2Index = c;
                        break;
                    case "Residual assessment price 2 effective date":
                        structure.ResidualAssessmentPrice2EffectiveDateIndex = c;
                        break;
                    case "aim type":
                        structure.AimTypeIndex = c;
                        break;
                    case "aim rate":
                        structure.AimRateIndex = c;
                        break;
                    case "standard code":
                        structure.StandardCodeIndex = c;
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
                    case "home postcode deprivation":
                        structure.HomePostcodeDeprivationIndex = c;
                        break;
                    case "Employment Status":
                        structure.EmploymentStatusIndex = c;
                        break;
                    case "Employment Status Applies":
                        structure.EmploymentStatusAppliesIndex = c;
                        break;
                    case "Employer Id":
                        structure.EmployerIdIndex = c;
                        break;
                    case "Small Employer":
                        structure.SmallEmployerIndex = c;
                        break;
                    case "LearnDelFAM":
                        structure.LearnDelFamIndex = c;
                        break;
                    default:
                        throw new ArgumentException($"Unexpected column in ILR table: {header}");
                }
            }

            return structure;
        }
        private static IlrLearnerReferenceData ParseCommitmentsTableRow(TableRow row, IlrTableStructure structure)
        {
            var rowData = new IlrLearnerReferenceData
            {
                LearnerId = row.ReadRowColumnValue<string>(structure.UlnIndex, "ULN", Defaults.LearnerId),
                AgreedPrice = row.ReadRowColumnValue<int>(structure.AgreedPriceIndex, "agreed price"),
                LearnerType = (LearnerType)row.ReadRowColumnValue<string>(structure.LearnerTypeIndex, "learner type", "programme only DAS").ToEnumByDescription(typeof(LearnerType)),
                StartDate = row.ReadRowColumnValue<DateTime>(structure.StartDateIndex, "start date"),
                PlannedEndDate = row.ReadRowColumnValue<DateTime>(structure.PlannedEndDateIndex, "planned end date"),
                ActualEndDate = row.ReadRowColumnValue<DateTime?>(structure.ActualEndDateIndex, "actual end date"),
                CompletionStatus = (CompletionStatus)row.ReadRowColumnValue<string>(structure.CompletionStatusIndex, "completion status").ToEnumByDescription(typeof(CompletionStatus)),
                Provider = row.ReadRowColumnValue<string>(structure.ProviderIndex, "provider", Defaults.ProviderId),
                TotalTrainingPrice1 = row.ReadRowColumnValue<int>(structure.TotalTrainingPrice1Index, "total training price 1"),
                TotalTrainingPrice1EffectiveDate = row.ReadRowColumnValue<DateTime>(structure.TotalTrainingPrice1EffectiveDateIndex, "total training price 1 effective date"),
                TotalAssessmentPrice1 = row.ReadRowColumnValue<int>(structure.TotalAssessmentPrice1Index, "total assessment price 1"),
                TotalAssessmentPrice1EffectiveDate = row.ReadRowColumnValue<DateTime>(structure.TotalAssessmentPrice1EffectiveDateIndex, "total assessment price 1 effective date"),
                TotalTrainingPrice2 = row.ReadRowColumnValue<int>(structure.TotalTrainingPrice2Index, "total training price 2"),
                TotalTrainingPrice2EffectiveDate = row.ReadRowColumnValue<DateTime>(structure.TotalTrainingPrice2EffectiveDateIndex, "total training price 2 effective date"),
                TotalAssessmentPrice2 = row.ReadRowColumnValue<int>(structure.TotalAssessmentPrice2Index, "total assessment price 2"),
                TotalAssessmentPrice2EffectiveDate = row.ReadRowColumnValue<DateTime>(structure.TotalAssessmentPrice2EffectiveDateIndex, "total assessment price 2 effective date"),
                ResidualTrainingPrice1 = row.ReadRowColumnValue<int>(structure.ResidualTrainingPrice1Index, "residual training price 1"),
                ResidualTrainingPrice1EffectiveDate = row.ReadRowColumnValue<DateTime>(structure.ResidualTrainingPrice1EffectiveDateIndex, "residual training price 1 effective date"),
                ResidualAssessmentPrice1 = row.ReadRowColumnValue<int>(structure.ResidualAssessmentPrice1Index, "residual assessment price 1"),
                ResidualAssessmentPrice1EffectiveDate = row.ReadRowColumnValue<DateTime>(structure.ResidualAssessmentPrice1EffectiveDateIndex, "residual assessment price 1 effective date"),
                ResidualTrainingPrice2 = row.ReadRowColumnValue<int>(structure.ResidualTrainingPrice2Index, "residual training price 2"),
                ResidualTrainingPrice2EffectiveDate = row.ReadRowColumnValue<DateTime>(structure.ResidualTrainingPrice2EffectiveDateIndex, "residual training price 2 effective date"),
                ResidualAssessmentPrice2 = row.ReadRowColumnValue<int>(structure.ResidualAssessmentPrice2Index, "residual assessment price 2"),
                ResidualAssessmentPrice2EffectiveDate = row.ReadRowColumnValue<DateTime>(structure.ResidualAssessmentPrice2EffectiveDateIndex, "residual assessment price 2 effective date"),
                AimType = (AimType)row.ReadRowColumnValue<string>(structure.AimTypeIndex, "aim type", "Programme").ToEnumByDescription(typeof(AimType)),
                AimRate = row.ReadRowColumnValue<string>(structure.AimRateIndex, "aim rate"),
                StandardCode = row.ReadRowColumnValue<long>(structure.StandardCodeIndex, "standard code"),
                FrameworkCode = row.ReadRowColumnValue<int>(structure.FrameworkCodeIndex, "framework code"),
                ProgrammeType = row.ReadRowColumnValue<int>(structure.ProgrammeTypeIndex, "programme type"),
                PathwayCode = row.ReadRowColumnValue<int>(structure.PathwayCodeIndex, "pathway code"),
                HomePostcodeDeprivation = row.ReadRowColumnValue<string>(structure.HomePostcodeDeprivationIndex, "home postcode deprivation"),
                EmploymentStatus = row.ReadRowColumnValue<string>(structure.EmploymentStatusIndex, "employment status"),
                EmploymentStatusApplies = row.ReadRowColumnValue<string>(structure.EmploymentStatusAppliesIndex, "employment status applies"),
                EmployerId = row.ReadRowColumnValue<string>(structure.EmployerIdIndex, "employer id"),
                SmallEmployer = row.ReadRowColumnValue<string>(structure.SmallEmployerIndex, "small employer"),
                LearnDelFam = row.ReadRowColumnValue<string>(structure.LearnDelFamIndex, "LearnDelFam")
            };

            if (rowData.StandardCode == 0 && rowData.FrameworkCode == 0)
            {
                rowData.StandardCode = Defaults.StandardCode;
            }

            if (rowData.FrameworkCode > 0 && rowData.TotalAssessmentPrice2 > 0 )
            {
                throw new Exception("Framework code and TotalAssessmentPrice2 can't be in the same scenario");
            }

            return rowData;
        }


        private class IlrTableStructure
        {
            public int UlnIndex { get; set; } = -1;
            public int AgreedPriceIndex { get; set; } = -1;
            public int LearnerTypeIndex { get; set; } = -1;
            public int StartDateIndex { get; set; } = -1;
            public int PlannedEndDateIndex { get; set; } = -1;
            public int ActualEndDateIndex { get; set; } = -1;
            public int CompletionStatusIndex { get; set; } = -1;
            public int ProviderIndex { get; set; } = -1;
            public int TotalTrainingPrice1Index { get; set; } = -1;
            public int TotalTrainingPrice1EffectiveDateIndex { get; set; } = -1;
            public int TotalAssessmentPrice1Index { get; set; } = -1;
            public int TotalAssessmentPrice1EffectiveDateIndex { get; set; } = -1;
            public int ResidualTrainingPrice1Index { get; set; } = -1;
            public int ResidualTrainingPrice1EffectiveDateIndex { get; set; } = -1;
            public int ResidualAssessmentPrice1Index { get; set; } = -1;
            public int ResidualAssessmentPrice1EffectiveDateIndex { get; set; } = -1;
            public int TotalTrainingPrice2Index { get; set; } = -1;
            public int TotalTrainingPrice2EffectiveDateIndex { get; set; } = -1;
            public int TotalAssessmentPrice2Index { get; set; } = -1;
            public int TotalAssessmentPrice2EffectiveDateIndex { get; set; } = -1;
            public int AimTypeIndex { get; set; } = -1;
            public int AimRateIndex { get; set; } = -1;
            public int StandardCodeIndex { get; set; } = -1;
            public int FrameworkCodeIndex { get; set; } = -1;
            public int ProgrammeTypeIndex { get; set; } = -1;
            public int PathwayCodeIndex { get; set; } = -1;
            public int HomePostcodeDeprivationIndex { get; set; } = -1;
            public int EmploymentStatusIndex { get; set; } = -1;
            public int EmploymentStatusAppliesIndex { get; set; } = -1;
            public int EmployerIdIndex { get; set; } = -1;
            public int SmallEmployerIndex { get; set; } = -1;
            public int LearnDelFamIndex { get; set; } = -1;
            public int ResidualTrainingPrice2Index { get; set; } = -1;
            public int ResidualTrainingPrice2EffectiveDateIndex { get; set; } = -1;
            public int ResidualAssessmentPrice2Index { get; set; } = -1;
            public int ResidualAssessmentPrice2EffectiveDateIndex { get; set; } = -1;
        }
    }
}
