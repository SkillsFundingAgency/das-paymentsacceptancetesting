using System;
using System.Data.SqlClient;
using Dapper;
using ProviderPayments.TestStack.Core;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class AcceptanceTestDataHelper
    {
        internal static void CreateTestRun(string runId, DateTime startDate, string machineName, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                connection.Execute("INSERT INTO AT.TestRuns (RunId,StartDtTm,MachineName) VALUES (@runId,@startDate,@machineName)",
                    new { runId, startDate, machineName });
            }
        }

        internal static void Log(string runId, int level, DateTime date, string message, Exception exception, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                connection.Execute("INSERT INTO AT.Logs (RunId,LogLevel,LogDtTm,LogMessage,ExceptionDetails) VALUES (@runId,@level,@date,@message,@exception)",
                    new { runId, level, date, message, exception = exception?.ToString() });
            }
        }
    }
}
