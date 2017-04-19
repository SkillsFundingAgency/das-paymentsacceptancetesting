using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.SubmissionDataLockRules
{
    public class DisadvantageUpliftDataLockRule : SubmissionDataLockRuleBase
    {
        public DisadvantageUpliftDataLockRule() : base("disadvantage uplift")
        {
        }

        protected override IEnumerable<SubmissionDataLockResult> FilterPeriodStatuses(SubmissionDataLockPeriodResults periodStatuses)
        {
            return periodStatuses.Matches.Where(m => m.TransactionType == ReferenceDataModels.TransactionType.FirstDisadvantagePayment
                                                  || m.TransactionType == ReferenceDataModels.TransactionType.SecondDisadvantagePayment);
        }
    }
}
