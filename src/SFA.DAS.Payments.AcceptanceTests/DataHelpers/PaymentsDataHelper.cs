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



        internal static PaymentEntity[] GetAccountPaymentsForPeriod(long ukprn, long accountId, int year, int month, FundingSource fundingSource, EnvironmentVariables environmentVariables)
        {
            var collectionPeriodMonth = month + 1;
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = @"SELECT * 
                                FROM Payments.Payments 
                                WHERE UKPRN = @ukprn 
                                    AND CollectionPeriodMonth = @month 
                                    AND CollectionPeriodYear = @year 
                                    AND FundingSource =@fundingSource
                                        AND CommitmentId IN (SELECT CommitmentId FROM dbo.DasCommitments WHERE AccountId = @accountId)
                            UNION
SELECT * 
                                FROM Payments.Payments 
                                WHERE UKPRN = @ukprn 
                                    AND CollectionPeriodMonth = @collectionPeriodMonth 
                                    AND CollectionPeriodYear = @year 
                                    AND DeliveryMonth<CollectionPeriodMonth
                                    AND FundingSource =@fundingSource
                                    AND CommitmentId IN (SELECT CommitmentId FROM dbo.DasCommitments WHERE AccountId = @accountId)
";
                return connection.Query<PaymentEntity>(query, new { ukprn, month, year, collectionPeriodMonth, accountId,fundingSource }).ToArray();
            }
        }

        internal static PaymentEntity[] GetPaymentsForPeriod(long ukprn, int year, int month, FundingSource fundingSource, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = "SELECT * FROM Payments.Payments WHERE UKPRN = @ukprn AND CollectionPeriodMonth = @month AND CollectionPeriodYear = @year AND FundingSource = @fundingSource";
                return connection.Query<PaymentEntity>(query, new { ukprn, month, year, fundingSource }).ToArray();
            }
        }

    }
}
