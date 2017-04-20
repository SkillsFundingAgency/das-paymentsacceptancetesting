using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.SubmissionDataLockRules
{
    public class FrameworkUpliftCompletionDataLockRule : SubmissionDataLockRuleBase
    {
        public FrameworkUpliftCompletionDataLockRule() : base("framework uplift completion")
        {
        }

        protected override IEnumerable<SubmissionDataLockResult> FilterPeriodStatuses(SubmissionDataLockPeriodResults periodStatuses)
        {
            return periodStatuses.Matches.Where(m => m.TransactionType == ReferenceDataModels.TransactionType.Completion16To18FrameworkUplift);
        }
    }
}
