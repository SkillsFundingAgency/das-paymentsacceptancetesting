using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ProviderPayments.TestStack.Core;
using ProviderPayments.TestStack.Core.Domain;
using SFA.DAS.Payments.AcceptanceTests.Builders;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment;
using SFA.DAS.Payments.AcceptanceTests.Translators;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class EarningAndPaymentsSteps
    {
        public EarningAndPaymentsSteps(EarningAndPaymentsContext earningAndPaymentsContext)
        {
            EarningAndPaymentsContext = earningAndPaymentsContext;
        }

        public EarningAndPaymentsContext EarningAndPaymentsContext { get; set; }


        [When(@"an ILR file is submitted with the following data:")]
        public void WhenAnIlrFileIsSubmittedWithTheFollowingData(Table table)
        {
            // Store spec values in context
            EarningAndPaymentsContext.IlrStartDate = DateTime.Parse(table.Rows[0][0]);
            EarningAndPaymentsContext.IlrPlannedEndDate = DateTime.Parse(table.Rows[0][1]);
            EarningAndPaymentsContext.IlrActualEndDate = string.IsNullOrWhiteSpace(table.Rows[0][2]) ? null : (DateTime?)DateTime.Parse(table.Rows[0][2]);
            EarningAndPaymentsContext.IlrCompletionStatus = IlrTranslator.TranslateCompletionStatus(table.Rows[0][2]);


            // Setup reference data
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();
            var accountId = IdentifierGenerator.GenerateIdentifier();
            var ukprn = int.Parse(IdentifierGenerator.GenerateIdentifier(6, false));
            var uln = int.Parse(IdentifierGenerator.GenerateIdentifier(6, false));
            var commitmentId = IdentifierGenerator.GenerateIdentifier();

            EarningAndPaymentsContext.Ukprn = ukprn;

            AccountDataHelper.CreateAccount(accountId, accountId, EarningAndPaymentsContext.ReferenceDataContext.AccountBalance, environmentVariables);

            CommitmentDataHelper.CreateCommitment(commitmentId, ukprn, uln, accountId, EarningAndPaymentsContext.IlrStartDate,
                EarningAndPaymentsContext.IlrPlannedEndDate, EarningAndPaymentsContext.ReferenceDataContext.AgreedPrice, IlrBuilder.Defaults.StandardCode,
                IlrBuilder.Defaults.FrameworkCode, IlrBuilder.Defaults.ProgrammeType, IlrBuilder.Defaults.PathwayCode, environmentVariables);


            // Process months
            var processService = new ProcessService(new TestLogger());
            var earnedByPeriod = new Dictionary<string, decimal>();

            var periodId = 1;
            var date = EarningAndPaymentsContext.IlrStartDate.NextCensusDate();
            var endDate = (EarningAndPaymentsContext.IlrActualEndDate ?? EarningAndPaymentsContext.IlrPlannedEndDate);
            var lastCensusDate = endDate.NextCensusDate();
            while (date <= lastCensusDate)
            {
                var academicYear = date.GetAcademicYear();

                SetupEnvironmentVariablesForMonth(date, academicYear, environmentVariables, ref periodId);

                SubmitIlr(ukprn, uln, academicYear, date, endDate, environmentVariables, processService, earnedByPeriod);

                SubmitMonthEnd(date, environmentVariables, processService);

                date = date.AddDays(15).NextCensusDate();
            }
            EarningAndPaymentsContext.EarnedByPeriod = earnedByPeriod;
        }

        [Then(@"the provider earnings and payments break down as follows:")]
        public void ThenTheProviderEarningsBreakDownAsFollows(Table table)
        {
            var earnedRow = table.Rows.RowWithKey(RowKeys.Earnings);
            var levyPaidRow = table.Rows.RowWithKey(RowKeys.LevyPayment);
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            for (var colIndex = 1; colIndex < table.Header.Count; colIndex++)
            {
                var periodName = table.Header.ElementAt(colIndex);
                if (periodName == "...")
                {
                    continue;
                }

                var periodMonth = int.Parse(periodName.Substring(0, 2));
                var periodYear = int.Parse(periodName.Substring(3)) + 2000;


                VerifyEarningsForPeriod(periodName, colIndex, earnedRow);
                VerifyLevyPayments(periodName, periodYear, periodMonth, colIndex, levyPaidRow, environmentVariables);
            }
        }



        private void SetupEnvironmentVariablesForMonth(DateTime date, string academicYear, EnvironmentVariables environmentVariables, ref int periodId)
        {
            environmentVariables.CurrentYear = academicYear;
            environmentVariables.SummarisationPeriod = new SummarisationCollectionPeriod
            {
                PeriodId = periodId++,
                CollectionPeriod = "R" + (new DateTime(date.Year, date.Month, 1)).GetPeriodNumber().ToString("00"),
                CalendarMonth = date.Month,
                CalendarYear = date.Year,
                ActualsSchemaPeriod = date.Year + date.Month.ToString("00"),
                CollectionOpen = 1
            };
        }
        private void SubmitIlr(long ukprn, int uln, string academicYear, DateTime date, DateTime endDate, 
            EnvironmentVariables environmentVariables, ProcessService processService, Dictionary<string, decimal> earnedByPeriod)
        {
            var actualEndDate = date >= endDate ? EarningAndPaymentsContext.IlrActualEndDate : null;
            IlrSubmission submission = IlrBuilder.CreateAIlrSubmission()
                .WithUkprn(ukprn)
                .WithALearner()
                    .WithUln(uln)
                    .WithLearningDelivery()
                        .WithActualStartDate(EarningAndPaymentsContext.IlrStartDate)
                        .WithPlannedEndDate(EarningAndPaymentsContext.IlrPlannedEndDate)
                        .WithActualEndDate(actualEndDate)
                        .WithAgreedPrice(EarningAndPaymentsContext.ReferenceDataContext.AgreedPrice);

            AcceptanceTestDataHelper.AddCurrentActivePeriod(date.Year, date.Month, environmentVariables);

            var ilrStatusWatcher = new TestStatusWatcher(environmentVariables, $"Submit ILR to {date:dd/MM/yy}");
            processService.RunIlrSubmission(submission, environmentVariables, ilrStatusWatcher);

            var periodEarnings = EarningsDataHelper.GetPeriodisedValuesForUkprn(ukprn, environmentVariables).Last();
            earnedByPeriod.AddOrUpdate("08/" + academicYear.Substring(0, 2), periodEarnings.Period_1);
            earnedByPeriod.AddOrUpdate("09/" + academicYear.Substring(0, 2), periodEarnings.Period_2);
            earnedByPeriod.AddOrUpdate("10/" + academicYear.Substring(0, 2), periodEarnings.Period_3);
            earnedByPeriod.AddOrUpdate("11/" + academicYear.Substring(0, 2), periodEarnings.Period_4);
            earnedByPeriod.AddOrUpdate("12/" + academicYear.Substring(0, 2), periodEarnings.Period_5);
            earnedByPeriod.AddOrUpdate("01/" + academicYear.Substring(2), periodEarnings.Period_6);
            earnedByPeriod.AddOrUpdate("02/" + academicYear.Substring(2), periodEarnings.Period_7);
            earnedByPeriod.AddOrUpdate("03/" + academicYear.Substring(2), periodEarnings.Period_8);
            earnedByPeriod.AddOrUpdate("04/" + academicYear.Substring(2), periodEarnings.Period_9);
            earnedByPeriod.AddOrUpdate("05/" + academicYear.Substring(2), periodEarnings.Period_10);
            earnedByPeriod.AddOrUpdate("06/" + academicYear.Substring(2), periodEarnings.Period_11);
            earnedByPeriod.AddOrUpdate("07/" + academicYear.Substring(2), periodEarnings.Period_12);
        }
        private void SubmitMonthEnd(DateTime date, EnvironmentVariables environmentVariables, ProcessService processService)
        {
            var summarisationStatusWatcher = new TestStatusWatcher(environmentVariables, $"Summarise {date:dd/MM/yy}");
            processService.RunSummarisation(environmentVariables, summarisationStatusWatcher);
        }

        private void VerifyEarningsForPeriod(string periodName, int colIndex, TableRow earnedRow)
        {
            if (earnedRow == null)
            {
                return;
            }

            if (!EarningAndPaymentsContext.EarnedByPeriod.ContainsKey(periodName))
            {
                Assert.Fail($"Expected value for period {periodName} but none found");
            }

            var expectedEarning = decimal.Parse(earnedRow[colIndex]);
            Assert.IsTrue(EarningAndPaymentsContext.EarnedByPeriod.ContainsKey(periodName), $"Expected earning for period {periodName} but none found");
            Assert.AreEqual(expectedEarning, EarningAndPaymentsContext.EarnedByPeriod[periodName]);
        }
        private void VerifyLevyPayments(string periodName, int periodYear, int periodMonth, int colIndex, TableRow levyPaidRow, EnvironmentVariables environmentVariables)
        {
            if (levyPaidRow == null)
            {
                return;
            }

            var levyPayments = LevyPaymentDataHelper.GetLevyPaymentsForPeriod(EarningAndPaymentsContext.Ukprn, periodYear, periodMonth - 1, environmentVariables)
                    ?? new LevyPaymentEntity[0];
            if (levyPayments.Length < 0 || levyPayments.Length > 1)
            {
                Assert.Fail($"Should have no more than 1 payment for {periodName} but have {levyPayments.Length}");
            }

            var actualLevyPayment = levyPayments.Length == 0 ? 0m : levyPayments[0].Amount;
            var expectedLevyPayment = decimal.Parse(levyPaidRow[colIndex]);
            Assert.AreEqual(expectedLevyPayment, actualLevyPayment, $"Expected a levy payment of {expectedLevyPayment} but made a payment of {actualLevyPayment} for {periodName}");
        }

    }
}
