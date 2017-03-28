using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules
{
    public class Employer16To18IncentiveDataLockRule : DataLockRuleBase
    {
        public Employer16To18IncentiveDataLockRule() : base("employer 16-18 incentive")
        {
        }

        protected override IEnumerable<DataLockResult> FilterPeriodStatuses(DataLockPeriodResults periodStatuses)
        {
            return periodStatuses.Matches.Where(m => m.TransactionType == ReferenceDataModels.TransactionType.First16To18EmployerIncentive
                                                  || m.TransactionType == ReferenceDataModels.TransactionType.Second16To18EmployerIncentive);
        }
    }
}
