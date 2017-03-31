using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules
{
    public class FrameworkUpliftOnProgrammeDataLockRule : DataLockRuleBase
    {
        public FrameworkUpliftOnProgrammeDataLockRule() : base("framework uplift on-programme")
        {
        }

        protected override IEnumerable<DataLockResult> FilterPeriodStatuses(DataLockPeriodResults periodStatuses)
        {
            return periodStatuses.Matches.Where(m => m.TransactionType == ReferenceDataModels.TransactionType.OnProgramme16To18FrameworkUplift);
        }
    }
}
