using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels
{
    public class EarningsAndPaymentsBreakdown
    {
        public EarningsAndPaymentsBreakdown()
        {
            ProviderEarnedTotal = new List<PeriodValue>();
            ProviderEarnedFromSfa = new List<PeriodValue>();
            ProviderEarnedFromEmployers = new List<EmployerAccountPeriodValue>();
            ProviderPaidBySfa = new List<PeriodValue>();
            PaymentDueFromEmployers = new List<EmployerAccountPeriodValue>();
            EmployersLevyAccountDebited = new List<EmployerAccountPeriodValue>();
            SfaLevyBudget = new List<PeriodValue>();
            SfaLevyCoFundBudget = new List<PeriodValue>();
            SfaNonLevyCoFundBudget = new List<PeriodValue>();
            SfaLevyAdditionalPayments = new List<PeriodValue>();
            SfaNonLevyAdditionalPayments = new List<PeriodValue>();
            RefundTakenBySfa = new List<PeriodValue>();
            EmployersLevyAccountCredited= new List<EmployerAccountPeriodValue>();
        }

        public string ProviderId { get; set; }
        public List<PeriodValue> ProviderEarnedTotal { get; set; }
        public List<PeriodValue> ProviderEarnedFromSfa { get; set; }
        public List<EmployerAccountPeriodValue> ProviderEarnedFromEmployers { get; set; }
        public List<PeriodValue> ProviderPaidBySfa { get; set; }
        public List<EmployerAccountPeriodValue> PaymentDueFromEmployers { get; set; }
        public List<EmployerAccountPeriodValue> EmployersLevyAccountDebited { get; set; }
        public List<PeriodValue> SfaLevyBudget { get; set; }
        public List<PeriodValue> SfaLevyCoFundBudget { get; set; }
        public List<PeriodValue> SfaNonLevyCoFundBudget { get; set; }
        public List<PeriodValue> SfaLevyAdditionalPayments { get; set; }
        public List<PeriodValue> SfaNonLevyAdditionalPayments { get; set; }

        public List<PeriodValue> RefundTakenBySfa { get; set; }
        public List<EmployerAccountPeriodValue> EmployersLevyAccountCredited { get; set; }


    }
}
