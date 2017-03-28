using System;

namespace SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels
{
    public class EmploymentStatusReferenceData
    {
        public int EmployerId { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public DateTime EmploymentStatusApplies { get; set; }

        public string SmallEmployer { get; set; }
        public EmploymentStatusMonitoringType MonitoringType { get; set; }
        public int MonitoringCode { get; set; }
    }
}