using System;
using System.Data.SqlClient;
using Dapper;
using ProviderPayments.TestStack.Core;
using ProviderPayments.TestStack.Core.ExecutionStatus;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ExecutionManagers
{
    internal static class PreperationManager
    {
        internal static void PrepareDatabasesForTestRun()
        {
            PrepareDatabaseForAt();
            PrepareDatabaseForAllComponents();
        }
        internal static void PrepareDatabasesForScenario()
        {
            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                connection.Execute("DELETE FROM dbo.DasCommitments");
                connection.Execute("DELETE FROM dbo.DasAccounts");
                connection.Execute("DELETE FROM Payments.Payments");
                connection.Execute("DELETE FROM PaymentsDue.RequiredPayments");
                connection.Execute("DELETE FROM AT.ReferenceData");
            }
        }


        private static void PrepareDatabaseForAt()
        {
            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                connection.ExecuteScript(Properties.Resources.ddl_AT_deds_tables);
            }
        }
        private static void PrepareDatabaseForAllComponents()
        {
            var watcher = new RebuildStatusWatcher();

            PrepareDatabaseForComponent(TestEnvironment.ProcessService, ComponentType.DataLockSubmission, TestEnvironment.Variables, watcher);
            PrepareDatabaseForComponent(TestEnvironment.ProcessService, ComponentType.DataLockPeriodEnd, TestEnvironment.Variables, watcher);
            PrepareDatabaseForComponent(TestEnvironment.ProcessService, ComponentType.EarningsCalculator, TestEnvironment.Variables, watcher);
            PrepareDatabaseForComponent(TestEnvironment.ProcessService, ComponentType.PaymentsDue, TestEnvironment.Variables, watcher);
            PrepareDatabaseForComponent(TestEnvironment.ProcessService, ComponentType.LevyCalculator, TestEnvironment.Variables, watcher);
            PrepareDatabaseForComponent(TestEnvironment.ProcessService, ComponentType.CoInvestedPayments, TestEnvironment.Variables, watcher);
            PrepareDatabaseForComponent(TestEnvironment.ProcessService, ComponentType.ReferenceCommitments, TestEnvironment.Variables, watcher);
            PrepareDatabaseForComponent(TestEnvironment.ProcessService, ComponentType.ReferenceAccounts, TestEnvironment.Variables, watcher);
            PrepareDatabaseForComponent(TestEnvironment.ProcessService, ComponentType.PeriodEndScripts, TestEnvironment.Variables, watcher);
        }
        private static void PrepareDatabaseForComponent(ProcessService processService, ComponentType componentType, EnvironmentVariables environmentVariables, RebuildStatusWatcher watcher)
        {
            processService.RebuildDedsDatabase(componentType, environmentVariables, watcher);
            if (watcher.LastError != null)
            {
                throw new Exception($"Error rebuilding deds for {componentType} - {watcher.LastError.Message}", watcher.LastError);
            }
        }


        private class RebuildStatusWatcher : StatusWatcherBase
        {
            public Exception LastError { get; private set; }

            public override void ExecutionStarted(TaskDescriptor[] tasks)
            {
                LastError = null;
            }
            public override void ExecutionCompleted(Exception error)
            {
                LastError = error;
            }
        }
    }
}
