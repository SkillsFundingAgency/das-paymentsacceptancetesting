using System;

namespace SFA.DAS.Payments.AcceptanceTests.Builders
{
    public class IlrSubmissionDefaults
    {
        internal IlrSubmissionDefaults()
        {
            Ukprn = 123456;

            FirstUln = 120001;

            StandardCode = 98765;
            PathwayCode = 0;
            FrameworkCode = 0;
            ProgrammeType = 0;
            ActualStartDate = new DateTime(2017, 5, 1);
            PlannedEndDate = new DateTime(2018, 6, 2);
            ActualEndDate = null;
            AgreedPrice = 15000;
            ActFamCodeValue = 2;
        }

        public long Ukprn { get; set; }

        public long FirstUln { get; set; }

        public long StandardCode { get; set; }
        public int PathwayCode { get; set; }
        public int FrameworkCode { get; set; }
        public int ProgrammeType { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public Decimal AgreedPrice { get; set; }
        public short ActFamCodeValue { get; set; }
    }
}