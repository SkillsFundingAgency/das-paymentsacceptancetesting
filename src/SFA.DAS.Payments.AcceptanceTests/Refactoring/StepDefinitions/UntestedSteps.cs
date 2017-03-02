using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.StepDefinitions
{
    [Binding]
    public class UntestedSteps
    {
        [Given("the apprenticeship funding band maximum for each learner is (.*)")]
        public void GivenFundingBandMax(int fundingBandMax)
        {
        }
    }
}
