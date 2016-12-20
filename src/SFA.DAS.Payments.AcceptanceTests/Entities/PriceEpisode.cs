using System;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class PriceEpisode
    {
        public string Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal? Tnp1 { get; set; }
        public decimal? Tnp2 { get; set; }
        public decimal? Tnp3 { get; set; }
        public decimal? Tnp4 { get; set; }

        public decimal MonthlyPayment { get; set; }
        public decimal CompletionPayment { get; set; }

        public string DataLockMatchKey
        {
            get
            {
                if (EndDate.HasValue)
                {
                    var endDate = EndDate.Value == EndDate.Value.NextCensusDate()
                        ? EndDate.Value
                        : EndDate.Value.AddMonths(-1);

                    return StartDate.GetPeriod() + " - " + endDate.GetPeriod();
                }
                else
                {
                    return StartDate.GetPeriod() + " onwards";
                }
            }
        }
    }
}