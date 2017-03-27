using Dapper;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.ReferenceDataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.ExecutionManagers
{
    internal class PaymentsManager
    {
        internal static void SavePaymentDue(string requiredPaymentId,
                                            long ukprn,
                                            long uln,
                                           int? FrameworkCode,
                                            int? PathwayCode,
                                            int? ProgrammeType,
                                            long? StandardCode,
                                            CommitmentReferenceData commitment,
                                              string learnRefNumber,
                                              string collectionPeriodName,
                                              int collectionPeriodMonth,
                                              int collectionPeriodYear,
                                              int transactionType,
                                              ContractType contractType,
                                              decimal amountDue)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }
            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {

               

                connection.Execute("INSERT INTO PaymentsDue.RequiredPayments (" +
                                        "Id," +
                                        "CommitmentId," +
                                       "CommitmentVersionId," +
                                       "AccountId," +
                                       "AccountVersionId," +
                                       "uln," +
                                       "LearnRefNumber," +
                                       "AimSeqNumber," +
                                       "Ukprn," +
                                       "DeliveryMonth," +
                                       "DeliveryYear," +
                                       "CollectionPeriodName," +
                                       "CollectionPeriodMonth," +
                                       "CollectionPeriodYear," +
                                       "TransactionType," +
                                       "AmountDue," +
                                       "StandardCode," +
                                       "ProgrammeType," +
                                       "FrameworkCode," +
                                       "PathwayCode," +
                                       "ApprenticeshipContractType," +
                                       "SfaContributionPercentage," +
                                       "UseLevyBalance" +
                                   ") VALUES (" +
                                       "@requiredPaymentId," +
                                       "@commitmentId," +
                                       "@versionId," +
                                       "@EmployerAccountId," +
                                       "@accountVersionId," +
                                       "@uln," +
                                       "@learnRefNumber," +
                                       "1," +
                                       "@ukprn," +
                                       "@collectionPeriodMonth," +
                                       "@collectionPeriodYear," +
                                       "@collectionPeriodName," +
                                       "@collectionPeriodMonth," +
                                       "@collectionPeriodYear," +
                                       "@transactionType," +
                                       "@amountDue," +
                                       "@standardCode," +
                                       "@programmeType," +
                                       "@frameworkCode," +
                                       "@pathwayCode," +
                                        "@contractType," +
                                       "0.9," +
                                        "1" +
                                   ")",
                    new
                    {
                        requiredPaymentId,
                        commitmentId =  commitment == null ? (long?)null : commitment.CommitmentId,
                        VersionId = commitment==null?  0 : commitment.VersionId,
                        EmployerAccountId = commitment == null ? 0: commitment.EmployerAccountId,
                        accountVersionId = commitment == null ? 0 : commitment.VersionId,
                        uln,
                        learnRefNumber,
                        ukprn,
                        collectionPeriodName,
                        collectionPeriodMonth,
                        collectionPeriodYear,
                        transactionType,
                        amountDue,
                        StandardCode = StandardCode ,
                        ProgrammeType  = ProgrammeType,
                        FrameworkCode = FrameworkCode ,
                        PathwayCode = PathwayCode ,
                        contractType
                    });
            }
        }


        internal static void SavePayment(string requiredPaymentId,
                                            string collectionPeriodName,
                                            int collectionPeriodMonth,
                                            int collectionPeriodYear,
                                            int transactionType,
                                            FundingSource fundingSource,
                                            decimal amount)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }
            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {



                connection.Execute("INSERT INTO Payments.Payments (" +
                                        "RequiredPaymentId," +
                                       "DeliveryMonth," +
                                       "DeliveryYear," +
                                       "CollectionPeriodName," +
                                       "CollectionPeriodMonth," +
                                       "CollectionPeriodYear," +
                                        "FundingSource," +
                                       "TransactionType," +
                                       "Amount" +
                                   ") VALUES (" +
                                       "@requiredPaymentId," +
                                       "@collectionPeriodMonth," +
                                       "@collectionPeriodYear," +
                                       "@collectionPeriodName," +
                                       "@collectionPeriodMonth," +
                                       "@collectionPeriodYear," +
                                       "@fundingSource," +
                                       "@transactionType," +
                                       "@amount" +
                                   ")",
                    new
                    {
                        requiredPaymentId,
                        collectionPeriodName,
                        collectionPeriodMonth,
                        collectionPeriodYear,
                        fundingSource,
                        transactionType,
                        amount
                    });
            }
        }

    }
}
