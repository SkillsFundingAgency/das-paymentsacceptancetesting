using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules
{
    public class FrameworkUpliftBalancingDataLockRule : DataLockRuleBase
    {
        public FrameworkUpliftBalancingDataLockRule() : base("framework uplift balancing")
        {
        }

        protected override IEnumerable<DataLockResult> FilterPeriodStatuses(DataLockPeriodResults periodStatuses)
        {
            return periodStatuses.Matches.Where(m => m.TransactionType == ReferenceDataModels.TransactionType.Balancing16To18FrameworkUplift);
        }
    }
}