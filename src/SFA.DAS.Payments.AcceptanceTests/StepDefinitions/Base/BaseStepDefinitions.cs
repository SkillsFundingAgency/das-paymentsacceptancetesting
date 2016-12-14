using System;
using System.Collections.Generic;
using System.Linq;
using IlrGenerator;
using ProviderPayments.TestStack.Core;
using ProviderPayments.TestStack.Core.Domain;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.Entities;
using SFA.DAS.Payments.AcceptanceTests.Enums;
using SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment;
using SFA.DAS.Payments.AcceptanceTests.Translators;
using TechTalk.SpecFlow;
using IlrBuilder = SFA.DAS.Payments.AcceptanceTests.Builders.IlrBuilder;
using Learner = SFA.DAS.Payments.AcceptanceTests.Entities.Learner;
using LearningDelivery = SFA.DAS.Payments.AcceptanceTests.Entities.LearningDelivery;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Base
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

        protected void SetupPeriodReferenceData(DateTime date)
        {
            AccountDataHelper.ClearAccounts(EnvironmentVariables);
            CommitmentDataHelper.ClearCommitments(EnvironmentVariables);

            var period = date.GetPeriod();

            foreach (var employer in StepDefinitionsContext.ReferenceDataContext.Employers)
            {
                AccountDataHelper.CreateAccount(
                    employer.AccountId,
                    employer.AccountId.ToString(),
                    employer.GetBalanceForMonth(period),
                    EnvironmentVariables);
            }

            AccountDataHelper.UpdateAudit(EnvironmentVariables);

            foreach (var provider in StepDefinitionsContext.Providers)
            {
                foreach (var learner in provider.Learners)
                {
                    AddLearnerCommitmentsForPeriod(date, provider.Ukprn, learner);
                }
            }

            CommitmentDataHelper.UpdateEventStreamPointer(EnvironmentVariables);
        }

        protected void AddLearnerCommitment(long ukprn, Learner learner)
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
                new CommitmentEntity
                {
                    CommitmentId = commitmentId,
                    Ukprn = ukprn,
                    Uln = learner.Uln,
                    AccountId = accountId.ToString(),
                    StartDate = learner.LearningDelivery.StartDate,
                    EndDate = learner.LearningDelivery.PlannedEndDate,
                    AgreedCost = learner.LearningDelivery.PriceEpisodes[0].TotalPrice,
                    StandardCode = IlrBuilder.Defaults.StandardCode,
                    FrameworkCode = IlrBuilder.Defaults.FrameworkCode,
                    ProgrammeType = IlrBuilder.Defaults.ProgrammeType,
                    PathwayCode = IlrBuilder.Defaults.PathwayCode,
                    Priority = commitmentPriority,
                    VersionId = "1",
                    PaymentStatus = (int)CommitmentPaymentStatus.Active,
                    PaymentStatusDescription = CommitmentPaymentStatus.Active.ToString(),
                    Payable = true
                },
                EnvironmentVariables);
        }

        protected void AddLearnerCommitmentsForPeriod(DateTime date, long ukprn, Learner learner)
        {
            var learnerCommitments = StepDefinitionsContext.ReferenceDataContext.Commitments.Where(c => c.Learner == learner.Name);

            foreach (var commitment in learnerCommitments)
            {
                if (commitment.StartDate > date)
                {
                    continue;
                }

                var employer = StepDefinitionsContext.ReferenceDataContext.Employers?.SingleOrDefault(e => e.Name == commitment.Employer);
                var accountId = employer?.AccountId ?? long.Parse(IdentifierGenerator.GenerateIdentifier(8, false));

                var commitmentStartDate = commitment.StartDate ?? learner.LearningDelivery.StartDate;
                var commitmentEndDate = commitment.ActualEndDate ?? commitment.EndDate ?? learner.LearningDelivery.PlannedEndDate;
                var priceEpisode = learner.LearningDelivery.PriceEpisodes.Where(pe => pe.StartDate >= commitmentStartDate && pe.StartDate <= commitmentEndDate).OrderBy(pe => pe.StartDate).FirstOrDefault();

                CommitmentDataHelper.CreateCommitment(
                    new CommitmentEntity
                    {
                        CommitmentId = commitment.Id,
                        Ukprn = ukprn,
                        Uln = learner.Uln,
                        AccountId = accountId.ToString(),
                        StartDate = commitmentStartDate,
                        EndDate = commitment.ActualEndDate ?? commitment.EndDate ?? learner.LearningDelivery.PlannedEndDate,
                        AgreedCost = commitment.AgreedPrice ?? priceEpisode.TotalPrice,
                        StandardCode = IlrBuilder.Defaults.StandardCode,
                        FrameworkCode = IlrBuilder.Defaults.FrameworkCode,
                        ProgrammeType = IlrBuilder.Defaults.ProgrammeType,
                        PathwayCode = IlrBuilder.Defaults.PathwayCode,
                        Priority = commitment.Priority,
                        VersionId = "1",
                        PaymentStatus = (int) CommitmentPaymentStatus.Active,
                        PaymentStatusDescription = CommitmentPaymentStatus.Active.ToString(),
                        Payable = true
                    },
                    EnvironmentVariables);
            }
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
            SubmitMonthEnd(periodDate.NextCensusDate(),new ProcessService(new TestLogger()));

        }
        protected void SetupEarningsData(Provider provider, Learner learner)
        {
            StepDefinitionsContext.AddProviderLearner(provider, learner);


            //set a default employer
            StepDefinitionsContext.ReferenceDataContext.SetDefaultEmployer(
                                                new Dictionary<string, decimal> {
                                                    { "All", int.MaxValue }
                                                });

            //setup committment and employer ref data
            SetupReferenceData();

            SetupValidLearnersData(provider.Ukprn, learner);



        }

        protected void SetupValidLearnersData(long ukprn, Learner learner)
        {

            learner.LearningDelivery.PriceEpisodes[0].Tnp1 = learner.LearningDelivery.StandardCode > 0 ?
                                                                           learner.LearningDelivery.PriceEpisodes[0].TotalPrice * 0.8m :
                                                                           learner.LearningDelivery.PriceEpisodes[0].TotalPrice;

            if (learner.LearningDelivery.StandardCode > 0)
            {
                learner.LearningDelivery.PriceEpisodes[0].Tnp2 = learner.LearningDelivery.PriceEpisodes[0].TotalPrice * 0.2m;
                                                                           
            }
            //Save File Details
            LearnerDataHelper.SaveFileDetails(ukprn,
                                                EnvironmentVariables);

            //Save Learning Provider
            LearnerDataHelper.SaveLearningProvider(ukprn,
                                                    EnvironmentVariables);

            //Save the Learner
            LearnerDataHelper.SaveLearner(ukprn,
                                        learner.Uln,
                                        learner.LearnRefNumber,
                                        EnvironmentVariables);

            //save Learner delivery
            LearnerDataHelper.SaveLearningDelivery(ukprn,
                                                    learner.LearnRefNumber,
                                                    learner.LearningDelivery,
                                                    EnvironmentVariables);

            //save learning delivery FAM
            LearnerDataHelper.SaveLearningDeliveryFAM(ukprn,learner.LearnRefNumber,learner.LearningDelivery.StartDate,learner.LearningDelivery.PlannedEndDate, EnvironmentVariables);

            LearnerDataHelper.SaveTrailblazerApprenticeshipFinancialRecord(ukprn,1,learner.LearnRefNumber,learner.LearningDelivery.PriceEpisodes[0].Tnp1.Value,EnvironmentVariables);

            //save Trailblazer
            if (learner.LearningDelivery.PriceEpisodes[0].Tnp2.HasValue )
            {
                LearnerDataHelper.SaveTrailblazerApprenticeshipFinancialRecord(ukprn,2, learner.LearnRefNumber,learner.LearningDelivery.PriceEpisodes[0].Tnp2.Value,EnvironmentVariables);
            }

            var months =  ((learner.LearningDelivery.PlannedEndDate.Year - learner.LearningDelivery.StartDate.Year) * 12) + 
                        learner.LearningDelivery.PlannedEndDate.Month - learner.LearningDelivery.StartDate.Month;

            learner.LearningDelivery.PriceEpisodes[0].MonthlyPayment = learner.LearningDelivery.PriceEpisodes[0].TotalPrice * 0.8m / months;
            learner.LearningDelivery.PriceEpisodes[0].CompletionPayment = learner.LearningDelivery.PriceEpisodes[0].TotalPrice * 0.2m; //- ((learner.LearningDelivery.PriceEpisodes[0].TotalPrice * 0.8m) / months);

            //save the learning deliver values
            EarningsDataHelper.SaveLearningDeliveryValuesForUkprn(ukprn,learner.LearnRefNumber,
                                                                    learner.LearningDelivery,
                                                                    EnvironmentVariables);
        }

        protected void SetupEnvironmentVariablesForMonth(DateTime date, string academicYear,  ref int periodId)
        {
            EnvironmentVariables.CurrentYear = academicYear;
            EnvironmentVariables.CollectionPeriod = new CollectionPeriod
            {
                PeriodId = periodId++,
                Period = "R" + (new DateTime(date.Year, date.Month, 1)).GetPeriodNumber().ToString("00"),
                CalendarMonth = date.Month,
                CalendarYear = date.Year,
                ActualsSchemaPeriod = date.Year + date.Month.ToString("00"),
                CollectionOpen = 1
            };
        }

        protected void SubmitIlr(long ukprn, Learner[] learners, string academicYear, DateTime date
          , ProcessService processService, Dictionary<string, decimal> earnedByPeriod, Dictionary<string, DataLockMatch[]> dataLockMatchesByPeriod)
        {
            var submissionLearners = learners.Select(l => new Learner
            {
                Name = l.Name,
                Uln = l.Uln,
                LearnRefNumber=l.LearnRefNumber,
                LearningDelivery = new LearningDelivery
                {
                    LearningDeliveryFams=l.LearningDelivery.LearningDeliveryFams,
                    LearnerType = l.LearningDelivery.LearnerType,
                    StartDate = l.LearningDelivery.StartDate,
                    PlannedEndDate = l.LearningDelivery.PlannedEndDate,
                    ActualEndDate = date >= l.LearningDelivery.ActualEndDate 
                                        ? l.LearningDelivery.ActualEndDate
                                        : null,
                    CompletionStatus = l.LearningDelivery.CompletionStatus,
                    StandardCode = l.LearningDelivery.StandardCode,
                    FrameworkCode= l.LearningDelivery.FrameworkCode,
                    PathwayCode=l.LearningDelivery.PathwayCode,
                    ProgrammeType=l.LearningDelivery.ProgrammeType,
                    PriceEpisodes = l.LearningDelivery.PriceEpisodes
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

            var dataLockMatches = DataLockDataHelper.GetDataLockMatchesForUkprn(ukprn, EnvironmentVariables) ?? new DataLockMatch[0];
            dataLockMatchesByPeriod.Add(date.GetPeriod(), dataLockMatches);
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
                var priceEpisodes = new List<PriceEpisode>();

                if (table.Header.Contains("Total training price") && table.Header.Contains("Residual training price"))
                {
                    var startDate = DateTime.Parse(table.Rows[rowIndex]["Total training price effective date"]);

                    priceEpisodes.Add(new PriceEpisode
                    {
                        Id = GetPriceEpisodeIdentifier(startDate),
                        StartDate = startDate,
                        EndDate = DateTime.Parse(table.Rows[rowIndex]["Residual training price effective date"]).AddDays(-1),
                        TotalPrice = decimal.Parse(table.Rows[rowIndex]["Total training price"]) + decimal.Parse(table.Rows[rowIndex]["Total assessment price"]),
                        Tnp1 = decimal.Parse(table.Rows[rowIndex]["Total training price"]),
                        Tnp2 = decimal.Parse(table.Rows[rowIndex]["Total assessment price"])
                    });

                    startDate = DateTime.Parse(table.Rows[rowIndex]["Residual training price effective date"]);

                    priceEpisodes.Add(new PriceEpisode
                    {
                        Id = GetPriceEpisodeIdentifier(startDate),
                        StartDate = startDate,
                        TotalPrice = decimal.Parse(table.Rows[rowIndex]["Residual training price"]) + decimal.Parse(table.Rows[rowIndex]["Residual assessment price"]),
                        Tnp3 = decimal.Parse(table.Rows[rowIndex]["Residual training price"]),
                        Tnp4 = decimal.Parse(table.Rows[rowIndex]["Residual assessment price"])
                    });
                }
                else
                {
                    var startDate = DateTime.Parse(table.Rows[rowIndex]["start date"]);

                    priceEpisodes.Add(new PriceEpisode
                    {
                        Id = GetPriceEpisodeIdentifier(startDate),
                        StartDate = startDate,
                        TotalPrice = decimal.Parse(table.Rows[rowIndex]["agreed price"]),
                        Tnp1 = decimal.Parse(table.Rows[rowIndex]["agreed price"]) * 0.8m,
                        Tnp2 = decimal.Parse(table.Rows[rowIndex]["agreed price"]) - decimal.Parse(table.Rows[rowIndex]["agreed price"]) * 0.8m
                    });
                }

                var learner = new Learner
                {
                    Name = table.Rows[rowIndex].ContainsKey("ULN") ? table.Rows[rowIndex]["ULN"] : string.Empty,

                    LearningDelivery = new LearningDelivery
                    {
                        LearningDeliveryFams = StepDefinitionsContext.ReferenceDataContext.LearningDeliveryFams,
                        LearnerType = LearnerType.ProgrammeOnlyDas,
                        StartDate = DateTime.Parse(table.Rows[rowIndex]["start date"]),
                        PlannedEndDate = table.Header.Contains("planned end date") ? 
                                        DateTime.Parse(table.Rows[rowIndex]["planned end date"]) : 
                                        DateTime.Parse(table.Rows[rowIndex]["start date"]).AddMonths(12),
                        ActualEndDate =
                            !table.Header.Contains("actual end date") ||
                            string.IsNullOrWhiteSpace(table.Rows[rowIndex]["actual end date"])
                                ? null
                                : (DateTime?)DateTime.Parse(table.Rows[rowIndex]["actual end date"]),
                        CompletionStatus = table.Header.Contains("completion status") ?
                            IlrTranslator.TranslateCompletionStatus(table.Rows[rowIndex]["completion status"]) :
                            CompletionStatus.InProgress,

                        FrameworkCode = table.Header.Contains("framework code") ? int.Parse(table.Rows[rowIndex]["framework code"]) : IlrBuilder.Defaults.FrameworkCode,
                        ProgrammeType = table.Header.Contains("programme type") ? int.Parse(table.Rows[rowIndex]["programme type"]) : IlrBuilder.Defaults.ProgrammeType,
                        PathwayCode = table.Header.Contains("pathway code") ? int.Parse(table.Rows[rowIndex]["pathway code"]) : IlrBuilder.Defaults.PathwayCode,

                        PriceEpisodes = priceEpisodes.ToArray()
                    }
                };


                if (table.Rows[rowIndex].ContainsKey("ULN"))
                {
                    long uln = 0;
                    long.TryParse(table.Rows[rowIndex]["ULN"], out uln);
                    learner.Uln = uln;
                }

                learner.Uln = learner.Uln > 0 ? learner.Uln : long.Parse(IdentifierGenerator.GenerateIdentifier(10, false));
                learner.LearnRefNumber = learner.Uln.ToString();

                var standardCode = table.Header.Contains("standard code") ? int.Parse(table.Rows[rowIndex]["standard code"]) : IlrBuilder.Defaults.StandardCode;

                learner.LearningDelivery.StandardCode = learner.LearningDelivery.FrameworkCode > 0 &&
                                                        learner.LearningDelivery.PathwayCode > 0 &&
                                                        learner.LearningDelivery.ProgrammeType > 0 ? 0 : standardCode;



                var provider = table.ContainsColumn("Provider")
                    ? table.Rows[rowIndex]["Provider"]
                    : "provider";

                StepDefinitionsContext.AddProviderLearner(provider, learner);
            }
        }

        protected void UpdateCommitmentsPaymentStatuses(DateTime censusDate)
        {
            foreach (var commitment in StepDefinitionsContext.ReferenceDataContext.Commitments)
            {
                if (commitment.StopPeriodCensusDate.HasValue && commitment.StopPeriodCensusDate <= censusDate)
                {
                    CommitmentDataHelper.UpdateCommitmentStatus(commitment.Id, commitment.Status, EnvironmentVariables);
                }
            }
        }

        protected CommitmentPaymentStatus GetCommitmentStatusOrThrow(string status)
        {
            CommitmentPaymentStatus paymentStatus;

            if (Enum.TryParse(status, true, out paymentStatus))
            {
                return paymentStatus;
            }

            throw new ArgumentException($"Invalid commitment status value: {status}");
        }

        private string GetPriceEpisodeIdentifier(DateTime date)
        {
            return $"{IlrBuilder.Defaults.StandardCode}-25-{date.ToString("yyyy-MM-dd")}";
        }
    }
}
