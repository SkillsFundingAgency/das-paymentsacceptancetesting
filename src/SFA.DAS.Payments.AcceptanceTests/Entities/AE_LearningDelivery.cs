using System;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class AE_LearningDelivery
    {
        public long UKPRN { get; set; }
        public long ULN { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public CompletionStatus CompletionStatus { get; set; }
        public decimal NegotiatedPrice { get; set; }
        public decimal MonthlyInstallment { get; set; }
        public decimal CompletionPayment { get; set; }

    }
}
