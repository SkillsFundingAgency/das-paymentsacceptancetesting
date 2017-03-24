using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.TableParsers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.StepDefinitions
{
    [Binding]
    public class DataLockSteps
    {
        public DataLockSteps(DataLockContext dataLockContext)
        {
            DataLockContext = dataLockContext;
        }

        public DataLockContext DataLockContext { get; }

        [Then(@"the data lock status will be as follows:")]
        public void ThenTheDataLockStatusWillBeAsFollows(Table table)
        {
            DataLockTableParser.ParseDataLockStatusTableIntoContext(DataLockContext, Defaults.LearnerId, table);
        }

        [Then(@"the data lock status of the ILR in (.*) is:")] //what is the point of this date?
        public void ThenTheDataLockStatusWillBeAsFollowsOnSpecificDate(string specDate, Table table)
        {
            ThenTheDataLockStatusWillBeAsFollows(table);
        }
    }
}