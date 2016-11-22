
using IlrGenerator;
using ProviderPayments.TestStack.Core;
using ProviderPayments.TestStack.Core.Domain;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IlrBuilder = SFA.DAS.Payments.AcceptanceTests.Builders.IlrBuilder;
using LearningDelivery = SFA.DAS.Payments.AcceptanceTests.Contexts.LearningDelivery;
using TechTalk.SpecFlow;
using SFA.DAS.Payments.AcceptanceTests.Translators;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    public class BaseStepDefinitions

    {

        #region Properties
        protected StepDefinitionsContext StepDefinitionsContext { get; set; }
        protected EnvironmentVariables EnvironmentVariables { get; set; }

        #endregion

        #region Constructor
        public BaseStepDefinitions(StepDefinitionsContext stepDefinitionsContext)
        {
            StepDefinitionsContext = stepDefinitionsContext;
            EnvironmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

        }
        #endregion

        protected void SetupReferenceData()
        {
            foreach (var employer in StepDefinitionsContext.ReferenceDataContext.Employers)
            {
                AccountDataHelper.CreateAccount(employer.AccountId, employer.AccountId.ToString(), 0.00m, EnvironmentVariables);
            }

            AccountDataHelper.UpdateAudit(EnvironmentVariables);

            foreach (var provider in StepDefinitionsContext.Providers)
            {
                foreach (var learner in provider.Learners)
                {
                    AddLearnerCommitment(provider.Ukprn, learner);
                }
            }

            CommitmentDataHelper.UpdateEventStreamPointer(EnvironmentVariables);
        }

        protected void AddLearnerCommitment(long ukprn, Contexts.Learner learner)
        {
            var commitmentId = long.Parse(IdentifierGenerator.GenerateIdentifier(6, false));
            var commitmentPriority = 1;
            var accountId = long.Parse(IdentifierGenerator.GenerateIdentifier(8, false));

            var commitment = StepDefinitionsContext.ReferenceDataContext.Commitments?.SingleOrDefault(c => c.Learner == learner.Name);

            if (commitment != null)
            {
                commitmentId = commitment.Id;
                commitmentPriority = commitment.Priority;

                var employer = StepDefinitionsContext.ReferenceDataContext.Employers?.SingleOrDefault(e => e.Name == commitment.Employer);

                if (employer != null)
                {
                    accountId = employer.AccountId;
                }
            }

            CommitmentDataHelper.CreateCommitment(
                commitmentId,
                ukprn,
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
                "1",EnvironmentVariables);
        }

        protected void SubmitMonthEnd(DateTime date, ProcessService processService)
        {
            var summarisationStatusWatcher = new TestStatusWatcher(EnvironmentVariables, $"Summarise {date:dd/MM/yy}");
            processService.RunSummarisation(EnvironmentVariables, summarisationStatusWatcher);
        }


        protected void UpdateAccountsBalances(string month)
        {
            foreach (var employer in StepDefinitionsContext.ReferenceDataContext.Employers)
            {
                AccountDataHelper.UpdateAccountBalance(employer.AccountId, employer.GetBalanceForMonth(month), EnvironmentVariables);
            }
        }

        protected void RunMonthEnd(DateTime date)
        {
           

            var periodDate = new DateTime(2016, 09, 20);
            var academicYear = date.GetAcademicYear();
            var periodId = 1;

            SetupEnvironmentVariablesForMonth(date, academicYear, ref periodId);


            // Process month end now
            SubmitMonthEnd(new DateTime(2016, 09, 20).NextCensusDate(),new ProcessService(new TestLogger()));

        }
        protected void SetupEarningsData(Provider provider, Contexts.Learner learner)
        {
            StepDefinitionsContext.AddProviderLearner(provider, learner);


            //set a default employer
            StepDefinitionsContext.ReferenceDataContext.SetDefaultEmployer(
                                                new Dictionary<string, decimal> {
                                                    { "All", int.MaxValue }
                                                });

            //setup committment and employer ref data
            SetupReferenceData();

            //Save File Details
            LearnerDataHelper.SaveFileDetails(provider.Ukprn,
                                                EnvironmentVariables);

            //Save Learning Provider
            LearnerDataHelper.SaveLearningProvider(provider.Ukprn,
                                                    EnvironmentVariables);

            //Save the Learner
            LearnerDataHelper.SaveLearner(provider.Ukprn,
                                        learner.Uln,
                                        EnvironmentVariables);

            //save Learner delivery
            LearnerDataHelper.SaveLearningDelivery(provider.Ukprn,
                                                    learner.LearningDelivery.StartDate,
                                                    learner.LearningDelivery.PlannedEndDate,
                                                    EnvironmentVariables);

            //save learning delivery FAM
            LearnerDataHelper.SaveLearningDeliveryFAM(provider.Ukprn, EnvironmentVariables);

            //save Trailblazer
            LearnerDataHelper.SaveTrailblazerApprenticeshipFinancialRecord(provider.Ukprn,
                                                                            1,
                                                                            12000,
                                                                            EnvironmentVariables);
            LearnerDataHelper.SaveTrailblazerApprenticeshipFinancialRecord(provider.Ukprn,
                                                                            2,
                                                                            3000,
                                                                            EnvironmentVariables);
            //save the learning deliver values
            EarningsDataHelper.SaveLearningDeliveryValuesForUkprn(provider.Ukprn,
                                                                    learner.Uln,
                                                                    15000,
                                                                    new DateTime(2017, 09, 01),
                                                                    new DateTime(2018, 09, 08),
                                                                    1000,
                                                                    3000,
                                                                    EnvironmentVariables);
        }

        protected void SetupEnvironmentVariablesForMonth(DateTime date, string academicYear,  ref int periodId)
        {
            EnvironmentVariables.CurrentYear = academicYear;
            EnvironmentVariables.SummarisationPeriod = new SummarisationCollectionPeriod
            {
                PeriodId = periodId++,
                CollectionPeriod = "R" + (new DateTime(date.Year, date.Month, 1)).GetPeriodNumber().ToString("00"),
                CalendarMonth = date.Month,
                CalendarYear = date.Year,
                ActualsSchemaPeriod = date.Year + date.Month.ToString("00"),
                CollectionOpen = 1
            };
        }

        protected void SubmitIlr(long ukprn, Contexts.Learner[] learners, string academicYear, DateTime date
          , ProcessService processService, Dictionary<string, decimal> earnedByPeriod)
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

            AcceptanceTestDataHelper.AddCurrentActivePeriod(date.Year, date.Month, EnvironmentVariables);

            var ilrStatusWatcher = new TestStatusWatcher(EnvironmentVariables, $"Submit ILR to {date:dd/MM/yy}");
            processService.RunIlrSubmission(submission, EnvironmentVariables, ilrStatusWatcher);

            var periodEarnings = EarningsDataHelper.GetPeriodisedValuesForUkprn(ukprn, EnvironmentVariables).LastOrDefault() ?? new PeriodisedValuesEntity();
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


        protected void SetupContextProviders(Table table)
        {
            if (table.ContainsColumn("Provider"))
            {
                for (var rowIndex = 0; rowIndex < table.RowCount; rowIndex++)
                {
                    StepDefinitionsContext.AddProvider(table.Rows[rowIndex]["Provider"]);
                }
            }   
            else
            {
                StepDefinitionsContext.SetDefaultProvider();
            }
        }

        protected void SetupContexLearners(Table table)
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

                StepDefinitionsContext.AddProviderLearner(provider, learner);
            }
        }

    }
}
