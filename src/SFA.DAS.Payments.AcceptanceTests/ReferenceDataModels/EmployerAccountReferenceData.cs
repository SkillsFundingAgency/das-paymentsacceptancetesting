using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels
{
    public class EmployerAccountReferenceData
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public List<PeriodValue> PeriodBalances { get; set; }
        public bool IsLevyPayer { get; set; } = true;
    }
}