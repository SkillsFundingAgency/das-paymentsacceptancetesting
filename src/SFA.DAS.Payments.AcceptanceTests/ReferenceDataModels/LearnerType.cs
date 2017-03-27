using System.ComponentModel;

namespace SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels
{
    public enum LearnerType
    {
        Undefined = 0,

        [Description("programme only DAS")]
        ProgrammeOnlyDas = 1,

        [Description("16-18 programme only DAS")]
        ProgrammeOnlyDas1618 = 2,

        [Description("19-24 programme only DAS")]
        ProgrammeOnlyDas1924 = 3,

        [Description("programme only non-DAS")]
        ProgrammeOnlyNonDas = 4,

        [Description("16-18 programme only non-DAS")]
        ProgrammeOnlyNonDas1618 = 5,

        [Description("19-24 programme only non-DAS")]
        ProgrammeOnlyNonDas1924 = 6,
    }
}
