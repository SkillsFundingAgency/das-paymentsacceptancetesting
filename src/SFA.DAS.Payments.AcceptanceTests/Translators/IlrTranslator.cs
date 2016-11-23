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
            return CompletionStatus.InProgress;
        }
    }
}
