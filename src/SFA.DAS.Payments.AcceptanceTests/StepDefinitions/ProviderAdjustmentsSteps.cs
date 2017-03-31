using SFA.DAS.Payments.AcceptanceTests.Contexts;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class ProviderAdjustmentsSteps
    {
        public ProviderAdjustmentsSteps(ProviderAdjustmentsContext providerAdjustmentsContext)
        {
            ProviderAdjustmentsContext = providerAdjustmentsContext;
        }

        public ProviderAdjustmentsContext ProviderAdjustmentsContext { get; }

        [Given("that the previous EAS entries for a provider are as follows:")]
        public void GivenThatThePreviousEasEntrierForAProviderAreAsFollows(Table table)
        {
            
        }

        [When("the following EAS entries are submitted:")]
        public void WhenTheFollowingEasEntriesAreSubmitted(Table table)
        {
            
        }

        [Then("the following adjustments will be generated:")]
        public void ThenTheFollowingAdjustmentsWillBeGenerated(Table table)
        {
            
        }
    }
}