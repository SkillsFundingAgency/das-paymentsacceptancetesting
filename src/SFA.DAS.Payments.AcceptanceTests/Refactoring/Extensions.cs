using System;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring
{
    internal static class Extensions
    {
        internal static T ReadRowColumnValue<T>(this TableRow row, int columnIndex, string columnName, T defaultValue = default(T))
        {
            if (columnIndex > -1 && !string.IsNullOrWhiteSpace(row[columnIndex]))
            {
                try
                {
                    return (T)Convert.ChangeType(row[columnIndex], typeof(T));
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"'{row[columnIndex]}' is not a valid {columnName}", ex);
                }
            }
            return defaultValue;
        }
    }
}
