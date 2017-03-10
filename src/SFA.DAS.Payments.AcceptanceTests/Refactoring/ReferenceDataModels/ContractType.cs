using System.ComponentModel;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels
{
    public enum ContractType
    {
        [Description("DAS")]
        ContractWithEmployer = 1,

        [Description("Non-DAS")]
        ContractWithSfa = 2
    }
}