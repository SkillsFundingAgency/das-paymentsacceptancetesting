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

                var isDas = learningDetails.LearnerType == LearnerType.ProgrammeOnlyNonDas ||
                            learningDetails.LearnerType == LearnerType.ProgrammeOnlyNonDas1618||
                            learningDetails.LearnerType == LearnerType.ProgrammeOnlyNonDas1924? false: true;

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
                                       "IlrSubmissionDateTime," +
                                       "PriceEpisodeIdentifier" +
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
                                       "@sfaContributionPercentage," +
                                       "@fundingLineType," +
                                        "@useLevyBalance," +
                                        "@learnAimRef," +
                                        "@StartDate," +
                                        "@IlrSubmissionDateTime," +
                                        "'2-403-1-06/05/2017'" +
                                   ")",
                    new
                    {
                        requiredPaymentId,
                        commitmentId = !isDas? (long?)null : commitment.CommitmentId,
                        VersionId = !isDas ?  null : commitment.VersionId,
                        EmployerAccountId = !isDas ? (long?)null : commitment.EmployerAccountId,
                        accountVersionId = !isDas ? null : commitment.VersionId.Split('-')[1],
                        uln,
                        learnRefNumber,
                        ukprn,
                        collectionPeriodName,
                        collectionPeriodMonth,
                        collectionPeriodYear,
                        transactionType,
                        amountDue,
                        StandardCode = learningDetails.StandardCode == 0 ? null : (int?)learningDetails.StandardCode,
                        ProgrammeType = learningDetails.ProgrammeType == 0 ? null : (int?)learningDetails.ProgrammeType,
                        FrameworkCode = learningDetails.FrameworkCode == 0 ? null : (int?)learningDetails.FrameworkCode,
                        PathwayCode = learningDetails.PathwayCode == 0 ? null : (int?)learningDetails.PathwayCode,
                        ContractType = isDas ? 1 : 2,
                        UseLevyBalance = isDas ? 1 : 0,
                        SfaContributionPercentage = 0.90 ,
                        FundingLineType = isDas? "19 + Apprenticeship(From May 2017) Levy Contract" : "19 + Apprenticeship(From May 2017) Non-Levy Contract",
                        LearnAimRef = String.IsNullOrEmpty(learningDetails.LearnAimRef) ? "ZPROG001" : learningDetails.LearnAimRef,
                        StartDate = learningDetails.StartDate,
                        AimSequenceNumber = learningDetails.AimSequenceNumber == 0 ? 1 : learningDetails.AimSequenceNumber,
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

        public static void AddRequiredPaymentForReversal(int deliveryMonth, int deliveryYear, decimal amountDue, TransactionType transactionType)
        {
            if (TestEnvironment.ValidateSpecsOnly)
            {
                return;
            }
            using (var connection = new SqlConnection(TestEnvironment.Variables.DedsDatabaseConnectionString))
            {

                connection.Execute("Insert Into [Adjustments].[ManualAdjustments]" +
                                "([RequiredPaymentIdToReverse]" +
                                ",[ReasonForReversal]" +
                                ",[RequestorName]" +
                                ",[DateUploaded]" +
                                ",[RequiredPaymentIdForReversal])" +
                            "Select Id," +
                                    "'Test scenario', " +
                                    "'Test', " +
                                    "getDate()," +
                                    "NULL" +
                    "  From PaymentsDue.RequiredPayments Where " +
                    " DeliveryMonth = @deliveryMonth and DeliveryYear = @deliveryYear and " +
                    " TransactionType = @transactionType And AmountDue = @amountDue",
                  new
                  {
                      deliveryMonth,
                      deliveryYear,
                      transactionType,
                      amountDue
                  });
            }
        }

    }
}