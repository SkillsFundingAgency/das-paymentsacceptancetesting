using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Assertions;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ExecutionManagers;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.TableParsers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class EarningAndPaymentSteps
    {
        public EarningAndPaymentSteps(EmployerAccountContext employerAccountContext, 
                                      EarningsAndPaymentsContext earningsAndPaymentsContext,
                                      DataLockContext dataLockContext,
                                      SubmissionContext submissionContext, 
                                      LookupContext lookupContext)
        {
            EmployerAccountContext = employerAccountContext;
            EarningsAndPaymentsContext = earningsAndPaymentsContext;
            DataLockContext = dataLockContext;
            SubmissionContext = submissionContext;
            LookupContext = lookupContext;
        }
        public EmployerAccountContext EmployerAccountContext { get; }
        public DataLockContext DataLockContext { get; }
        public EarningsAndPaymentsContext EarningsAndPaymentsContext { get; }
        public SubmissionContext SubmissionContext { get; }
        public LookupContext LookupContext { get; }


        [Then("the provider earnings and payments break down as follows:")]
        public void ThenProviderEarningAndPaymentsBreakDownTo(Table earningAndPayments)
        {
            ThenProviderEarningAndPaymentsBreakDownTo(Defaults.ProviderIdSuffix, earningAndPayments);
        }

        [Then("the earnings and payments break down for provider (.*) is as follows:")]
        public void ThenProviderEarningAndPaymentsBreakDownTo(string providerIdSuffix, Table earningAndPayments)
        {
            if (!SubmissionContext.HaveSubmissionsBeenDone)
            {
                SubmissionContext.SubmissionResults = SubmissionManager.SubmitIlrAndRunMonthEndAndCollateResults(SubmissionContext.IlrLearnerDetails, SubmissionContext.FirstSubmissionDate,
                    LookupContext, EmployerAccountContext.EmployerAccounts, SubmissionContext.ContractTypes, SubmissionContext.EmploymentStatus, SubmissionContext.LearningSupportStatus);
                SubmissionContext.HaveSubmissionsBeenDone = true;
            }

            var providerBreakdown = EarningsAndPaymentsContext.OverallEarningsAndPayments.SingleOrDefault(x => x.ProviderId == "provider " + providerIdSuffix);
            if (providerBreakdown == null)
            {
                providerBreakdown = new EarningsAndPaymentsBreakdown { ProviderId = "provider " + providerIdSuffix };
                EarningsAndPaymentsContext.OverallEarningsAndPayments.Add(providerBreakdown);
            }

            EarningAndPaymentTableParser.ParseEarningsAndPaymentsTableIntoContext(providerBreakdown, earningAndPayments);
            AssertResults();
        }

        [Then("the transaction types for the payments are:")]
        public void ThenTheTransactionTypesForEarningsAre(Table earningBreakdown)
        {
            ThenTheTransactionTypesForNamedProviderEarningsAre(Defaults.ProviderIdSuffix, earningBreakdown);
        }

        [Then("the transaction types for the payments for provider (.*) are:")]
        public void ThenTheTransactionTypesForNamedProviderEarningsAre(string providerIdSuffix, Table transactionTypes)
        {
            if (!SubmissionContext.HaveSubmissionsBeenDone)
            {
                SubmissionContext.SubmissionResults = SubmissionManager.SubmitIlrAndRunMonthEndAndCollateResults(SubmissionContext.IlrLearnerDetails, SubmissionContext.FirstSubmissionDate,
                    LookupContext, EmployerAccountContext.EmployerAccounts, SubmissionContext.ContractTypes, SubmissionContext.EmploymentStatus, SubmissionContext.LearningSupportStatus);
                SubmissionContext.HaveSubmissionsBeenDone = true;
            }

            TransactionTypeTableParser.ParseTransactionTypeTableIntoContext(EarningsAndPaymentsContext, $"provider {providerIdSuffix}", transactionTypes);
            AssertResults();
        }

        [Then(@"the provider earnings and payments break down for ULN (.*) as follows:")]
        public void ThenTheProviderEarningsAndPaymentsBreakDownForUlnAsFollows(string learnerId, Table earningAndPayments)
        {
            if (!SubmissionContext.HaveSubmissionsBeenDone)
            {
                SubmissionContext.SubmissionResults = SubmissionManager.SubmitIlrAndRunMonthEndAndCollateResults(SubmissionContext.IlrLearnerDetails, SubmissionContext.FirstSubmissionDate,
                    LookupContext, EmployerAccountContext.EmployerAccounts, SubmissionContext.ContractTypes, SubmissionContext.EmploymentStatus, SubmissionContext.LearningSupportStatus);
                SubmissionContext.HaveSubmissionsBeenDone = true;
            }

            var breakdown = new LearnerEarningsAndPaymentsBreakdown
            {
                ProviderId = Defaults.ProviderId, // This may not be true in every case, need to check specs
                LearnerId = learnerId
            };
            EarningsAndPaymentsContext.LearnerOverallEarningsAndPayments.Add(breakdown);
            EarningAndPaymentTableParser.ParseEarningsAndPaymentsTableIntoContext(breakdown, earningAndPayments);
            AssertResults();
        }

        private void AssertResults()
        {
            PaymentsAndEarningsAssertions.AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext, SubmissionContext, EmployerAccountContext);
            TransactionTypeAssertions.AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext, SubmissionContext, EmployerAccountContext);
            //DataLockAssertions.AssertPaymentsAndEarningsResults(DataLockContext, SubmissionContext);
        }
    }
}
