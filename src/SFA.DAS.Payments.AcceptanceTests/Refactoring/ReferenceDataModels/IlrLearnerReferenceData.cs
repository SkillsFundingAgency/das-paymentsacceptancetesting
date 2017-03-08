using System;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels
{
    public class IlrLearnerReferenceData
    {
        public string LearnerId { get; set; }
        public long Uln { get; set; }
        public decimal AgreedPrice { get; set; }
        public LearnerType LearnerType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public CompletionStatus CompletionStatus { get; set; }
        public string Provider { get; set; }
        public int TotalTrainingPrice1 { get; set; }
        public DateTime TotalTrainingPrice1EffectiveDate { get; set; }
        public int TotalAssessmentPrice1 { get; set; }
        public DateTime TotalAssessmentPrice1EffectiveDate { get; set; }
        public int ResidualTrainingPrice { get; set; }
        public DateTime ResidualTrainingPriceEffectiveDate { get; set; }
        public int ResidualAssessmentPrice { get; set; }
        public DateTime ResidualAssessmentPriceEffectiveDate { get; set; }
        public int TotalTrainingPrice2 { get; set; }
        public DateTime TotalTrainingPrice2EffectiveDate { get; set; }
        public int TotalAssessmentPrice2 { get; set; }
        public DateTime TotalAssessmentPrice2EffectiveDate { get; set; }
        public AimType AimType { get; set; }
        public string AimRate { get; set; }
        public long StandardCode { get; set; }
        public int FrameworkCode { get; set; }
        public int ProgrammeType { get; set; }
        public int PathwayCode { get; set; }
        public string HomePostcodeDeprivation { get; set; }
        public string EmploymentStatus { get; set; }
        public string EmploymentStatusApplies { get; set; }
        public string EmployerId { get; set; }
        public string SmallEmployer { get; set; }
        public string LearnDelFam { get; set; }
    }
}
