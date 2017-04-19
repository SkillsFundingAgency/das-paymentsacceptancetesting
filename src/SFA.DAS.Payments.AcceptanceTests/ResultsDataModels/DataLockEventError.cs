using System;

namespace SFA.DAS.Payments.AcceptanceTests.ResultsDataModels
{
    public class DataLockEventError
    {
        public Guid DataLockEventId { get; set; }
        public string ErrorCode { get; set; }
        public string SystemDescription { get; set; }
    }
}
