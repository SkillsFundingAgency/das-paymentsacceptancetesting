using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.TableParsers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class DataLockSteps
    {
        public DataLockSteps(DataLockContext dataLockContext)
        {
            DataLockContext = dataLockContext;
        }

        public DataLockContext DataLockContext { get; }

        [Given(@"the following commitment exists for an apprentice:")]
        public void GivenTheFollowingCommitmentExistsForAnApprentice(Table table)
        {
        }

        [Then(@"the following data lock event is returned:")]
        public void ThenTheFollowingDataLockEventIsReturned(Table table)
        {
        }

        [Then(@"the data lock event has the following errors:")]
        public void ThenTheDataLockEventHasTheFollowingErrors(Table table)
        {
        }

        [Then(@"the data lock event has the following periods")]
        public void ThenTheDataLockEventHasTheFollowingPeriods(Table table)
        {
        }

        [Then(@"the data lock event used the following commitments")]
        public void ThenTheDataLockEventUsedTheFollowingCommitments(Table table)
        {
        }

    }
}