using SFA.DAS.Payments.AcceptanceTests.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
