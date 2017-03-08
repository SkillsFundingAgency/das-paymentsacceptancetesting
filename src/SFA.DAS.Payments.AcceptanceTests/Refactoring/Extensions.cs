using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

        internal static object ToEnumByDescription(this string description, Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("enumType must be an Enum", nameof(enumType));
            }

            foreach (Enum enumValue in Enum.GetValues(enumType))
            {
                var enumDescription = enumValue.GetEnumDescription();
                if (enumDescription.Equals(description, StringComparison.CurrentCultureIgnoreCase))
                {
                    return enumValue;
                }
            }

            throw new ArgumentException($"Cannot find {enumType.Name} with description {description}");
        }
        internal static string GetEnumDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return value.ToString();
        }
    }
}
