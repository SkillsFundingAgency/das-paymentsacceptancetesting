using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules
{
    public class EnglishAndMathsOnProgrammeDataLockRule : DataLockRuleBase
    {
        public EnglishAndMathsOnProgrammeDataLockRule() : base("english and maths on-programme")
        {
        }

        protected override IEnumerable<DataLockResult> FilterPeriodStatuses(DataLockPeriodResults periodStatuses)
        {
            return periodStatuses.Matches.Where(m => m.TransactionType == ReferenceDataModels.TransactionType.OnProgrammeMathsAndEnglish);
        }
    }
}
