using System.Text.RegularExpressions;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring
{
    internal static class Validations
    {
        internal static bool IsValidPeriodName(string periodName)
        {
            return Regex.IsMatch(periodName, @"[0-9]{2}\/[0-9]{2}", RegexOptions.IgnoreCase);
        }
    }
}
