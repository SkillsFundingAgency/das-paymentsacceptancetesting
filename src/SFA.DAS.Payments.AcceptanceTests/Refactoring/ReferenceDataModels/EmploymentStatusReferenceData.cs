using System;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels
{
    public class EmploymentStatusReferenceData
    {
        public int EmployerId { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public DateTime EmploymentStatusApplies { get; set; }
        public string SmallEmployer { get; set; }
    }
}