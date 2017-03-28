using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules
{
    public class FrameworkUpliftCompletionDataLockRule : DataLockRuleBase
    {
        public FrameworkUpliftCompletionDataLockRule() : base("framework uplift completion")
        {
        }

        protected override IEnumerable<DataLockResult> FilterPeriodStatuses(DataLockPeriodResults periodStatuses)
        {
            return periodStatuses.Matches.Where(m => m.TransactionType == ReferenceDataModels.TransactionType.Completion16To18FrameworkUplift);
        }
    }
}