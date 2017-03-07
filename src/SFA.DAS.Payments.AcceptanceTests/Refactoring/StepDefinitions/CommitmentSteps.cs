using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ExecutionManagers;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.TableParsers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.StepDefinitions
{
    [Binding]
    public class CommitmentSteps
    {
        public CommitmentSteps(CommitmentsContext commitmentsContext)
        {
            CommitmentsContext = commitmentsContext;
        }
        public CommitmentsContext CommitmentsContext { get; }

        [Given("the following commitments exist:")]
        public void GivenCommitmentsExistForLearners(Table commitments)
        {
            CommitmentsTableParser.ParseCommitmentsIntoContext(CommitmentsContext, commitments);
            foreach (var commitment in CommitmentsContext.Commitments)
            {
                CommitmentManager.AddCommitment(commitment);
            }
        }

        [Given("the following commitments exist on (.*):")] // do we really care about the date?
        public void GivenCommitmentsExistForLearnersAtSpecificDate(string specDate, Table commitments)
        {
            GivenCommitmentsExistForLearners(commitments);
        }
    }
}
