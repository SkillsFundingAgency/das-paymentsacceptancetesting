using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.SubmissionDataLockRules
{
    public class EnglishAndMathsBalancingDataLockRule : SubmissionDataLockRuleBase
    {
        public EnglishAndMathsBalancingDataLockRule() : base("english and maths balancing")
        {
        }

        protected override IEnumerable<SubmissionDataLockResult> FilterPeriodStatuses(SubmissionDataLockPeriodResults periodStatuses)
        {
            return periodStatuses.Matches.Where(m => m.TransactionType == ReferenceDataModels.TransactionType.BalancingMathsAndEnglish);
        }
    }
}
