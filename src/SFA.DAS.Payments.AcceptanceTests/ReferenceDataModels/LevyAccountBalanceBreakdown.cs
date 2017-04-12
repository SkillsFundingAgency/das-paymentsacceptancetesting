using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels
{
    public class LevyAccountBalanceBreakdown
    {
        public LevyAccountBalanceBreakdown()
        {
            LevyAccountBalance= new List<PeriodValue>();
           
        }

        public string ProviderId { get; set; }
        public List<PeriodValue> LevyAccountBalance { get; set; }
    

    }
}
