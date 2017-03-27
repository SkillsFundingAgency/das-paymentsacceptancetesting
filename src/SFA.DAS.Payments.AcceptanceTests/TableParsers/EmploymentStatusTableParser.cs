using System;
using System.Linq;
using System.Text.RegularExpressions;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.TableParsers
{
    public static class EmploymentStatusTableParser
    {
        public static void ParseEmploymentStatusIntoContext(SubmissionContext submissionContext, Table employmentStatus)
        {
            if (employmentStatus.Rows.Count < 1)
            {
                throw new ArgumentOutOfRangeException("Employment status table must have at least 1 row");
            }

            var structure = ParseEmploymentStatusTableStructure(employmentStatus);
            foreach (var row in employmentStatus.Rows)
            {
                submissionContext.EmploymentStatus.Add(ParseEmploymentStatusTableRow(row, structure));
            }
        }


        private static EmploymentStatusTableColumnStructure ParseEmploymentStatusTableStructure(Table contractTypes)
        {
            var structure = new EmploymentStatusTableColumnStructure();

            for (var c = 0; c < contractTypes.Header.Count; c++)
            {
                var header = contractTypes.Header.ElementAt(c);
                switch (header)
                {
                    case "Employer":
                        structure.EmployerIndex = c;
                        break;
                    case "Employment Status":
                        structure.EmploymentStatusIndex = c;
                        break;
                    case "Employment Status Applies":
                        structure.EmploymentStatusAppliesIndex = c;
                        break;
                    case "Small Employer":
                        structure.SmallEmployerIndex = c;
                        break;
                    default:
                        throw new ArgumentException($"Unexpected column in contract types table: {header}");
                }
            }

            if (structure.EmployerIndex == -1)
            {
                throw new ArgumentException("Contract types table is missing Employer column");
            }
            if (structure.EmploymentStatusIndex == -1)
            {
                throw new ArgumentException("Contract types table is missing Employment Status column");
            }
            if (structure.EmploymentStatusAppliesIndex == -1)
            {
                throw new ArgumentException("Contract types table is missing Employment Status Applies column");
            }

            return structure;
        }
        private static EmploymentStatusReferenceData ParseEmploymentStatusTableRow(TableRow row, EmploymentStatusTableColumnStructure structure)
        {
            var employerReference = row.ReadRowColumnValue<string>(structure.EmployerIndex, "Employer");
            int employerId;
            if (string.IsNullOrEmpty(employerReference))
            {
                employerId = 0;
            }
            else
            {
                var employerMatch = Regex.Match(employerReference, "^employer ([0-9]{1,})$");
                if (!employerMatch.Success)
                {
                    throw new ArgumentException($"Employer '{employerReference}' is not a valid employer reference");
                }
                employerId = int.Parse(employerMatch.Groups[1].Value);
            }

            var status = new EmploymentStatusReferenceData
            {
                EmployerId = employerId,
                EmploymentStatus = (EmploymentStatus)row.ReadRowColumnValue<string>(structure.EmploymentStatusIndex, "Employment Status").ToEnumByDescription(typeof(EmploymentStatus)),
                EmploymentStatusApplies = row.ReadRowColumnValue<DateTime>(structure.EmploymentStatusAppliesIndex, "Employment Status Applies"),

            };

            var smallEmployer = row.ReadRowColumnValue<string>(structure.SmallEmployerIndex, "Small Employer");
            if (smallEmployer?.Length > 3)
            {
                status.MonitoringType = (EmploymentStatusMonitoringType)smallEmployer.Substring(0, 3).ToEnumByDescription(typeof(EmploymentStatusMonitoringType));
                status.MonitoringCode = int.Parse(smallEmployer.Substring(3));
            }

            return status;
        }


        private class EmploymentStatusTableColumnStructure
        {
            public int EmployerIndex { get; set; } = -1;
            public int EmploymentStatusIndex { get; set; } = -1;
            public int EmploymentStatusAppliesIndex { get; set; } = -1;
            public int SmallEmployerIndex { get; set; } = -1;
        }
    }
}
