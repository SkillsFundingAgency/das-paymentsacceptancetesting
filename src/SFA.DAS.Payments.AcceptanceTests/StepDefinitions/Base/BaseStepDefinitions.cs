﻿using System;
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
using ApprenticeshipPriceEpisode = SFA.DAS.Payments.AcceptanceTests.Entities.ApprenticeshipPriceEpisode;

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

        protected void AddLearnerCommitment(long ukprn, 
                                            Learner learner, 
                                            long? standardCode = null,
                                            int? frameworkCode = null,
                                            int? programmeType = null,
                                            int? pathwayCode = null)
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
                    StartDate = learner.LearningDelivery.EpisodeStartDate,
                    EndDate = learner.LearningDelivery.PriceEpisodePlannedEndDate,
                    AgreedCost = learner.LearningDelivery.PriceEpisodeTotalTNPPrice,
                    StandardCode = standardCode ?? IlrBuilder.Defaults.StandardCode,
                    FrameworkCode = frameworkCode ?? IlrBuilder.Defaults.FrameworkCode,
                    ProgrammeType = programmeType ?? IlrBuilder.Defaults.ProgrammeType,
                    PathwayCode = pathwayCode ?? IlrBuilder.Defaults.PathwayCode,
                    Priority = commitmentPriority,
                    VersionId = "1",
                    PaymentStatus = (int)CommitmentPaymentStatus.Active,
                    PaymentStatusDescription = CommitmentPaymentStatus.Active.ToString(),
                    Payable = true
                },
                EnvironmentVariables);
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

            learner.LearningDelivery.TNP1 = learner.LearningDelivery.StandardCode > 0 ?
                                                                           learner.LearningDelivery.PriceEpisodeTotalTNPPrice * 0.8m :
                                                                           learner.LearningDelivery.PriceEpisodeTotalTNPPrice;

            if (learner.LearningDelivery.StandardCode > 0)
            {
                learner.LearningDelivery.TNP2 = learner.LearningDelivery.PriceEpisodeTotalTNPPrice * 0.2m;
                                                                           
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
            LearnerDataHelper.SaveLearningDeliveryFAM(ukprn,learner.LearnRefNumber, EnvironmentVariables);


            LearnerDataHelper.SaveTrailblazerApprenticeshipFinancialRecord(ukprn,1,learner.LearnRefNumber,learner.LearningDelivery.TNP1.Value,EnvironmentVariables);

            //save Trailblazer
            if (learner.LearningDelivery.TNP2.HasValue )
            {
                LearnerDataHelper.SaveTrailblazerApprenticeshipFinancialRecord(ukprn,2, learner.LearnRefNumber,learner.LearningDelivery.TNP2.Value,EnvironmentVariables);
            }

            var months =  ((learner.LearningDelivery.PriceEpisodePlannedEndDate.Year - learner.LearningDelivery.EpisodeStartDate.Year) * 12) + 
                        learner.LearningDelivery.PriceEpisodePlannedEndDate.Month - learner.LearningDelivery.EpisodeStartDate.Month;

            learner.LearningDelivery.PriceEpisodeInstalmentValue = (learner.LearningDelivery.PriceEpisodeTotalTNPPrice * 0.8m) / months;
            learner.LearningDelivery.PriceEpisodeCompletionElement = learner.LearningDelivery.PriceEpisodeTotalTNPPrice - ((learner.LearningDelivery.PriceEpisodeTotalTNPPrice * 0.8m) / months);

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
          , ProcessService processService, Dictionary<string, decimal> earnedByPeriod)
        {
            var submissionLearners = learners.Select(l => new Learner
            {
                Name = l.Name,
                Uln = l.Uln,
                LearnRefNumber=l.LearnRefNumber,
                LearningDelivery = new ApprenticeshipPriceEpisode
                {
                    PriceEpisodeTotalTNPPrice = l.LearningDelivery.PriceEpisodeTotalTNPPrice,
                    LearnerType = l.LearningDelivery.LearnerType,
                    EpisodeStartDate = l.LearningDelivery.EpisodeStartDate,
                    PriceEpisodePlannedEndDate = l.LearningDelivery.PriceEpisodePlannedEndDate,
                    PriceEpisodeActualEndDate = date >= l.LearningDelivery.PriceEpisodeActualEndDate ? l.LearningDelivery.PriceEpisodeActualEndDate : null,
                    CompletionStatus = l.LearningDelivery.CompletionStatus,
                    StandardCode = l.LearningDelivery.StandardCode,
                    FrameworkCode= l.LearningDelivery.FrameworkCode,
                    PathwayCode=l.LearningDelivery.PathwayCode,
                    ProgrammeType=l.LearningDelivery.ProgrammeType
                    
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
                var learner = new Learner
                {
                    Name = table.Rows[rowIndex].ContainsKey("ULN") ? table.Rows[rowIndex]["ULN"] : string.Empty,
                   
                    LearningDelivery = new ApprenticeshipPriceEpisode
                    {
                        PriceEpisodeTotalTNPPrice = decimal.Parse(table.Rows[rowIndex]["agreed price"]),
                        LearnerType = LearnerType.ProgrammeOnlyDas,
                        EpisodeStartDate = DateTime.Parse(table.Rows[rowIndex]["start date"]),
                        PriceEpisodePlannedEndDate = table.Header.Contains("planned end date") ? 
                                        DateTime.Parse(table.Rows[rowIndex]["planned end date"]) : 
                                        DateTime.Parse(table.Rows[rowIndex]["start date"]).AddMonths(12),
                        PriceEpisodeActualEndDate =
                            !table.Header.Contains("actual end date") ||
                            string.IsNullOrWhiteSpace(table.Rows[rowIndex]["actual end date"])
                                ? null
                                : (DateTime?)DateTime.Parse(table.Rows[rowIndex]["actual end date"]),
                        CompletionStatus = table.Header.Contains("completion status") ?
                            IlrTranslator.TranslateCompletionStatus(table.Rows[rowIndex]["completion status"]) :
                            CompletionStatus.InProgress,

                        FrameworkCode = table.Header.Contains("framework code") ? int.Parse(table.Rows[rowIndex]["framework code"]) : IlrBuilder.Defaults.FrameworkCode,
                        ProgrammeType = table.Header.Contains("programme type") ? int.Parse(table.Rows[rowIndex]["programme type"]) : IlrBuilder.Defaults.ProgrammeType,
                        PathwayCode = table.Header.Contains("pathway code") ? int.Parse(table.Rows[rowIndex]["pathway code"]) : IlrBuilder.Defaults.PathwayCode

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
    }
}
