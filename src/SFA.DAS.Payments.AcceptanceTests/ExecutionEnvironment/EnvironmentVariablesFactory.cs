using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProviderPayments.TestStack.Core;

namespace SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment
{
    internal static class EnvironmentVariablesFactory
    {
        internal static EnvironmentVariables GetEnvironmentVariables()
        {
            return new EnvironmentVariables
            {
                TransientConnectionString = GetTransientConnectionString(),
                DedsDatabaseConnectionString = GetDedsConnectionString(),
                WorkingDirectory = GetWorkingDirectory(),
                CurrentYear = DateTime.Today.GetAcademicYear(),
                LogLevel = "DEBUG"
            };
        }

        private static string GetTransientConnectionString()
        {
            return ConfigurationManager.AppSettings["TransientConnectionString"];
        }
        private static string GetDedsConnectionString()
        {
            return ConfigurationManager.AppSettings["DedsConnectionString"];
        }
        private static string GetWorkingDirectory()
        {
            return "C:\\temp\\PaymentsAT\\";
        }
    }
}
