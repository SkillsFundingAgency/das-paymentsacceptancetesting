using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts
{
    public class CommitmentsContext
    {
        public CommitmentsContext()
        {
            Commitments = new List<CommitmentReferenceData>();
        }
        public List<CommitmentReferenceData> Commitments { get; set; }
    }
}
