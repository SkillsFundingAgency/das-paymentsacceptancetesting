using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions.FeatureSteps
{
    [Binding]
    [Scope(Feature = "Calculate the payments")]
    public class PaymentsCalcFeatureSteps
    {
        [Given(@"an employer levy balance of (.*)")]
        public void GivenAnEmployerLevyBalanceOf(int p0)
        {
            //TODO
        }


        [When(@"a payment of (.*) is due")]
        public void WhenAPaymentOfIsDue(int p0)
        {
            //TODO
        }


        [Then(@"the employer levy account is debited by (.*)")]
        public void ThenTheEmployerLevyAccountIsDebitedBy(int p0)
        {
            //TODO
        }

        [Then(@"the provider is paid (.*) by the SFA")]
        public void ThenTheProviderIsPaidByTheSFA(int p0)
        {
            //TODO
        }

        [Then(@"the provider is due (.*) from the employer")]
        public void ThenTheProviderIsDueFromTheEmployer(int p0)
        {
            //TODO
        }

    }
}
