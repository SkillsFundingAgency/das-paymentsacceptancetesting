using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Translators;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class EarningsSteps
    {
        public EarningsSteps(EarningContext earningContext)
        {
            EarningContext = earningContext;
        }

        public EarningContext EarningContext { get; set; }


        [When(@"an ILR file is submitted with the following data:")]
        public void WhenAnILRFileIsSubmittedWithTheFollowingData(Table table)
        {
            EarningContext.IlrStartDate = DateTime.Parse(table.Rows[0][0]);
            EarningContext.IlrPlannedEndDate = DateTime.Parse(table.Rows[0][1]);
            EarningContext.IlrActualEndDate = string.IsNullOrWhiteSpace(table.Rows[0][2]) ? null : (DateTime?)DateTime.Parse(table.Rows[0][2]);
            EarningContext.IlrCompletionStatus = IlrTranslator.TranslateCompletionStatus(table.Rows[0][2]);

            //TODO: Submit ILRs for each academic year and aggregate the results in context
        }

        [Then(@"the provider earnings break down as follows:")]
        public void ThenTheProviderEarningsBreakDownAsFollows(Table table)
        {
            //TODO: Assert earnings match table
            ScenarioContext.Current.Pending();
        }
    }
}
