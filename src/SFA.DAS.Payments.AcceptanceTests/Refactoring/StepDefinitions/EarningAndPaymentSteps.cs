using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Assertions;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ExecutionManagers;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.TableParsers;
using TechTalk.SpecFlow;
using System;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.StepDefinitions
{
    [Binding]
    public class EarningAndPaymentSteps
    {
        public EarningAndPaymentSteps(EmployerAccountContext employerAccountContext,
                                    EarningsAndPaymentsContext earningsAndPaymentsContext, 
                                    SubmissionContext submissionContext, 
                                    LookupContext lookupContext,
                                    CommitmentsContext commitmentsContext)
        {
            EmployerAccountContext = employerAccountContext;
            EarningsAndPaymentsContext = earningsAndPaymentsContext;
            SubmissionContext = submissionContext;
            LookupContext = lookupContext;
            CommitmentsContext = commitmentsContext;
        }
        public EmployerAccountContext EmployerAccountContext { get; }
        public EarningsAndPaymentsContext EarningsAndPaymentsContext { get; }
        public SubmissionContext SubmissionContext { get; }
        public LookupContext LookupContext { get; }
        public CommitmentsContext CommitmentsContext { get; }



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
                SubmissionContext.SubmissionResults = SubmissionManager.SubmitIlrAndRunMonthEndAndCollateResults(SubmissionContext.IlrLearnerDetails,
                    LookupContext, EmployerAccountContext.EmployerAccounts, SubmissionContext.ContractTypes,
                    SubmissionContext.EmploymentStatus, SubmissionContext.LearningSupportStatus,SubmissionContext.Periods);
                SubmissionContext.HaveSubmissionsBeenDone = true;
            }

            var providerBreakdown = EarningsAndPaymentsContext.OverallEarningsAndPayments.SingleOrDefault(x => x.ProviderId == "provider " + providerIdSuffix);
            if (providerBreakdown == null)
            {
                providerBreakdown = new EarningsAndPaymentsBreakdown { ProviderId = "provider " + providerIdSuffix };
                EarningsAndPaymentsContext.OverallEarningsAndPayments.Add(providerBreakdown);
            }

            EarningAndPaymentTableParser.ParseEarningsAndPaymentsTableIntoContext(providerBreakdown, earningAndPayments);
            PaymentsAndEarningsAssestions.AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext, SubmissionContext, EmployerAccountContext);
            TransactionTypeAssertions.AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext, SubmissionContext, EmployerAccountContext);
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
                SubmissionContext.SubmissionResults = SubmissionManager.SubmitIlrAndRunMonthEndAndCollateResults(SubmissionContext.IlrLearnerDetails,
                    LookupContext, EmployerAccountContext.EmployerAccounts, SubmissionContext.ContractTypes, SubmissionContext.EmploymentStatus, 
                    SubmissionContext.LearningSupportStatus, SubmissionContext.Periods);
                SubmissionContext.HaveSubmissionsBeenDone = true;
            }

            TransactionTypeTableParser.ParseTransactionTypeTableIntoContext(EarningsAndPaymentsContext, $"provider {providerIdSuffix}", transactionTypes);
            PaymentsAndEarningsAssestions.AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext, SubmissionContext, EmployerAccountContext);
            TransactionTypeAssertions.AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext, SubmissionContext, EmployerAccountContext);
        }

        [Then(@"the provider earnings and payments break down for ULN (.*) as follows:")]
        public void ThenTheProviderEarningsAndPaymentsBreakDownForUlnAsFollows(string learnerId, Table earningAndPayments)
        {
            if (!SubmissionContext.HaveSubmissionsBeenDone)
            {
                SubmissionContext.SubmissionResults = SubmissionManager.SubmitIlrAndRunMonthEndAndCollateResults(SubmissionContext.IlrLearnerDetails,
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
            PaymentsAndEarningsAssestions.AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext, SubmissionContext, EmployerAccountContext);
            TransactionTypeAssertions.AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext, SubmissionContext, EmployerAccountContext);
        }

        [Given(@"the following earnings and payments have been made to the provider for (.*):")]
        public void GivenTheFollowingEarningsAndPaymentsHaveBeenMadeToTheProviderForLearner(string learnerName, Table table)
        {
            
            var learnerBreakdown = new EarningsAndPaymentsBreakdown { ProviderId = "provider " + Defaults.ProviderIdSuffix };
            EarningAndPaymentTableParser.ParseEarningsAndPaymentsTableIntoContext(learnerBreakdown, table);
            
            var learner = LookupContext.AddOrGetUln(learnerName);
            var provider = LookupContext.AddOrGetUkprn(learnerBreakdown.ProviderId);


            var commitment = CommitmentsContext.Commitments.FirstOrDefault();
            foreach (var earned in learnerBreakdown.ProviderEarnedTotal)
            {
                var requiredPaymentId = Guid.NewGuid().ToString();
                var month = earned.PeriodName.Split('/').First();
                var year = $"20{earned.PeriodName.Split('/').Last()}";

                if (earned.Value > 0)
                {
                    PaymentsManager.SavePaymentDue(requiredPaymentId,provider,learner,null,null,null,Defaults.StandardCode,
                                                        commitment,learnerName, earned.PeriodName,
                                                        int.Parse(month), int.Parse(year),
                                                        (int)TransactionType.OnProgram,
                                                        commitment == null? ContractType.ContractWithSfa : ContractType.ContractWithEmployer,
                                                        earned.Value);
                    var levyPayment = learnerBreakdown.SfaLevyBudget.Where(x => x.PeriodName == earned.PeriodName).SingleOrDefault();
                    if (levyPayment != null && levyPayment.Value > 0)
                    {
                        PaymentsManager.SavePayment(requiredPaymentId,  earned.PeriodName, int.Parse(month), int.Parse(year),
                                                          (int)TransactionType.OnProgram, FundingSource.Levy, levyPayment.Value);
                    }

                    var earnedFromEmployer = learnerBreakdown.ProviderEarnedFromEmployers.Where(x => x.PeriodName == earned.PeriodName).SingleOrDefault();
                    if (earnedFromEmployer != null && earnedFromEmployer.Value > 0)
                    {
                        PaymentsManager.SavePayment(requiredPaymentId, earned.PeriodName, int.Parse(month), int.Parse(year),
                                                          (int)TransactionType.OnProgram, FundingSource.CoInvestedEmployer, earnedFromEmployer.Value);
                    }

                    var coInvestedBySfaLevy= learnerBreakdown.SfaLevyCoFundBudget.Where(x => x.PeriodName == earned.PeriodName).SingleOrDefault();
                    if (coInvestedBySfaLevy != null && coInvestedBySfaLevy.Value > 0)
                    {
                        PaymentsManager.SavePayment(requiredPaymentId,  earned.PeriodName, int.Parse(month), int.Parse(year),
                                                          (int)TransactionType.OnProgram, FundingSource.CoInvestedSfa, coInvestedBySfaLevy.Value);
                    }

                    var coInvestedBySfaNonLevy = learnerBreakdown.SfaNonLevyCoFundBudget.Where(x => x.PeriodName == earned.PeriodName).SingleOrDefault();
                    if (coInvestedBySfaNonLevy != null && coInvestedBySfaNonLevy.Value > 0)
                    {
                        PaymentsManager.SavePayment(requiredPaymentId, earned.PeriodName, int.Parse(month), int.Parse(year),
                                                          (int)TransactionType.OnProgram, FundingSource.CoInvestedSfa, coInvestedBySfaNonLevy.Value);
                    }

                }

            }

          
        }
    }
}
