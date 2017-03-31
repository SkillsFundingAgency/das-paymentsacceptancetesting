using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules
{
    public abstract class DataLockRuleBase
    {
        private readonly string _transactionTypeName;

        protected DataLockRuleBase(string transactionTypeName)
        {
            _transactionTypeName = transactionTypeName;
        }

        public virtual void AssertPaymentTypeDataLockMatches(List<DataLockPeriodMatch> expectedPeriodMatches, LearnerResults[] learnerResults)
        {
            var allStatuses = learnerResults.SelectMany(l => l.DataLockResults).ToArray();
            foreach (var period in expectedPeriodMatches)
            {
                var periodStatuses = allStatuses.FirstOrDefault(s => s.MatchPeriod == period.PeriodName);
                var match = FilterPeriodStatuses(periodStatuses).FirstOrDefault();
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

        protected abstract IEnumerable<DataLockResult> FilterPeriodStatuses(DataLockPeriodResults periodStatuses);
    }
}
