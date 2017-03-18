using SFA.DAS.Payments.AcceptanceTests.Refactoring.ExecutionManagers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.StepDefinitions
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
