using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.SubmissionDataLockRules
{
    public class FrameworkUpliftOnProgrammeDataLockRule : SubmissionDataLockRuleBase
    {
        public FrameworkUpliftOnProgrammeDataLockRule() : base("framework uplift on-programme")
        {
        }

        protected override IEnumerable<SubmissionDataLockResult> FilterPeriodStatuses(SubmissionDataLockPeriodResults periodStatuses)
        {
            return periodStatuses.Matches.Where(m => m.TransactionType == ReferenceDataModels.TransactionType.OnProgramme16To18FrameworkUplift);
        }
    }
}
