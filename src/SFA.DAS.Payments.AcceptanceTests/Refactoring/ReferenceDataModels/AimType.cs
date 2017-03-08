using System.ComponentModel;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels
{
    public enum AimType
    {
        Programme = 1,

        [Description("maths or english")]
        MathsOrEnglish = 2,
    }
}
