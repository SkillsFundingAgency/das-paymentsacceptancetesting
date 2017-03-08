using System;
using System.Collections.Generic;
using System.Linq;
using IlrGenerator;
using ProviderPayments.TestStack.Core.ExecutionStatus;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ExecutionManagers
{
    internal static class SubmissionManager
    {
        internal static List<LearnerResults> SubmitIlrAndRunMonthEndAndCollateResults(List<IlrLearnerReferenceData> ilrLearnerDetails)
        {
            // get periods
            // collate details by provider
            // submit ILR for each provider for each period
            // run month end for period as a whole
            // Collect earns results
            // Collect payments results

            var periods = ExtractPeriods(ilrLearnerDetails);
            var providerLearners = GroupLearnersByProvider(ilrLearnerDetails);
            foreach (var period in periods)
            {
                SetEnvironmentToPeriod(period);
                foreach (var providerDetails in providerLearners)
                {
                    BuildAndSubmitIlr(providerDetails, period);
                }
                RunMonthEnd(period);
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
        private static ProviderSubmissionDetails[] GroupLearnersByProvider(List<IlrLearnerReferenceData> ilrLearnerDetails)
        {
            return (from x in ilrLearnerDetails
                    group x by x.Provider into g
                    select new ProviderSubmissionDetails
                    {
                        ProviderId = g.Key,
                        Ukprn = 0l, //TODO
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
        private static void BuildAndSubmitIlr(ProviderSubmissionDetails providerDetails, string period)
        {
            IlrSubmission submission = BuildIlrSubmission(providerDetails);
            TestEnvironment.ProcessService.RunIlrSubmission(submission, TestEnvironment.Variables, new LoggingStatusWatcher($"ILR submission for provider {providerDetails.ProviderId} in {period}"));
        }
        private static void RunMonthEnd(string period)
        {
            TestEnvironment.ProcessService.RunSummarisation(TestEnvironment.Variables, new LoggingStatusWatcher($"Month end for {period}"));
        }


        private static IlrSubmission BuildIlrSubmission(ProviderSubmissionDetails providerDetails)
        {
            var learners = (from x in providerDetails.LearnerDetails
                            group x by x.Uln into g
                            select BuildLearner(g.ToArray())).ToArray();
            return new IlrSubmission
            {
                Ukprn = providerDetails.Ukprn,
                Learners = learners
            };
        }
        private static Learner BuildLearner(IlrLearnerReferenceData[] learnerDetails)
        {
            var deliveries = learnerDetails.Select(x =>
            {
                var financialRecords = new List<FinancialRecord>();
                financialRecords.Add(new FinancialRecord
                {
                    Code = 1,
                    Type = "TNP",
                    Amount = x.TotalTrainingPrice1,
                    Date = x.TotalTrainingPrice1EffectiveDate
                });
                financialRecords.Add(new FinancialRecord
                {
                    Code = 2,
                    Type = "TNP",
                    Amount = x.TotalAssessmentPrice1,
                    Date = x.TotalAssessmentPrice1EffectiveDate
                });
                if(x.TotalTrainingPrice2 > 0 || x.TotalAssessmentPrice2 > 0)
                {
                    financialRecords.Add(new FinancialRecord
                    {
                        Code = 1,
                        Type = "TNP",
                        Amount = x.TotalTrainingPrice2,
                        Date = x.TotalTrainingPrice2EffectiveDate
                    });
                    financialRecords.Add(new FinancialRecord
                    {
                        Code = 2,
                        Type = "TNP",
                        Amount = x.TotalAssessmentPrice2,
                        Date = x.TotalAssessmentPrice2EffectiveDate
                    });
                }
                if (x.ResidualTrainingPrice > 0 || x.ResidualAssessmentPrice > 0)
                {
                    financialRecords.Add(new FinancialRecord
                    {
                        Code = 3,
                        Type = "TNP",
                        Amount = x.ResidualTrainingPrice,
                        Date = x.ResidualTrainingPriceEffectiveDate
                    });
                    financialRecords.Add(new FinancialRecord
                    {
                        Code = 4,
                        Type = "TNP",
                        Amount = x.ResidualAssessmentPrice,
                        Date = x.ResidualAssessmentPriceEffectiveDate
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
                Uln = learnerDetails[0].Uln,
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
