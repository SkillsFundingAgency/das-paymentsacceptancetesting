using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.ResultsDataModels
{
    public class LearnerResults
    {
        public LearnerResults()
        {
            Earnings = new List<EarningsResult>();
            Payments = new List<PaymentResult>();
            DataLockResults = new List<DataLockPeriodResults>();
            LevyAccountBalanceResults = new List<LevyAccountBalanceResult>();
        }
        public string ProviderId { get; set; }
        public string LearnerId { get; set; }
        public List<EarningsResult> Earnings { get; set; }
        public List<PaymentResult> Payments { get; set; }
        public List<DataLockPeriodResults> DataLockResults { get; set; }
        public List<LevyAccountBalanceResult> LevyAccountBalanceResults { get; set; }

    }
}
