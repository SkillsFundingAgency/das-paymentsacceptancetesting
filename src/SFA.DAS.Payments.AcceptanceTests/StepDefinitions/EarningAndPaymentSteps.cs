using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Assertions;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ExecutionManagers;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.TableParsers;
using TechTalk.SpecFlow;
using System;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ExecutionManagers;
using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class EarningAndPaymentSteps
    {
        public EarningAndPaymentSteps(EmployerAccountContext employerAccountContext,
                                      EarningsAndPaymentsContext earningsAndPaymentsContext,
                                      DataLockContext dataLockContext,
                                      SubmissionDataLockContext submissionDataLockContext,
                                      SubmissionContext submissionContext,
                                      LookupContext lookupContext,
                                    CommitmentsContext commitmentsContext)
        {
            EmployerAccountContext = employerAccountContext;
            EarningsAndPaymentsContext = earningsAndPaymentsContext;
            DataLockContext = dataLockContext;
            SubmissionDataLockContext = submissionDataLockContext;
            SubmissionContext = submissionContext;
            LookupContext = lookupContext;
            CommitmentsContext = commitmentsContext;
        }
        public EmployerAccountContext EmployerAccountContext { get; }
        public DataLockContext DataLockContext { get; }
        public SubmissionDataLockContext SubmissionDataLockContext { get; }
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
                LearnerReferenceNumber = learnerId
            };
            EarningsAndPaymentsContext.LearnerOverallEarningsAndPayments.Add(breakdown);
            EarningAndPaymentTableParser.ParseEarningsAndPaymentsTableIntoContext(breakdown, earningAndPayments);
            AssertResults();
        }

        private void AssertResults()
        {
            PaymentsAndEarningsAssertions.AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext, SubmissionContext, EmployerAccountContext);
            TransactionTypeAssertions.AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext, SubmissionContext, EmployerAccountContext);
            SubmissionDataLockAssertions.AssertPaymentsAndEarningsResults(SubmissionDataLockContext, SubmissionContext);
        }


        [Given(@"following learning has been recorded for previous payments:")]
        public void GivenFollowingLearningHasBeenRecordedForPreviousSubmission(Table table)
        {
            IlrTableParser.ParseIlrTableIntoContext(SubmissionContext.HistoricalLearningDetails, table);
        }

        [Given(@"the following (.*) earnings and payments have been made to the (.*) for (.*):")]
        public void GivenTheFollowingEarningsAndPaymentsHaveBeenMadeToTheProviderAForLearnerA(string aimType, string providerName, string learnerRefererenceNumber, Table table)
        {
            var paymentsAimType = (AimType)aimType.ToEnumByDescription(typeof(AimType));
            CreatePreviousEarningsAndPayments(providerName, learnerRefererenceNumber, table, paymentsAimType);
        }

        [Given(@"the following earnings and payments have been made to the (.*) for (.*):")]
        public void GivenTheFollowingEarningsAndPaymentsHaveBeenMadeToTheProviderAForLearnerA(string providerName, string learnerRefererenceNumber, Table table)
        {
            CreatePreviousEarningsAndPayments(providerName, learnerRefererenceNumber, table, AimType.Programme);
        }

        [Given(@"the following payments have been made to the (.*) for (.*):")]
        public void GivenTheFollowingPaymentsHaveBeenMadeToTheProviderAForLearnerA(string providerName, string learnerRefererenceNumber, Table table)
        {
            var context = new EarningsAndPaymentsContext();
            TransactionTypeTableParser.ParseTransactionTypeTableIntoContext(context, providerName, table);


            var learningDetails = SubmissionContext.HistoricalLearningDetails.Single(x => x.LearnerReference.Equals(learnerRefererenceNumber, StringComparison.InvariantCultureIgnoreCase));

            long learnerUln;
            if (!string.IsNullOrEmpty(learningDetails.Uln))
            {
                learnerUln = long.Parse(learningDetails.Uln);
                LookupContext.AddUln(learnerRefererenceNumber, learnerUln);
            }
            else
            {
                learnerUln = LookupContext.AddOrGetUln(learnerRefererenceNumber);
            }


            var provider = LookupContext.AddOrGetUkprn(providerName);

            var commitment = CommitmentsContext.Commitments.FirstOrDefault(x => x.ProviderId == providerName && x.LearnerId == learnerRefererenceNumber);

            CreatePayment(context.ProviderEarnedForOnProgramme, provider, learnerUln, learnerRefererenceNumber, commitment, learningDetails,TransactionType.OnProgram, commitment==null ? FundingSource.CoInvestedSfa : FundingSource.Levy);
            CreatePayment(context.ProviderEarnedForLearningSupport, provider, learnerUln, learnerRefererenceNumber, commitment, learningDetails, TransactionType.LearningSupport, FundingSource.FullyFundedSfa);
            CreatePayment(context.ProviderEarnedForFrameworkUpliftOnProgramme, provider, learnerUln, learnerRefererenceNumber, commitment, learningDetails, TransactionType.OnProgramme16To18FrameworkUplift,FundingSource.FullyFundedSfa);

        }

        private void CreatePayment(List<ProviderEarnedPeriodValue> paymentValues, long ukprn, 
                                    long uln, string learnRefNumber, 
                                    CommitmentReferenceData commitment, 
                                    IlrLearnerReferenceData learningDetails,
                                    TransactionType transactionType,
                                    FundingSource fundingSource)
        {

            foreach (var payment in paymentValues)
            {
                if (payment.Value > 0)
                {
                    var requiredPaymentId = Guid.NewGuid().ToString();
                    var month = int.Parse(payment.PeriodName.Substring(0, 2));
                    var year = int.Parse(payment.PeriodName.Substring(3, 2)) + 2000;
                    var date = new DateTime(year, month, 1);
                    var periodNumber = date.GetPeriodNumber();
                    var periodName = $"{TestEnvironment.Variables.OpaRulebaseYear}-R" + periodNumber.ToString("00");


                    PaymentsManager.SavePaymentDue(requiredPaymentId, ukprn, uln, commitment, learnRefNumber, periodName,
                                                    month, year, (int)transactionType, payment.Value, learningDetails);

                    PaymentsManager.SavePayment(requiredPaymentId, periodName, month, year, (int)transactionType, fundingSource, payment.Value);

                }
            }
        }


        private void CreatePreviousEarningsAndPayments(string providerName, string learnerRefererenceNumber, Table table, AimType paymentsAimType)
        {

            var learnerBreakdown = new EarningsAndPaymentsBreakdown { ProviderId = providerName };
            EarningAndPaymentTableParser.ParseEarningsAndPaymentsTableIntoContext(learnerBreakdown, table);

            var learningDetails = SubmissionContext.HistoricalLearningDetails.Where(x => x.AimType == paymentsAimType && x.LearnerReference.Equals(learnerRefererenceNumber, StringComparison.InvariantCultureIgnoreCase)).Single();

            long learnerUln;
            if (!string.IsNullOrEmpty(learningDetails.Uln))
            {
                learnerUln = long.Parse(learningDetails.Uln);
                LookupContext.AddUln(learnerRefererenceNumber, learnerUln);
            }
            else
            {
                learnerUln = LookupContext.AddOrGetUln(learnerRefererenceNumber);
            }


            var provider = LookupContext.AddOrGetUkprn(learnerBreakdown.ProviderId);

            var commitment = CommitmentsContext.Commitments.FirstOrDefault(x => x.ProviderId == learnerBreakdown.ProviderId && x.LearnerId == learnerRefererenceNumber);

            foreach (var earned in learnerBreakdown.ProviderEarnedTotal)
            {
                var requiredPaymentId = Guid.NewGuid().ToString();
                var month = int.Parse(earned.PeriodName.Substring(0, 2));
                var year = int.Parse(earned.PeriodName.Substring(3, 2)) + 2000;
                var date = new DateTime(year, month, 1);
                var periodNumber = date.GetPeriodNumber();
                var periodName = $"{TestEnvironment.Variables.OpaRulebaseYear}-R" + periodNumber.ToString("00");

                if (earned.Value > 0)
                {
                    PaymentsManager.SavePaymentDue(requiredPaymentId, provider, learnerUln,
                                                        commitment, learnerRefererenceNumber, periodName,
                                                        month, year, learningDetails.AimType == AimType.Programme ? (int)TransactionType.OnProgram : (int)TransactionType.OnProgrammeMathsAndEnglish
                                                        , earned.Value, learningDetails);

                    var levyPayment = learnerBreakdown.SfaLevyBudget.Where(x => x.PeriodName == earned.PeriodName).SingleOrDefault();
                    if (levyPayment != null && levyPayment.Value > 0)
                    {
                        PaymentsManager.SavePayment(requiredPaymentId, periodName, month, year,
                                                           learningDetails.AimType == AimType.Programme ? (int)TransactionType.OnProgram : (int)TransactionType.OnProgrammeMathsAndEnglish, FundingSource.Levy, levyPayment.Value);
                    }

                    var earnedFromEmployer = learnerBreakdown.ProviderEarnedFromEmployers.Where(x => x.PeriodName == earned.PeriodName).SingleOrDefault();
                    if (earnedFromEmployer != null && earnedFromEmployer.Value > 0)
                    {
                        PaymentsManager.SavePayment(requiredPaymentId, periodName, month, year,
                                                           learningDetails.AimType == AimType.Programme ? (int)TransactionType.OnProgram : (int)TransactionType.OnProgrammeMathsAndEnglish, FundingSource.CoInvestedEmployer, earnedFromEmployer.Value);
                    }

                    var coInvestedBySfaLevy = learnerBreakdown.SfaLevyCoFundBudget.Where(x => x.PeriodName == earned.PeriodName).SingleOrDefault();
                    if (coInvestedBySfaLevy != null && coInvestedBySfaLevy.Value > 0)
                    {
                        PaymentsManager.SavePayment(requiredPaymentId, periodName, month, year,
                                                           learningDetails.AimType == AimType.Programme ? (int)TransactionType.OnProgram : (int)TransactionType.OnProgrammeMathsAndEnglish, FundingSource.CoInvestedSfa, coInvestedBySfaLevy.Value);
                    }

                    var coInvestedBySfaNonLevy = learnerBreakdown.SfaNonLevyCoFundBudget.Where(x => x.PeriodName == earned.PeriodName).SingleOrDefault();
                    if (coInvestedBySfaNonLevy != null && coInvestedBySfaNonLevy.Value > 0)
                    {
                        PaymentsManager.SavePayment(requiredPaymentId, periodName, month, year,
                                                          learningDetails.AimType == AimType.Programme ? (int)TransactionType.OnProgram : (int)TransactionType.OnProgrammeMathsAndEnglish, FundingSource.CoInvestedSfa, coInvestedBySfaNonLevy.Value);
                    }

                    var aditionalPayments = learnerBreakdown.SfaLevyAdditionalPayments.Where(x => x.PeriodName == earned.PeriodName).SingleOrDefault();
                    if (aditionalPayments != null && aditionalPayments.Value > 0)
                    {
                        PaymentsManager.SavePayment(requiredPaymentId, periodName, month, year,
                                                          learningDetails.AimType == AimType.Programme ? (int)TransactionType.OnProgram : (int)TransactionType.OnProgrammeMathsAndEnglish, FundingSource.FullyFundedSfa, aditionalPayments.Value);
                    }

                }
            }


        }
    }
}
