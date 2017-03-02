using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.StepDefinitions
{
    [Binding]
    public class SubmissionSteps
    {
        [When("an ILR file is submitted with the following data:")]
        public void WhenAnIlrFileIsSubmitted(Table ilrDetails)
        {

        }

        [When("the providers submit the following ILR files:")]
        public void WhenMultipleIlrFilesAreSubmitted(Table ilrDetails)
        {

        }
    }
}
