using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts
{
    public class DataLockContext
    {
        public DataLockContext()
        {
            DataLockStatusForOnProgramme = new List<DataLockPeriodMatch>();
            DataLockStatusForCompletion = new List<DataLockPeriodMatch>();
            DataLockStatusForBalancing = new List<DataLockPeriodMatch>();
            DataLockStatusForProvider16To18Incentive = new List<DataLockPeriodMatch>();
            DataLockStatusForEmployer16To18Incentive = new List<DataLockPeriodMatch>();
            DataLockStatusForFrameworkUpliftOnProgramme = new List<DataLockPeriodMatch>();
            DataLockStatusForFrameworkUpliftCompletion = new List<DataLockPeriodMatch>();
            DataLockStatusForFrameworkUpliftBalancing = new List<DataLockPeriodMatch>();
            DataLockStatusForDisadvantageUplift = new List<DataLockPeriodMatch>();
            DataLockStatusForEnglishAndMathOnProgramme = new List<DataLockPeriodMatch>();
            DataLockStatusForEnglishAndMathBalancing = new List<DataLockPeriodMatch>();
            DataLockStatusForLearningSupport = new List<DataLockPeriodMatch>();
        }

        public List<DataLockPeriodMatch> DataLockStatusForOnProgramme { get; set; }
        public List<DataLockPeriodMatch> DataLockStatusForCompletion { get; set; }
        public List<DataLockPeriodMatch> DataLockStatusForBalancing { get; set; }
        public List<DataLockPeriodMatch> DataLockStatusForProvider16To18Incentive { get; set; }
        public List<DataLockPeriodMatch> DataLockStatusForEmployer16To18Incentive { get; set; }
        public List<DataLockPeriodMatch> DataLockStatusForFrameworkUpliftOnProgramme { get; set; }
        public List<DataLockPeriodMatch> DataLockStatusForFrameworkUpliftCompletion { get; set; }
        public List<DataLockPeriodMatch> DataLockStatusForFrameworkUpliftBalancing { get; set; }
        public List<DataLockPeriodMatch> DataLockStatusForDisadvantageUplift { get; set; }
        public List<DataLockPeriodMatch> DataLockStatusForEnglishAndMathOnProgramme { get; set; }
        public List<DataLockPeriodMatch> DataLockStatusForEnglishAndMathBalancing { get; set; }
        public List<DataLockPeriodMatch> DataLockStatusForLearningSupport { get; set; }
    }
}