using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.StepDefinitions
{
    [Binding]
    public class SubmissionSteps
    {
        [When("an ILR file is submitted with the following data:")]
        [When(@"an ILR file is submitted every month with the following data:")] //Duplicate?
        public void WhenAnIlrFileIsSubmitted(Table ilrDetails)
        {
            //TODO
        }

        [When("the providers submit the following ILR files:")]
        public void WhenMultipleIlrFilesAreSubmitted(Table ilrDetails)
        {
            //TODO
        }

        [When("an ILR file is submitted for the first time on (.*) with the following data:")]
        [When(@"an ILR file is submitted on (.*) with the following data:")]
        public void WhenIlrSubmittedOnSpecificDate(string specSumissionDate, Table ilrDetails)
        {
            //TODO
        }

        [When("the Contract type in the ILR is:")]
        public void WhenTheContractTypeInTheIlrIs(Table contractTypes)
        {
            //TODO
        }

        [When("the employment status in the ILR is:")]
        public void WhenTheEmploymentStatusInTheIlrIs(Table contractTypes)
        {
            //TODO
        }

        [Then(@"the data lock status will be as follows:")]
        public void ThenTheDataLockStatusWillBeAsFollows(Table table)
        {
            //TODO
        }

        [Then(@"the data lock status of the ILR in (.*) is:")] //what is the point of this date?
        public void ThenTheDataLockStatusWillBeAsFollowsOnSpecificDate(string specDate, Table table)
        {
            //TODO
        }
    }
}
