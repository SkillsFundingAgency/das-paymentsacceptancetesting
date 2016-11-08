using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.Contexts
{
    public class Employer
    {
        public string Name { get; set; }
        public long AccountId { get; set; }
        public Dictionary<string, decimal> MonthlyAccountBalance { private get; set; }

        public decimal GetBalanceForMonth(string month)
        {
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