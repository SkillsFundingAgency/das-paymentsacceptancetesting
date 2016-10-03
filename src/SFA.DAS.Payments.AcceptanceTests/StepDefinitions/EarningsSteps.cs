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
    public class EarningsSteps
    {
        public EarningsSteps(EarningContext earningContext)
        {
            EarningContext = earningContext;
        }

        public EarningContext EarningContext { get; set; }


        [When(@"an ILR file is submitted with the following data:")]
        public void WhenAnIlrFileIsSubmittedWithTheFollowingData(Table table)
        {
            // Store spec values in context
            EarningContext.IlrStartDate = DateTime.Parse(table.Rows[0][0]);
            EarningContext.IlrPlannedEndDate = DateTime.Parse(table.Rows[0][1]);
            EarningContext.IlrActualEndDate = string.IsNullOrWhiteSpace(table.Rows[0][2]) ? null : (DateTime?)DateTime.Parse(table.Rows[0][2]);
            EarningContext.IlrCompletionStatus = IlrTranslator.TranslateCompletionStatus(table.Rows[0][2]);


            // Setup reference data
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();
            var accountId = IdentifierGenerator.GenerateIdentifier();
            var ukprn = int.Parse(IdentifierGenerator.GenerateIdentifier(6, false));
            var uln = int.Parse(IdentifierGenerator.GenerateIdentifier(6, false));
            var commitmentId = IdentifierGenerator.GenerateIdentifier();

            EarningContext.Ukprn = ukprn;

            AccountDataHelper.CreateAccount(accountId, accountId, EarningContext.ReferenceDataContext.AccountBalance, environmentVariables);

            CommitmentDataHelper.CreateCommitment(commitmentId, ukprn, uln, accountId, EarningContext.IlrStartDate,
                EarningContext.IlrPlannedEndDate, EarningContext.ReferenceDataContext.AgreedPrice, IlrBuilder.Defaults.StandardCode,
                IlrBuilder.Defaults.FrameworkCode, IlrBuilder.Defaults.ProgrammeType, IlrBuilder.Defaults.PathwayCode, environmentVariables);


            // Submit ILR and capture output
            var processService = new ProcessService(new TestLogger());
            var earnedByPeriod = new Dictionary<string, decimal>();

            var periodId = 1;
            var date = EarningContext.IlrStartDate.NextCensusDate();
            var endDate = (EarningContext.IlrActualEndDate ?? EarningContext.IlrPlannedEndDate);
            var lastCensusDate = endDate.NextCensusDate();
            while (date <= lastCensusDate)
            {
                var academicYear = date.GetAcademicYear();
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

                var nextCensusDate = date.AddDays(15).NextCensusDate();

                // Submit ILR
                var actualEndDate = date >= endDate ? EarningContext.IlrActualEndDate : null;
                IlrSubmission submission = IlrBuilder.CreateAIlrSubmission()
                    .WithUkprn(ukprn)
                    .WithALearner()
                        .WithUln(uln)
                        .WithLearningDelivery()
                            .WithActualStartDate(EarningContext.IlrStartDate)
                            .WithPlannedEndDate(EarningContext.IlrPlannedEndDate)
                            .WithActualEndDate(actualEndDate)
                            .WithAgreedPrice(EarningContext.ReferenceDataContext.AgreedPrice);

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

                // Run month end
                var summarisationStatusWatcher = new TestStatusWatcher(environmentVariables, $"Summarise {date:dd/MM/yy}");
                processService.RunSummarisation(environmentVariables, summarisationStatusWatcher);

                // Move on
                date = nextCensusDate;
            }
            EarningContext.EarnedByPeriod = earnedByPeriod;
        }

        [Then(@"the provider earnings and payments break down as follows:")]
        public void ThenTheProviderEarningsBreakDownAsFollows(Table table)
        {
            var earnedRow = table.Rows.ElementAt(0);
            var levyPaidRow = table.Rows.ElementAt(1);
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


                // Verify earnings
                if (!EarningContext.EarnedByPeriod.ContainsKey(periodName))
                {
                    Assert.Fail($"Expected value for period {periodName} but none found");
                }

                var expectedEarning = decimal.Parse(earnedRow[colIndex]);
                Assert.IsTrue(EarningContext.EarnedByPeriod.ContainsKey(periodName), $"Expected earning for period {periodName} but none found");
                Assert.AreEqual(expectedEarning, EarningContext.EarnedByPeriod[periodName]);

                // Verify levy payments
                var levyPayments = LevyPaymentDataHelper.GetLevyPaymentsForPeriod(EarningContext.Ukprn, periodYear, periodMonth - 1, environmentVariables)
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
}
