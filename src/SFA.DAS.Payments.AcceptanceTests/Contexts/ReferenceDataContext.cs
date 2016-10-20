using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.Contexts
{
    public class ReferenceDataContext
    {
        public LearnerType LearnerType { get; set; }
        public decimal FundingMaximum { get; set; }
        public decimal AgreedPrice { get; set; }
        public Dictionary<string, decimal> MonthlyAccountBalance { get; set; }
        public Commitment[] Commitments { get; set; }
    }
}
