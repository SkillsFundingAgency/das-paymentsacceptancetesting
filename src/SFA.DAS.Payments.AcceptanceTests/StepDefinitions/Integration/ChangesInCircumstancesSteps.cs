using System;
using System.Linq;
using NUnit.Framework;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Entities;
using SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment;
using SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Base;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Integration
{
    [Binding]
    public class ChangesInCircumstancesSteps : BaseStepDefinitions
    {
        public ChangesInCircumstancesSteps(StepDefinitionsContext stepDefinitionsContext)
            : base(stepDefinitionsContext)
        {
        }

        [When(@"an ILR file is submitted on (.*) with the following data:")]
        public void WhenAnIlrFileIsSubmittedOnADayWithTheFollowingData(string date, Table table)
        {
            ProcessIlrFileSubmissions(table);
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

        private void ProcessIlrFileSubmissions(Table table)
        {
            SetupContextProviders(table);
            SetupContexLearners(table);

            var startDate = StepDefinitionsContext.GetIlrStartDate().NextCensusDate();
            ProcessMonths(startDate);
        }

        private void ProcessMonths(DateTime start)
        {
            var processService = new ProcessService(new TestLogger());

            var periodId = 1;
            var date = start.NextCensusDate();
            var endDate = StepDefinitionsContext.GetIlrEndDate();
            var lastCensusDate = endDate.NextCensusDate();

            while (date <= lastCensusDate)
            {
                var period = date.GetPeriod();

                SetupPeriodReferenceData(date);

                UpdateAccountsBalances(period);
                UpdateCommitmentsPaymentStatuses(date);

                var academicYear = date.GetAcademicYear();

                SetupEnvironmentVariablesForMonth(date, academicYear, ref periodId);

                foreach (var provider in StepDefinitionsContext.Providers)
                {
                    SubmitIlr(provider.Ukprn, provider.Learners, academicYear, date, processService, provider.EarnedByPeriod, provider.DataLockMatchesByPeriod);
                }

                SubmitMonthEnd(date, processService);

                date = date.AddDays(15).NextCensusDate();
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
                        var employer = matchesRow[priceEpisode.DataLockMatchKey];

                        var priceEpisodeActualMatches = periodMatches
                            .Where(
                                m =>
                                    m.PriceEpisodeId == priceEpisode.Id &&
                                    m.PriceEpisodeStartDate == priceEpisode.StartDate)
                            .ToArray();

                        Assert.AreEqual(1, priceEpisodeActualMatches.Length,
                            $"Expecting to find a data lock match for employer {employer} in period {period} and the price episode that spans {priceEpisode.DataLockMatchKey}.");

                        var commitments = StepDefinitionsContext.ReferenceDataContext.Commitments
                            .Where(c => c.Id == priceEpisodeActualMatches[0].CommitmentId)
                            .ToArray();

                        Assert.AreEqual(1, commitments.Length,
                            $"Expecting to find a matching commitment for period {period} and the price episode that spans {priceEpisode.DataLockMatchKey}.");

                        Assert.AreEqual(employer, commitments[0].Employer,
                            $"Expecting to find a matching commitment for employer {employer} in period {period} for a price episode that spans {priceEpisode.DataLockMatchKey}.");
                    }
                    else
                    {
                        var priceEpisodeActualMatches = periodMatches
                            .Where(
                                m =>
                                    m.PriceEpisodeId == priceEpisode.Id &&
                                    m.PriceEpisodeStartDate == priceEpisode.StartDate)
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