using Dapper;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
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
                                              decimal amountDue,
                                              DateTime learningStartDate,
                                              string learnAimRef="ZPROG001",
                                              int aimSequenceNumber = 1)
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
                                       "FundingLineType," +
                                       "UseLevyBalance," +
                                       "LearnAimRef," +
                                       "LearningStartDate," +
                                       "IlrSubmissionDateTime" +
                                   ") VALUES (" +
                                       "@requiredPaymentId," +
                                       "@commitmentId," +
                                       "@versionId," +
                                       "@EmployerAccountId," +
                                       "@accountVersionId," +
                                       "@uln," +
                                       "@learnRefNumber," +
                                       "@aimSequenceNumber," +
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
                                       "'19 + Apprenticeship(From May 2017) Levy Contract'," +
                                        "1," +
                                        "@learnAimRef," +
                                        "@learningStartDate," +
                                        "@IlrSubmissionDateTime" +
                                   ")",
                    new
                    {
                        requiredPaymentId,
                        commitmentId = commitment == null ? (long?)null : commitment.CommitmentId,
                        VersionId = commitment == null ? "0-000" : commitment.VersionId,
                        EmployerAccountId = commitment == null ? 0 : commitment.EmployerAccountId,
                        accountVersionId = commitment == null ? 0 : int.Parse(commitment.VersionId.Split('-')[1]),
                        uln,
                        learnRefNumber,
                        ukprn,
                        collectionPeriodName,
                        collectionPeriodMonth,
                        collectionPeriodYear,
                        transactionType,
                        amountDue,
                        StandardCode = StandardCode == 0? null : StandardCode,
                        ProgrammeType = ProgrammeType == 0? null : ProgrammeType,
                        FrameworkCode = FrameworkCode ==0 ? null : FrameworkCode,
                        PathwayCode = PathwayCode == 0 ? null : PathwayCode,
                        contractType,
                        learnAimRef,
                        learningStartDate,
                        aimSequenceNumber,
                        ilrSubmissiondateTime = DateTime.Now.AddMonths(-3)
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