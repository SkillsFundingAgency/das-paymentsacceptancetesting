using System;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.Translators
{
    internal static class IlrTranslator
    {
        internal static CompletionStatus TranslateCompletionStatus(string completionStatus)
        {

            if (completionStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase))
            {
                return CompletionStatus.Completed;
            }

            else if (completionStatus.Equals("Transferred", StringComparison.OrdinalIgnoreCase) ||
                completionStatus.Equals("withdrawn", StringComparison.OrdinalIgnoreCase))
            {
                return CompletionStatus.Transferred;
            }

            else if (completionStatus.Equals("Planned Break", StringComparison.OrdinalIgnoreCase))
            {
                return CompletionStatus.PlannedBreak;
            }
            return CompletionStatus.Continuing;
        }
    }
}
