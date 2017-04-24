using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.SubmissionDataLockRules
{
    public abstract class IncentiveBeyondMainAimDataLockRuleBase : SubmissionDataLockRuleBase
    {
        protected IncentiveBeyondMainAimDataLockRuleBase(string transactionTypeName) : base(transactionTypeName)
        {
        }

        protected override SubmissionDataLockPeriodResults GetPeriodStatuses(SubmissionDataLockPeriodResults[] allStatuses, SubmissionDataLockPeriodMatch period)
        {
            var currentPeriodMatch = base.GetPeriodStatuses(allStatuses, period);
            if (currentPeriodMatch != null)
            {
                return currentPeriodMatch;
            }

            return allStatuses.OrderByDescending(x => Extensions.ToPeriodDateTime(x.MatchPeriod)).FirstOrDefault();
        }
    }
}