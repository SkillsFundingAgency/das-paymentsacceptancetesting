using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.StepDefinitions
{
    [Binding]
    public class UntestedSteps
    {
        [Given("the apprenticeship funding band maximum for each learner is (.*)")]
        [Given(@"the apprenticeship funding band maximum is (.*)")] // Duplicate?
        public void GivenFundingBandMax(int fundingBandMax)
        {
        }

        [Given("The learner is programme only DAS")]
        [Given("Two learners are programme only DAS")]
        public void GivenTheLearnerIsProgrammeOnlyDas()
        {
        }

        [Given("the learner changes employers")]
        public void GivenTheLearnerChangesEmployers(Table employmentDates)
        {
        }




        [Then(@"the data lock status will be as follows:")]
        public void ThenTheDataLockStatusWillBeAsFollows(Table table)
        {
            //TODO
            // To be refactored
        }

        [Then(@"the data lock status of the ILR in (.*) is:")] //what is the point of this date?
        public void ThenTheDataLockStatusWillBeAsFollowsOnSpecificDate(string specDate, Table table)
        {
            //TODO
            // To be refactored
        }
    }
}
