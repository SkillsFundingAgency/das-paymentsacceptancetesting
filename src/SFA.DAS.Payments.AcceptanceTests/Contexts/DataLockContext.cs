using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Contexts
{
    public class DataLockContext
    {
        public DataLockContext()
        {
            DataLockEvents = new List<DataLockEventReferenceData>();
        }

        public List<DataLockEventReferenceData> DataLockEvents { get; set; }
    }
}