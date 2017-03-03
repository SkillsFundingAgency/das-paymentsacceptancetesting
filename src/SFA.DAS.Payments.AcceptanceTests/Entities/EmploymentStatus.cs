using SFA.DAS.Payments.AcceptanceTests.Enums;
using System;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class EmploymentStatus
    {
        public EmploymentType StatusCode { get; set; }
        public DateTime DateFrom { get; set; }
        public int EmployerId { get; set; }
        public EmploymentStatusMonitoring EmploymentStatusMonitoring { get; set; }
    }
}
