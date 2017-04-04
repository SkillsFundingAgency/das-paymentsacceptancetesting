using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Payments.AcceptanceTests.ResultsDataModels
{
    public class LevyAccountBalanceResult
    {
        public string CalculationPeriod { get; set; }
        public string DeliveryPeriod { get; set; }
        public decimal Amount { get; set; }
    }
}
