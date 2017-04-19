using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.SubmissionDataLockRules
{
    public class EnglishAndMathsOnProgrammeDataLockRule : SubmissionDataLockRuleBase
    {
        public EnglishAndMathsOnProgrammeDataLockRule() : base("english and maths on-programme")
        {
        }

        protected override IEnumerable<SubmissionDataLockResult> FilterPeriodStatuses(SubmissionDataLockPeriodResults periodStatuses)
        {
            return periodStatuses.Matches.Where(m => m.TransactionType == ReferenceDataModels.TransactionType.OnProgrammeMathsAndEnglish);
        }
    }
}
