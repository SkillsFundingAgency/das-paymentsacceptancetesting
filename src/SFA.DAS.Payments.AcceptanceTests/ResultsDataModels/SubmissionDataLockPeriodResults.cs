using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.ResultsDataModels
{
    public class SubmissionDataLockPeriodResults
    {
        public string CalculationPeriod { get; set; }
        public string MatchPeriod { get; set; }
        public List<SubmissionDataLockResult> Matches { get; set; }
    }
}
