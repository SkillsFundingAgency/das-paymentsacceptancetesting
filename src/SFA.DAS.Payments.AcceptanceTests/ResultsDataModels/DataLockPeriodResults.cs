using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.ResultsDataModels
{
    public class DataLockPeriodResults
    {
        public string CalculationPeriod { get; set; }
        public string MatchPeriod { get; set; }
        public List<DataLockResult> Matches { get; set; }
    }
}