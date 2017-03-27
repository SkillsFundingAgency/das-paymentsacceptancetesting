using System.ComponentModel;

namespace SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels
{
    public enum ContractType
    {
        [Description("DAS")]
        ContractWithEmployer = 1,

        [Description("Non-DAS")]
        ContractWithSfa = 2
    }
}