using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.StepDefinitions.FeatureSteps
{
    [Binding]
    [Scope(Feature = "Amount due is calculated based on previously earned amount")]
    public class EarningsCalcFeatureSteps
    {
        [Given(@"a provider has previously earned (.*) in period R(.*)")]
        public void GivenAProviderHasPreviouslyEarned(decimal amountEarned, string inPeriod)
        {
            //TODO
        }



        [When(@"an earning of (.*) is calculated for period R(.*)")]
        public void WhenAnEarningIsCalculated(decimal amountEarned, string inPeriod)
        {
            //TODO
        }

        [When(@"the planned course duration covers (.*) months")]
        public void WhenPlannedCourseDurationCover(int numberOfMonths)
        {
            //TODO
        }

        [When(@"there is an agreed price of (.*)")]
        public void WhenThereIsAnAgreedPriceOf(decimal agreedPrice)
        {
            //TODO
        }

        [When(@"the actual duration of learning is (.*) months")]
        public void WhenTheActualDurationOfLearningIsMonths(int numberOfMonths)
        {
            //TODO
        }



        [Then(@"a payment of (.*) is due")]
        public void ThenPaymentIsDue(decimal paymentDue)
        {
            //TODO
        }

        [Then(@"the monthly earnings is (.*)")]
        public void ThenTheMonthlyEarningsIs(decimal earnedAmount)
        {
            //TODO
        }

        [Then(@"the completion payment is (.*)")]
        public void ThenTheCompletionPaymentIs(decimal paymentDue)
        {
            //TODO
        }

        [Then(@"the balancing payment is (.*)")]
        public void ThenTheBalancingPaymentIs(decimal paymentDue)
        {
            //TODO
        }
    }
}
