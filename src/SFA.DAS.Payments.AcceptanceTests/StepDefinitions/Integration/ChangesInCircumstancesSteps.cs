using System;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
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
            var ilrSubmissionDate = DateTime.Parse(date);
            ProcessIlrFileSubmissions(table, actualSubmissionDate: ilrSubmissionDate);
        }

        [Then(@"the data lock status of the ILR in (.*) is:")]
        public void ThenTheDataLockStatusOfTheIlrPriceEpisodesIs(string date, Table table)
        {
        }

        private void ProcessIlrFileSubmissions(Table table, DateTime? actualSubmissionDate = null)
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
                    SubmitIlr(provider.Ukprn, provider.Learners, academicYear, date, processService, provider.EarnedByPeriod);
                }

                SubmitMonthEnd(date, processService);

                date = date.AddDays(15).NextCensusDate();
            }
        }
    }
}