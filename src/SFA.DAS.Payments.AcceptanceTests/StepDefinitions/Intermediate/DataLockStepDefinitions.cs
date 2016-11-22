
using NUnit.Framework;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using IlrBuilder = SFA.DAS.Payments.AcceptanceTests.Builders.IlrBuilder;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Intermediate
{
    [Binding]
    public class DataLockStepDefinitions    : BaseStepDefinitions
    {
        public DataLockStepDefinitions(StepDefinitionsContext context)
            :base(context)
        {

        }

        [Given(@"There is no employer data in the committments for UKPRN (.*)")]
        public void GivenThereIsNoEmployerDataInTheCommittmentsForUKPRN(long ukprn)
        {
            StepDefinitionsContext.AddProvider("provider", ukprn);
        }


        [When(@"an ILR file is submitted with the following data for UKPRN (.*):")]
        public void WhenAnILRFileIsSubmittedWithTheFollowingDataForUKPRN(long ukprn, Table table)
        {
           
            SetupContexLearners(table);
                      
            var provider = StepDefinitionsContext.GetDefaultProvider();

            var startDate = StepDefinitionsContext.GetIlrStartDate().NextCensusDate();
            
            SubmitIlr(ukprn, provider.Learners,
                startDate.GetAcademicYear(),
                startDate.NextCensusDate(),
                new ProcessService(new TestLogger()),
                provider.EarnedByPeriod);
        }


        [Then(@"a datalock error (.*) is produced")]
        public void ThenADatalockErrorOfDLOCK_WillBeProduced(string errorCode)
        {
            var provider = StepDefinitionsContext.GetDefaultProvider();
            var startDate = StepDefinitionsContext.GetIlrStartDate().NextCensusDate();

            var validationError = ValidationErrorsDataHelper.GetValidationErrors(provider.Ukprn,EnvironmentVariables);

            Assert.IsNotNull(validationError, "There is no validation error entity present");
            Assert.IsNotNull(validationError.Any(x => x.RuleId == errorCode));
        }

        [Given(@"No matching record found in the employer digital account for the ULN (.*)")]
        public void GivenNoMatchingRecordFoundInTheEmployerDigitalAccountForTheULN(long uln)
        {
            
            StepDefinitionsContext.SetDefaultProvider();
            //set a default employer
            StepDefinitionsContext.ReferenceDataContext.SetDefaultEmployer(
                                                new Dictionary<string, decimal> {
                                                    { "All", int.MaxValue }
                                                });
          
        }

        [When(@"an ILR file is submitted with the following data for the ULN (.*):")]
        public void WhenAnILRFileIsSubmittedWithTheFollowingDataForTheULN(long uln, Table table)
        {
           

            SetupContexLearners(table);

            var provider = StepDefinitionsContext.GetDefaultProvider();
            //set the uln to a dummy number passed in
            provider.Learners.ToList().ForEach(x => x.Uln = uln);

            SetupReferenceData();
            
            var startDate = StepDefinitionsContext.GetIlrStartDate().NextCensusDate();

            //flip the ULN's again so not found for processing
            provider.Learners.ToList().ForEach(x => x.Uln = uln + 99);

            SubmitIlr(provider.Ukprn, provider.Learners,
                startDate.GetAcademicYear(),
                startDate.NextCensusDate(),
                new ProcessService(new TestLogger()),
                provider.EarnedByPeriod);
        }


        [Given(@"No matching record found in the employer digital account for the standard code (.*)")]
        public void GivenNoMatchingRecordFoundInTheEmployerDigitalAccountForTheStandardCode(long standardCode)
        {
            StepDefinitionsContext.SetDefaultProvider();
            //set a default employer
            StepDefinitionsContext.ReferenceDataContext.SetDefaultEmployer(
                                                new Dictionary<string, decimal> {
                                                    { "All", int.MaxValue }
                                                });
           
        }

        [When(@"an ILR file is submitted with the following data for the standard code (.*):")]
        public void WhenAnILRFileIsSubmittedWithTheFollowingDataForTheStandardCode(long standardCode, Table table)
        {
            SetupContexLearners(table);
            
            var provider = StepDefinitionsContext.GetDefaultProvider();
            var employer = StepDefinitionsContext.ReferenceDataContext.Employers[0];
            AccountDataHelper.CreateAccount(employer.AccountId, employer.AccountId.ToString(), 0.00m, EnvironmentVariables);

            foreach (var learner in provider.Learners)
            {
                CommitmentDataHelper.CreateCommitment(
               long.Parse(IdentifierGenerator.GenerateIdentifier(6, false)),
               provider.Ukprn,
               learner.Uln,
               employer.AccountId.ToString(),
               learner.LearningDelivery.StartDate,
               learner.LearningDelivery.PlannedEndDate,
               learner.LearningDelivery.AgreedPrice,
               standardCode,    
               IlrBuilder.Defaults.FrameworkCode,
               IlrBuilder.Defaults.ProgrammeType,
               IlrBuilder.Defaults.PathwayCode,
               1,"1", EnvironmentVariables);
            }

            var startDate = StepDefinitionsContext.GetIlrStartDate().NextCensusDate();
            SubmitIlr(provider.Ukprn, provider.Learners,
                startDate.GetAcademicYear(),
                startDate.NextCensusDate(),
                new ProcessService(new TestLogger()),
                provider.EarnedByPeriod);

        }




        [Given(@"No matching record found in the employer digital account for various parameters")]
        public void GivenNoMatchingRecordFoundInTheEmployerDigitalAccountForVariousParameters()
        {
            StepDefinitionsContext.SetDefaultProvider();
            //set a default employer
            StepDefinitionsContext.ReferenceDataContext.SetDefaultEmployer(
                                                new Dictionary<string, decimal> {
                                                    { "All", int.MaxValue }
                                                });

        }

        [When(@"an ILR file is submitted with the following data for the (.*), (.*),(.*),(.*) and (.*):")]
        public void WhenAnILRFileIsSubmittedWithTheFollowingDataForTheAnd(long standardCode, 
                                                                            int frameworkCode, 
                                                                            int programmeType, 
                                                                            int pathwayCode,
                                                                            decimal negotiatedCost, 
                                                                            Table table)
        {
            SetupContexLearners(table);

            var provider = StepDefinitionsContext.GetDefaultProvider();
            var employer = StepDefinitionsContext.ReferenceDataContext.Employers[0];
            AccountDataHelper.CreateAccount(employer.AccountId, employer.AccountId.ToString(), negotiatedCost, EnvironmentVariables);

            foreach (var learner in provider.Learners)
            {
                CommitmentDataHelper.CreateCommitment(
               long.Parse(IdentifierGenerator.GenerateIdentifier(6, false)),
               provider.Ukprn,
               learner.Uln,
               employer.AccountId.ToString(),
               learner.LearningDelivery.StartDate,
               learner.LearningDelivery.PlannedEndDate,
               learner.LearningDelivery.AgreedPrice,
               standardCode,
               frameworkCode,
               programmeType,
               pathwayCode,
               1, "1", EnvironmentVariables);
            }

            var startDate = StepDefinitionsContext.GetIlrStartDate().NextCensusDate();
            SubmitIlr(provider.Ukprn, provider.Learners,
                startDate.GetAcademicYear(),
                startDate.NextCensusDate(),
                new ProcessService(new TestLogger()),
                provider.EarnedByPeriod);
        }

    }
}
