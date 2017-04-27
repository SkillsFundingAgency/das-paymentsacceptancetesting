using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.SubmissionDataLockRules
{
    public class Employer16To18IncentiveDataLockRule : SubmissionDataLockRuleBase
    {
        public Employer16To18IncentiveDataLockRule() : base("employer 16-18 incentive")
        {
        }

        protected override IEnumerable<SubmissionDataLockResult> FilterPeriodStatuses(SubmissionDataLockPeriodResults periodStatuses)
        {
            return periodStatuses.Matches.Where(m => m.TransactionType == ReferenceDataModels.TransactionType.First16To18EmployerIncentive
                                                  || m.TransactionType == ReferenceDataModels.TransactionType.Second16To18EmployerIncentive);
        }
    }
}
