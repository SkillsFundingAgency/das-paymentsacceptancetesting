using SFA.DAS.Payments.AcceptanceTests.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Intermediate
{
    [Binding]
    public class DataLockStepDefinitions    : BaseStepDefinitions
    {
        public DataLockStepDefinitions(StepDefinitionsContext context)
            :base(context)
        {

        }

        [Given(@"There is no employer data in the committments")]
        public void GivenThereIsNoEmployerDataInTheCommittments()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"an ILR file is submitted with the following data for UKPRN (.*):")]
        public void WhenAnILRFileIsSubmittedWithTheFollowingDataForUKPRN(long ukprn, Table table)
        {
            // Store spec values in context
            SetupContextProviders(table);
            SetupContexLearners(table);

            // Setup reference data
            SetupReferenceData();
        }


        [Then(@"a datalock error (.*) is produced")]
        public void ThenADatalockErrorOfDLOCK_WillBeProduced(string errorCode)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
