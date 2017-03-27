using System.ComponentModel;

namespace SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels
{
    public enum CompletionStatus
    {
        Continuing = 1,

        Completed = 2,

        Withdrawn = 3,

        [Description("planned break")]
        PlannedBreak = 6,
    }
}
