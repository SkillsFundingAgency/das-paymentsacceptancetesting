using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                periods.Add($"{date.Month:00}/{date.Year}");
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
                        LearnerDetails = g.ToArray()
                    }).ToArray();
        }
        private static void SetEnvironmentToPeriod(string period)
        {
            var month = int.Parse(period.Substring(0, 2));
            var year = int.Parse(period.Substring(3, 4));
            var date = new DateTime(year, month, 1);

            TestEnvironment.Variables.CollectionPeriod = new ProviderPayments.TestStack.Core.Domain.CollectionPeriod
            {
                CalendarMonth = 1,
                CalendarYear = 2017,
                Period = period,
                PeriodId = date.GetPeriodNumber(),
                CollectionOpen = 1
            };
        }
        private static void BuildAndSubmitIlr(ProviderSubmissionDetails providerDetails, string period)
        {
            byte[] data = null;
            TestEnvironment.ProcessService.RunIlrSubmission(data, TestEnvironment.Variables, new LoggingStatusWatcher($"ILR submission for provider {providerDetails.ProviderId} in {period}"));
        }
        private static void RunMonthEnd(string period)
        {
            TestEnvironment.ProcessService.RunSummarisation(TestEnvironment.Variables, new LoggingStatusWatcher($"Month end for {period}"));
        }


        private class ProviderSubmissionDetails
        {
            public string ProviderId { get; set; }
            public IlrLearnerReferenceData[] LearnerDetails { get; set; }
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
