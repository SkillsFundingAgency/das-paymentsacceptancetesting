using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.SubmissionDataLockRules
{
    public class BalancingDataLockRule : SubmissionDataLockRuleBase
    {
        public BalancingDataLockRule() : base("balancing")
        {
        }

        protected override IEnumerable<SubmissionDataLockResult> FilterPeriodStatuses(SubmissionDataLockPeriodResults periodStatuses)
        {
            return periodStatuses.Matches.Where(m => m.TransactionType == ReferenceDataModels.TransactionType.Balancing);
        }
    }
}