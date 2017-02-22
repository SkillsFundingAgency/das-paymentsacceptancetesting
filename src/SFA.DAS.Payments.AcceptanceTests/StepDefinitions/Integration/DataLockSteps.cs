using System;
using System.Linq;
using NUnit.Framework;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.Entities;
using SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment;
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
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            //foreach (var column in table.Header)
            //{
            //    var entity = new SpecFlowEntity
            //    {
            //        Name = "datalock",
            //        Field = column.Trim(),
            //        Type = "column"
            //    };

            //    SpecFlowEntitiesDataHelper.AddEntityRow(entity, environmentVariables);
            //}

            foreach (var row in table.Rows)
            {
                foreach (var key in row.Keys)
                {
                    var entity = new SpecFlowEntity
                    {
                        Name = "datalock status",
                        Field = key,
                        Type = "column"
                    };

                    SpecFlowEntitiesDataHelper.AddEntityRow(entity, environmentVariables);
                }

                var keyEntity = new SpecFlowEntity
                {
                    Name = "datalock status",
                    Field = row[0],
                    Type = "key"
                };

                SpecFlowEntitiesDataHelper.AddEntityRow(keyEntity, environmentVariables);
            }
        }

        [Then(@"the data lock status will be as follows:")]
        public void ThenTheDataLockStatusWillBeAsFollows(Table table)
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            //foreach (var column in table.Header)
            //{
            //    var entity = new SpecFlowEntity
            //    {
            //        Name = "datalock",
            //        Field = column.Trim(),
            //        Type = "column"
            //    };

            //    SpecFlowEntitiesDataHelper.AddEntityRow(entity, environmentVariables);
            //}

            foreach (var row in table.Rows)
            {
                foreach (var key in row.Keys)
                {
                    var entity = new SpecFlowEntity
                    {
                        Name = "datalock status",
                        Field = key,
                        Type = "column"
                    };

                    SpecFlowEntitiesDataHelper.AddEntityRow(entity, environmentVariables);
                }

                var keyEntity = new SpecFlowEntity
                {
                    Name = "datalock status",
                    Field = row[0],
                    Type = "key"
                };

                SpecFlowEntitiesDataHelper.AddEntityRow(keyEntity, environmentVariables);
            }
        }


        [Then(@"a (.*) error message will be produced")]
        public void ThenADataLockErrorMessageWillBeProduced(string errorCode)
        {
            var provider = StepDefinitionsContext.GetDefaultProvider();

            var validationError = ValidationErrorsDataHelper.GetValidationErrors(provider.Ukprn, EnvironmentVariables);

            Assert.IsNotNull(validationError, "There is no validation error entity present");
            Assert.IsTrue(validationError.Any(x => x.RuleId == errorCode));
        }

        private void VerifyProviderDataLockMatchesForPeriod(string period, Table table, Provider provider)
        {
            var commitmentMatchesRow = table.Rows.RowWithKey(RowKeys.DataLockMatchingCommitment);
            var priceMatchesRow = table.Rows.RowWithKey(RowKeys.DataLockMatchingPrice);

            var periodMatches = provider.DataLockMatchesByPeriod[period];

            foreach (var learningDelivery in provider.Learners[0].LearningDeliveries)
            {
                var priceEpisodes = learningDelivery.PriceEpisodes;

                foreach (var priceEpisode in priceEpisodes)
                {
                    if (period.GetCensusDate() >= priceEpisode.StartDate)
                    {
                        VerifyDataLockCommitmentMatchesForPeriod(priceEpisode, commitmentMatchesRow, periodMatches, period);
                        VerifyDataLockPriceMatchesForPeriod(priceEpisode, priceMatchesRow, periodMatches, period);
                    }
                }
            }
        }

        private bool ExpectingDataLockMatchForPriceEpisode(string priceEpisodeKey, TableRow matchesRow)
        {
            return !string.IsNullOrWhiteSpace(matchesRow[priceEpisodeKey]);
        }

        private void VerifyDataLockCommitmentMatchesForPeriod(PriceEpisode priceEpisode, TableRow commitmentMatchesRow, DataLockMatch[] periodMatches, string period)
        {
            if (commitmentMatchesRow == null)
            {
                return;
            }

            if (commitmentMatchesRow.ContainsKey(priceEpisode.DataLockMatchKey))
            {
                if (ExpectingDataLockMatchForPriceEpisode(priceEpisode.DataLockMatchKey, commitmentMatchesRow))
                {
                    var matchingValue = commitmentMatchesRow[priceEpisode.DataLockMatchKey];

                    var priceEpisodeActualMatches = periodMatches
                        .Where(
                            m =>
                                m.PriceEpisodeId == priceEpisode.Id)
                        .ToArray();

                    Assert.AreEqual(1, priceEpisodeActualMatches.Length,
                        $"Expecting to find a data lock match for employer {matchingValue} in period {period} and the price episode that spans {priceEpisode.DataLockMatchKey}.");

                    var commitments = StepDefinitionsContext.ReferenceDataContext.Commitments
                        .Where(c => c.Id == priceEpisodeActualMatches[0].CommitmentId)
                        .Select(c => new { Id = c.Id, Employer = c.Employer })
                        .Distinct()
                        .ToArray();

                    Assert.AreEqual(1, commitments.Length,
                        $"Expecting to find a matching commitment for period {period} and the price episode that spans {priceEpisode.DataLockMatchKey}.");

                    long matchingValueLong;
                    if (long.TryParse(matchingValue, out matchingValueLong))
                    {
                        Assert.AreEqual(matchingValueLong, commitments[0].Id,
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

        private void VerifyDataLockPriceMatchesForPeriod(PriceEpisode priceEpisode, TableRow priceMatchesRow, DataLockMatch[] periodMatches, string period)
        {
            if (priceMatchesRow == null)
            {
                return;
            }

            if (priceMatchesRow.ContainsKey(priceEpisode.DataLockMatchKey))
            {
                if (ExpectingDataLockMatchForPriceEpisode(priceEpisode.DataLockMatchKey, priceMatchesRow))
                {
                    var matchingValue = decimal.Parse(priceMatchesRow[priceEpisode.DataLockMatchKey]);

                    var priceEpisodeActualMatches = periodMatches
                        .Where(
                            m =>
                                m.PriceEpisodeId == priceEpisode.Id)
                        .ToArray();

                    Assert.AreEqual(1, priceEpisodeActualMatches.Length,
                        $"Expecting to find a data lock match for employer {matchingValue} in period {period} and the price episode that spans {priceEpisode.DataLockMatchKey}.");

                    var commitments = StepDefinitionsContext.ReferenceDataContext.Commitments
                        .Where(c => c.Id == priceEpisodeActualMatches[0].CommitmentId && c.AgreedPrice == priceEpisodeActualMatches[0].Price)
                        .Select(c => new { Price = c.AgreedPrice })
                        .Distinct()
                        .ToArray();

                    Assert.AreEqual(1, commitments.Length,
                        $"Expecting to find a matching commitment for period {period} and the price episode that spans {priceEpisode.DataLockMatchKey}.");

                    Assert.AreEqual(matchingValue, commitments[0].Price,
                        $"Expecting to find a matching commitment for employer {matchingValue} in period {period} for a price episode that spans {priceEpisode.DataLockMatchKey}.");
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
}