using System;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels
{
    public class IlrLearnerReferenceData
    {
        public string Uln { get; set; }
        public decimal AgreedPrice { get; set; }
        public string LearnerType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public string CompletionStatus { get; set; }
        public string Provider { get; set; }
        public decimal TotalTrainingPrice1 { get; set; }
        public DateTime TotalTrainingPrice1EffectiveDate { get; set; }
        public decimal TotalAssessmentPrice1 { get; set; }
        public DateTime TotalAssessmentPrice1EffectiveDate { get; set; }
        public decimal ResidualTrainingPrice { get; set; }
        public DateTime ResidualTrainingPriceEffectiveDate { get; set; }
        public decimal ResidualAssessmentPrice { get; set; }
        public DateTime ResidualAssessmentPriceEffectiveDate { get; set; }
        public decimal TotalTrainingPrice2 { get; set; }
        public DateTime TotalTrainingPrice2EffectiveDate { get; set; }
        public decimal TotalAssessmentPrice2 { get; set; }
        public DateTime TotalAssessmentPrice2EffectiveDate { get; set; }
        public string AimType { get; set; }
        public string AimRate { get; set; }
        public string StandardCode { get; set; }
        public string FrameworkCode { get; set; }
        public string ProgrammeType { get; set; }
        public string PathwayCode { get; set; }
        public string HomePostcodeDeprivation { get; set; }
        public string EmploymentStatus { get; set; }
        public string EmploymentStatusApplies { get; set; }
        public string EmployerId { get; set; }
        public string SmallEmployer { get; set; }
        public string LearnDelFam { get; set; }
    }
}
