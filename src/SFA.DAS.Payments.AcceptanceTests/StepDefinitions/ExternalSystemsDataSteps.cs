using SFA.DAS.Payments.AcceptanceTests.ExecutionManagers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class ExternalSystemsDataSteps
    {
        [Given("the apprenticeship funding band maximum for each learner is (.*)")]
        [Given(@"the apprenticeship funding band maximum is (.*)")] // Duplicate?
        public void GivenFundingBandMax(int fundingBandMax)
        {
            ReferenceDataManager.SetFundingBandMax(fundingBandMax);
        }
    }
}
