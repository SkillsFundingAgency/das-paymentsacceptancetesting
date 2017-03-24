using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts
{
    public class EmployerAccountContext
    {
        public EmployerAccountContext()
        {
            EmployerAccounts = new List<EmployerAccountReferenceData>();
        }

        public List<EmployerAccountReferenceData> EmployerAccounts { get; set; }
    }
}