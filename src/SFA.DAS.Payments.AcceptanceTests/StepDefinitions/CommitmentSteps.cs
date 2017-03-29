using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ExecutionManagers;
using SFA.DAS.Payments.AcceptanceTests.TableParsers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class CommitmentSteps
    {
        public CommitmentSteps(CommitmentsContext commitmentsContext, LookupContext lookupContext)
        {
            CommitmentsContext = commitmentsContext;
            LookupContext = lookupContext;
        }
        public CommitmentsContext CommitmentsContext { get; }
        public LookupContext LookupContext { get; }

        [Given("the following commitments exist:")]
        public void GivenCommitmentsExistForLearners(Table commitments)
        {
            CommitmentsTableParser.ParseCommitmentsIntoContext(CommitmentsContext, commitments, LookupContext);
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
