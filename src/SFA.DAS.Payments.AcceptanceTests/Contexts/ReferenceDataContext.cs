using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.Entities;
using SFA.DAS.Payments.AcceptanceTests.Enums;
using IlrBuilder = SFA.DAS.Payments.AcceptanceTests.Builders.IlrBuilder;

namespace SFA.DAS.Payments.AcceptanceTests.Contexts
{
    public class ReferenceDataContext
    {
        public LearnerType LearnerType { get; set; }
        public decimal FundingMaximum { get; set; }
        public decimal AgreedPrice { get; set; }
        public Commitment[] Commitments { get; set; }
        public Employer[] Employers { get; private set; }

        public LearningDeliveryFam[] LearningDeliveryFams { get; set; }

        public List<EmploymentStatus> EmploymentStatuses { get; set; }

        
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
                VersionId = 1,
                Learner = string.Empty,
                Priority = 1,
                Provider = "provider",
                StandardCode=IlrBuilder.Defaults.StandardCode,
                Status = CommitmentPaymentStatus.Active
            };

            Commitments = new[] { commitment };
        }

        public void SetDefaultEmployer(Dictionary<string, decimal> monthlyBalance)
        {
            var employer = new Employer
            {
                Name = "employer",
                AccountId = long.Parse(IdentifierGenerator.GenerateIdentifier(8, false)),
                LearnersType = LearnerType.ProgrammeOnlyDas,
                MonthlyAccountBalance = monthlyBalance
            };

            Employers = new[] { employer };
        }

        public void SetEmployerLearnersType(string employerName, LearnerType learnersType)
        {
            var employer = Employers.Single(e => e.Name == employerName);

            employer.LearnersType = learnersType;
        }

        public void AddLearningDeliveryFam(LearningDeliveryFam learningDeliveryFam)
        {
            var fams = LearningDeliveryFams?.ToList() ?? new List<LearningDeliveryFam>();
            fams.Add(learningDeliveryFam);

            LearningDeliveryFams = fams.ToArray();

        }

        public void AddEmploymentStatus(EmploymentStatus status)
        {
            var statuses = EmploymentStatuses?.ToList() ?? new List<EmploymentStatus>();
            statuses.Add(status);

            EmploymentStatuses = statuses;

        }
    }
}
