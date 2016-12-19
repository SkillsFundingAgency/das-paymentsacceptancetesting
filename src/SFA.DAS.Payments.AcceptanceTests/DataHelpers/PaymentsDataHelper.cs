using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.Enums;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class PaymentsDataHelper
    {



        internal static PaymentEntity[] GetAccountPaymentsForPeriod(long ukprn, long accountId, long? uln, int year, int month, FundingSource fundingSource, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = @"SELECT * 
                                FROM Payments.Payments p
                                JOIN dbo.DasCommitments c On c.CommitmentId = p.CommitmentId
                                WHERE p.UKPRN = @ukprn 
                                    AND p.CollectionPeriodMonth = @month 
                                    AND p.CollectionPeriodYear = @year 
                                    AND p.FundingSource = @fundingSource
                                    AND c.AccountId = @accountId";
                query = uln.HasValue ? query + "   AND c.ULN = " + uln.Value : query;
                return connection.Query<PaymentEntity>(query, new { ukprn, month, year, accountId, fundingSource }).ToArray();
            }


        }

        internal static PaymentEntity[] GetPaymentsForPeriod(long ukprn, long? uln, int year, int month, FundingSource fundingSource, EnvironmentVariables environmentVariables)
        {
            if (uln.HasValue)
            {
                using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
                {
                    var query = "SELECT * FROM Payments.Payments p " +
                        " JOIN dbo.DasCommitments c On c.CommitmentId = p.CommitmentId" +
                        " WHERE p.UKPRN = @ukprn AND CollectionPeriodMonth = @month AND " +
                        " CollectionPeriodYear = @year AND FundingSource = @fundingSource AND " +
                        " c.ULN = @uln";
                    return connection.Query<PaymentEntity>(query, new { ukprn, month, year, fundingSource,uln = uln.Value }).ToArray();
                }
            }
            else
            {
                using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
                {
                    var query = "SELECT * FROM Payments.Payments WHERE UKPRN = @ukprn AND CollectionPeriodMonth = @month AND CollectionPeriodYear = @year AND FundingSource = @fundingSource";
                    return connection.Query<PaymentEntity>(query, new { ukprn, month, year, fundingSource }).ToArray();
                }
            }

        }

    }
}
