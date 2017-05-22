using System;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.Assertions.DataLockRules
{
    public class DataLockCommitmentVersionRule : DataLockRuleBase
    {
        public override void AssertDataLockEvents(DataLockContext context, LearnerResults[] results)
        {
            foreach (var expected in context.DataLockEventCommitments)
            {
                var actual = GetEventsForPriceEpisode(results, expected.PriceEpisodeIdentifier)
                    .SelectMany(x => x.CommitmentVersions)
                    .FirstOrDefault(e => e.CommitmentVersion == expected.ApprenticeshipVersion);
                if (actual == null)
                {
                    throw new Exception($"Event for price episode {expected.PriceEpisodeIdentifier} does not contain commitment version with with version id {expected.ApprenticeshipVersion}");
                }

                if (expected.StandardCode > 0)
                {
                    if (expected.StandardCode != actual.CommitmentStandardCode)
                    {
                        throw new Exception($"Expected programe type of {expected.StandardCode} but actually {actual.CommitmentStandardCode}");
                    }
                }
                else
                {
                    if (expected.ProgrammeType != actual.CommitmentProgrammeType)
                    {
                        throw new Exception($"Expected programe type of {expected.ProgrammeType} but actually {actual.CommitmentProgrammeType}");
                    }
                    if (expected.FrameworkCode != actual.CommitmentFrameworkCode)
                    {
                        throw new Exception($"Expected programe type of {expected.FrameworkCode} but actually {actual.CommitmentFrameworkCode}");
                    }
                    if (expected.PathwayCode != actual.CommitmentPathwayCode)
                    {
                        throw new Exception($"Expected programe type of {expected.PathwayCode} but actually {actual.CommitmentPathwayCode}");
                    }
                }

                if (expected.StartDate != actual.CommitmentStartDate)
                {
                    throw new Exception($"Expected start date of {expected.StartDate} but actually {actual.CommitmentStartDate}");
                }
                if (expected.NegotiatedPrice != actual.CommitmentNegotiatedPrice)
                {
                    throw new Exception($"Expected negotiated price of {expected.NegotiatedPrice} but actually {actual.CommitmentNegotiatedPrice}");
                }
                if (expected.EffectiveDate != actual.CommitmentEffectiveDate)
                {
                    throw new Exception($"Expected effective date of {expected.EffectiveDate} but actually {actual.CommitmentEffectiveDate}");
                }
            }
        }
    }
}
