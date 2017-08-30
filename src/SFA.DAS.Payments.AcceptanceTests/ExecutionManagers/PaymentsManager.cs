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
                                            CommitmentReferenceData commitment,
                                              string learnRefNumber,
                                              string collectionPeriodName,
                                              int collectionPeriodMonth,
                                              int collectionPeriodYear,
                                              int transactionType,
                                              decimal amountDue,
                                              IlrLearnerReferenceData learningDetails)
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
                                        "@useLevyBalance," +
                                        "@learnAimRef," +
                                        "@StartDate," +
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
                        StandardCode = learningDetails.StandardCode == 0? null : (int?)learningDetails.StandardCode,
                        ProgrammeType = learningDetails.ProgrammeType == 0? null : (int?)learningDetails.ProgrammeType,
                        FrameworkCode = learningDetails.FrameworkCode ==0 ? null : (int?)learningDetails.FrameworkCode,
                        PathwayCode = learningDetails.PathwayCode == 0 ? null : (int?)learningDetails.PathwayCode,
                        ContractType = commitment == null ? ContractType.ContractWithSfa : ContractType.ContractWithEmployer,
                        UseLevyBalance = commitment == null ? 0: 1,
                        LearnAimRef = String.IsNullOrEmpty(learningDetails.LearnAimRef) ? "ZPROG001" : learningDetails.LearnAimRef,
                        StartDate = learningDetails.StartDate,
                        AimSequenceNumber  = learningDetails.AimSequenceNumber == 0 ? 1 : learningDetails.AimSequenceNumber,
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