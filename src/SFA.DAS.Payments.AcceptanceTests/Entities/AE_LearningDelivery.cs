using SFA.DAS.Payments.AcceptanceTests.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
