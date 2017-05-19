using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Contexts
{
    public class DataLockContext
    {
        public DataLockContext()
        {
            DataLockEvents = new List<DataLockEventReferenceData>();
            DataLockEventErrors = new List<DataLockEventErrorReferenceData>();
            DataLockEventCommitments = new List<DataLockEventCommitmentReferenceData>();
            DataLockEventPeriods = new List<DataLockEventPeriodReferenceData>();
        }

        public bool ExpectsNoDataLockEvents { get; set; }
        public List<DataLockEventReferenceData> DataLockEvents { get; set; }
        public List<DataLockEventErrorReferenceData> DataLockEventErrors { get; set; }
        public List<DataLockEventCommitmentReferenceData> DataLockEventCommitments { get; set; }
        public List<DataLockEventPeriodReferenceData> DataLockEventPeriods { get; set; }
    }
}