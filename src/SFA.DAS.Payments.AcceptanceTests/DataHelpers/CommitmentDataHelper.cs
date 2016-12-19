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
                                        + "Priority, "
                                        + "VersionId, "
                                        + "PaymentStatus, "
                                        + "PaymentStatusDescription, "
                                        + "Payable"
                                    + ") VALUES ("
                                        + "@CommitmentId, "
                                        + "@Uln, "
                                        + "@Ukprn, "
                                        + "@AccountId, "
                                        + "@StartDate, "
                                        + "@EndDate, "
                                        + "@AgreedCost, "
                                        + "@StandardCode, "
                                        + "@ProgrammeType, "
                                        + "@FrameworkCode, "
                                        + "@PathwayCode, "
                                        + "@Priority, "
                                        + "@VersionId, "
                                        + "@PaymentStatus, "
                                        + "@PaymentStatusDescription, "
                                        + "@Payable"
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
                        standardCode = commitment.StandardCode,
                        frameworkCode = commitment.FrameworkCode,
                        programmeType = commitment.ProgrammeType,
                        pathwayCode = commitment.PathwayCode,
                        priority = commitment.Priority,
                        versionId = commitment.VersionId,
                        paymentStatus = commitment.PaymentStatus,
                        paymentStatusDescription = commitment.PaymentStatusDescription,
                        payable = commitment.Payable
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

        internal static void UpdateCommitmentStatus(long commitmentId, CommitmentPaymentStatus paymentStatus, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                connection.Execute("UPDATE dbo.DasCommitments SET " +
                                       "PaymentStatus = @paymentStatus, " +
                                       "PaymentStatusDescription = @paymentStatusDescription, " +
                                       "Payable = @payable " +
                                   "WHERE CommitmentId = @commitmentId",
                                   new
                                   {
                                       commitmentId = commitmentId,
                                       paymentStatus = (int)paymentStatus,
                                       paymentStatusDescription = paymentStatus.ToString(),
                                       payable = paymentStatus == CommitmentPaymentStatus.Active || paymentStatus == CommitmentPaymentStatus.Completed
                                   });
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

    }
}
