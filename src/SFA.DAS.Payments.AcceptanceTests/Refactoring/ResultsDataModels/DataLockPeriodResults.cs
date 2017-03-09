using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels
{
    public class DataLockPeriodResults
    {
        public string Period { get; set; }
        public List<DataLockResult> Matches { get; set; }
    }
}