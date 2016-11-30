using NUnit.Framework;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Base;
using TechTalk.SpecFlow;
using IlrBuilder = SFA.DAS.Payments.AcceptanceTests.Builders.IlrBuilder;
using System;
using System.Collections.Generic;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.Enums;


namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Intermediate
{
    [Binding, Scope(Feature  = "Datalock validation fails for different reasons")]
    public class DataLockStepDefinitions : BaseStepDefinitions
    {
        public DataLockStepDefinitions(StepDefinitionsContext context)
            : base(context)
        {

        }


        [Given(@"the following commitment exists for an apprentice:")]
        public void GivenTheFollowingCommitmentExistsForAnApprentice(Table table)
        {

            SetupCommitments(table);

        }

        [Given(@"the following commitments exist for an apprentice:")]
        public void GivenTheFollowingCommitmentsExistForAnApprentice(Table table)
        {
            SetupCommitments(table);
          
        }


        [When(@"an ILR file is submitted with the following data:")]
        public void WhenAndILRIsSubmittedWithTheFollowingData(Table table)
        {

            SetupContexLearners(table);

            var provider = StepDefinitionsContext.GetDefaultProvider();
            var ukprn = long.Parse(table.Rows[0]["UKPRN"]);
            var startDate = StepDefinitionsContext.GetIlrStartDate().NextCensusDate();

            SubmitIlr(ukprn, provider.Learners,
                startDate.GetAcademicYear(),
                startDate.NextCensusDate(),
                new ProcessService(new TestLogger()),
                provider.EarnedByPeriod);

            //Update the UKPRN to the one from ILR as this is the one which will be in the validation error table
            provider.Ukprn = ukprn;
        }



        [Then(@"a datalock error (.*) is produced")]
        public void ThenADatalockErrorOfDLOCK_WillBeProduced(string errorCode)
        {
            var provider = StepDefinitionsContext.GetDefaultProvider();
            var startDate = StepDefinitionsContext.GetIlrStartDate().NextCensusDate();

            var validationError = ValidationErrorsDataHelper.GetValidationErrors(provider.Ukprn, EnvironmentVariables);

            Assert.IsNotNull(validationError, "There is no validation error entity present");
            Assert.IsTrue(validationError.Any(x => x.RuleId == errorCode));
        }




        private void SetupCommitments(Table table)
        {
            StepDefinitionsContext.ReferenceDataContext.SetDefaultEmployer(
                                             new Dictionary<string, decimal> {
                                                    { "All", int.MaxValue }
                                             });
            var employer = StepDefinitionsContext.ReferenceDataContext.Employers[0];

            AccountDataHelper.CreateAccount(employer.AccountId, employer.AccountId.ToString(), 0.00m, EnvironmentVariables);

            foreach (var row in table.Rows)
            {
                //var row = table.Rows[0];

                var ukprn = long.Parse(row["UKPRN"]);
                var startDate = DateTime.Parse(row["start date"]);


                var frameworkCode = table.Header.Contains("framework code") ? Int32.Parse(row["framework code"]) : IlrBuilder.Defaults.FrameworkCode;
                var programmeType = table.Header.Contains("programme type") ? Int32.Parse(row["programme type"]) : IlrBuilder.Defaults.ProgrammeType;
                var pathwayCode = table.Header.Contains("pathway code") ? Int32.Parse(row["pathway code"]) : IlrBuilder.Defaults.PathwayCode;

                var standardCode = table.Header.Contains("standard code") ? Int32.Parse(row["standard code"]) : IlrBuilder.Defaults.StandardCode;

                if (frameworkCode > 0 && programmeType > 0 && pathwayCode > 0)
                {
                    standardCode = 0;
                }


                StepDefinitionsContext.AddProvider("provider", ukprn);

                CommitmentDataHelper.CreateCommitment(
                    new CommitmentEntity
                    {
                        CommitmentId = long.Parse(IdentifierGenerator.GenerateIdentifier(6, false)),
                        Ukprn = ukprn,
                        Uln = long.Parse(row["ULN"]),
                        AccountId = employer.AccountId.ToString(),
                        StartDate = startDate,
                        EndDate = startDate.AddMonths(12),
                        AgreedCost = decimal.Parse(row["agreed price"]),
                        StandardCode = standardCode,
                        FrameworkCode = frameworkCode,
                        ProgrammeType = programmeType,
                        PathwayCode = pathwayCode,
                        Priority = 1,
                        VersionId = "1",
                        PaymentStatus = (int)CommitmentPaymentStatus.Active,
                        PaymentStatusDescription = CommitmentPaymentStatus.Active.ToString(),
                        Payable = true
                    },
                    EnvironmentVariables);
            }

        }



    }
}
