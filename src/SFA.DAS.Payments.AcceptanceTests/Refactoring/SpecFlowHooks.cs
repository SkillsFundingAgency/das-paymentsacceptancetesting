using SFA.DAS.Payments.AcceptanceTests.Refactoring.ExecutionManagers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring
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
    }
}
