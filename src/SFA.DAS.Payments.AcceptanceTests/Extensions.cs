using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests
{
    internal static class Extensions
    {
        internal static string[] AcademicYearsUntil(this DateTime startDate, DateTime endDate)
        {
            var date = new DateTime(startDate.Year, startDate.Month, 1);
            var to = new DateTime(endDate.Year, endDate.Month, 1);

            var academicYears = new HashSet<int>();

            while (date < to)
            {
                var year = int.Parse(date.GetAcademicYear());
                academicYears.Add(year);
                date = date.AddMonths(1);
            }

            return academicYears.OrderBy(y => y).Select(y => y.ToString()).ToArray();
        }
        internal static string GetAcademicYear(this DateTime date)
        {
            var startYear = (date.Month < 8 ? date.Year - 1 : date.Year) - 2000;
            var endYear = startYear + 1;
            return int.Parse(startYear.ToString() + endYear.ToString()).ToString();
        }

        internal static DateTime NextCensusDate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        internal static int GetPeriodNumber(this DateTime date)
        {
            if (date.Month < 8)
            {
                return date.Month + 5;
            }
            return date.Month - 7;
        }

        internal static DateTime ToPeriodDateTime(this string name)
        {
            return new DateTime(int.Parse(name.Substring(3, 2)) + 2000, int.Parse(name.Substring(0, 2)), 1);
        }
        internal static string ToPeriodName(this DateTime date)
        {
            return $"{date.Month:00}/{date.Year - 2000:00}";
        }



        internal static T ReadRowColumnValue<T>(this TableRow row, int columnIndex, string columnName, T defaultValue = default(T))
        {
            if (columnIndex > -1 && !string.IsNullOrWhiteSpace(row[columnIndex]))
            {
                try
                {
                    if (typeof(T) == typeof(DateTime?))
                    {
                        return (T)Convert.ChangeType(row[columnIndex], typeof(DateTime));
                    }

                    return (T)Convert.ChangeType(row[columnIndex], typeof(T));
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"'{row[columnIndex]}' is not a valid {columnName}", ex);
                }
            }
            return defaultValue;
        }

        internal static int ParseColumnValue(this TableRow row, int columnIndex) 
        {
            if (columnIndex > -1 && !string.IsNullOrWhiteSpace(row[columnIndex]))
            {
                var value = row[columnIndex].Trim(' ', '%');
                if (int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                {
                    return result;
                }
            }
            return -1;
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


        internal static void AddOrUpdate(this Dictionary<string, decimal> dictionary, string key, decimal value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        internal static TableRow RowWithKey(this TableRows rows, string key)
        {
            return rows.FirstOrDefault(r => r[0].Equals(key, StringComparison.OrdinalIgnoreCase));
        }

        internal static string GetPeriod(this DateTime date)
        {
            var month = date.Month < 10 ? "0" + date.Month : date.Month.ToString();
            return $"{month}/{date.Year - 2000}";
        }


        internal static DateTime GetCensusDate(this string period)
        {
            var month = int.Parse(period.Split('/')[0]);
            var year = int.Parse(period.Split('/')[1]) + 2000;

            return new DateTime(year, month, 1).NextCensusDate();
        }

        internal static decimal GetDecimalValue(this string value)
        {
            decimal result = 0m;

            if (!string.IsNullOrEmpty(value))
            {
                decimal.TryParse(value, out result);
            }
            return result;
        }

        internal static bool Contains(this TableRow row,string columnName)
        {
            return string.IsNullOrEmpty(row.Value<string>(columnName)) ? false : true;
        }
        internal static T Value<T>(this TableRow row, string columnName)
        {
            var data = row.Where(x => x.Key.Equals(columnName, StringComparison.InvariantCultureIgnoreCase));
            return data.Any() ? (T)Convert.ChangeType(data.FirstOrDefault().Value, typeof(T)) : default(T);
        }
       
    }
}
