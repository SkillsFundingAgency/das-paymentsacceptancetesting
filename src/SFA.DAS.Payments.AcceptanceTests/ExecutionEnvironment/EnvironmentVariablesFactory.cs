using System;
using System.Configuration;
using ProviderPayments.TestStack.Core;

namespace SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment
{
    internal static class EnvironmentVariablesFactory
    {
        internal static EnvironmentVariables GetEnvironmentVariables()
        {
            return new EnvironmentVariables
            {
                TransientConnectionString = ConfigurationManager.AppSettings["TransientConnectionString"],
                DedsDatabaseConnectionString = ConfigurationManager.AppSettings["DedsConnectionString"],
                WorkingDirectory = ConfigurationManager.AppSettings["WorkingDir"],
                CurrentYear = DateTime.Today.GetAcademicYear(),
                LogLevel = "Trace",

                AccountsApiBaseUrl = "",
                AccountsApiClientSecret = "",
                AccountsApiClientToken = "",
                AccountsApiIdentifierUri = "",
                AccountsApiTenant = ""
            };
        }
    }
}
