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
            SetupContextLearners(table);

            // Setup reference data
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();
            EarningAndPaymentsContext.Ukprn = int.Parse(IdentifierGenerator.GenerateIdentifier(8, false));

            SetupReferenceData(environmentVariables);

            // Process months
            ProcessMonths(EarningAndPaymentsContext.IlrStartDate.NextCensusDate(), environmentVariables);
        }

        [When(@"an ILR file is submitted in (.*) with the following data:")]
        public void WhenAnIlrFileIsSubmittedInAMonthWithTheFollowingData(string month, Table table)
        {
            // Store spec values in context
            SetupContextLearners(table);

            // Setup reference data
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();
            EarningAndPaymentsContext.Ukprn = int.Parse(IdentifierGenerator.GenerateIdentifier(8, false));

            SetupReferenceData(environmentVariables);

            // Process months
            var submissionDate = new DateTime(int.Parse(month.Substring(3)) + 2000, int.Parse(month.Substring(0, 2)), 1).NextCensusDate();

            ProcessMonths(submissionDate, environmentVariables);
        }

        [Then(@"the provider earnings and payments break down as follows:")]
        public void ThenTheProviderEarningsBreakDownAsFollows(Table table)
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


                VerifyEarningsForPeriod(periodName, colIndex, earnedRow);
                VerifyGovtCofinancePayments(periodName, periodDate, colIndex, govtCofundRow, environmentVariables);

                foreach (var employer in EarningAndPaymentsContext.ReferenceDataContext.Employers)
                {
                    var levyPaidRow = table.Rows.RowWithKey(RowKeys.DefaultLevyPayment)
                        ?? table.Rows.RowWithKey($"{employer.Name}{RowKeys.LevyPayment}");

                    var employerCofundRow = table.Rows.RowWithKey(RowKeys.DefaultCoFinanceEmployerPayment)
                                            ?? table.Rows.RowWithKey($"{RowKeys.CoFinanceEmployerPayment}{employer.Name}");

                    VerifyLevyPayments(periodName, periodDate, employer.AccountId, colIndex, levyPaidRow, environmentVariables);
                    VerifyEmployerCofinancePayments(periodName, periodDate, employer.AccountId, colIndex, employerCofundRow, environmentVariables);
                }
            }
        }

        [Then(@"the transaction types for the payments are:")]
        public void ThenTheTransactionsForThePaymentsAre(Table table)
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

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

                VerifyPaymentsDueByTransactionType(periodName, periodDate, colIndex, TransactionType.OnProgram, onProgramRow, environmentVariables);
                VerifyPaymentsDueByTransactionType(periodName, periodDate, colIndex, TransactionType.Completion, completionRow, environmentVariables);
                VerifyPaymentsDueByTransactionType(periodName, periodDate, colIndex, TransactionType.Balancing, balancingRow, environmentVariables);
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
        private void SetupContextLearners(Table table)
        {
            EarningAndPaymentsContext.Learners = new Contexts.Learner[table.RowCount];

            for (var rowIndex = 0; rowIndex < table.RowCount; rowIndex++)
            {
                EarningAndPaymentsContext.Learners[rowIndex] = new Contexts.Learner
                {
                    Name = table.Rows[rowIndex].ContainsKey("ULN") ? table.Rows[rowIndex]["ULN"] : string.Empty,
                    Uln = long.Parse(IdentifierGenerator.GenerateIdentifier(10, false)),
                    LearningDelivery = new LearningDelivery
                    {
                        AgreedPrice = decimal.Parse(table.Rows[rowIndex]["agreed price"]),
                        LearnerType = LearnerType.ProgrammeOnlyDas,
                        StartDate = DateTime.Parse(table.Rows[rowIndex]["start date"]),
                        PlannedEndDate = DateTime.Parse(table.Rows[rowIndex]["planned end date"]),
                        ActualEndDate = !table.Header.Contains("actual end date") || string.IsNullOrWhiteSpace(table.Rows[rowIndex]["actual end date"]) ? null : (DateTime?)DateTime.Parse(table.Rows[rowIndex]["actual end date"]),
                        CompletionStatus = IlrTranslator.TranslateCompletionStatus(table.Rows[rowIndex]["completion status"])
                    }
                };
            }
        }
        private void SetupReferenceData(EnvironmentVariables environmentVariables)
        {
            foreach (var employer in EarningAndPaymentsContext.ReferenceDataContext.Employers)
            {
                AccountDataHelper.CreateAccount(employer.AccountId, employer.AccountId.ToString(), 0.00m, environmentVariables);
            }

            AccountDataHelper.UpdateAudit(environmentVariables);

            foreach (var learner in EarningAndPaymentsContext.Learners)
            {
                AddLearnerCommitment(learner, environmentVariables);
            }

            CommitmentDataHelper.UpdateEventStreamPointer(environmentVariables);
        }
        private void ProcessMonths(DateTime start, EnvironmentVariables environmentVariables)
        {
            var processService = new ProcessService(new TestLogger());
            var earnedByPeriod = new Dictionary<string, decimal>();

            var periodId = 1;
            var date = start.NextCensusDate();
            var endDate = EarningAndPaymentsContext.IlrEndDate;
            var lastCensusDate = endDate.NextCensusDate();

            while (date <= lastCensusDate)
            {
                var period = date.GetPeriod();
                UpdateAccountsBalances(period, environmentVariables);

                var academicYear = date.GetAcademicYear();

                SetupEnvironmentVariablesForMonth(date, academicYear, environmentVariables, ref periodId);

                SubmitIlr(EarningAndPaymentsContext.Ukprn, EarningAndPaymentsContext.Learners, academicYear, date, environmentVariables, processService, earnedByPeriod);

                SubmitMonthEnd(date, environmentVariables, processService);

                date = date.AddDays(15).NextCensusDate();
            }

            EarningAndPaymentsContext.EarnedByPeriod = earnedByPeriod;
        }
        private void SubmitIlr(long ukprn, Contexts.Learner[] learners, string academicYear, DateTime date,
            EnvironmentVariables environmentVariables, ProcessService processService, Dictionary<string, decimal> earnedByPeriod)
        {
            var submissionLearners = learners.Select(l => new Contexts.Learner
            {
                Name = l.Name,
                Uln = l.Uln,
                LearningDelivery = new LearningDelivery
                {
                    AgreedPrice = l.LearningDelivery.AgreedPrice,
                    LearnerType = l.LearningDelivery.LearnerType,
                    StartDate = l.LearningDelivery.StartDate,
                    PlannedEndDate = l.LearningDelivery.PlannedEndDate,
                    ActualEndDate = date >= l.LearningDelivery.ActualEndDate ? l.LearningDelivery.ActualEndDate : null,
                    CompletionStatus = l.LearningDelivery.CompletionStatus
                }
            }).ToArray();

            IlrSubmission submission = IlrBuilder.CreateAIlrSubmission()
                .WithUkprn(ukprn)
                .WithMultipleLearners()
                    .WithLearners(submissionLearners);

            AcceptanceTestDataHelper.AddCurrentActivePeriod(date.Year, date.Month, environmentVariables);

            var ilrStatusWatcher = new TestStatusWatcher(environmentVariables, $"Submit ILR to {date:dd/MM/yy}");
            processService.RunIlrSubmission(submission, environmentVariables, ilrStatusWatcher);

            var periodEarnings = EarningsDataHelper.GetPeriodisedValuesForUkprn(ukprn, environmentVariables).LastOrDefault() ?? new PeriodisedValuesEntity();
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
            Assert.AreEqual(expectedEarning, EarningAndPaymentsContext.EarnedByPeriod[periodName], $"Expected earning of {expectedEarning} for period {periodName} but found {EarningAndPaymentsContext.EarnedByPeriod[periodName]}");
        }
        private void VerifyLevyPayments(string periodName, DateTime periodDate, long accountId,
            int colIndex, TableRow levyPaidRow, EnvironmentVariables environmentVariables)
        {
            if (levyPaidRow == null)
            {
                return;
            }

            var levyPaymentDate = periodDate.AddMonths(-1);

            var levyPayments = LevyPaymentDataHelper.GetAccountLevyPaymentsForPeriod(EarningAndPaymentsContext.Ukprn, accountId,
                levyPaymentDate.Year, levyPaymentDate.Month, environmentVariables)
                               ?? new LevyPaymentEntity[0];

            var actualLevyPayment = levyPayments.Length == 0 ? 0m : levyPayments.Sum(p => p.Amount);
            var expectedLevyPayment = decimal.Parse(levyPaidRow[colIndex]);
            Assert.AreEqual(expectedLevyPayment, actualLevyPayment, $"Expected a levy payment of {expectedLevyPayment} but made a payment of {actualLevyPayment} for {periodName}");
        }
        private void VerifyGovtCofinancePayments(string periodName, DateTime periodDate,
            int colIndex, TableRow govtCofundRow, EnvironmentVariables environmentVariables)
        {
            if (govtCofundRow == null)
            {
                return;
            }

            var cofinancePayments = CoFinancePaymentsDataHelper.GetCoInvestedPaymentsForPeriod(EarningAndPaymentsContext.Ukprn,
                periodDate.Year, periodDate.Month, environmentVariables)
                                    ?? new CoFinancePaymentEntity[0];

            var actualGovtPayment = cofinancePayments.Where(p => p.FundingSource == 2).Sum(p => p.Amount);
            var expectedGovtPayment = decimal.Parse(govtCofundRow[colIndex]);
            Assert.AreEqual(expectedGovtPayment, actualGovtPayment, $"Expected a government co-finance payment of {expectedGovtPayment} but made a payment of {actualGovtPayment} for {periodName}");
        }
        private void VerifyEmployerCofinancePayments(string periodName, DateTime periodDate, long accountId,
            int colIndex, TableRow employerCofundRow, EnvironmentVariables environmentVariables)
        {
            if (employerCofundRow == null)
            {
                return;
            }

            var employerPaymentDate = periodDate.AddMonths(-1);

            var cofinancePayments = CoFinancePaymentsDataHelper.GetAccountCoInvestedPaymentsForPeriod(EarningAndPaymentsContext.Ukprn, accountId,
                employerPaymentDate.Year, employerPaymentDate.Month, environmentVariables)
                                    ?? new CoFinancePaymentEntity[0];

            var actualEmployerPayment = cofinancePayments.Where(p => p.FundingSource == 3).Sum(p => p.Amount);
            var expectedEmployerPayment = decimal.Parse(employerCofundRow[colIndex]);
            Assert.AreEqual(expectedEmployerPayment, actualEmployerPayment, $"Expected a employer co-finance payment of {expectedEmployerPayment} but made a payment of {actualEmployerPayment} for {periodName}");
        }
        private void VerifyPaymentsDueByTransactionType(string periodName, DateTime periodDate, int colIndex, TransactionType paymentType,
            TableRow paymentsRow, EnvironmentVariables environmentVariables)
        {
            if (paymentsRow == null)
            {
                return;
            }

            var paymentsDueDate = periodDate.AddMonths(-1);

            var paymentsDue = PaymentsDueDataHelper.GetPaymentsDueForPeriod(EarningAndPaymentsContext.Ukprn,
                paymentsDueDate.Year, paymentsDueDate.Month, environmentVariables)
                              ?? new RequiredPaymentEntity[0];

            var actualPaymentDue = paymentsDue.Length == 0 ? 0m : paymentsDue.Where(p => p.TransactionType == (int)paymentType).Sum(p => p.AmountDue);
            var expectedPaymentDue = decimal.Parse(paymentsRow[colIndex]);
            Assert.AreEqual(expectedPaymentDue, actualPaymentDue, $"Expected a {paymentType} payment due of {expectedPaymentDue} but made a payment of {actualPaymentDue} for {periodName}");
        }

        private void UpdateAccountsBalances(string month, EnvironmentVariables environmentVariables)
        {
            foreach (var employer in EarningAndPaymentsContext.ReferenceDataContext.Employers)
            {
                AccountDataHelper.UpdateAccountBalance(employer.AccountId, employer.GetBalanceForMonth(month), environmentVariables);
            }
        }

        private void AddLearnerCommitment(Contexts.Learner learner, EnvironmentVariables environmentVariables)
        {
            var commitmentId = long.Parse(IdentifierGenerator.GenerateIdentifier(6, false));
            var commitmentPriority = 1;
            var accountId = long.Parse(IdentifierGenerator.GenerateIdentifier(8, false));

            var commitment = EarningAndPaymentsContext.ReferenceDataContext.Commitments?.SingleOrDefault(c => c.Learner == learner.Name);

            if (commitment != null)
            {
                commitmentId = commitment.Id;
                commitmentPriority = commitment.Priority;

                var employer = EarningAndPaymentsContext.ReferenceDataContext.Employers?.SingleOrDefault(e => e.Name == commitment.Employer);

                if (employer != null)
                {
                    accountId = employer.AccountId;
                }
            }

            CommitmentDataHelper.CreateCommitment(
                commitmentId,
                EarningAndPaymentsContext.Ukprn,
                learner.Uln,
                accountId.ToString(),
                learner.LearningDelivery.StartDate,
                learner.LearningDelivery.PlannedEndDate,
                learner.LearningDelivery.AgreedPrice,
                IlrBuilder.Defaults.StandardCode,
                IlrBuilder.Defaults.FrameworkCode,
                IlrBuilder.Defaults.ProgrammeType,
                IlrBuilder.Defaults.PathwayCode,
                commitmentPriority,
                "1",
                environmentVariables);
        }



        [Given(@"Provider has previously earned (.*) in the period")]
        public void GivenProviderHasPreviouslyEarnedInPeriod(decimal previousEarning)
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();
            EarningAndPaymentsContext.Ukprn = int.Parse(IdentifierGenerator.GenerateIdentifier(8, false));

            //setuo a learner
            var learner = new Contexts.Learner
            {
                Name = string.Empty,
                Uln = long.Parse(IdentifierGenerator.GenerateIdentifier(10, false)),
                LearningDelivery = new LearningDelivery
                {
                    AgreedPrice = 15000,
                    LearnerType = LearnerType.ProgrammeOnlyDas,
                    StartDate = new DateTime(2017, 09, 01),
                    PlannedEndDate = new DateTime(2018, 09, 08),
                    ActualEndDate = null,
                    CompletionStatus = CompletionStatus.InProgress
                }
            };


            EarningAndPaymentsContext.Learners = new Contexts.Learner[1] { learner };

            //set a default employer
            EarningAndPaymentsContext.ReferenceDataContext.SetDefaultEmployer(
                                                new Dictionary<string, decimal> {
                                                    { "All", int.MaxValue }
                                                });

            //setup committment and employer ref data
            SetupReferenceData(environmentVariables);

            //Save File Details
            LearnerDataHelper.SaveFileDetails(EarningAndPaymentsContext.Ukprn, 
                                                environmentVariables);

            //Save Learning Provider
            LearnerDataHelper.SaveLearningProvider(EarningAndPaymentsContext.Ukprn,
                                                    environmentVariables);

            //Save the Learner
            LearnerDataHelper.SaveLearner(EarningAndPaymentsContext.Ukprn, 
                                        learner.Uln,  
                                        environmentVariables);

            //save Learner delivery
            LearnerDataHelper.SaveLearningDelivery(EarningAndPaymentsContext.Ukprn,
                                                    learner.LearningDelivery.StartDate,
                                                    learner.LearningDelivery.PlannedEndDate, 
                                                    environmentVariables);

            //save learning delivery FAM
            LearnerDataHelper.SaveLearningDeliveryFAM(EarningAndPaymentsContext.Ukprn, environmentVariables);

            //save Trailblazer
            LearnerDataHelper.SaveTrailblazerApprenticeshipFinancialRecord(EarningAndPaymentsContext.Ukprn,
                                                                            1,
                                                                            12000, 
                                                                            environmentVariables);
            LearnerDataHelper.SaveTrailblazerApprenticeshipFinancialRecord(EarningAndPaymentsContext.Ukprn, 
                                                                            2, 
                                                                            3000, 
                                                                            environmentVariables);

            var committment = EarningAndPaymentsContext.ReferenceDataContext.Commitments.First();
            var account = EarningAndPaymentsContext.ReferenceDataContext.Employers.FirstOrDefault(x => x.Name == committment.Employer);

            //save the learning deliver values
            EarningsDataHelper.SaveLearningDeliveryValuesForUkprn(EarningAndPaymentsContext.Ukprn, 
                                                                    learner.Uln,
                                                                    15000, 
                                                                    new DateTime(2017, 09, 01),
                                                                    new DateTime(2018, 09, 08), 
                                                                    1000, 
                                                                    3000, 
                                                                    environmentVariables);

            //Save the previous earning
            EarningsDataHelper.SaveEarnedAmount(EarningAndPaymentsContext.Ukprn, 
                                                committment.Id, 
                                                account.AccountId,
                                                EarningAndPaymentsContext.Learners[0].Uln, 
                                                "R01", 
                                                08, 
                                                2016, 
                                                1, 
                                                previousEarning, environmentVariables);




        }

        [When(@"an earning of (.*) is calculated for the period")]
        public void AnEarningIsCalculatedForThePeriod(decimal earnedAmount)
        {

            // Setup reference data
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            //save the periodiosed values
            EarningsDataHelper.SavePeriodisedValuesForUkprn(EarningAndPaymentsContext.Ukprn, 
                                                            new Dictionary<string, decimal> { { "Period_1", earnedAmount } },
                                                            environmentVariables);
            
           
            // Process month end now
            SubmitMonthEnd(new DateTime(2016, 09, 01).NextCensusDate(), 
                            environmentVariables, 
                            new ProcessService(new TestLogger()));
        }


        [Then(@"a payment of (.*) is due")]
        public void ThenAPaymentIsDue(decimal dueAmount)
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();
            
            //Get the due amount 
            var earning = PaymentsDueDataHelper.GetPaymentsDueForPeriod(EarningAndPaymentsContext.Ukprn,
                                                                        2016,
                                                                        08,
                                                                        environmentVariables)
                                                                        .FirstOrDefault();
             
            if (dueAmount != 0)
            {
                Assert.IsNotNull(earning, $"Expected earning for the period but nothing found");
                Assert.AreEqual(dueAmount, earning.AmountDue, $"Expected earning of {dueAmount} for period R02 but found {earning.AmountDue}");
            }
            else
            {
                Assert.IsNull(earning, $"There was no expected earning for the period but earnigs data found");

            }
        }
    }
}
