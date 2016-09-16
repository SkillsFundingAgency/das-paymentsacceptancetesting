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
        public void WhenAnILRFileIsSubmittedWithTheFollowingData(Table table)
        {
            throw new Exception("FAILURE!!!!");

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

            AccountDataHelper.CreateAccount(accountId, accountId, EarningContext.ReferenceDataContext.AccountBalance, environmentVariables);

            CommitmentDataHelper.CreateCommitment(commitmentId, ukprn, uln, accountId, EarningContext.IlrStartDate,
                EarningContext.IlrPlannedEndDate, EarningContext.ReferenceDataContext.AgreedPrice, IlrBuilder.Defaults.StandardCode,
                IlrBuilder.Defaults.FrameworkCode, IlrBuilder.Defaults.ProgrammeType, IlrBuilder.Defaults.PathwayCode, environmentVariables);


            // Submit ILR and capture output
            var processService = new ProcessService(new TestLogger());
            var earnedByPeriod = new List<PeriodisedValuesEntity>();

            var endDate = EarningContext.IlrActualEndDate ?? EarningContext.IlrPlannedEndDate;
            var academicYearsToSubmitTo = EarningContext.IlrStartDate.AcademicYearsUntil(endDate);
            foreach (var academicYear in academicYearsToSubmitTo)
            {
                environmentVariables.CurrentYear = academicYear;

                var actualEndDateForYear = academicYear == academicYearsToSubmitTo[academicYearsToSubmitTo.Length - 1]
                    ? EarningContext.IlrActualEndDate : null;

                IlrSubmission submission = IlrBuilder.CreateAIlrSubmission()
                    .WithUkprn(ukprn)
                    .WithALearner()
                        .WithLearningDelivery()
                            .WithActualStartDate(EarningContext.IlrStartDate)
                            .WithPlannedEndDate(EarningContext.IlrPlannedEndDate)
                            .WithActualEndDate(actualEndDateForYear)
                            .WithAgreedPrice(EarningContext.ReferenceDataContext.AgreedPrice);

                var statusWatcher = new TestStatusWatcher(environmentVariables, $"Submit ILR to year {academicYear}");
                processService.RunIlrSubmission(submission, environmentVariables, statusWatcher);

                earnedByPeriod.AddRange(EarningsDataHelper.GetPeriodisedValuesForUkprn(ukprn, environmentVariables));
            }

            EarningContext.EarnedByPeriod = earnedByPeriod.ToArray();
        }

        [Then(@"the provider earnings break down as follows:")]
        public void ThenTheProviderEarningsBreakDownAsFollows(Table table)
        {
            var values = new Dictionary<string, decimal>();

            // Convert periodised values
            for (var i = 0; i < EarningContext.EarnedByPeriod.Length; i++)
            {
                var periodisedValues = EarningContext.EarnedByPeriod[i];
                var academicYear = EarningContext.IlrStartDate.AddYears(i).GetAcademicYear();

                values.Add("08/" + academicYear.Substring(0, 2), periodisedValues.Period_1);
                values.Add("09/" + academicYear.Substring(0, 2), periodisedValues.Period_2);
                values.Add("10/" + academicYear.Substring(0, 2), periodisedValues.Period_3);
                values.Add("11/" + academicYear.Substring(0, 2), periodisedValues.Period_4);
                values.Add("12/" + academicYear.Substring(0, 2), periodisedValues.Period_5);
                values.Add("01/" + academicYear.Substring(2), periodisedValues.Period_6);
                values.Add("02/" + academicYear.Substring(2), periodisedValues.Period_7);
                values.Add("03/" + academicYear.Substring(2), periodisedValues.Period_8);
                values.Add("04/" + academicYear.Substring(2), periodisedValues.Period_9);
                values.Add("05/" + academicYear.Substring(2), periodisedValues.Period_10);
                values.Add("06/" + academicYear.Substring(2), periodisedValues.Period_11);
                values.Add("07/" + academicYear.Substring(2), periodisedValues.Period_12);
            }


            // Assert correctness
            var row = table.Rows.First();
            for (var colIndex = 1; colIndex < table.Header.Count; colIndex++)
            {
                var periodName = table.Header.ElementAt(colIndex);
                if (periodName == "...")
                {
                    continue;
                }

                if (!values.ContainsKey(periodName))
                {
                    Assert.Fail($"Expected value for period {periodName} but none found");
                }

                var expectedValue = decimal.Parse(row[colIndex]);
                Assert.IsTrue(values.ContainsKey(periodName), $"Expected value for period {periodName} but none found");
                Assert.AreEqual(expectedValue, values[periodName]);
            }
        }

    }
}
