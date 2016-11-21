using System;
using System.Collections.Generic;
using System.Linq;
using IlrGenerator;
using NUnit.Framework;
using ProviderPayments.TestStack.Core;
using ProviderPayments.TestStack.Core.Domain;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment;
using SFA.DAS.Payments.AcceptanceTests.Translators;
using TechTalk.SpecFlow;
using IlrBuilder = SFA.DAS.Payments.AcceptanceTests.Builders.IlrBuilder;
using LearningDelivery = SFA.DAS.Payments.AcceptanceTests.Contexts.LearningDelivery;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class EarningAndPaymentsSteps : BaseCalculationSteps
    {

        public EarningAndPaymentsSteps(EarningAndPaymentsContext earningAndPaymentsContext)
            :base(earningAndPaymentsContext)
        {

        }

        [When(@"an ILR file is submitted with the following data:")]
        public void WhenAnIlrFileIsSubmittedWithTheFollowingData(Table table)
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            // Store spec values in context
            SetupContextProviders(table);
            SetupContexLearners(table);

            // Setup reference data
            SetupReferenceData();

            // Process months
            var startDate = EarningAndPaymentsContext.GetIlrStartDate().NextCensusDate();
            ProcessMonths(startDate);
        }

        [When(@"an ILR file is submitted in (.*) with the following data:")]
        public void WhenAnIlrFileIsSubmittedInAMonthWithTheFollowingData(string month, Table table)
        {
           

            // Store spec values in context
            SetupContextProviders(table);
            SetupContexLearners(table);

            // Setup reference data
            SetupReferenceData();

            // Process months
            var submissionDate = new DateTime(int.Parse(month.Substring(3)) + 2000, int.Parse(month.Substring(0, 2)), 1).NextCensusDate();
            ProcessMonths(submissionDate);
        }

        [When(@"the providers submit the following ILR files:")]
        public void WhenTheProvidersSubmitTheFollowingIlrFiles(Table table)
        {
          

            // Store spec values in context
            SetupContextProviders(table);
            SetupContexLearners(table);

            // Setup reference data
            SetupReferenceData();

            // Process months
            var startDate = EarningAndPaymentsContext.GetIlrStartDate().NextCensusDate();
            ProcessMonths(startDate);
        }

        [Then(@"the provider earnings and payments break down as follows:")]
        public void ThenTheProviderEarningsBreakDownAsFollows(Table table)
        {
            var provider = EarningAndPaymentsContext.Providers[0];

            VerifyProviderEarningsAndPayments(provider.Ukprn, table);
        }

        [Then(@"the earnings and payments break down for (.*) is as follows:")]
        public void ThenAProviderEarningsBreakDownAsFollows(string providerName, Table table)
        {
            var provider = EarningAndPaymentsContext.Providers.Single(p => p.Name == providerName);

            VerifyProviderEarningsAndPayments(provider.Ukprn, table);
        }

        [Then(@"the transaction types for the payments are:")]
        public void ThenTheTransactionsForThePaymentsAre(Table table)
        {
            
            var ukprn = EarningAndPaymentsContext.Providers[0].Ukprn;

            var onProgramRow = table.Rows.RowWithKey(RowKeys.OnProgramPayment);
            var completionRow = table.Rows.RowWithKey(RowKeys.CompletionPayment);
            var balancingRow = table.Rows.RowWithKey(RowKeys.BalancingPayment);

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

                VerifyPaymentsDueByTransactionType(ukprn, periodName, periodDate, colIndex, TransactionType.OnProgram, onProgramRow, environmentVariables);
                VerifyPaymentsDueByTransactionType(ukprn, periodName, periodDate, colIndex, TransactionType.Completion, completionRow, environmentVariables);
                VerifyPaymentsDueByTransactionType(ukprn, periodName, periodDate, colIndex, TransactionType.Balancing, balancingRow, environmentVariables);
            }
        }

         private void SetupContextProviders(Table table)
        {
            if (table.ContainsColumn("Provider"))
            {
                for (var rowIndex = 0; rowIndex < table.RowCount; rowIndex++)
                {
                    EarningAndPaymentsContext.AddProvider(table.Rows[rowIndex]["Provider"]);
                }
            }
            else
            {
                EarningAndPaymentsContext.SetDefaultProvider();
            }
        }
        private void SetupContexLearners(Table table)
        {
            for (var rowIndex = 0; rowIndex < table.RowCount; rowIndex++)
            {
                var learner = new Contexts.Learner
                {
                    Name = table.Rows[rowIndex].ContainsKey("ULN") ? table.Rows[rowIndex]["ULN"] : string.Empty,
                    Uln = long.Parse(IdentifierGenerator.GenerateIdentifier(10, false)),
                    LearningDelivery = new LearningDelivery
                    {
                        AgreedPrice = decimal.Parse(table.Rows[rowIndex]["agreed price"]),
                        LearnerType = LearnerType.ProgrammeOnlyDas,
                        StartDate = DateTime.Parse(table.Rows[rowIndex]["start date"]),
                        PlannedEndDate = DateTime.Parse(table.Rows[rowIndex]["planned end date"]),
                        ActualEndDate =
                            !table.Header.Contains("actual end date") ||
                            string.IsNullOrWhiteSpace(table.Rows[rowIndex]["actual end date"])
                                ? null
                                : (DateTime?)DateTime.Parse(table.Rows[rowIndex]["actual end date"]),
                        CompletionStatus =
                            IlrTranslator.TranslateCompletionStatus(table.Rows[rowIndex]["completion status"])
                    }
                };

                var provider = table.ContainsColumn("Provider")
                    ? table.Rows[rowIndex]["Provider"]
                    : "provider";

                EarningAndPaymentsContext.AddProviderLearner(provider, learner);
            }
        }
        
        private void ProcessMonths(DateTime start)
        {
            var processService = new ProcessService(new TestLogger());

            var periodId = 1;
            var date = start.NextCensusDate();
            var endDate = EarningAndPaymentsContext.GetIlrEndDate();
            var lastCensusDate = endDate.NextCensusDate();

            while (date <= lastCensusDate)
            {
                var period = date.GetPeriod();
                UpdateAccountsBalances(period);

                var academicYear = date.GetAcademicYear();

                SetupEnvironmentVariablesForMonth(date, academicYear, ref periodId);

                foreach (var provider in EarningAndPaymentsContext.Providers)
                {
                    SubmitIlr(provider.Ukprn, provider.Learners, academicYear, date, processService, provider.EarnedByPeriod);
                }

                SubmitMonthEnd(date, processService);

                date = date.AddDays(15).NextCensusDate();
            }
        }
       
        private void VerifyProviderEarningsAndPayments(long ukprn, Table table)
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            var earnedRow = table.Rows.RowWithKey(RowKeys.Earnings);
            var govtCofundRow = table.Rows.RowWithKey(RowKeys.CoFinanceGovernmentPayment);

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


                VerifyEarningsForPeriod(ukprn, periodName, colIndex, earnedRow);
                VerifyGovtCofinancePayments(ukprn, periodName, periodDate, colIndex, govtCofundRow, environmentVariables);

                foreach (var employer in EarningAndPaymentsContext.ReferenceDataContext.Employers)
                {
                    var levyPaidRow = table.Rows.RowWithKey(RowKeys.DefaultLevyPayment)
                        ?? table.Rows.RowWithKey($"{employer.Name}{RowKeys.LevyPayment}");

                    var employerCofundRow = table.Rows.RowWithKey(RowKeys.DefaultCoFinanceEmployerPayment)
                                            ?? table.Rows.RowWithKey($"{RowKeys.CoFinanceEmployerPayment}{employer.Name}");

                    VerifyLevyPayments(ukprn, periodName, periodDate, employer.AccountId, colIndex, levyPaidRow, environmentVariables);
                    VerifyEmployerCofinancePayments(ukprn, periodName, periodDate, employer.AccountId, colIndex, employerCofundRow, environmentVariables);
                }
            }
        }

        private void VerifyEarningsForPeriod(long ukprn, string periodName, int colIndex, TableRow earnedRow)
        {
            if (earnedRow == null)
            {
                return;
            }

            var earnedByPeriod = EarningAndPaymentsContext.GetProviderEarnedByPeriod(ukprn);

            if (!earnedByPeriod.ContainsKey(periodName))
            {
                Assert.Fail($"Expected value for period {periodName} but none found");
            }

            var expectedEarning = decimal.Parse(earnedRow[colIndex]);
            Assert.IsTrue(earnedByPeriod.ContainsKey(periodName), $"Expected earning for period {periodName} but none found");
            Assert.AreEqual(expectedEarning, earnedByPeriod[periodName], $"Expected earning of {expectedEarning} for period {periodName} but found {earnedByPeriod[periodName]}");
        }
        private void VerifyLevyPayments(long ukprn, string periodName, DateTime periodDate, long accountId,
            int colIndex, TableRow levyPaidRow, EnvironmentVariables environmentVariables)
        {
            if (levyPaidRow == null)
            {
                return;
            }

            var levyPaymentDate = periodDate.AddMonths(-1);

            var levyPayments = PaymentsDataHelper.GetAccountPaymentsForPeriod(ukprn, accountId,
                levyPaymentDate.Year, levyPaymentDate.Month,FundingSource.Levy, environmentVariables)
                               ?? new PaymentEntity[0];

            var actualLevyPayment = levyPayments.Length == 0 ? 0m : levyPayments.Sum(p => p.Amount);
            var expectedLevyPayment = decimal.Parse(levyPaidRow[colIndex]);
            Assert.AreEqual(expectedLevyPayment, actualLevyPayment, $"Expected a levy payment of {expectedLevyPayment} but made a payment of {actualLevyPayment} for {periodName}");
        }
        private void VerifyGovtCofinancePayments(long ukprn, string periodName, DateTime periodDate,
            int colIndex, TableRow govtCofundRow, EnvironmentVariables environmentVariables)
        {
            if (govtCofundRow == null)
            {
                return;
            }

            var cofinancePayments = PaymentsDataHelper.GetPaymentsForPeriod(ukprn,
                periodDate.Year, periodDate.Month,FundingSource.CoInvestedSfa, environmentVariables)
                                    ?? new PaymentEntity[0];

            var actualGovtPayment = cofinancePayments.Sum(p => p.Amount);
            var expectedGovtPayment = decimal.Parse(govtCofundRow[colIndex]);
            Assert.AreEqual(expectedGovtPayment, actualGovtPayment, $"Expected a government co-finance payment of {expectedGovtPayment} but made a payment of {actualGovtPayment} for {periodName}");
        }
        private void VerifyEmployerCofinancePayments(long ukprn, string periodName, DateTime periodDate, long accountId,
            int colIndex, TableRow employerCofundRow, EnvironmentVariables environmentVariables)
        {
            if (employerCofundRow == null)
            {
                return;
            }

            var employerPaymentDate = periodDate.AddMonths(-1);

            var cofinancePayments = PaymentsDataHelper.GetAccountPaymentsForPeriod(ukprn, accountId,
                employerPaymentDate.Year, employerPaymentDate.Month,FundingSource.CoInvestedEmployer, environmentVariables)
                                    ?? new PaymentEntity[0];

            var actualEmployerPayment = cofinancePayments.Sum(p => p.Amount);
            var expectedEmployerPayment = decimal.Parse(employerCofundRow[colIndex]);
            Assert.AreEqual(expectedEmployerPayment, actualEmployerPayment, $"Expected a employer co-finance payment of {expectedEmployerPayment} but made a payment of {actualEmployerPayment} for {periodName}");
        }
        private void VerifyPaymentsDueByTransactionType(long ukprn, string periodName, DateTime periodDate, int colIndex, TransactionType paymentType,
            TableRow paymentsRow, EnvironmentVariables environmentVariables)
        {
            if (paymentsRow == null)
            {
                return;
            }

            var paymentsDueDate = periodDate.AddMonths(-1);

            var paymentsDue = PaymentsDueDataHelper.GetPaymentsDueForPeriod(ukprn,
                paymentsDueDate.Year, paymentsDueDate.Month, environmentVariables)
                              ?? new RequiredPaymentEntity[0];

            var actualPaymentDue = paymentsDue.Length == 0 ? 0m : paymentsDue.Where(p => p.TransactionType == (int)paymentType).Sum(p => p.AmountDue);
            var expectedPaymentDue = decimal.Parse(paymentsRow[colIndex]);
            Assert.AreEqual(expectedPaymentDue, actualPaymentDue, $"Expected a {paymentType} payment due of {expectedPaymentDue} but made a payment of {actualPaymentDue} for {periodName}");
        }
        
        

        

        

    }
}
