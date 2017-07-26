using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.ResultsDataModels
{
    public class LearnerResults
    {
        public LearnerResults()
        {
            Earnings = new List<EarningsResult>();
            Payments = new List<PaymentResult>();
            LevyAccountBalanceResults = new List<LevyAccountBalanceResult>();
            SubmissionDataLockResults = new List<SubmissionDataLockPeriodResults>();
        }
        public string ProviderId { get; set; }
        public string LearnerReferenceNumber { get; set; }
        public List<EarningsResult> Earnings { get; set; }
        public List<PaymentResult> Payments { get; set; }
        public List<LevyAccountBalanceResult> LevyAccountBalanceResults { get; set; }
        public DataLockEventResult[] DataLockEvents { get; set; }
        public List<SubmissionDataLockPeriodResults> SubmissionDataLockResults { get; set; }
    }
}
