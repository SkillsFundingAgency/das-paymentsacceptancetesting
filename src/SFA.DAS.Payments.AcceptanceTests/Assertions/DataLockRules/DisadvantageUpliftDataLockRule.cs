using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules
{
    public class DisadvantageUpliftDataLockRule : DataLockRuleBase
    {
        public DisadvantageUpliftDataLockRule() : base("disadvantage uplift")
        {
        }

        protected override IEnumerable<DataLockResult> FilterPeriodStatuses(DataLockPeriodResults periodStatuses)
        {
            return periodStatuses.Matches.Where(m => m.TransactionType == ReferenceDataModels.TransactionType.FirstDisadvantagePayment 
                                                  || m.TransactionType == ReferenceDataModels.TransactionType.SecondDisadvantagePayment);
        }
    }
}
