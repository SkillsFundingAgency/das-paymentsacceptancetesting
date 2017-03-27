using System;
using System.Collections.Generic;
using System.Linq;
using IlrGenerator;
using ProviderPayments.TestStack.Core.ExecutionStatus;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.DataCollectors;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ExecutionManagers
{
    internal static class SubmissionManager
    {
        private const string FamCodeAct = "ACT";
        private const short FamCodeActDasValue = 1;
        private const short FamCodeActNonDasValue = 2;

        internal static List<LearnerResults> SubmitIlrAndRunMonthEndAndCollateResults(List<IlrLearnerReferenceData> ilrLearnerDetails,
                                                                                   LookupContext lookupContext,
                                                                                   List<EmployerAccountReferenceData> employers,
                                                                                   List<ContractTypeReferenceData> contractTypes,
                                                                                   List<EmploymentStatusReferenceData> employmentStatus,
                                                                                   List<LearningSupportReferenceData> learningSupportStatus)
        {

            return SubmitIlrAndRunMonthEndAndCollateResults(ilrLearnerDetails, lookupContext, employers, contractTypes, employmentStatus, learningSupportStatus, null);
        }
        internal static List<LearnerResults> SubmitIlrAndRunMonthEndAndCollateResults(List<IlrLearnerReferenceData> ilrLearnerDetails,
                                                                                      LookupContext lookupContext,
                                                                                      List<EmployerAccountReferenceData> employers,
                                                                                      List<ContractTypeReferenceData> contractTypes,
                                                                                      List<EmploymentStatusReferenceData> employmentStatus,
                                                                                      List<LearningSupportReferenceData> learningSupportStatus,
                                                                                      string[] periods)
        {
            var results = new List<LearnerResults>();
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return results;
            }


            periods = periods ?? ExtractPeriods(ilrLearnerDetails);
            var providerLearners = GroupLearnersByProvider(ilrLearnerDetails, lookupContext);
            foreach (var period in periods)
            {
                SetEnvironmentToPeriod(period);
                EmployerAccountManager.UpdateAccountBalancesForPeriod(employers, period);

                foreach (var providerDetails in providerLearners)
                {
                    SetupDisadvantagedPostcodeUplift(providerDetails);
                    BuildAndSubmitIlr(providerDetails, period, lookupContext, contractTypes, employmentStatus, learningSupportStatus);
                }
                RunMonthEnd(period);

                EarningsCollector.CollectForPeriod(period, results, lookupContext);
                DataLockResultCollector.CollectForPeriod(period, results, lookupContext);
            }
            PaymentsDataCollector.CollectForPeriod(results, lookupContext);

            return results;
        }

        private static string[] ExtractPeriods(List<IlrLearnerReferenceData> ilrLearnerDetails)
        {
            var periods = new List<string>();

            var earliestDate = ilrLearnerDetails.Select(x => x.StartDate).Min();
            var latestPlannedDate = ilrLearnerDetails.Select(x => x.PlannedEndDate).Max();
            var latestActualDate = ilrLearnerDetails.Select(x => x.ActualEndDate).Max();
            var latestDate = latestActualDate.HasValue && latestActualDate > latestPlannedDate ? latestActualDate : latestPlannedDate;

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
        private static void SetupDisadvantagedPostcodeUplift(ProviderSubmissionDetails providerDetails)
        {
            var homePostcodeDeprivation = providerDetails.LearnerDetails.Select(l => l.HomePostcodeDeprivation)
                                                                        .FirstOrDefault(d => !string.IsNullOrEmpty(d));
            if (!string.IsNullOrEmpty(homePostcodeDeprivation))
            {
                ReferenceDataManager.AddDisadvantagedPostcodeUplift(homePostcodeDeprivation);
            }
        }
        private static void BuildAndSubmitIlr(ProviderSubmissionDetails providerDetails, string period, LookupContext lookupContext, List<ContractTypeReferenceData> contractTypes, List<EmploymentStatusReferenceData> employmentStatus, List<LearningSupportReferenceData> learningSupportStatus)
        {
            IlrSubmission submission = BuildIlrSubmission(providerDetails, lookupContext, contractTypes, employmentStatus, learningSupportStatus);
            TestEnvironment.ProcessService.RunIlrSubmission(submission, TestEnvironment.Variables, new LoggingStatusWatcher($"ILR submission for provider {providerDetails.ProviderId} in {period}"));
        }
        private static void RunMonthEnd(string period)
        {
            TestEnvironment.ProcessService.RunSummarisation(TestEnvironment.Variables, new LoggingStatusWatcher($"Month end for {period}"));
        }


        private static IlrSubmission BuildIlrSubmission(ProviderSubmissionDetails providerDetails, LookupContext lookupContext, List<ContractTypeReferenceData> contractTypes, List<EmploymentStatusReferenceData> employmentStatus, List<LearningSupportReferenceData> learningSupportStatus)
        {
            var learners = (from x in providerDetails.LearnerDetails
                            group x by x.LearnerId into g
                            select BuildLearner(g.ToArray(), lookupContext, contractTypes, employmentStatus, learningSupportStatus)).ToArray();
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
        private static Learner BuildLearner(IlrLearnerReferenceData[] learnerDetails, LookupContext lookupContext, List<ContractTypeReferenceData> contractTypes, List<EmploymentStatusReferenceData> employmentStatus, List<LearningSupportReferenceData> learningSupportStatus)
        {
            var deliveries = learnerDetails.Select(x =>
            {
                var financialRecords = BuildLearningDeliveryFinancials(x);

                return new LearningDelivery
                {
                    StandardCode = x.StandardCode,
                    FrameworkCode = x.FrameworkCode,
                    ProgrammeType = x.ProgrammeType,
                    PathwayCode = x.PathwayCode,
                    ActualStartDate = x.StartDate,
                    PlannedEndDate = x.PlannedEndDate,
                    ActualEndDate = x.ActualEndDate,
                    //ActFamCodeValue = IsLearnerTypeLevy(x.LearnerType) ? (short)1 : (short)2,
                    FamRecords = BuildLearningDeliveryFamCodes(x, contractTypes, learningSupportStatus),
                    CompletionStatus = (IlrGenerator.CompletionStatus)(int)x.CompletionStatus,
                    Type = (IlrGenerator.AimType)(int)x.AimType,
                    FinancialRecords = financialRecords
                };
            }).ToArray();
            var employmentStatuses = employmentStatus.Select(s => new IlrGenerator.EmploymentStatus
            {
                EmployerId = s.EmployerId,
                DateFrom = s.EmploymentStatusApplies,
                EmploymentStatusMonitoring = new EmploymentStatusMonitoring
                {
                    Type = s.MonitoringType.ToString(),
                    Code = s.MonitoringCode
                },
                StatusCode = (int)s.EmploymentStatus
            }).ToArray();

            return new Learner
            {
                Uln = lookupContext.AddOrGetUln(learnerDetails[0].LearnerId),
                DateOfBirth = GetDateOfBirthBasedOnLearnerType(learnerDetails[0].LearnerType),
                LearningDeliveries = deliveries,
                EmploymentStatuses = employmentStatuses
            };
        }
        private static FinancialRecord[] BuildLearningDeliveryFinancials(IlrLearnerReferenceData learnerReferenceData)
        {
            var agreedTrainingPrice = (int)Math.Floor(learnerReferenceData.AgreedPrice * 0.8m);
            var agreedAssesmentPrice = learnerReferenceData.AgreedPrice - agreedTrainingPrice;

            var financialRecords = new List<FinancialRecord>();
            financialRecords.Add(new FinancialRecord
            {
                Code = 1,
                Type = "TNP",
                Amount = learnerReferenceData.TotalTrainingPrice1 == 0 ? agreedTrainingPrice : learnerReferenceData.TotalTrainingPrice1,
                Date = learnerReferenceData.TotalTrainingPrice1EffectiveDate == DateTime.MinValue ? learnerReferenceData.StartDate : learnerReferenceData.TotalTrainingPrice1EffectiveDate
            });
            financialRecords.Add(new FinancialRecord
            {
                Code = 2,
                Type = "TNP",
                Amount = learnerReferenceData.TotalAssessmentPrice1 == 0 ? agreedAssesmentPrice : learnerReferenceData.TotalAssessmentPrice1,
                Date = learnerReferenceData.TotalAssessmentPrice1EffectiveDate == DateTime.MinValue ? learnerReferenceData.StartDate : learnerReferenceData.TotalAssessmentPrice1EffectiveDate
            });
            if (learnerReferenceData.TotalTrainingPrice2 > 0 || learnerReferenceData.TotalAssessmentPrice2 > 0)
            {
                financialRecords.Add(new FinancialRecord
                {
                    Code = 1,
                    Type = "TNP",
                    Amount = learnerReferenceData.TotalTrainingPrice2,
                    Date = learnerReferenceData.TotalTrainingPrice2EffectiveDate == DateTime.MinValue ? learnerReferenceData.StartDate : learnerReferenceData.TotalTrainingPrice2EffectiveDate
                });
                financialRecords.Add(new FinancialRecord
                {
                    Code = 2,
                    Type = "TNP",
                    Amount = learnerReferenceData.TotalAssessmentPrice2,
                    Date = learnerReferenceData.TotalAssessmentPrice2EffectiveDate == DateTime.MinValue ? learnerReferenceData.StartDate : learnerReferenceData.TotalAssessmentPrice2EffectiveDate
                });
            }
            if (learnerReferenceData.ResidualTrainingPrice > 0 || learnerReferenceData.ResidualAssessmentPrice > 0)
            {
                financialRecords.Add(new FinancialRecord
                {
                    Code = 3,
                    Type = "TNP",
                    Amount = learnerReferenceData.ResidualTrainingPrice,
                    Date = learnerReferenceData.ResidualTrainingPriceEffectiveDate == DateTime.MinValue ? learnerReferenceData.StartDate : learnerReferenceData.ResidualTrainingPriceEffectiveDate
                });
                financialRecords.Add(new FinancialRecord
                {
                    Code = 4,
                    Type = "TNP",
                    Amount = learnerReferenceData.ResidualAssessmentPrice,
                    Date = learnerReferenceData.ResidualAssessmentPriceEffectiveDate == DateTime.MinValue ? learnerReferenceData.StartDate : learnerReferenceData.ResidualAssessmentPriceEffectiveDate
                });
            }

            return financialRecords.ToArray();
        }
        private static LearningDeliveryFamRecord[] BuildLearningDeliveryFamCodes(IlrLearnerReferenceData learnerDetails,
            List<ContractTypeReferenceData> contractTypes, List<LearningSupportReferenceData> learningSupportStatus)
        {
            var learningEndDate = (!learnerDetails.ActualEndDate.HasValue || learnerDetails.PlannedEndDate > learnerDetails.ActualEndDate.Value)
                ? learnerDetails.PlannedEndDate : learnerDetails.ActualEndDate.Value;

            var actFamCodes = BuildActFamCodes(learnerDetails.LearnerType, learnerDetails.StartDate, learningEndDate, contractTypes);
            var lsfFamCodes = BuildLsfFamCodes(learningSupportStatus);
            var eefFamCodes = BuildEefFamCodes(learnerDetails);

            return actFamCodes.Concat(lsfFamCodes).Concat(eefFamCodes).ToArray();
        }
        private static LearningDeliveryFamRecord[] BuildActFamCodes(LearnerType learnerType, DateTime learningStart, DateTime learningEnd, List<ContractTypeReferenceData> contractTypes)
        {
            if (contractTypes.Any())
            {
                return contractTypes.Select(x => new LearningDeliveryFamRecord
                {
                    FamType = FamCodeAct,
                    Code = x.ContractType == ContractType.ContractWithEmployer ? FamCodeActDasValue : FamCodeActNonDasValue,
                    From = x.DateFrom,
                    To = x.DateTo
                }).ToArray();
            }
            return new[]
            {
                new LearningDeliveryFamRecord
                {
                    FamType = FamCodeAct,
                    Code = IsLearnerTypeLevy(learnerType) ? FamCodeActDasValue : FamCodeActNonDasValue,
                    From = learningStart,
                    To = learningEnd
                }
            };
        }
        private static LearningDeliveryFamRecord[] BuildLsfFamCodes(List<LearningSupportReferenceData> learningSupportStatus)
        {
            return learningSupportStatus.Select(s => new LearningDeliveryFamRecord
            {
                FamType = "LSF",
                Code = s.LearningSupportCode,
                From = s.DateFrom,
                To = s.DateTo
            }).ToArray();
        }
        private static LearningDeliveryFamRecord[] BuildEefFamCodes(IlrLearnerReferenceData learnerDetails)
        {
            if (learnerDetails.LearnDelFam == null || !learnerDetails.LearnDelFam.ToUpper().StartsWith("EEF"))
            {
                return new LearningDeliveryFamRecord[0];
            }

            return new[]
            {
                new LearningDeliveryFamRecord
                {
                    FamType = "EEF",
                    Code = int.Parse(learnerDetails.LearnDelFam.Substring(3)),
                    From = learnerDetails.StartDate
                }
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
