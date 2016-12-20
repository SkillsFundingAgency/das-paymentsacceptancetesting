using System;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests
{
    [Binding]
    public static class SpecFlowHooks
    {
        internal static string RunId { get; private set; }

        [BeforeTestRun]
        public static void PrepareTestRun()
        {
            RunId = IdentifierGenerator.GenerateIdentifier(12);

            AcceptanceTestDataHelper.CreateTestRun(RunId, DateTime.Now, Environment.MachineName, EnvironmentVariablesFactory.GetEnvironmentVariables());
        }

        [BeforeTestRun]
        public static void PrepareDeds()
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            var databaseHelper = new DatabaseHelper(environmentVariables);
            databaseHelper.RunDedsScrips();

            var processService = new ProcessService(new TestLogger());
            processService.RebuildDedsDatabase(ComponentType.DataLockSubmission, environmentVariables);
            processService.RebuildDedsDatabase(ComponentType.DataLockPeriodEnd, environmentVariables);
            processService.RebuildDedsDatabase(ComponentType.EarningsCalculator, environmentVariables);
            processService.RebuildDedsDatabase(ComponentType.PaymentsDue, environmentVariables);
            processService.RebuildDedsDatabase(ComponentType.LevyCalculator, environmentVariables);
            processService.RebuildDedsDatabase(ComponentType.CoInvestedPayments, environmentVariables);
            processService.RebuildDedsDatabase(ComponentType.ReferenceCommitments, environmentVariables);
            processService.RebuildDedsDatabase(ComponentType.ReferenceAccounts, environmentVariables);
            processService.RebuildDedsDatabase(ComponentType.PeriodEndScripts, environmentVariables);
        }

        [BeforeScenario]
        public static void ClearCollectionPeriodMapping()
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            AcceptanceTestDataHelper.ClearCollectionPeriodMapping(environmentVariables);
        }

        [BeforeScenario]
        public static void ClearOldStuff()
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            AcceptanceTestDataHelper.ClearOldDedsIlrSubmissions(environmentVariables);
        }

        [BeforeScenario]
        public static void Start()
        {
            Console.WriteLine("Start: " + DateTime.Now.ToString());
        }

        [AfterScenario]
        public static void Finish()
        {
            Console.WriteLine("Finish: " + DateTime.Now.ToString());
        }
    }
}
