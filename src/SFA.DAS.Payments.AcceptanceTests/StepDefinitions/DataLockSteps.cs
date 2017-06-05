using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Assertions;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ExecutionManagers;
using SFA.DAS.Payments.AcceptanceTests.TableParsers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class DataLockSteps
    {
        public DataLockSteps(DataLockContext dataLockContext, CommitmentsContext commitmentsContext, SubmissionContext submissionContext, EmployerAccountContext employerAccountContext, LookupContext lookupContext)
        {
            DataLockContext = dataLockContext;
            CommitmentsContext = commitmentsContext;
            SubmissionContext = submissionContext;
            EmployerAccountContext = employerAccountContext;
            LookupContext = lookupContext;
        }

        public DataLockContext DataLockContext { get; }
        public CommitmentsContext CommitmentsContext { get; }
        public SubmissionContext SubmissionContext { get; }
        public EmployerAccountContext EmployerAccountContext { get; }
        public LookupContext LookupContext { get; }

        [Then(@"the following data lock event is returned:")]
        public void ThenTheFollowingDataLockEventIsReturned(Table table)
        {
            EnsureSubmissionsHaveHappened();

            DataLockEventsTableParser.ParseDataLockEventsIntoContext(DataLockContext, table, LookupContext);

            DataLockAssertions.AssertDataLockOutput(DataLockContext, SubmissionContext.SubmissionResults.ToArray());
        }

        [Then("no data lock event is returned")]
        public void ThenNoDataLockEventIsReturned()
        {
            EnsureSubmissionsHaveHappened();

            DataLockContext.ExpectsNoDataLockEvents = true;

            DataLockAssertions.AssertDataLockOutput(DataLockContext, SubmissionContext.SubmissionResults.ToArray());
        }

        [Then(@"the data lock event has the following errors:")]
        public void ThenTheDataLockEventHasTheFollowingErrors(Table table)
        {
            EnsureSubmissionsHaveHappened();

            DataLockEventErrorsTableParser.ParseDataLockEventErrorsIntoContext(DataLockContext, table, LookupContext);

            DataLockAssertions.AssertDataLockOutput(DataLockContext, SubmissionContext.SubmissionResults.ToArray());
        }

        [Then(@"the data lock event has the following periods")]
        public void ThenTheDataLockEventHasTheFollowingPeriods(Table table)
        {
            EnsureSubmissionsHaveHappened();

            DataLockEventPeriodTableParser.ParseDataLockEventPeriodsIntoContext(DataLockContext, table, LookupContext);

            DataLockAssertions.AssertDataLockOutput(DataLockContext, SubmissionContext.SubmissionResults.ToArray());
        }

        [Then(@"the data lock event used the following commitments")]
        public void ThenTheDataLockEventUsedTheFollowingCommitments(Table table)
        {
            EnsureSubmissionsHaveHappened();

            DataLockEventCommitmentsTableParser.ParseDataLockEventCommitmentsIntoContext(DataLockContext, table, LookupContext);

            DataLockAssertions.AssertDataLockOutput(DataLockContext, SubmissionContext.SubmissionResults.ToArray());
        }


        private void EnsureSubmissionsHaveHappened()
        {
            if (!SubmissionContext.HaveSubmissionsBeenDone)
            {
                var periodsToSubmitTo = new[]
                {
                    //SubmissionContext.IlrLearnerDetails.Min(x => x.StartDate).ToString("MM/yy")
                    CommitmentsContext.Commitments.Max(x=>x.EffectiveFrom).ToString("MM/yy")
                };
                SubmissionContext.SubmissionResults = SubmissionManager.SubmitIlrAndRunMonthEndAndCollateResults(SubmissionContext.IlrLearnerDetails, SubmissionContext.FirstSubmissionDate,
                    LookupContext, EmployerAccountContext.EmployerAccounts, SubmissionContext.ContractTypes, SubmissionContext.EmploymentStatus, SubmissionContext.LearningSupportStatus, periodsToSubmitTo);
                SubmissionContext.HaveSubmissionsBeenDone = true;
            }
        }

    }
}