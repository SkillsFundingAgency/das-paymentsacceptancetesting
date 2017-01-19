using System;
using System.Linq;
using NUnit.Framework;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.Enums;
using SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment;
using SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Base;
using TechTalk.SpecFlow;
using SFA.DAS.Payments.AcceptanceTests.Entities;
using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Integration
{
    [Binding]
    public class EarningAndPaymentsSteps : BaseStepDefinitions
    {
        public EarningAndPaymentsSteps(StepDefinitionsContext earningAndPaymentsContext)
            :base(earningAndPaymentsContext)
        {
        }

        [When(@"an ILR file is submitted with the following data:")]
        public void WhenAnIlrFileIsSubmittedWithTheFollowingData(Table table)
        {
            ProcessIlrFileSubmissions(table);
        }

        [When(@"an ILR file is submitted every month with the following data:")]
        public void WhenAnIlrFileIsSubmittedEveryMonthWithTheFollowingData(Table table)
        {
            ProcessIlrFileSubmissions(table);
        }

        [When(@"an ILR file is submitted in (.*) with the following data:")]
        public void WhenAnIlrFileIsSubmittedInAMonthWithTheFollowingData(string month, Table table)
        {
            var submissionDate = new DateTime(int.Parse(month.Substring(3)) + 2000, int.Parse(month.Substring(0, 2)), 1).NextCensusDate();
            ProcessIlrFileSubmissions(table, submissionDate);
        }

        [When(@"an ILR file is submitted for the first time in (.*) with the following data:")]
        public void WhenAnIlrFileIsSubmittedForTheFirstTimeInAMonthWithTheFollowingData(string month, Table table)
        {
            var submissionDate = new DateTime(int.Parse(month.Substring(3)) + 2000, int.Parse(month.Substring(0, 2)), 1).NextCensusDate();
            ProcessIlrFileSubmissions(table, submissionDate);
        }

        [When(@"the providers submit the following ILR files:")]
        public void WhenTheProvidersSubmitTheFollowingIlrFiles(Table table)
        {
            ProcessIlrFileSubmissions(table);
        }

        [When(@"an ILR file is submitted on (.*) with the following data:")]
        public void WhenAnIlrFileIsSubmittedOnADayWithTheFollowingData(string date, Table table)
        {
            ProcessIlrFileSubmissions(table);
        }

        [When(@"an ILR file is submitted on (.*) with the following data:"),Scope(Scenario = "Earnings and payments for a DAS learner, levy available, where a learner switches from DAS to Non Das employer at the end of month")]
        public void WhenAnIlrFileIsSubmittedOnADayWithTheFollowingDataNoSubmission(string date, Table table)
        {
            ScenarioContext.Current.Add("learners", table);
            
        }

        [When(@"the Contract type in the ILR is:")]
        public void WhenTheContractTypeInTheIlrIs(Table table)
        {
            Table learnerTable;

            ScenarioContext.Current.TryGetValue("learners",out learnerTable);

            for (var rowIndex = 0; rowIndex < table.RowCount; rowIndex++)
            {
                var famCode = new LearningDeliveryFam
                {
                    FamCode = table.Rows[rowIndex]["contract type"] == "DAS" ? 1 : 2,
                    StartDate = DateTime.Parse(table.Rows[rowIndex]["date from"]),
                    EndDate = DateTime.Parse(table.Rows[rowIndex]["date to"])
                };
                StepDefinitionsContext.ReferenceDataContext.AddLearningDeliveryFam(famCode);
            }
            ProcessIlrFileSubmissions(learnerTable);
        }


        [Then(@"the provider earnings and payments break down as follows:")]
        public void ThenTheProviderEarningsBreakDownAsFollows(Table table)
        {
            var provider = StepDefinitionsContext.Providers[0];

            VerifyProviderEarningsAndPayments(provider.Ukprn,null, table);
        }

        [Then(@"the earnings and payments break down for (.*) is as follows:")]
        public void ThenAProviderEarningsBreakDownAsFollows(string providerName, Table table)
        {
            var provider = StepDefinitionsContext.Providers.Single(p => p.Name == providerName);

            VerifyProviderEarningsAndPayments(provider.Ukprn,null, table);
        }

        [Then(@"the transaction types for the payments are:")]
        public void ThenTheTransactionsForThePaymentsAre(Table table)
        {
            
            var ukprn = StepDefinitionsContext.Providers[0].Ukprn;

            var onProgramRow = table.Rows.RowWithKey(RowKeys.OnProgramPayment);
            var completionRow = table.Rows.RowWithKey(RowKeys.CompletionPayment);
            var balancingRow = table.Rows.RowWithKey(RowKeys.BalancingPayment);
            var employerIncentiveRow = table.Rows.RowWithKey(RowKeys.EmployerIncentive);
            var providerIncentiveRow = table.Rows.RowWithKey(RowKeys.ProviderIncentive);

            for (var colIndex = 1; colIndex < table.Header.Count; colIndex++)
            {
                var periodName = table.Header.ElementAt(colIndex);
                if (periodName == "...")
                {
                    continue;
                }

                var periodMonth = int.Parse(periodName.Substring(0, 2));
                var periodYear = int.Parse(periodName.Substring(3)) + 2000;
                var periodDate = new DateTime(periodYear, periodMonth, 1).NextCensusDate();

                VerifyPaymentsDueByTransactionType(ukprn, periodName, periodDate, colIndex, TransactionType.OnProgram, onProgramRow);
                VerifyPaymentsDueByTransactionType(ukprn, periodName, periodDate, colIndex, TransactionType.Completion, completionRow);
                VerifyPaymentsDueByTransactionType(ukprn, periodName, periodDate, colIndex, TransactionType.Balancing, balancingRow);

                VerifyPaymentsDueByTransactionType(ukprn, periodName, 
                                                    periodDate, colIndex, 
                                                    new TransactionType[] {
                                                            TransactionType.First16To18EmployerIncentive,
                                                            TransactionType.Second16To18EmployerIncentive  }, 
                                                    employerIncentiveRow);
                VerifyPaymentsDueByTransactionType(ukprn, periodName, 
                                                    periodDate, colIndex, 
                                                    new TransactionType[] {
                                                            TransactionType.First16To18ProviderIncentive,
                                                            TransactionType.Second16To18ProviderIncentive},
                                                    providerIncentiveRow);

            }
        }

        [Then(@"the provider earnings and payments break down for ULN (.*) as follows:")]
        public void ThenTheProviderEarningsAndPaymentsBreakDownForAUlnAsFollows(long uln, Table table)
        {
            var provider = StepDefinitionsContext.Providers.Single();

            Assert.IsTrue(provider.EarnedByPeriodByUln.ContainsKey(uln));

            VerifyProviderEarningsAndPayments(provider.Ukprn,uln, table);
        }


        private void ProcessIlrFileSubmissions(Table table, DateTime? firstSubmissionDate = null)
        {
            SetupContextProviders(table);
            SetupContexLearners(table);

            var startDate = firstSubmissionDate ?? StepDefinitionsContext.GetIlrStartDate().NextCensusDate();
            ProcessMonths(startDate);
        }

        private void ProcessMonths(DateTime start)
        {
            var processService = new ProcessService(new TestLogger());

            var periodId = 1;
            var date = start.NextCensusDate();
            var endDate = StepDefinitionsContext.GetIlrEndDate();
            var lastCensusDate = endDate.NextCensusDate();

            while (date <= lastCensusDate)
            {
                var period = date.GetPeriod();

                SetupPeriodReferenceData(date);

                UpdateAccountsBalances(period);
                UpdateCommitmentsPaymentStatuses(date);

                var academicYear = date.GetAcademicYear();

                SetupEnvironmentVariablesForMonth(date, academicYear, ref periodId);

                foreach (var provider in StepDefinitionsContext.Providers)
                {
                    SubmitIlr(provider, 
                                academicYear, 
                                date, 
                                processService);
                }

                SubmitMonthEnd(date, processService);

                date = date.AddDays(15).NextCensusDate();
            }
        }
       
        private void VerifyProviderEarningsAndPayments(long ukprn, long? uln, Table table)
        {
            var earnedRow = table.Rows.RowWithKey(RowKeys.Earnings);
            var govtCofundLevyContractRow = table.Rows.RowWithKey(RowKeys.CoFinanceGovernmentPaymentForLevyContracts);
            var govtCofundNonLevyContractRow = table.Rows.RowWithKey(RowKeys.CoFinanceGovernmentPaymentForNonLevyContracts);
            var govtAdditionalPaymentsRow = table.Rows.RowWithKey(RowKeys.SfaAdditionalPaymentsBudget);

            for (var colIndex = 1; colIndex < table.Header.Count; colIndex++)
            {
                var periodName = table.Header.ElementAt(colIndex);
                if (periodName == "...")
                {
                    continue;
                }

                var periodMonth = int.Parse(periodName.Substring(0, 2));
                var periodYear = int.Parse(periodName.Substring(3)) + 2000;
                var periodDate = new DateTime(periodYear, periodMonth, 1).NextCensusDate();


                VerifyEarningsForPeriod(ukprn,uln, periodName, colIndex, earnedRow);
                VerifyGovtCofinanceLevyContractPayments(ukprn,uln, periodName, periodDate, colIndex, govtCofundLevyContractRow);
                VerifyGovtCofinanceNonLevyContractPayments(ukprn, uln, periodName, periodDate, colIndex, govtCofundNonLevyContractRow);
                VerifyAdditionalGovtFundedEarnings(ukprn, uln, periodName, periodDate, colIndex, govtAdditionalPaymentsRow);

                if (StepDefinitionsContext.ReferenceDataContext.Employers != null)
                {
                    foreach (var employer in StepDefinitionsContext.ReferenceDataContext.Employers)
                    {
                        var levyPaidRow = table.Rows.RowWithKey(RowKeys.DefaultLevyPayment)
                            ?? table.Rows.RowWithKey($"{employer.Name}{RowKeys.LevyPayment}");

                        var employerCofundRow = table.Rows.RowWithKey(RowKeys.DefaultCoFinanceEmployerPayment)
                                                ?? table.Rows.RowWithKey($"{RowKeys.CoFinanceEmployerPayment}{employer.Name}");

                        VerifyLevyPayments(ukprn, uln, periodName, periodDate, employer.AccountId, colIndex, levyPaidRow);
                        VerifyEmployerCofinancePayments(ukprn, uln, periodName, periodDate, employer.AccountId, colIndex, employerCofundRow);
                    }
                }
            }
        }

        private void VerifyEarningsForPeriod(long ukprn, long? uln , string periodName, int colIndex, TableRow earnedRow)
        {
            if (earnedRow == null)
            {
                return;
            }

            var earnedByPeriod = StepDefinitionsContext.GetProviderEarnedByPeriod(ukprn,uln);

            if (!earnedByPeriod.ContainsKey(periodName))
            {
                Assert.Fail($"Expected value for period {periodName} but none found");
            }

            var expectedEarning = decimal.Parse(earnedRow[colIndex]);
            Assert.IsTrue(earnedByPeriod.ContainsKey(periodName), $"Expected earning for period {periodName} but none found");
            Assert.AreEqual(expectedEarning, earnedByPeriod[periodName], $"Expected earning of {expectedEarning} for period {periodName} but found {earnedByPeriod[periodName]}");
        }
        private void VerifyLevyPayments(long ukprn,
                                        long? uln, 
                                        string periodName, 
                                        DateTime periodDate, 
                                        long accountId,
                                        int colIndex, 
                                        TableRow levyPaidRow)
        {
            if (levyPaidRow == null)
            {
                return;
            }

            var levyPaymentDate = periodDate.AddMonths(-1);

            var levyPayments = PaymentsDataHelper.GetAccountPaymentsForPeriod(
                                                    ukprn,
                                                    accountId,
                                                    uln,
                                                    levyPaymentDate.Year,
                                                    levyPaymentDate.Month,
                                                    FundingSource.Levy,
                                                    ContractType.ContractWithEmployer,
                                                    EnvironmentVariables)
                               ?? new PaymentEntity[0];

            var actualLevyPayment = levyPayments.Length == 0 ? 0m : levyPayments.Sum(p => p.Amount);
            var expectedLevyPayment = decimal.Parse(levyPaidRow[colIndex]);
            Assert.AreEqual(expectedLevyPayment, actualLevyPayment, $"Expected a levy payment of {expectedLevyPayment} but made a payment of {actualLevyPayment} for {periodName}");
        }
        private void VerifyGovtCofinanceLevyContractPayments(long ukprn, 
                                                long? uln,
                                                string periodName, 
                                                DateTime periodDate,
                                                int colIndex,
                                                TableRow govtCofundRow)
        {
            if (govtCofundRow == null)
            {
                return;
            }

            var cofinancePayments = PaymentsDataHelper.GetPaymentsForPeriod(
                                                        ukprn,
                                                        uln,
                                                        periodDate.Year,
                                                        periodDate.Month,
                                                        FundingSource.CoInvestedSfa,
                                                        ContractType.ContractWithEmployer,
                                                        EnvironmentVariables)
                                    ?? new PaymentEntity[0];

            var actualGovtPayment = cofinancePayments.Sum(p => p.Amount);
            var expectedGovtPayment = decimal.Parse(govtCofundRow[colIndex]);
            Assert.AreEqual(expectedGovtPayment, actualGovtPayment, $"Expected a levy contract government co-finance payment of {expectedGovtPayment} but made a payment of {actualGovtPayment} for {periodName}");
        }
        private void VerifyGovtCofinanceNonLevyContractPayments(long ukprn,
                                                long? uln,
                                                string periodName,
                                                DateTime periodDate,
                                                int colIndex,
                                                TableRow govtCofundRow)
        {
            if (govtCofundRow == null)
            {
                return;
            }

            var cofinancePayments = PaymentsDataHelper.GetPaymentsForPeriod(
                                                        ukprn,
                                                        uln,
                                                        periodDate.Year,
                                                        periodDate.Month,
                                                        FundingSource.CoInvestedSfa,
                                                        ContractType.ContractWithSfa,
                                                        EnvironmentVariables)
                                    ?? new PaymentEntity[0];

            var actualGovtPayment = cofinancePayments.Sum(p => p.Amount);
            var expectedGovtPayment = decimal.Parse(govtCofundRow[colIndex]);
            Assert.AreEqual(expectedGovtPayment, actualGovtPayment, $"Expected a non levy contract government co-finance payment of {expectedGovtPayment} but made a payment of {actualGovtPayment} for {periodName}");
        }
        private void VerifyEmployerCofinancePayments(long ukprn,long? uln, string periodName, DateTime periodDate, long accountId,
            int colIndex, TableRow employerCofundRow)
        {
            if (employerCofundRow == null)
            {
                return;
            }

            var employerPaymentDate = periodDate.AddMonths(-1);

            var cofinancePayments = PaymentsDataHelper.GetAccountPaymentsForPeriod(
                                                        ukprn,
                                                        accountId,
                                                        uln,
                                                        employerPaymentDate.Year,
                                                        employerPaymentDate.Month,
                                                        FundingSource.CoInvestedEmployer,
                                                        ContractType.ContractWithEmployer,
                                                        EnvironmentVariables)
                                    ?? new PaymentEntity[0];

            var actualEmployerPayment = cofinancePayments.Sum(p => p.Amount);
            var expectedEmployerPayment = decimal.Parse(employerCofundRow[colIndex]);
            Assert.AreEqual(expectedEmployerPayment, actualEmployerPayment, $"Expected a employer co-finance payment of {expectedEmployerPayment} but made a payment of {actualEmployerPayment} for {periodName}");
        }
        private void VerifyPaymentsDueByTransactionType(long ukprn, 
                                                        string periodName, 
                                                        DateTime periodDate, 
                                                        int colIndex, 
                                                        TransactionType paymentType, 
                                                        TableRow paymentsRow)
        {
            VerifyPaymentsDueByTransactionType(ukprn, periodName, periodDate, colIndex, new TransactionType[] { paymentType }, paymentsRow);
        }

        private void VerifyPaymentsDueByTransactionType(long ukprn, 
                                                        string periodName, 
                                                        DateTime periodDate, 
                                                        int colIndex, 
                                                        TransactionType[] paymentTypes,
                                                        TableRow paymentsRow)
        {
            if (paymentsRow == null)
            {
                return;
            }

            var paymentTypesFilter = Array.ConvertAll(paymentTypes, value => (int)value);
            
            var paymentsDueDate = periodDate.AddMonths(-1);

            var paymentsDue = PaymentsDueDataHelper.GetPaymentsDueForPeriod(ukprn,
                paymentsDueDate.Year, paymentsDueDate.Month, EnvironmentVariables)
                              ?? new RequiredPaymentEntity[0];

            var actualPaymentDue = paymentsDue.Length == 0 ? 0m : paymentsDue.Where(p => paymentTypesFilter.Contains(p.TransactionType)).Sum(p => p.AmountDue);
            var expectedPaymentDue = decimal.Parse(paymentsRow[colIndex]);
            
            Assert.AreEqual(expectedPaymentDue, actualPaymentDue, $"Expected {string.Join(" and ",paymentTypes)} payment due of {expectedPaymentDue} but made a payment of {actualPaymentDue} for {periodName}");
        }

        private void VerifyAdditionalGovtFundedEarnings(long ukprn,
                                      long? uln,
                                      string periodName,
                                      DateTime periodDate,
                                      int colIndex,
                                      TableRow additionalEarningsRow)
        {
            if (additionalEarningsRow == null)
            {
                return;
            }

            var additionalEarnings = PaymentsDataHelper.GetPaymentsForPeriod(
                                                       ukprn,
                                                       uln,
                                                       periodDate.Year,
                                                       periodDate.Month,
                                                       FundingSource.FullyFundedSfa,
                                                       ContractType.ContractWithEmployer,
                                                       EnvironmentVariables)
                                   ?? new PaymentEntity[0];

            var actualEarningsDue = additionalEarnings.Length == 0 ? 0m : additionalEarnings.Sum(p => p.Amount);

            var expectedPaymentDue = decimal.Parse(additionalEarningsRow[colIndex]);
            Assert.AreEqual(expectedPaymentDue, actualEarningsDue, $"Expected additional earnings of {expectedPaymentDue} but earned a payment of {actualEarningsDue} for {periodName}");

        }
    }
}
