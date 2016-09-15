using System;
using System.Linq;
using ProviderPayments.TestStack.Core;
using ProviderPayments.TestStack.Core.ExecutionStatus;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;

namespace SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment
{
    internal class TestStatusWatcher : StatusWatcherBase
    {
        private readonly EnvironmentVariables _environmentVariables;
        private readonly string _processName;

        public TestStatusWatcher(EnvironmentVariables environmentVariables, string processName)
        {
            _environmentVariables = environmentVariables;
            _processName = processName;
        }

        public override void ExecutionStarted(TaskDescriptor[] tasks)
        {
            var message = $"Starting execution of process {_processName} with tasks " + tasks.Select(t => t.Id).Aggregate((x, y) => $"{x}, {y}");
            AcceptanceTestDataHelper.Log(SpecFlowHooks.RunId, 1, DateTime.Now, message, null, _environmentVariables);
        }
        public override void TaskStarted(string taskId)
        {
            AcceptanceTestDataHelper.Log(SpecFlowHooks.RunId, 1, DateTime.Now, $"Executing task {taskId}", null, _environmentVariables);
        }
        public override void TaskCompleted(string taskId, Exception error)
        {
            AcceptanceTestDataHelper.Log(SpecFlowHooks.RunId, 1, DateTime.Now, $"Finished execeuting task {taskId}", error, _environmentVariables);
        }
        public override void ExecutionCompleted(Exception error)
        {
            AcceptanceTestDataHelper.Log(SpecFlowHooks.RunId, 1, DateTime.Now, $"Finished execeuting process {_processName}", error, _environmentVariables);
        }
    }
}
