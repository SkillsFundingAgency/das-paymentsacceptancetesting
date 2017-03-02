using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.StepDefinitions
{
    [Binding]
    public class EarningAndPaymentSteps
    {
        [Then("the provider earnings and payments break down as follows:")]
        public void ThenProviderEarningAndPaymentsBreakDownTo(Table earningAndPayments)
        {

        }

        [Then("the earnings and payments break down for provider (.*) is as follows:")]
        public void ThenProviderEarningAndPaymentsBreakDownTo(string providerIdSuffix, Table earningAndPayments)
        {

        }
    }
}
