using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.TableParsers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class SubmissionDataLockSteps
    {
        public SubmissionDataLockSteps(SubmissionDataLockContext submissonDataLockContext)
        {
            SubmissionDataLockContext = submissonDataLockContext;
        }

        public SubmissionDataLockContext SubmissionDataLockContext { get; }

        [Then(@"the data lock status will be as follows:")]
        public void ThenTheDataLockStatusWillBeAsFollows(Table table)
        {
            SubmissionDataLockTableParser.ParseDataLockStatusTableIntoContext(SubmissionDataLockContext, Defaults.LearnerId, table);
        }

        [Then(@"the data lock status of the ILR in (.*) is:")] //what is the point of this date?
        public void ThenTheDataLockStatusWillBeAsFollowsOnSpecificDate(string specDate, Table table)
        {
            ThenTheDataLockStatusWillBeAsFollows(table);
        }
    }
}
