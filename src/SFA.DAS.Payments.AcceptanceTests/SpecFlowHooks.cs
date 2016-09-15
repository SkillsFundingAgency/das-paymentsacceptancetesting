using System;
using System.Globalization;
using System.Threading;
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
        public static void PrepateDeds()
        {
            var processService = new ProcessService(new TestLogger());
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            processService.RebuildDedsDatabase(ComponentType.DataLock, environmentVariables);
            processService.RebuildDedsDatabase(ComponentType.EarningsCalculator, environmentVariables);
            processService.RebuildDedsDatabase(ComponentType.LevyCalculator, environmentVariables);
        }
        
    }
}
