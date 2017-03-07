using System;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels
{
    public class CommitmentReferenceData
    {
        public int CommitmentId { get; set; }
        public int VersionId { get; set; }
        public int EmployerAccountId { get; set; }
        public string LearnerId { get; set; }
        public long Uln { get; set; }
        public int Priority { get; set; }
        public string ProviderId { get; set; }
        public long Ukprn { get; set; }
        public int AgreedPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public string Status { get; set; }
        public long? StandardCode { get; set; }
        public int? ProgrammeType { get; set; }
        public int? FrameworkCode { get; set; }
        public int? PathwayCode { get; set; }
    }
}
