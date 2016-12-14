using System;
using System.Data.SqlClient;
using Dapper;
using ProviderPayments.TestStack.Core;

namespace SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment
{
    internal class DatabaseHelper
    {
        private readonly EnvironmentVariables _environment;

        public DatabaseHelper(EnvironmentVariables environment)
        {
            _environment = environment;
        }

        internal void RunDedsScrips()
        {
            using (var connection = new SqlConnection(_environment.DedsDatabaseConnectionString))
            {
                connection.Open();
                try
                {
                    RunScript(connection, Properties.Resources.ilr_deds);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        private void RunScript(SqlConnection connection, string script)
        {
            var commands = script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var command in commands)
            {
                connection.Execute(command);
            }
        }
    }
}
