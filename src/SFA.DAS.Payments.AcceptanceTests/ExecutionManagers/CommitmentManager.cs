using System.Data.SqlClient;
using Dapper;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.ExecutionManagers
{
    internal static class CommitmentManager
    {
        internal static void AddCommitment(CommitmentReferenceData commitment)
        {
            if(TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }

            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                connection.Execute("INSERT INTO dbo.DasCommitments " +
                                   "(CommitmentId, VersionId, Uln, Ukprn, AccountId, StartDate, EndDate, AgreedCost, StandardCode, ProgrammeType, FrameworkCode, PathwayCode, PaymentStatus, PaymentStatusDescription, Priority, EffectiveFromDate, EffectiveToDate) " +
                                   "VALUES" +
                                   "(@CommitmentId, @VersionId, @Uln, @Ukprn, @AccountId, @StartDate, @EndDate, @AgreedCost, @StandardCode, @ProgrammeType, @FrameworkCode, @PathwayCode, @PaymentStatus, @PaymentStatusDescription, @Priority, @EffectiveFromDate, @EffectiveToDate)",
                                   new
                                   {
                                       CommitmentId = commitment.CommitmentId,
                                       VersionId = commitment.VersionId,
                                       Uln = commitment.Uln,
                                       Ukprn = commitment.Ukprn,
                                       AccountId = commitment.EmployerAccountId,
                                       StartDate = commitment.StartDate,
                                       EndDate = commitment.EndDate,
                                       AgreedCost = commitment.AgreedPrice,
                                       StandardCode = commitment.StandardCode,
                                       ProgrammeType = commitment.ProgrammeType,
                                       FrameworkCode = commitment.FrameworkCode,
                                       PathwayCode = commitment.PathwayCode,
                                       PaymentStatus = (int)commitment.Status,
                                       PaymentStatusDescription = commitment.Status.ToString(),
                                       Priority = commitment.Priority,
                                       EffectiveFromDate = commitment.EffectiveFrom,
                                       EffectiveToDate = commitment.EffectiveTo
                                   });
            }
        }
    }
}
