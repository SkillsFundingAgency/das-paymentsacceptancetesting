using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;

namespace SFA.DAS.Payments.AcceptanceTests.Contexts
{
    public class ReferenceDataContext
    {
        public LearnerType LearnerType { get; set; }
        public decimal FundingMaximum { get; set; }
        public decimal AgreedPrice { get; set; }
        public Commitment[] Commitments { get; set; }
        public Employer[] Employers { get; private set; }

        public ReferenceDataContext()
        {
            SetDefaultCommitment();
        }

        public void AddEmployer(Employer employer)
        {
            var contextEmployers = Employers?.ToList() ?? new List<Employer>();
            contextEmployers.Add(employer);

            Employers = contextEmployers.ToArray();
        }

        private void SetDefaultCommitment()
        {
            var commitment = new Commitment
            {
                Employer = "employer",
                Id = long.Parse(IdentifierGenerator.GenerateIdentifier(6, false)),
                Learner = string.Empty,
                Priority = 1,
                Provider = "provider"
            };

            Commitments = new[] { commitment };
        }
    }
}
