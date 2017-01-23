using System;
using System.Data.SqlClient;
using Dapper;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class CommitmentDataHelper
    {
        internal static void CreateCommitment(CommitmentEntity commitment, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                connection.Execute("INSERT INTO DasCommitments ("
                                        + "CommitmentId, "
                                        + "VersionId, "
                                        + "Uln, "
                                        + "Ukprn, "
                                        + "AccountId, "
                                        + "StartDate, "
                                        + "EndDate, "
                                        + "AgreedCost, "
                                        + "StandardCode, "
                                        + "ProgrammeType, "
                                        + "FrameworkCode, "
                                        + "PathwayCode, "
                                        + "PaymentStatus, "
                                        + "PaymentStatusDescription, "
                                        + "Priority, "
                                        + "EffectiveFromDate, "
                                        + "EffectiveToDate"
                                    + ") VALUES ("
                                        + "@commitmentId, "
                                        + "@versionId, "
                                        + "@uln, "
                                        + "@ukprn, "
                                        + "@accountId, "
                                        + "@startDate, "
                                        + "@endDate, "
                                        + "@agreedCost, "
                                        + "@standardCode, "
                                        + "@programmeType, "
                                        + "@frameworkCode, "
                                        + "@pathwayCode, "
                                        + "@paymentStatus, "
                                        + "@paymentStatusDescription, "
                                        + "@priority, "
                                        + "@effectiveFromDate, "
                                        + "@effectiveToDate"
                                    + ")",
                    new
                    {
                        commitmentId = commitment.CommitmentId,
                        ukprn = commitment.Ukprn,
                        uln = commitment.Uln,
                        accountId = commitment.AccountId,
                        startDate = commitment.StartDate,
                        endDate = commitment.EndDate,
                        agreedCost = commitment.AgreedCost,
                        standardCode = commitment.StandardCode > 0 ? commitment.StandardCode : (long?)null,
                        frameworkCode = commitment.FrameworkCode > 0 ? commitment.FrameworkCode : (int?)null,
                        programmeType = commitment.ProgrammeType > 0 ? commitment.ProgrammeType : (int?)null,
                        pathwayCode = commitment.PathwayCode > 0 ? commitment.PathwayCode : (int?)null,
                        priority = commitment.Priority,
                        versionId = commitment.VersionId,
                        paymentStatus = commitment.PaymentStatus,
                        paymentStatusDescription = commitment.PaymentStatusDescription,
                        effectiveFromDate = commitment.EffectiveFrom,
                        effectiveToDate = commitment.EffectiveTo
                    });
            }
        }

        internal static void UpdateEventStreamPointer(EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                connection.Execute("INSERT INTO EventStreamPointer SELECT ISNULL(MAX(EventId),0) + 1, GETDATE() FROM EventStreamPointer");
            }
        }

        internal static void ClearCommitments(EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                connection.Execute("DELETE FROM dbo.DasCommitments");
                connection.Execute("DELETE FROM dbo.EventStreamPointer");
            }
        }

        internal static void UpdateCommitmentEffectiveTo(long commitmentId, long versionId, DateTime effectiveTo, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                connection.Execute("UPDATE dbo.DasCommitments SET " +
                                       "EffectiveToDate = @effectiveTo " +
                                   "WHERE CommitmentId = @commitmentId " +
                                   "AND VersionId = @versionId",
                                   new
                                   {
                                       effectiveTo,
                                       commitmentId,
                                       versionId
                                   });
            }
        }

        internal static void CreateNewCommmitmentVersion(long commitmentId, long versionId, CommitmentPaymentStatus paymentStatus, DateTime effectiveFrom, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                connection.Execute("INSERT INTO DasCommitments ("
                                        + "CommitmentId, "
                                        + "VersionId, "
                                        + "Uln, "
                                        + "Ukprn, "
                                        + "AccountId, "
                                        + "StartDate, "
                                        + "EndDate, "
                                        + "AgreedCost, "
                                        + "StandardCode, "
                                        + "ProgrammeType, "
                                        + "FrameworkCode, "
                                        + "PathwayCode, "
                                        + "PaymentStatus, "
                                        + "PaymentStatusDescription, "
                                        + "Priority, "
                                        + "EffectiveFromDate"
                                    + ") "
                                    + "SELECT "
                                        + "CommitmentId, "
                                        + "VersionId + 1, "
                                        + "Uln, "
                                        + "Ukprn, "
                                        + "AccountId, "
                                        + "StartDate, "
                                        + "EndDate, "
                                        + "AgreedCost, "
                                        + "StandardCode, "
                                        + "ProgrammeType, "
                                        + "FrameworkCode, "
                                        + "PathwayCode, "
                                        + "@paymentStatus, "
                                        + "@paymentStatusDescription, "
                                        + "Priority, "
                                        + "@effectiveFromDate "
                                    + "FROM DasCommitments "
                                    + "WHERE CommitmentId = @commitmentId "
                                    + "AND VersionId = @versionId",
                    new
                    {
                        commitmentId,
                        versionId,
                        paymentStatus = (int)paymentStatus,
                        paymentStatusDescription = paymentStatus.ToString(),
                        effectiveFromDate = effectiveFrom
                    });
            }
        }
    }
}
