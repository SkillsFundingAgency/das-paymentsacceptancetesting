using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.DataCollectors
{
    public static class EventsDataCollector
    {
        private static readonly int SeperatorLength = 100;
        private static readonly string TableSeperator = new string('-', SeperatorLength);

        public static void CaptureEventsDataForScenario()
        {
            var scenarioName = ScenarioContext.Current.ScenarioInfo.Title;
            var script = BuildScript(scenarioName);

            var scenarioDirectory = new DirectoryInfo(TestEnvironment.Variables.IlrFileDirectory).Parent.FullName;
            var path = Path.Combine(scenarioDirectory, "events.sql");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.WriteAllText(path, script);
        }

        private static string BuildScript(string scenarioName)
        {
            var script = new StringBuilder();
            script.AppendLine($"/{new string('*', SeperatorLength - 1)}");
            script.AppendLine($"* Scenario: {scenarioName}");
            script.AppendLine($"* Produced on {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            script.AppendLine($"{new string('*', SeperatorLength - 1)}/");

            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                connection.Open();
                try
                {
                    AppendTableDataToScript("DataLock.DataLockEvents", connection, script);
                    AppendTableDataToScript("DataLock.DataLockEventPeriods", connection, script);
                    AppendTableDataToScript("DataLock.DataLockEventErrors", connection, script);
                    AppendTableDataToScript("DataLock.DataLockEventCommitmentVersions", connection, script);

                    AppendTableDataToScript("Submissions.SubmissionEvents", connection, script);

                    AppendTableDataToScript("PaymentsDue.RequiredPayments", connection, script);
                    AppendTableDataToScript("Payments.Periods", connection, script);
                    AppendTableDataToScript("Payments.Payments", connection, script);
                }
                finally
                {
                    connection.Close();
                }
            }

            return script.ToString();
        }
        private static void AppendTableDataToScript(string tableName, SqlConnection connection, StringBuilder script)
        {
            script.AppendLine(TableSeperator);
            script.AppendLine($"-- {tableName}");
            script.AppendLine(TableSeperator);

            var structure = GetTableStructure(tableName, connection);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM {tableName}";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AppendTableRowToScript(tableName, reader, structure, script);
                    }
                }
            }
        }
        private static TableColumn[] GetTableStructure(string tableName, SqlConnection connection)
        {
            var structure = new List<TableColumn>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT name, system_type_id, is_nullable FROM sys.columns WHERE [object_id] = OBJECT_ID('{tableName}') ORDER BY column_id";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        structure.Add(new TableColumn
                        {
                            Name = reader.GetString(0),
                            TypeId = reader.GetByte(1),
                            IsNullable = reader.GetBoolean(2)
                        });
                    }
                }
            }
            return structure.ToArray();
        }
        private static void AppendTableRowToScript(string tableName, SqlDataReader reader, TableColumn[] structure, StringBuilder script)
        {
            script.AppendLine($"INSERT INTO {tableName}");
            script.AppendLine("(");
            for (var i = 0; i < structure.Length; i++)
            {
                if (i > 0)
                {
                    script.AppendLine(",");
                }
                script.Append($"\t{structure[i].Name}");
            }
            script.AppendLine();
            script.AppendLine(")");
            script.AppendLine("VALUES");
            script.AppendLine("(");
            for (var i = 0; i < structure.Length; i++)
            {
                if (i > 0)
                {
                    script.AppendLine(",");
                }

                var value = reader[i];
                if (value == DBNull.Value)
                {
                    script.Append("\tNULL");
                }
                else
                {
                    script.Append($"\t'{value}'");
                }
            }
            script.AppendLine();
            script.AppendLine(")");
            script.AppendLine();
        }


        private class TableColumn
        {
            public string Name { get; set; }
            public int TypeId { get; set; }
            public bool IsNullable { get; set; }
        }
    }
}
