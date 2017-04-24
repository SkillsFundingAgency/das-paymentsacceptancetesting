using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.SubmissionDataLockRules
{
    public abstract class SubmissionDataLockRuleBase
    {
        private readonly string _transactionTypeName;

        protected SubmissionDataLockRuleBase(string transactionTypeName)
        {
            _transactionTypeName = transactionTypeName;
        }

        public virtual void AssertPaymentTypeDataLockMatches(List<SubmissionDataLockPeriodMatch> expectedPeriodMatches, LearnerResults[] learnerResults)
        {
            var allStatuses = learnerResults.SelectMany(l => l.SubmissionDataLockResults).ToArray();
            foreach (var period in expectedPeriodMatches)
            {
                var periodStatuses = GetPeriodStatuses(allStatuses, period); // allStatuses.FirstOrDefault(s => s.MatchPeriod == period.PeriodName);
                var match = periodStatuses == null ? null : FilterPeriodStatuses(periodStatuses).FirstOrDefault();
                if (match == null)
                {
                    throw new ArgumentException($"Expected {_transactionTypeName} match for commitment {period.CommitmentId} v{period.CommitmentVersion} in period {period.PeriodName}, but none was found");
                }
                if (period.CommitmentId != match.CommitmentId || period.CommitmentVersion != match.CommitmentVersion)
                {
                    throw new ArgumentException($"Expected {_transactionTypeName} match for commitment {period.CommitmentId} v{period.CommitmentVersion} in period {period.PeriodName}, but found match for commitment {match.CommitmentId} v{match.CommitmentVersion}");
                }
            }
        }

        protected virtual SubmissionDataLockPeriodResults GetPeriodStatuses(SubmissionDataLockPeriodResults[] allStatuses, SubmissionDataLockPeriodMatch period)
        {
            return allStatuses.FirstOrDefault(s => s.MatchPeriod == period.PeriodName);
        }
        protected abstract IEnumerable<SubmissionDataLockResult> FilterPeriodStatuses(SubmissionDataLockPeriodResults periodStatuses);
    }
}
