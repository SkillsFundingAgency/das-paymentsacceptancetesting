using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules
{
    public class DataLockEventsRule : DataLockRuleBase
    {
        public override void AssertDataLockEvents(DataLockContext context, LearnerResults[] results)
        {
            if (context.ExpectsNoDataLockEvents)
            {
                var numberOfDataLockErrors = results.SelectMany(l => l.DataLockEvents).Count();
                if (numberOfDataLockErrors > 0)
                {
                    throw new Exception($"Did not expect any data lock errors, however found {numberOfDataLockErrors}");
                }
                return;
            }

            foreach (var expected in context.DataLockEvents)
            {
                var eventsForPriceEpisode = GetEventsForPriceEpisode(results, expected.PriceEpisodeIdentifier);
                var actual = eventsForPriceEpisode.FirstOrDefault(x => x.CommitmentId == expected.ApprenticeshipId);

                if (actual == null)
                {
                    var foundCommitmentIds = eventsForPriceEpisode.Any()
                        ? eventsForPriceEpisode.Select(x => x.CommitmentId.ToString()).Distinct().Aggregate((x, y) => $"{x}, {y}")
                        : "no commitment ids";
                    throw new Exception($"Expected event to have commitment id {expected.ApprenticeshipId} but actually had {foundCommitmentIds}");
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
                if (expected.ILrEffectiveFrom != actual.IlrPriceEffectiveFromDate)
                {
                    throw new Exception($"Expected event to have effective from date of {expected.ILrEffectiveFrom} but actually had {actual.IlrPriceEffectiveFromDate}");
                }
                if (expected.ILrEffectiveTo.HasValue && expected.ILrEffectiveTo != actual.IlrPriceEffectiveToDate)
                {
                    throw new Exception($"Expected event to have effective to date of {expected.ILrEffectiveTo} but actually had {actual.IlrPriceEffectiveToDate}");
                }
            }
        }
    }
}
