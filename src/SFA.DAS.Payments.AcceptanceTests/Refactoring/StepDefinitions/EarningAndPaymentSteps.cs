using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.TableParsers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.StepDefinitions
{
    [Binding]
    public class EarningAndPaymentSteps
    {

        public EarningAndPaymentSteps(EarningsAndPaymentsContext earningsAndPaymentsContext)
        {
            EarningsAndPaymentsContext = earningsAndPaymentsContext;
        }
        public EarningsAndPaymentsContext EarningsAndPaymentsContext { get; }


        [Then("the provider earnings and payments break down as follows:")]
        public void ThenProviderEarningAndPaymentsBreakDownTo(Table earningAndPayments)
        {
            ThenProviderEarningAndPaymentsBreakDownTo(Defaults.ProviderIdSuffix, earningAndPayments);
        }

        [Then("the earnings and payments break down for provider (.*) is as follows:")]
        public void ThenProviderEarningAndPaymentsBreakDownTo(string providerIdSuffix, Table earningAndPayments)
        {
            EarningAndPaymentTableParser.ParseEarningsAndPaymentsTableIntoContext(EarningsAndPaymentsContext.OverallEarningsAndPayments, earningAndPayments);
        }

        [Then("the transaction types for the payments are:")]
        public void ThenTheTransactionTypesForEarningsAre(Table earningBreakdown)
        {
            ThenTheTransactionTypesForNamedProviderEarningsAre(Defaults.ProviderId, earningBreakdown);
        }

        [Then("the transaction types for the payments for provider (.*) are:")]
        public void ThenTheTransactionTypesForNamedProviderEarningsAre(string providerIdSuffix, Table transactionTypes)
        {
            TransactionTypeTableParser.ParseTransactionTypeTableIntoContext(EarningsAndPaymentsContext, $"provider {providerIdSuffix}", transactionTypes);
        }

        [Then(@"the provider earnings and payments break down for ULN (.*) as follows:")]
        public void ThenTheProviderEarningsAndPaymentsBreakDownForUlnAsFollows(string uln, Table earningAndPayments)
        {
            var breakdown = new LearnerEarningsAndPaymentsBreakdown
            {
                Uln = uln
            };
            EarningsAndPaymentsContext.LearnerOverallEarningsAndPayments.Add(breakdown);
            EarningAndPaymentTableParser.ParseEarningsAndPaymentsTableIntoContext(breakdown, earningAndPayments);
        }
    }
}
