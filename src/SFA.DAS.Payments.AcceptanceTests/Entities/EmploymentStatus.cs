using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Payments.AcceptanceTests.Entities
{
    public class EmploymentStatus
    {
        public int StatusCode { get; set; }
        public DateTime DateFrom { get; set; }
        public string EmployerId { get; set; }
        public EmploymentStatusMonitoring EmploymentStatusMonitoring { get; set; }
    }
}
