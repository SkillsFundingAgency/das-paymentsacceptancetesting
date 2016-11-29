
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

        [When(@"monthly payment process runs for the following ILR data:")]
        public void WhenMonthlyPaymentProcessRunsForTheFollowingILRData(Table table)
        {
            // Setup reference data
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            SetupContexLearners(table);

            var provider = StepDefinitionsContext.GetDefaultProvider();
            provider.Ukprn = long.Parse( table.Rows[0]["UKPRN"]);
            var learner = provider.Learners[0];
           
          
            var startDate = StepDefinitionsContext.GetIlrStartDate().NextCensusDate();

           
            LearnerDataHelper.SaveFileDetails(provider.Ukprn,
                                             EnvironmentVariables);

            //Save Learning Provider
            LearnerDataHelper.SaveLearningProvider(provider.Ukprn,
                                                    EnvironmentVariables);

            //Save the Learner
            LearnerDataHelper.SaveLearner(provider.Ukprn,
                                        learner.Uln,
                                        EnvironmentVariables);

            //save Learner delivery
            LearnerDataHelper.SaveLearningDelivery(provider.Ukprn,
                                                    learner.LearningDelivery.StartDate,
                                                    learner.LearningDelivery.PlannedEndDate,
                                                    learner.LearningDelivery.StandardCode,
                                                    learner.LearningDelivery.ProgrammeType,
                                                    EnvironmentVariables);

            //save learning delivery FAM
            LearnerDataHelper.SaveLearningDeliveryFAM(provider.Ukprn, EnvironmentVariables);


            LearnerDataHelper.SaveTrailblazerApprenticeshipFinancialRecord(provider.Ukprn,
                                                                            1,
                                                                              learner.LearningDelivery.StandardCode > 0 ? 
                                                                              learner.LearningDelivery.AgreedPrice * 0.8m :
                                                                              learner.LearningDelivery.AgreedPrice,
                                                                           EnvironmentVariables);

            //save Trailblazer
            if (learner.LearningDelivery.StandardCode > 0)
            {
                
                LearnerDataHelper.SaveTrailblazerApprenticeshipFinancialRecord(provider.Ukprn,
                                                                            2,
                                                                            learner.LearningDelivery.AgreedPrice * 0.2m,
                                                                            EnvironmentVariables);
            }
            //save the learning deliver values
            EarningsDataHelper.SaveLearningDeliveryValuesForUkprn(provider.Ukprn,
                                                                    learner.Uln,
                                                                    learner.LearningDelivery.AgreedPrice,
                                                                    learner.LearningDelivery.StartDate,
                                                                    learner.LearningDelivery.StartDate.AddMonths(12),
                                                                    (learner.LearningDelivery.AgreedPrice * 0.8m)/12,
                                                                    learner.LearningDelivery.AgreedPrice - ((learner.LearningDelivery.AgreedPrice * 0.8m) / 12),
                                                                    learner.LearningDelivery.StandardCode,
                                                                    learner.LearningDelivery.ProgrammeType,
                                                                    EnvironmentVariables);

            //var committment = StepDefinitionsContext.ReferenceDataContext.Commitments.First();
            //var account = StepDefinitionsContext.ReferenceDataContext.Employers.First(x => x.Name == committment.Employer);

            //Delete the commitment

            //var committment = StepDefinitionsContext.ReferenceDataContext.Commitments.First();
            //CommitmentDataHelper.DeleteCommmitment(provider.Ukprn, learner.Uln,EnvironmentVariables);
            RunMonthEnd(provider.Learners[0].LearningDelivery.StartDate);
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
                                long.Parse(IdentifierGenerator.GenerateIdentifier(6, false)),
                                ukprn,
                                long.Parse(row["ULN"]),
                                employer.AccountId.ToString(),
                                startDate,
                                startDate.AddMonths(12),
                                decimal.Parse(row["agreed price"]),
                                standardCode,
                                frameworkCode,
                                 programmeType,
                                pathwayCode,
                                1, "1", EnvironmentVariables);
            }

        }



    }
}
