using System.IO;
using SFA.DAS.Payments.AcceptanceTests.DataCollectors;
using SFA.DAS.Payments.AcceptanceTests.ExecutionManagers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests
{
    [Binding]
    public class SpecFlowHooks
    {
        [BeforeTestRun]
        public static void PrepareDatabasesForTest()
        {
            PreperationManager.PrepareDatabasesForTestRun();
        }

        [BeforeScenario]
        public static void PrepareDatabasesForScenario()
        {
            PreperationManager.PrepareDatabasesForScenario();
        }

        [BeforeScenario]
        public static void SetFileDirectoryForScenario()
        {
            var scenarioDirectoryName = ScenarioContext.Current.ScenarioInfo.Title.Replace(",", "")
                                                                                  .Replace("&", "and")
                                                                                  .Replace("\\", "")
                                                                                  .Replace(" ", "_")
                                                                                  .Replace("*", "_")
                                                                                  .Replace(":", "_");
            if (scenarioDirectoryName.Length > 50)
            {
                scenarioDirectoryName = scenarioDirectoryName.Substring(0, 50);
            }

            TestEnvironment.BaseScenarioDirectory = Path.Combine(TestEnvironment.Variables.WorkingDirectory, "Collect", scenarioDirectoryName );
            if (Directory.Exists(TestEnvironment.BaseScenarioDirectory))
            {
                foreach (var file in Directory.GetFiles(TestEnvironment.BaseScenarioDirectory))
                {
                    File.Delete(file);
                }
                foreach (var dir in Directory.GetDirectories(TestEnvironment.BaseScenarioDirectory))
                {
                    foreach (var file in Directory.GetFiles(dir))
                    {
                        File.Delete(file);
                    }
                    Directory.Delete(dir);
                }
            }
            else
            {
                Directory.CreateDirectory(TestEnvironment.BaseScenarioDirectory);
            }
        }

        [AfterScenario]
        public static void CaptureDetailsForScenario()
        {
            SavedDataCollector.CaptureEventsDataForScenario();
            SavedDataCollector.CapturePaymentsDataForScenario();

        }

    }
}
