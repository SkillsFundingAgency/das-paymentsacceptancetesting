using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.Entities;
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

        internal static decimal GetValueForPeriod(this PeriodisedValuesEntity entity, int periodNumber)
        {
            switch (periodNumber)
            {
                case 1:
                    return entity.Period_1;
                case 2:
                    return entity.Period_2;
                case 3:
                    return entity.Period_3;
                case 4:
                    return entity.Period_4;
                case 5:
                    return entity.Period_5;
                case 6:
                    return entity.Period_6;
                case 7:
                    return entity.Period_7;
                case 8:
                    return entity.Period_8;
                case 9:
                    return entity.Period_9;
                case 10:
                    return entity.Period_10;
                case 11:
                    return entity.Period_11;
                case 12:
                    return entity.Period_12;
            }

            throw new IndexOutOfRangeException("Invalid periodNumber. Must be between 1 and 12 inclusive");
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
    }
}
