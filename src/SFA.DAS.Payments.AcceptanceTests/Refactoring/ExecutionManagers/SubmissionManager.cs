using System;
using System.Collections.Generic;
using System.Linq;
using IlrGenerator;
using ProviderPayments.TestStack.Core.ExecutionStatus;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ExecutionManagers
{
    internal static class SubmissionManager
    {
        internal static List<LearnerResults> SubmitIlrAndRunMonthEndAndCollateResults(List<IlrLearnerReferenceData> ilrLearnerDetails, LookupContext lookupContext)
        {
            var periods = ExtractPeriods(ilrLearnerDetails);
            var providerLearners = GroupLearnersByProvider(ilrLearnerDetails, lookupContext);
            foreach (var period in periods)
            {
                SetEnvironmentToPeriod(period);
                foreach (var providerDetails in providerLearners)
                {
                    BuildAndSubmitIlr(providerDetails, period, lookupContext);
                }
                RunMonthEnd(period);

                //TODO: Collect results
            }

            return new List<LearnerResults>();
        }

        private static string[] ExtractPeriods(List<IlrLearnerReferenceData> ilrLearnerDetails)
        {
            var periods = new List<string>();

            var earliestDate = ilrLearnerDetails.Select(x => x.StartDate).Min();
            var latestPlannedDate = ilrLearnerDetails.Select(x => x.PlannedEndDate).Max();
            var latestActualDate = ilrLearnerDetails.Select(x => x.ActualEndDate).Max();
            var latestDate = latestPlannedDate > latestActualDate ? latestPlannedDate : latestActualDate;

            var date = earliestDate;
            while (date <= latestDate)
            {
                periods.Add($"{date.Month:00}/{date.Year - 2000}");
                date = date.AddMonths(1);
            }

            return periods.ToArray();
        }
        private static ProviderSubmissionDetails[] GroupLearnersByProvider(List<IlrLearnerReferenceData> ilrLearnerDetails, LookupContext lookupContext)
        {
            return (from x in ilrLearnerDetails
                    group x by x.Provider into g
                    select new ProviderSubmissionDetails
                    {
                        ProviderId = g.Key,
                        Ukprn = lookupContext.AddOrGetUkprn(g.Key),
                        LearnerDetails = g.ToArray()
                    }).ToArray();
        }
        private static void SetEnvironmentToPeriod(string period)
        {
            var month = int.Parse(period.Substring(0, 2));
            var year = int.Parse(period.Substring(3, 2)) + 2000;
            var date = new DateTime(year, month, 1);
            var periodNumber = date.GetPeriodNumber();

            TestEnvironment.Variables.CurrentYear = date.GetAcademicYear();
            TestEnvironment.Variables.CollectionPeriod = new ProviderPayments.TestStack.Core.Domain.CollectionPeriod
            {
                PeriodId = date.GetPeriodNumber(),
                Period = "R" + periodNumber.ToString("00"),
                CalendarMonth = date.Month,
                CalendarYear = date.Year,
                ActualsSchemaPeriod = date.Year + date.Month.ToString("00"),
                CollectionOpen = 1
            };
        }
        private static void BuildAndSubmitIlr(ProviderSubmissionDetails providerDetails, string period, LookupContext lookupContext)
        {
            IlrSubmission submission = BuildIlrSubmission(providerDetails, lookupContext);
            TestEnvironment.ProcessService.RunIlrSubmission(submission, TestEnvironment.Variables, new LoggingStatusWatcher($"ILR submission for provider {providerDetails.ProviderId} in {period}"));
        }
        private static void RunMonthEnd(string period)
        {
            TestEnvironment.ProcessService.RunSummarisation(TestEnvironment.Variables, new LoggingStatusWatcher($"Month end for {period}"));
        }


        private static IlrSubmission BuildIlrSubmission(ProviderSubmissionDetails providerDetails, LookupContext lookupContext)
        {
            var learners = (from x in providerDetails.LearnerDetails
                            group x by x.LearnerId into g
                            select BuildLearner(g.ToArray(), lookupContext)).ToArray();
            var submission = new IlrSubmission
            {
                Ukprn = providerDetails.Ukprn,
                Learners = learners
            };
            for (var i = 0; i < submission.Learners.Length; i++)
            {
                submission.Learners[i].LearnRefNumber = (i + 1).ToString();
            }
            return submission;
        }
        private static Learner BuildLearner(IlrLearnerReferenceData[] learnerDetails, LookupContext lookupContext)
        {
            var deliveries = learnerDetails.Select(x =>
            {
                var agreedTrainingPrice = (int)Math.Floor(x.AgreedPrice * 0.8m);
                var agreedAssesmentPrice = x.AgreedPrice - agreedTrainingPrice;

                var financialRecords = new List<FinancialRecord>();
                financialRecords.Add(new FinancialRecord
                {
                    Code = 1,
                    Type = "TNP",
                    Amount = x.TotalTrainingPrice1 == 0 ? agreedTrainingPrice : x.TotalTrainingPrice1,
                    Date = x.TotalTrainingPrice1EffectiveDate == DateTime.MinValue ? x.StartDate : x.TotalTrainingPrice1EffectiveDate
                });
                financialRecords.Add(new FinancialRecord
                {
                    Code = 2,
                    Type = "TNP",
                    Amount = x.TotalAssessmentPrice1 == 0 ? agreedAssesmentPrice : x.TotalAssessmentPrice1,
                    Date = x.TotalAssessmentPrice1EffectiveDate == DateTime.MinValue ? x.StartDate : x.TotalAssessmentPrice1EffectiveDate
                });
                if (x.TotalTrainingPrice2 > 0 || x.TotalAssessmentPrice2 > 0)
                {
                    financialRecords.Add(new FinancialRecord
                    {
                        Code = 1,
                        Type = "TNP",
                        Amount = x.TotalTrainingPrice2,
                        Date = x.TotalTrainingPrice2EffectiveDate == DateTime.MinValue ? x.StartDate : x.TotalTrainingPrice2EffectiveDate
                    });
                    financialRecords.Add(new FinancialRecord
                    {
                        Code = 2,
                        Type = "TNP",
                        Amount = x.TotalAssessmentPrice2,
                        Date = x.TotalAssessmentPrice2EffectiveDate == DateTime.MinValue ? x.StartDate : x.TotalAssessmentPrice2EffectiveDate
                    });
                }
                if (x.ResidualTrainingPrice > 0 || x.ResidualAssessmentPrice > 0)
                {
                    financialRecords.Add(new FinancialRecord
                    {
                        Code = 3,
                        Type = "TNP",
                        Amount = x.ResidualTrainingPrice,
                        Date = x.ResidualTrainingPriceEffectiveDate == DateTime.MinValue ? x.StartDate : x.ResidualTrainingPriceEffectiveDate
                    });
                    financialRecords.Add(new FinancialRecord
                    {
                        Code = 4,
                        Type = "TNP",
                        Amount = x.ResidualAssessmentPrice,
                        Date = x.ResidualAssessmentPriceEffectiveDate == DateTime.MinValue ? x.StartDate : x.ResidualAssessmentPriceEffectiveDate
                    });
                }

                return new LearningDelivery
                {
                    StandardCode = x.StandardCode,
                    FrameworkCode = x.FrameworkCode,
                    ProgrammeType = x.ProgrammeType,
                    PathwayCode = x.PathwayCode,
                    ActualStartDate = x.StartDate,
                    PlannedEndDate = x.PlannedEndDate,
                    ActualEndDate = x.ActualEndDate,
                    ActFamCodeValue = IsLearnerTypeLevy(x.LearnerType) ? (short)1 : (short)2,
                    CompletionStatus = (IlrGenerator.CompletionStatus)(int)x.CompletionStatus,
                    Type = (IlrGenerator.AimType)(int)x.AimType,
                    FinancialRecords = financialRecords.ToArray()
                };
            }).ToArray();

            return new Learner
            {
                Uln = lookupContext.AddOrGetUln(learnerDetails[0].LearnerId),
                DateOfBirth = GetDateOfBirthBasedOnLearnerType(learnerDetails[0].LearnerType),
                LearningDeliveries = deliveries
            };
        }
        private static bool IsLearnerTypeLevy(LearnerType learnerType)
        {
            if (learnerType == LearnerType.ProgrammeOnlyDas
                || learnerType == LearnerType.ProgrammeOnlyDas1618
                || learnerType == LearnerType.ProgrammeOnlyDas1924)
            {
                return true;
            }
            return false;
        }
        private static DateTime GetDateOfBirthBasedOnLearnerType(LearnerType learnerType)
        {
            if (learnerType == LearnerType.ProgrammeOnlyDas1618 || learnerType == LearnerType.ProgrammeOnlyNonDas1618)
            {
                return DateTime.Today.AddYears(-17);
            }
            if (learnerType == LearnerType.ProgrammeOnlyNonDas1924 || learnerType == LearnerType.ProgrammeOnlyNonDas1924)
            {
                return DateTime.Today.AddYears(-20);
            }
            return DateTime.Today.AddYears(-25);
        }


        private class ProviderSubmissionDetails
        {
            public string ProviderId { get; set; }
            public IlrLearnerReferenceData[] LearnerDetails { get; set; }
            public long Ukprn { get; set; }
        }
        private class LoggingStatusWatcher : StatusWatcherBase
        {
            private readonly string _processName;

            public LoggingStatusWatcher(string processName)
            {
                _processName = processName;
            }

            public override void ExecutionStarted(TaskDescriptor[] tasks)
            {
                TestEnvironment.Logger.Info($"Started execution of {_processName}");
            }
            public override void TaskStarted(string taskId)
            {
                TestEnvironment.Logger.Info($"Started task {taskId} of {_processName}");
            }
            public override void TaskCompleted(string taskId, Exception error)
            {
                TestEnvironment.Logger.Info($"Completed task {taskId} of {_processName}");
            }
            public override void ExecutionCompleted(Exception error)
            {
                TestEnvironment.Logger.Info($"Completed execution of {_processName}");
            }
        }
    }
}
