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
    public static class SavedDataCollector
    {
        private static readonly int SeperatorLength = 100;
        private static readonly string TableSeperator = new string('-', SeperatorLength);

        public static void CaptureEventsDataForScenario()
        {
            var script = InitialiseScript();

            AppendTableDataToScript("DataLock.DataLockEvents",  script);
            AppendTableDataToScript("DataLock.DataLockEventPeriods",  script);
            AppendTableDataToScript("DataLock.DataLockEventErrors",  script);
            AppendTableDataToScript("DataLock.DataLockEventCommitmentVersions",  script);
            AppendTableDataToScript("Submissions.SubmissionEvents",  script);

            SaveSqlFile("events.sql",script.ToString(), TestEnvironment.BaseScenarioDirectory);
        }
        public static void CapturePaymentsDataForScenario()
        {
            var script = InitialiseScript();

            AppendTableDataToScript("PaymentsDue.RequiredPayments",  script);
            AppendTableDataToScript("Payments.Periods",  script);
            AppendTableDataToScript("Payments.Payments", script);

            SaveSqlFile("payments.sql", script.ToString(), TestEnvironment.BaseScenarioDirectory);
        }

        public static void CaptureAccountsDataForScenario()
        {
            var script = InitialiseScript();

            AppendTableDataToScript("dbo.DasAccounts", script);
             
            SaveSqlFile("dbo_Accounts.sql", script.ToString(), TestEnvironment.Variables.IlrFileDirectory);
        }
        public static void CaptureCommitmentsDataForScenario()
        {
            
            var script = InitialiseScript();

            AppendTableDataToScript("dbo.DasCommitments", script);

            SaveSqlFile("dbo_DasCommitments.sql", script.ToString(), TestEnvironment.Variables.IlrFileDirectory);
        }

        public static void SaveSqlFile(string fileName,  string script, string directoryPath)
        {
            
          
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var path = Path.Combine(directoryPath, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.WriteAllText(path, script);
        }

        private static StringBuilder InitialiseScript()
        {
            var scenarioName = ScenarioContext.Current.ScenarioInfo.Title;

            var script = new StringBuilder();
            script.AppendLine($"/{new string('*', SeperatorLength - 1)}");
            script.AppendLine($"* Scenario: {scenarioName}");
            script.AppendLine($"* Produced on {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            script.AppendLine($"{new string('*', SeperatorLength - 1)}/");

            return script;
        }
        private static void AppendTableDataToScript(string tableName, StringBuilder script)
        {
            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                connection.Open();
                try
                {
                    AppendTableDataToScript(tableName, connection, script);

                }
                finally
                {
                    connection.Close();
                }
            }
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
                    if (structure[i].TypeId == 40 || structure[i].TypeId == 61)
                    {
                        script.Append($"\t'{((DateTime)value).ToString("yyyy-MM-dd hh:mm:ss")}'");
                    }
                    else
                    {
                        script.Append($"\t'{value}'");
                    }
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
