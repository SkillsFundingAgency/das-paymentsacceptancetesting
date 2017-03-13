using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.StepDefinitions.FeatureSteps
{
    [Binding]
    [Scope(Feature = "Datalock validation fails for different reasons")]
    public class DataLockFeatureSteps
    {
        [Given("the following commitment exists for an apprentice:")]
        [Given("the following commitments exist for an apprentice:")]
        public void GivenTheFollowingExists(Table table)
        {
            //TODO
        }



        [When("an ILR file is submitted with the following data:")]
        public void WhenAnIlrFileIsSubmittedWithTheFollowingData(Table table)
        {
            //TODO
        }

        [When("monthly payment process runs for the following ILR data:")]
        public void WhenMonthlyProcessRunsForTheFollowingData(Table table)
        {
            //TODO
        }



        [Then(@"a datalock error (.*) is produced")]
        public void ThenADataLockErrorIsProduced(string expectedDataLockCode)
        {
            //TODO
        }
    }
}
