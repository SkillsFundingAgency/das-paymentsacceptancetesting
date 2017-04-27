using System;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules
{
    public class DataLockEventsRule : DataLockRuleBase
    {
        public override void AssertDataLockEvents(DataLockContext context, LearnerResults[] results)
        {
            foreach (var expected in context.DataLockEvents)
            {
                var actual = GetEventForPriceEpisode(results, expected.PriceEpisodeIdentifier);

                if (expected.ApprenticeshipId != actual.CommitmentId)
                {
                    throw new Exception($"Expected event to have commitment id {expected.ApprenticeshipId} but actually had {actual.CommitmentId}");
                }
                if (expected.Uln != actual.Uln)
                {
                    throw new Exception($"Expected event to have ULN {expected.Uln} but actually had {actual.Uln}");
                }
                if (expected.IlrStartDate != actual.IlrStartDate)
                {
                    throw new Exception($"Expected event to have start date {expected.IlrStartDate} but actually had {actual.IlrStartDate}");
                }
                if (expected.IlrTrainingPrice != actual.IlrTrainingPrice)
                {
                    throw new Exception($"Expected event to have training price {expected.IlrTrainingPrice} but actually had {actual.IlrTrainingPrice}");
                }
                if (expected.IlrEndpointAssementPrice != actual.IlrEndpointAssessorPrice)
                {
                    throw new Exception($"Expected event to have endpoint assessment price {expected.IlrEndpointAssementPrice} but actually had {actual.IlrEndpointAssessorPrice}");
                }
            }
        }
    }
}
