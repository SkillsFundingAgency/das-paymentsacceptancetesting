using System.Data.SqlClient;
using Dapper;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ExecutionManagers
{
    public static class ReferenceDataManager
    {
        public static void SetFundingBandMax(int value)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }

            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                connection.Execute("DELETE FROM AT.ReferenceData WHERE [Key]='Cap' AND [Type]='FundingBandCap'");
                connection.Execute("INSERT INTO AT.ReferenceData ([Key],[Value],[Type]) VALUES ('Cap',@value,'FundingBandCap')", new { value });
            }
        }
        public static void AddDisadvantagedPostcodeUplift(string value)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }

            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                connection.Execute("DELETE FROM AT.ReferenceData WHERE [Key]='OX17 1EZ' AND [Type]='PostCode'");
                connection.Execute("INSERT INTO AT.ReferenceData ([Key],[Value],[Type]) VALUES ('OX17 1EZ',@value,'PostCode')", new { value });
            }
        }
    }
}
