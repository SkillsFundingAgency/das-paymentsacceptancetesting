﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.Payments.AcceptanceTests.Assertions.SubmissionDataLockRules;
using SFA.DAS.Payments.AcceptanceTests.Contexts;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions
{
    public static class SubmissionDataLockAssertions
    {
        public static void AssertPaymentsAndEarningsResults(SubmissionDataLockContext dataLockContext, SubmissionContext submissionContext)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }

            var submissionResults = submissionContext.SubmissionResults.ToArray();
            new OnProgrammeDataLockRule().AssertPaymentTypeDataLockMatches(dataLockContext.DataLockStatusForOnProgramme, submissionResults);
            new CompletionDataLockRule().AssertPaymentTypeDataLockMatches(dataLockContext.DataLockStatusForCompletion, submissionResults);
            new BalancingDataLockRule().AssertPaymentTypeDataLockMatches(dataLockContext.DataLockStatusForBalancing, submissionResults);
            new DisadvantageUpliftDataLockRule().AssertPaymentTypeDataLockMatches(dataLockContext.DataLockStatusForDisadvantageUplift, submissionResults);
            new Employer16To18IncentiveDataLockRule().AssertPaymentTypeDataLockMatches(dataLockContext.DataLockStatusForEmployer16To18Incentive, submissionResults);
            new Provider16To18IncentiveDataLockRule().AssertPaymentTypeDataLockMatches(dataLockContext.DataLockStatusForProvider16To18Incentive, submissionResults);
            new EnglishAndMathsOnProgrammeDataLockRule().AssertPaymentTypeDataLockMatches(dataLockContext.DataLockStatusForEnglishAndMathOnProgramme, submissionResults);
            new EnglishAndMathsBalancingDataLockRule().AssertPaymentTypeDataLockMatches(dataLockContext.DataLockStatusForEnglishAndMathBalancing, submissionResults);
            new FrameworkUpliftOnProgrammeDataLockRule().AssertPaymentTypeDataLockMatches(dataLockContext.DataLockStatusForFrameworkUpliftOnProgramme, submissionResults);
            new FrameworkUpliftCompletionDataLockRule().AssertPaymentTypeDataLockMatches(dataLockContext.DataLockStatusForFrameworkUpliftCompletion, submissionResults);
            new FrameworkUpliftBalancingDataLockRule().AssertPaymentTypeDataLockMatches(dataLockContext.DataLockStatusForFrameworkUpliftBalancing, submissionResults);
            new LearningSupportDataLockRule().AssertPaymentTypeDataLockMatches(dataLockContext.DataLockStatusForLearningSupport, submissionResults);
        }
    }
}
