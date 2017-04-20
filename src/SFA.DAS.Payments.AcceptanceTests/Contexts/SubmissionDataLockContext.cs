using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Contexts
{
    public class SubmissionDataLockContext
    {
        public SubmissionDataLockContext()
        {
            DataLockStatusForOnProgramme = new List<SubmissionDataLockPeriodMatch>();
            DataLockStatusForCompletion = new List<SubmissionDataLockPeriodMatch>();
            DataLockStatusForBalancing = new List<SubmissionDataLockPeriodMatch>();
            DataLockStatusForProvider16To18Incentive = new List<SubmissionDataLockPeriodMatch>();
            DataLockStatusForEmployer16To18Incentive = new List<SubmissionDataLockPeriodMatch>();
            DataLockStatusForFrameworkUpliftOnProgramme = new List<SubmissionDataLockPeriodMatch>();
            DataLockStatusForFrameworkUpliftCompletion = new List<SubmissionDataLockPeriodMatch>();
            DataLockStatusForFrameworkUpliftBalancing = new List<SubmissionDataLockPeriodMatch>();
            DataLockStatusForDisadvantageUplift = new List<SubmissionDataLockPeriodMatch>();
            DataLockStatusForEnglishAndMathOnProgramme = new List<SubmissionDataLockPeriodMatch>();
            DataLockStatusForEnglishAndMathBalancing = new List<SubmissionDataLockPeriodMatch>();
            DataLockStatusForLearningSupport = new List<SubmissionDataLockPeriodMatch>();
        }

        public List<SubmissionDataLockPeriodMatch> DataLockStatusForOnProgramme { get; set; }
        public List<SubmissionDataLockPeriodMatch> DataLockStatusForCompletion { get; set; }
        public List<SubmissionDataLockPeriodMatch> DataLockStatusForBalancing { get; set; }
        public List<SubmissionDataLockPeriodMatch> DataLockStatusForProvider16To18Incentive { get; set; }
        public List<SubmissionDataLockPeriodMatch> DataLockStatusForEmployer16To18Incentive { get; set; }
        public List<SubmissionDataLockPeriodMatch> DataLockStatusForFrameworkUpliftOnProgramme { get; set; }
        public List<SubmissionDataLockPeriodMatch> DataLockStatusForFrameworkUpliftCompletion { get; set; }
        public List<SubmissionDataLockPeriodMatch> DataLockStatusForFrameworkUpliftBalancing { get; set; }
        public List<SubmissionDataLockPeriodMatch> DataLockStatusForDisadvantageUplift { get; set; }
        public List<SubmissionDataLockPeriodMatch> DataLockStatusForEnglishAndMathOnProgramme { get; set; }
        public List<SubmissionDataLockPeriodMatch> DataLockStatusForEnglishAndMathBalancing { get; set; }
        public List<SubmissionDataLockPeriodMatch> DataLockStatusForLearningSupport { get; set; }
    }
}
