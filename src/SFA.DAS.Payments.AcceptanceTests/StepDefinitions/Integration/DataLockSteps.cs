using System;
using System.Linq;
using NUnit.Framework;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Entities;
using SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Base;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Integration
{
    [Binding]
    public class DataLockSteps : BaseStepDefinitions
    {
        public DataLockSteps(StepDefinitionsContext stepDefinitionsContext)
            : base(stepDefinitionsContext)
        {
        }

        [Then(@"the data lock status of the ILR in (.*) is:")]
        public void ThenTheDataLockStatusOfTheIlrPriceEpisodesIs(string date, Table table)
        {
            var period = DateTime.Parse(date).AddMonths(-1).GetPeriod();

            var matchesRow = table.Rows.RowWithKey(RowKeys.DataLockMatchingCommitment);

            foreach (var provider in StepDefinitionsContext.Providers)
            {
                VerifyProviderDataLockMatchesForPeriod(period, matchesRow, provider);
            }
        }

        private void VerifyProviderDataLockMatchesForPeriod(string period, TableRow matchesRow, Provider provider)
        {
            var periodMatches = provider.DataLockMatchesByPeriod[period];
            var priceEpisodes = provider.Learners[0].LearningDelivery.PriceEpisodes;

            foreach (var priceEpisode in priceEpisodes)
            {
                if (matchesRow.ContainsKey(priceEpisode.DataLockMatchKey))
                {
                    if (ExpectingDataLockMatchForPriceEpisode(priceEpisode.DataLockMatchKey, matchesRow))
                    {
                        var matchingValue = matchesRow[priceEpisode.DataLockMatchKey];

                        var priceEpisodeActualMatches = periodMatches
                            .Where(
                                m =>
                                    m.PriceEpisodeId == priceEpisode.Id)
                            .ToArray();

                        Assert.AreEqual(1, priceEpisodeActualMatches.Length,
                            $"Expecting to find a data lock match for employer {matchingValue} in period {period} and the price episode that spans {priceEpisode.DataLockMatchKey}.");

                        var commitments = StepDefinitionsContext.ReferenceDataContext.Commitments
                            .Where(c => c.Id == priceEpisodeActualMatches[0].CommitmentId)
                            .ToArray();

                        Assert.AreEqual(1, commitments.Length,
                            $"Expecting to find a matching commitment for period {period} and the price episode that spans {priceEpisode.DataLockMatchKey}.");

                        if (!string.IsNullOrEmpty(commitments[0].ComitmentIdenifier))
                        {
                           
                            Assert.AreEqual(matchingValue, commitments[0].ComitmentIdenifier,
                                $"Expecting to find a matching commitment for commitment id  {matchingValue} in period {period} for a price episode that spans {priceEpisode.DataLockMatchKey}.");
                        }
                        else
                        {
                            Assert.AreEqual(matchingValue, commitments[0].Employer,
                                $"Expecting to find a matching commitment for employer {matchingValue} in period {period} for a price episode that spans {priceEpisode.DataLockMatchKey}.");

                        }
                    }
                    else
                    {
                        var priceEpisodeActualMatches = periodMatches
                            .Where(
                                m =>
                                    m.PriceEpisodeId == priceEpisode.Id)
                            .ToArray();

                        Assert.AreEqual(0, priceEpisodeActualMatches.Length,
                            $"Expecting no data lock match for period {period} for a price episode that spans {priceEpisode.DataLockMatchKey}.");
                    }
                }
            }
        }

        private bool ExpectingDataLockMatchForPriceEpisode(string priceEpisodeKey, TableRow matchesRow)
        {
            return !string.IsNullOrWhiteSpace(matchesRow[priceEpisodeKey]);
        }
    }
}