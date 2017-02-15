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
using CompletionStatus = SFA.DAS.Payments.AcceptanceTests.Enums.CompletionStatus;

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
                    AddLearnerCommitment(provider.Ukprn, learner, provider.Name);
                }
            }

            CommitmentDataHelper.UpdateEventStreamPointer(EnvironmentVariables);
        }

        protected void SetupPeriodReferenceData(DateTime date)
        {
            AccountDataHelper.ClearAccounts(EnvironmentVariables);
            CommitmentDataHelper.ClearCommitments(EnvironmentVariables);

            var period = date.GetPeriod();

            if (StepDefinitionsContext.DasScenario)
            {
                foreach (var employer in StepDefinitionsContext.ReferenceDataContext.Employers)
                {
                    AccountDataHelper.CreateAccount(
                        employer.AccountId,
                        employer.AccountId.ToString(),
                        employer.GetBalanceForMonth(period),
                        EnvironmentVariables);
                }

                AccountDataHelper.UpdateAudit(EnvironmentVariables);
            }

            foreach (var provider in StepDefinitionsContext.Providers)
            {
                foreach (var learner in provider.Learners)
                {
                    if (learner.LearningDelivery.LearnerType == LearnerType.ProgrammeOnlyNonDas)
                    {
                        continue;
                    }

                    AddLearnerCommitmentsForPeriod(date, provider.Ukprn, learner, provider.Name);
                }
            }

            CommitmentDataHelper.UpdateEventStreamPointer(EnvironmentVariables);
        }

        protected void AddLearnerCommitment(long ukprn, Learner learner, string provider)
        {
            var commitmentId = long.Parse(IdentifierGenerator.GenerateIdentifier(6, false));
            var commitmentPriority = 1;
            var accountId = long.Parse(IdentifierGenerator.GenerateIdentifier(8, false));

            var commitment = StepDefinitionsContext.ReferenceDataContext.Commitments?.SingleOrDefault(c => c.Learner == learner.Name && c.Provider == provider);

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
                    StandardCode = commitment.StandardCode.HasValue ? commitment.StandardCode.Value : IlrBuilder.Defaults.StandardCode,
                    FrameworkCode = IlrBuilder.Defaults.FrameworkCode,
                    ProgrammeType = IlrBuilder.Defaults.ProgrammeType,
                    PathwayCode = IlrBuilder.Defaults.PathwayCode,
                    Priority = commitmentPriority,
                    VersionId = commitment.VersionId,
                    PaymentStatus = (int)commitment.Status,
                    PaymentStatusDescription = commitment.Status.ToString(),
                    EffectiveFrom = learner.LearningDelivery.StartDate
                },
                EnvironmentVariables);
        }

        protected void AddLearnerCommitmentsForPeriod(DateTime date, long ukprn, Learner learner, string provider)
        {
            var learnerCommitments = StepDefinitionsContext.ReferenceDataContext.Commitments.Where(c => c.Learner == learner.Name && c.Provider == provider);

            foreach (var commitment in learnerCommitments)
            {
                if (commitment.StartDate > date)
                {
                    continue;
                }

                if (commitment.EffectiveFrom.HasValue && commitment.EffectiveFrom.Value > date)
                {
                    continue;
                }

                var employer = StepDefinitionsContext.ReferenceDataContext.Employers?.SingleOrDefault(e => e.Name == commitment.Employer);
                var accountId = employer?.AccountId ?? long.Parse(IdentifierGenerator.GenerateIdentifier(8, false));

                var commitmentStartDate = commitment.StartDate ?? learner.LearningDelivery.StartDate;
                var commitmentEffectiveFromDate = commitment.EffectiveFrom ?? commitmentStartDate;
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
                        StandardCode = commitment.StandardCode.HasValue ? commitment.StandardCode.Value : IlrBuilder.Defaults.StandardCode,
                        FrameworkCode = IlrBuilder.Defaults.FrameworkCode,
                        ProgrammeType = IlrBuilder.Defaults.ProgrammeType,
                        PathwayCode = IlrBuilder.Defaults.PathwayCode,
                        Priority = commitment.Priority,
                        VersionId = commitment.VersionId,
                        PaymentStatus = commitment.StopPeriodCensusDate.HasValue
                                            ? (int)CommitmentPaymentStatus.Active
                                            : (int)commitment.Status,
                        PaymentStatusDescription = commitment.StopPeriodCensusDate.HasValue
                                            ? CommitmentPaymentStatus.Active.ToString()
                                            : commitment.Status.ToString(),
                        EffectiveFrom = commitmentEffectiveFromDate,
                        EffectiveTo = commitment.EffectiveTo
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
            if (StepDefinitionsContext.DasScenario)
            {
                foreach (var employer in StepDefinitionsContext.ReferenceDataContext.Employers)
                {
                    AccountDataHelper.UpdateAccountBalance(employer.AccountId, employer.GetBalanceForMonth(month), EnvironmentVariables);
                }
            }
        }

        protected void RunMonthEnd(DateTime date)
        {


            var periodDate = new DateTime(2016, 09, 20);
            var academicYear = date.GetAcademicYear();
            var periodId = 1;

            SetupEnvironmentVariablesForMonth(date, academicYear, ref periodId);


            // Process month end now
            SubmitMonthEnd(periodDate.NextCensusDate(), new ProcessService(new TestLogger()));

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
            LearnerDataHelper.SaveLearningDeliveryFAM(ukprn, learner.LearnRefNumber, learner.LearningDelivery.StartDate, learner.LearningDelivery.PlannedEndDate, EnvironmentVariables);

            LearnerDataHelper.SaveTrailblazerApprenticeshipFinancialRecord(ukprn, 1, learner.LearnRefNumber, learner.LearningDelivery.PriceEpisodes[0].Tnp1.Value, EnvironmentVariables);

            //save Trailblazer
            if (learner.LearningDelivery.PriceEpisodes[0].Tnp2.HasValue)
            {
                LearnerDataHelper.SaveTrailblazerApprenticeshipFinancialRecord(ukprn, 2, learner.LearnRefNumber, learner.LearningDelivery.PriceEpisodes[0].Tnp2.Value, EnvironmentVariables);
            }

            var months = ((learner.LearningDelivery.PlannedEndDate.Year - learner.LearningDelivery.StartDate.Year) * 12) +
                        learner.LearningDelivery.PlannedEndDate.Month - learner.LearningDelivery.StartDate.Month;

            learner.LearningDelivery.PriceEpisodes[0].MonthlyPayment = learner.LearningDelivery.PriceEpisodes[0].TotalPrice * 0.8m / months;
            learner.LearningDelivery.PriceEpisodes[0].CompletionPayment = learner.LearningDelivery.PriceEpisodes[0].TotalPrice * 0.2m; //- ((learner.LearningDelivery.PriceEpisodes[0].TotalPrice * 0.8m) / months);

            //save the learning deliver values
            EarningsDataHelper.SaveLearningDeliveryValuesForUkprn(ukprn, learner.LearnRefNumber,
                                                                    learner.LearningDelivery,
                                                                    EnvironmentVariables);
        }

        protected void SetupEnvironmentVariablesForMonth(DateTime date, string academicYear, ref int periodId)
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

        protected void SubmitIlr(Provider provider,
                                string academicYear,
                                DateTime date,
                                ProcessService processService)
        {
            var submissionLearners = provider.Learners.Select(l =>
            {
                var learner = new Learner
                {
                    Name = l.Name,
                    Uln = l.Uln,
                    LearnRefNumber = l.LearnRefNumber,
                    DateOfBirth = l.DateOfBirth,
                    EmployerId = l.EmployerId,
                    EmploymentStatus = l.EmploymentStatus,
                    EmploymentStatusDate = l.EmploymentStatusDate,
                    EmploymentStatusMonitoring = l.EmploymentStatusMonitoring 

                };

                foreach (var ld in l.LearningDeliveries)
                {
                    learner.LearningDeliveries.Add(
                        new LearningDelivery
                        {
                            LearningDeliveryFams = ld.LearningDeliveryFams,
                            LearnerType = ld.LearnerType,
                            StartDate = ld.StartDate,
                            PlannedEndDate = ld.PlannedEndDate,
                            ActualEndDate = date >= ld.ActualEndDate
                                ? ld.ActualEndDate
                                : null,
                            CompletionStatus = ld.CompletionStatus,
                            StandardCode = ld.StandardCode,
                            FrameworkCode = ld.FrameworkCode,
                            PathwayCode = ld.PathwayCode,
                            ProgrammeType = ld.ProgrammeType,
                            PriceEpisodes = ld.PriceEpisodes
                        });
                }

                return learner;
            }).ToArray();

            IlrSubmission submission = IlrBuilder.CreateAIlrSubmission()
                .WithUkprn(provider.Ukprn)
                .WithMultipleLearners()
                    .WithLearners(submissionLearners);

            AcceptanceTestDataHelper.AddCurrentActivePeriod(date.Year, date.Month, EnvironmentVariables);

            var ilrStatusWatcher = new TestStatusWatcher(EnvironmentVariables, $"Submit ILR to {date:dd/MM/yy}");
            processService.RunIlrSubmission(submission, EnvironmentVariables, ilrStatusWatcher);

            var periodEarnings = EarningsDataHelper.GetPeriodisedValuesForUkprnSummary(provider.Ukprn, EnvironmentVariables).LastOrDefault() ?? new PeriodisedValuesEntity();
            PopulateEarnedByPeriodValues(academicYear, provider.EarnedByPeriod, periodEarnings);

            //populate by Uln values
            var periodEarningsByUln = EarningsDataHelper.GetPeriodisedValuesForUkprn(provider.Ukprn, EnvironmentVariables);
            PopulateEarnedByPeriodByUln(academicYear, provider.EarnedByPeriodByUln, periodEarningsByUln);


            var dataLockMatches = DataLockDataHelper.GetDataLockMatchesForUkprn(provider.Ukprn, EnvironmentVariables) ?? new DataLockMatch[0];
            provider.DataLockMatchesByPeriod.Add(date.GetPeriod(), dataLockMatches);
        }

        private static void PopulateEarnedByPeriodByUln(string academicYear, Dictionary<long, Dictionary<string, decimal>> earnedByPeriodByUln, PeriodisedValuesEntity[] periodEarnings)
        {

            foreach (var data in periodEarnings)
            {
                var earnedByPeriod = new Dictionary<string, decimal>();
                if (!earnedByPeriodByUln.ContainsKey(data.Uln))
                {
                    earnedByPeriodByUln.Add(data.Uln, earnedByPeriod);
                }
                else
                {
                    earnedByPeriod = earnedByPeriodByUln[data.Uln];
                }
                PopulateEarnedByPeriodValues(academicYear, earnedByPeriod, data);
            }

        }

        private static void PopulateEarnedByPeriodValues(string academicYear, Dictionary<string, decimal> earnedByPeriod, PeriodisedValuesEntity periodEarnings)
        {
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
                var provider = table.ContainsColumn("Provider")
                    ? table.Rows[rowIndex]["Provider"]
                    : "provider";

                if(table.Header.Contains("LearnDelFAM") && !string.IsNullOrEmpty(table.Rows[rowIndex]["LearnDelFAM"]))
                {
                    var famValue = table.Rows[rowIndex]["LearnDelFAM"];
                    var famCode = new LearningDeliveryFam
                    {
                        FamType = famValue.Substring(0, 3),
                        FamCode =int.Parse(famValue.Substring(3))
                    };

                    StepDefinitionsContext.ReferenceDataContext.AddLearningDeliveryFam(famCode);
                }

                var learningDelivery = new LearningDelivery
                {
                    LearningDeliveryFams = StepDefinitionsContext.ReferenceDataContext.LearningDeliveryFams,
                    LearnerType = table.Header.Contains("learner type")
                                    ? GetLearnerType(table.Rows[rowIndex]["learner type"])
                                    : LearnerType.ProgrammeOnlyDas,
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
                           CompletionStatus.Continuing,

                    FrameworkCode = table.Header.Contains("framework code") ? int.Parse(table.Rows[rowIndex]["framework code"]) : IlrBuilder.Defaults.FrameworkCode,
                    ProgrammeType = table.Header.Contains("programme type") ? int.Parse(table.Rows[rowIndex]["programme type"]) : IlrBuilder.Defaults.ProgrammeType,
                    PathwayCode = table.Header.Contains("pathway code") ? int.Parse(table.Rows[rowIndex]["pathway code"]) : IlrBuilder.Defaults.PathwayCode,
                };

                var standardCode = table.Header.Contains("standard code") ? int.Parse(table.Rows[rowIndex]["standard code"]) : IlrBuilder.Defaults.StandardCode;
                learningDelivery.StandardCode = learningDelivery.FrameworkCode > 0 &&
                                                        learningDelivery.PathwayCode > 0 &&
                                                        learningDelivery.ProgrammeType > 0 ? 0 : standardCode;

                var priceEpisodes = SetupPriceEpisodes(table, rowIndex, learningDelivery.StandardCode);
                learningDelivery.PriceEpisodes = priceEpisodes.ToArray();

                Learner learner = null;
                if (table.Rows[rowIndex].ContainsKey("ULN"))
                {
                    var learners = StepDefinitionsContext.GetProvider(provider).Learners;

                    if (learners != null)
                        learner = learners.SingleOrDefault(x => x.Name == table.Rows[rowIndex]["ULN"]);
                }

                if (learner == null)
                {
                    learner = new Learner();
                    learner.Name = table.Rows[rowIndex].ContainsKey("ULN") ? table.Rows[rowIndex]["ULN"] : string.Empty;

                    if (table.Rows[rowIndex].ContainsKey("ULN"))
                    {
                        learner.Uln = GetUln(learner.Name, table.Rows[rowIndex]["ULN"]);
                    }
                    else
                    {
                        learner.Uln = GetUln(learner.Name, string.Empty);
                    }

                    learner.LearnRefNumber =$"{StepDefinitionsContext.GetProvider(provider).Ukprn}-{rowIndex+1}";
                    learner.DateOfBirth = GetDateOfBirth(learningDelivery.LearnerType, learningDelivery.StartDate);
                    learner.EmploymentStatus = table.Rows[rowIndex].ContainsKey("Employment Status") && 
                                                table.Rows[rowIndex]["Employment Status"] == "Employed" ? 10 : (int?)null ;
                    learner.EmploymentStatusDate = table.Rows[rowIndex].ContainsKey("Employment Status Applies") ?
                                               DateTime.Parse(table.Rows[rowIndex]["Employment Status Applies"]) : (DateTime?)null;
                    learner.EmployerId = table.Rows[rowIndex].ContainsKey("Employer Id") ?
                                              table.Rows[rowIndex]["Employer Id"] : string.Empty;
                    if (table.Rows[rowIndex].ContainsKey("Small Employer") && !string.IsNullOrEmpty(table.Rows[rowIndex]["Small Employer"]))
                    {
                        var employerFlag = table.Rows[rowIndex]["Small Employer"];
                        learner.EmploymentStatusMonitoring = new Entities.EmploymentStatusMonitoring
                        {
                            Type = GetEmploymentStatusMonitringType(employerFlag.Substring(0, 3)),
                            Code = int.Parse(employerFlag.Substring(3))
                        };
                    }

                    StepDefinitionsContext.AddProviderLearner(provider, learner);
                }

                learner.LearningDeliveries.Add(learningDelivery);
            }
        }

        private DateTime GetDateOfBirth(LearnerType learnerType, DateTime startDate)
        {
            return learnerType == LearnerType.ProgrammeOnlyDas16To18 || learnerType == LearnerType.ProgrammeOnlyNonDas16To18
                       ? startDate.AddYears(-17)
                       : new DateTime(1985, 10, 10);

        }

        private EmploymentStatusMonitoringType GetEmploymentStatusMonitringType(string value)
        {
            EmploymentStatusMonitoringType type ;

            if (Enum.TryParse(value, true, out type))
            {
                return type;
            }
            throw new Exception($"Failed to parse the employment status monitoring type {value}");
        }

        private long GetUln(string learnerName, string inputUln)
        {
            long uln = 0;
            foreach (var provider in StepDefinitionsContext.Providers)
            {
                if (provider.Learners != null)
                {
                    var learner = provider.Learners.SingleOrDefault(x => x.Name.Equals(learnerName, StringComparison.CurrentCultureIgnoreCase));
                    if (learner != null)
                    {
                        uln = learner.Uln;
                        break;
                    }
                }
            }
            if (uln == 0)
            {
                long.TryParse(inputUln, out uln);
            }

            uln = uln > 0 ? uln : long.Parse(IdentifierGenerator.GenerateIdentifier(10, false));
            return uln;
        }

        private List<PriceEpisode> SetupPriceEpisodes(Table table, int rowIndex, long? standardCode)
        {
            var priceEpisodes = new List<PriceEpisode>();
            var startDate = DateTime.MinValue;

            if (table.Header.Contains("Total training price") || table.Header.Contains("Residual training price"))
            {
                if (!string.IsNullOrEmpty(table.Rows[rowIndex]["Total training price effective date"]))
                {
                    startDate = DateTime.Parse(table.Rows[rowIndex]["Total training price effective date"]);

                    priceEpisodes.Add(new PriceEpisode
                    {
                        Id = GetPriceEpisodeIdentifier(startDate, standardCode),
                        StartDate = startDate,
                        EndDate = table.Header.Contains("Residual training price effective date") && !string.IsNullOrEmpty(table.Rows[rowIndex]["Residual training price effective date"])
                          ? DateTime.Parse(table.Rows[rowIndex]["Residual training price effective date"]).AddDays(-1)
                          : string.IsNullOrEmpty(table.Rows[rowIndex]["actual end date"])
                              ? (DateTime?)null
                              : DateTime.Parse(table.Rows[rowIndex]["actual end date"]),
                        TotalPrice = table.Rows[rowIndex]["Total training price"].GetDecimalValue() + table.Rows[rowIndex]["Total assessment price"].GetDecimalValue(),
                        Tnp1 = table.Rows[rowIndex]["Total training price"].GetDecimalValue(),
                        Tnp2 = table.Rows[rowIndex]["Total assessment price"].GetDecimalValue()
                    });
                }
                if (table.Header.Contains("Residual training price effective date") && !string.IsNullOrEmpty(table.Rows[rowIndex]["Residual training price effective date"]))
                {
                    startDate = DateTime.Parse(table.Rows[rowIndex]["Residual training price effective date"]);

                    priceEpisodes.Add(new PriceEpisode
                    {
                        Id = GetPriceEpisodeIdentifier(startDate, standardCode),
                        StartDate = startDate,
                        TotalPrice = decimal.Parse(table.Rows[rowIndex]["Residual training price"]) + decimal.Parse(table.Rows[rowIndex]["Residual assessment price"]),
                        Tnp3 = decimal.Parse(table.Rows[rowIndex]["Residual training price"]),
                        Tnp4 = decimal.Parse(table.Rows[rowIndex]["Residual assessment price"])
                    });
                }

                return priceEpisodes;
            }

            if (table.Header.Contains("Total training price 1"))
            {
                var index = 1;

                while (table.Header.Contains($"Total training price {index}"))
                {
                    startDate = DateTime.Parse(table.Rows[rowIndex][$"Total training price {index} effective date"]);

                    priceEpisodes.Add(new PriceEpisode
                    {
                        Id = GetPriceEpisodeIdentifier(startDate, standardCode),
                        StartDate = startDate,
                        EndDate = table.Header.Contains($"Total training price {index + 1} effective date") && !string.IsNullOrEmpty(table.Rows[rowIndex][$"Total training price {index + 1} effective date"])
                            ? DateTime.Parse(table.Rows[rowIndex][$"Total training price {index + 1} effective date"]).AddDays(-1)
                            : string.IsNullOrEmpty(table.Rows[rowIndex]["actual end date"])
                                ? (DateTime?)null
                                : DateTime.Parse(table.Rows[rowIndex]["actual end date"]),
                        TotalPrice = table.Rows[rowIndex][$"Total training price {index}"].GetDecimalValue() + table.Rows[rowIndex][$"Total assessment price {index}"].GetDecimalValue(),
                        Tnp1 = table.Rows[rowIndex][$"Total training price {index}"].GetDecimalValue(),
                        Tnp2 = table.Rows[rowIndex][$"Total assessment price {index}"].GetDecimalValue()
                    });

                    index++;
                }

                return priceEpisodes;
            }
            if (table.Header.Contains("start date") && table.Header.Contains("agreed price"))
            {
                startDate = DateTime.Parse(table.Rows[rowIndex]["start date"]);

                priceEpisodes.Add(new PriceEpisode
                {
                    Id = GetPriceEpisodeIdentifier(startDate, standardCode),
                    StartDate = startDate,
                    TotalPrice = table.Rows[rowIndex]["agreed price"].GetDecimalValue(),
                    Tnp1 = table.Rows[rowIndex]["agreed price"].GetDecimalValue() * 0.8m,
                    Tnp2 = table.Rows[rowIndex]["agreed price"].GetDecimalValue() - table.Rows[rowIndex]["agreed price"].GetDecimalValue() * 0.8m
                });
            }
            return priceEpisodes;
        }

        protected void UpdateCommitmentsPaymentStatuses(DateTime censusDate)
        {
            foreach (var commitment in StepDefinitionsContext.ReferenceDataContext.Commitments)
            {
                if (commitment.StopPeriodCensusDate.HasValue && commitment.StopPeriodCensusDate <= censusDate)
                {
                    CommitmentDataHelper.UpdateCommitmentEffectiveTo(commitment.Id, commitment.VersionId, commitment.StopPeriodCensusDate.Value.AddDays(-1), EnvironmentVariables);
                    CommitmentDataHelper.CreateNewCommmitmentVersion(commitment.Id, commitment.VersionId, commitment.Status, commitment.StopPeriodCensusDate.Value, EnvironmentVariables);
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

        private string GetPriceEpisodeIdentifier(DateTime date, long? standardCode)
        {
            return $"25-{(standardCode.HasValue ? standardCode.Value : IlrBuilder.Defaults.StandardCode)}-{date.ToString("dd/MM/yyyy")}";
        }

        private LearnerType GetLearnerType(string learnerType)
        {
            LearnerType result = LearnerType.ProgrammeOnlyDas;
            switch (learnerType.Replace(" ", string.Empty).ToLowerInvariant())
            {
                case "programmeonlynon-das":
                    result = LearnerType.ProgrammeOnlyNonDas;
                    break;
                case "programmeonlydas":
                    result = LearnerType.ProgrammeOnlyDas;
                    break;
                case "16-18programmeonlydas":
                    result = LearnerType.ProgrammeOnlyDas16To18;
                    break;
                case "16-18programmeonlynon-das":
                    result = LearnerType.ProgrammeOnlyNonDas16To18;
                    break;
                case "19-24programmeonlynon-das":
                    result = LearnerType.ProgrammeOnlyNonDas19To24;
                    break;
                case "19-24programmeonlydas":
                    result = LearnerType.ProgrammeOnlyDas19To24;
                    break;

            }
            return result;
        }
    }
}
