using System;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class LearningDelivery
    {
        public decimal AgreedPrice { get; set; }
        public LearnerType LearnerType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public CompletionStatus CompletionStatus { get; set; }
    }
}