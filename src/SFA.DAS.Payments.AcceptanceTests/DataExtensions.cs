using System;
using System.Data.Common;
using System.Linq;
using Dapper;

namespace SFA.DAS.Payments.AcceptanceTests
{
    internal static class DataExtensions
    {
        internal static void ExecuteScript(this DbConnection connection, string script)
        {
            var commands = script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries)
                .Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            foreach (var command in commands)
            {
                try
                {
                    connection.Execute(command);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error running command from script. Error: {ex.Message}, Command: {command}", ex);
                }
            }
        }
    }
}
