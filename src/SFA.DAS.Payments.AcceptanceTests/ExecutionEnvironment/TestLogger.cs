using System;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment
{
    internal class TestLogger : ILogger
    {
        private EnvironmentVariables _environmentVariables;

        public TestLogger()
        {
            _environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();
        }

        public void Debug(string message)
        {
            AcceptanceTestDataHelper.Log(SpecFlowHooks.RunId, ScenarioContext.Current?.ScenarioInfo?.Title, 1, DateTime.Now, message, null, _environmentVariables);
        }

        public void Info(string message)
        {
            AcceptanceTestDataHelper.Log(SpecFlowHooks.RunId, ScenarioContext.Current?.ScenarioInfo?.Title, 2, DateTime.Now, message, null, _environmentVariables);
        }

        public void Warn(string message)
        {
            AcceptanceTestDataHelper.Log(SpecFlowHooks.RunId, ScenarioContext.Current?.ScenarioInfo?.Title, 3, DateTime.Now, message, null, _environmentVariables);
        }
        public void Warn(Exception exception, string message)
        {
            AcceptanceTestDataHelper.Log(SpecFlowHooks.RunId, ScenarioContext.Current?.ScenarioInfo?.Title, 3, DateTime.Now, message, exception, _environmentVariables);
        }

        public void Error(Exception exception, string message)
        {
            AcceptanceTestDataHelper.Log(SpecFlowHooks.RunId, ScenarioContext.Current?.ScenarioInfo?.Title, 4, DateTime.Now, message, exception, _environmentVariables);
        }
    }
}
