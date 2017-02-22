using System.Data.SqlClient;
using Dapper;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class SpecFlowEntitiesDataHelper
    {
        internal static void AddEntityRow(SpecFlowEntity entity, EnvironmentVariables environmentVariables)
        {
            using (var connection = new SqlConnection(environmentVariables.DedsDatabaseConnectionString))
            {
                connection.Execute("INSERT INTO SpecFlowEntities (Name, Field, Type) VALUES (@Name, @Field, @Type)", entity);
            }
        }
    }
}