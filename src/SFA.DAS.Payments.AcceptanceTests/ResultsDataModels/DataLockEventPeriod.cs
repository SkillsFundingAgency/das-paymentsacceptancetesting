using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Payments.AcceptanceTests.ResultsDataModels
{
    public class DataLockEventPeriod
    {
        public Guid DataLockEventId { get; set; }
        public string CollectionPeriodName { get; set; }
        public int CollectionPeriodMonth { get; set; }
        public int CollectionPeriodYear { get; set; }
        public string CommitmentVersion { get; set; }
        public bool IsPayable { get; set; }
        public int TransactionType { get; set; }
    }
}
