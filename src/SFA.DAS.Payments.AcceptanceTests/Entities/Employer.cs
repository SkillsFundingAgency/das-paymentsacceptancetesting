using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class Employer
    {
        public string Name { get; set; }
        public long AccountId { get; set; }
        public LearnerType LearnersType { get; set; }
        public Dictionary<string, decimal> MonthlyAccountBalance { private get; set; }

        public decimal GetBalanceForMonth(string month)
        {
            if(MonthlyAccountBalance.Count == 0)
            {
                return 0;
            }
            if (MonthlyAccountBalance.ContainsKey("All"))
            {
                return MonthlyAccountBalance["All"];
            }

            if (MonthlyAccountBalance.ContainsKey(month))
            {
                return MonthlyAccountBalance[month];
            }

            return MonthlyAccountBalance["..."];
        }
    }
}