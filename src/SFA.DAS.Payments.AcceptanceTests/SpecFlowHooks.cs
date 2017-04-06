using System.IO;
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
        public static void SetIlrFileDirectoryForScenario()
        {
            var scenarioDirectoryName = ScenarioContext.Current.ScenarioInfo.Title.Replace(",", "")
                                                                                  .Replace("&", "and")
                                                                                  .Replace("\\", "")
                                                                                  .Replace(" ", "_");
            if (scenarioDirectoryName.Length > 50)
            {
                scenarioDirectoryName = scenarioDirectoryName.Substring(0, 50);
            }

            TestEnvironment.Variables.IlrFileDirectory = Path.Combine(TestEnvironment.Variables.WorkingDirectory, "IlrFiles", scenarioDirectoryName);
            if (Directory.Exists(TestEnvironment.Variables.IlrFileDirectory))
            {
                foreach (var file in Directory.GetFiles(TestEnvironment.Variables.IlrFileDirectory))
                {
                    File.Delete(file);
                }
            }
            else
            {
                Directory.CreateDirectory(TestEnvironment.Variables.IlrFileDirectory);
            }
        }
    }
}
