using System;
using System.Data.SqlClient;
using Dapper;
using ProviderPayments.TestStack.Core;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring
{
    internal class AcceptanceTestsLogger : ILogger
    {
        private readonly string _runId;
        private readonly string _connectionString;

        public AcceptanceTestsLogger(string runId, string connectionString)
        {
            _runId = runId;
            _connectionString = connectionString;
        }

        public void Debug(string message)
        {
            Log(1, message);
        }

        public void Info(string message)
        {
            Log(2, message);
        }

        public void Warn(string message)
        {
            Log(3, message);
        }

        public void Warn(Exception exception, string message)
        {
            Log(3, message, exception);
        }

        public void Error(Exception exception, string message)
        {
            Log(4, message, exception);
        }


        private void Log(int level, string message, Exception exception = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("INSERT INTO AT.Logs (RunId, LogLevel, LogMessage, ExceptionDetails, ScenarioTitle) " +
                                   "VALUES (@RunId, @LogLevel, @Message, @ErrorDetails, @ScenarioTitle)",
                                   new
                                   {
                                       RunId = _runId,
                                       LogLevel = level,
                                       Message = message,
                                       ErrorDetails = exception?.ToString(),
                                       ScenarioTitle = ScenarioContext.Current?.ScenarioInfo?.Title
                                   });
            }
        }
    }
}