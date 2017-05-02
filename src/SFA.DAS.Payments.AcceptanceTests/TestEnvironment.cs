using System;
using System.Configuration;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;

namespace SFA.DAS.Payments.AcceptanceTests
{
    internal static class TestEnvironment
    {
        static TestEnvironment()
        {
            RunId = IdentifierGenerator.GenerateIdentifier();
            //ValidateSpecsOnly = true;

            Variables = new EnvironmentVariables
            {
                TransientConnectionString = ConfigurationManager.AppSettings["TransientConnectionString"],
                DedsDatabaseConnectionString = ConfigurationManager.AppSettings["DedsConnectionString"],
                WorkingDirectory = ConfigurationManager.AppSettings["WorkingDir"],
                IlrFileDirectory = System.IO.Path.Combine(ConfigurationManager.AppSettings["WorkingDir"], "Collect"),
                CurrentYear = DateTime.Today.GetAcademicYear(),
                LogLevel = "Trace",
                
                IlrAimRefLookups = new[]
                {
                    new IlrAimRefLookup { ProgrammeType = 2, FrameworkCode = 403, PathwayCode = 1, ComponentLearnAimRef = "60005105", MathsAndEnglishLearnAimRef = "50086832" }
                },

                AccountsApiBaseUrl = "",
                AccountsApiClientSecret = "",
                AccountsApiClientToken = "",
                AccountsApiIdentifierUri = "",
                AccountsApiTenant = ""
            };

            Logger = new AcceptanceTestsLogger(RunId, Variables.DedsDatabaseConnectionString);

            ProcessService = new ProcessService(Logger);
        }


        internal static string RunId { get; }
        internal static ILogger Logger { get; }
        internal static EnvironmentVariables Variables { get; }
        internal static ProcessService ProcessService { get; }
        internal static string DataCollectionDirectory { get; }
        internal static bool ValidateSpecsOnly { get; } = false;
        internal static string BaseScenarioDirectory { get; set; }
    }
}
