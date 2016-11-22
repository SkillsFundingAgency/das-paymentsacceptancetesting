using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class ValidationErrorsDataHelper
    {
        internal static VlidationErrorEntity[] GetValidationErrors(long ukprn,  EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                var query = "SELECT * FROM [DataLock].[ValidationError] WHERE UKPRN = @ukprn ";
                return connection.Query<VlidationErrorEntity>(query, new { ukprn}).ToArray();
            }
        }
    }
}
