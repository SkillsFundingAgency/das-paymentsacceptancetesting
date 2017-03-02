using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels
{
    public class EmployerAccountReferenceData
    {
        public int Id { get; set; }
        public long Balance { get; set; }
        public List<PeriodValue> PeriodBalances { get; set; }
    }
}