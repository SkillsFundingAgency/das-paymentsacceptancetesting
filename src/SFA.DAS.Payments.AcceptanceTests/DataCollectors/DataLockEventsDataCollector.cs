using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ResultsDataModels;

namespace SFA.DAS.Payments.AcceptanceTests.DataCollectors
{
    public static class DataLockEventsDataCollector
    {
        public static void CollectDataLockEventsForAllPeriods(List<LearnerResults> results, LookupContext lookupContext)
        {
            var events = GetAllDataLockEventsDataFromDeds();
            foreach (var learner in results)
            {
                var uln = lookupContext.GetUln(learner.LearnerReferenceNumber);
                learner.DataLockEvents = events.Where(e => e.Uln == uln).ToArray();
            }
        }

        private static DataLockEventResult[] GetAllDataLockEventsDataFromDeds()
        {
            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {
                var events = GetMainDataLockEvents(connection);
                foreach (var evt in events)
                {
                    PopulateEventChildProperties(connection, evt);
                }
                return events;
            }
        }
        private static DataLockEventResult[] GetMainDataLockEvents(SqlConnection connection)
        {
            return connection.Query<DataLockEventResult>("SELECT * FROM DataLock.DataLockEvents ORDER BY ULN").ToArray();
        }
        private static void PopulateEventChildProperties(SqlConnection connection, DataLockEventResult result)
        {
            result.Periods = connection.Query<DataLockEventPeriod>("SELECT * FROM DataLock.DataLockEventPeriods WHERE DataLockEventId = @DataLockEventId", 
                new { result.DataLockEventId }).ToArray();

            result.Errors = connection.Query<DataLockEventError>("SELECT * FROM DataLock.DataLockEventErrors WHERE DataLockEventId = @DataLockEventId",
                new { result.DataLockEventId }).ToArray();

            result.CommitmentVersions = connection.Query<DataLockEventCommitmentVersion>("SELECT * FROM DataLock.DataLockEventCommitmentVersions WHERE DataLockEventId = @DataLockEventId",
                new { result.DataLockEventId }).ToArray();
        }
    }
}
