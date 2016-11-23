
using NUnit.Framework;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Base;
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
            Assert.IsTrue(validationError.Any(x => x.RuleId == errorCode));
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


        [Given(@"No matching record found in the employer digital account")]
        public void GivenNoMatchingRecordFoundInTheEmployerDigitalAccountForTheStandardCode()
        {
            StepDefinitionsContext.SetDefaultProvider();
            //set a default employer
            StepDefinitionsContext.ReferenceDataContext.SetDefaultEmployer(
                                                new Dictionary<string, decimal> {
                                                    { "All", int.MaxValue }
                                                });
           
        }

        [When(@"an ILR file is submitted with the following data where standard code does not match:")]
        public void WhenAnILRFileIsSubmittedWithTheFollowingDataWhereStandardCodeDoesNotMatch(Table table)
        {
            SubmitIlrDataWithParameters(table);
        }

        [When(@"an ILR file is submitted with the following data where framework code does not match:")]
        public void WhenAnILRFileIsSubmittedWithTheFollowingDataWhereFrameworkCodeDoesNotMatch(Table table)
        {
            SubmitIlrDataWithParameters(table);
        }

        [When(@"an ILR file is submitted with the following data where programme type does not match:")]
        public void WhenAnILRFileIsSubmittedWithTheFollowingDataWhereProgrammeTypeDoesNotMatch(Table table)
        {
            SubmitIlrDataWithParameters(table);
        }

        [When(@"an ILR file is submitted with the following data where pathway code does not match:")]
        public void WhenAnILRFileIsSubmittedWithTheFollowingDataWherePathwayCodeDoesNotMatch(Table table)
        {
            SubmitIlrDataWithParameters(table);
        }

        [When(@"an ILR file is submitted with the following data where negotiated cost does not match:")]
        public void WhenAnILRFileIsSubmittedWithTheFollowingDataWhereNegotiatedCostDoesNotMatch(Table table)
        {
            SubmitIlrDataWithParameters(table,10);
        }

        [When(@"an ILR file is submitted with the following data where learning delivery start does not match:")]
        public void WhenAnILRFileIsSubmittedWithTheFollowingDataWhereLearningDeliveryStartDoesNotMatch(Table table)
        {
            SubmitIlrDataWithParameters(table,null,new DateTime(2017,12,12));
        }

        [When(@"an ILR file is submitted with the following data where there are multiple matching commitments:")]
        public void WhenAnILRFileIsSubmittedWithTheFollowingDataWhereThereAreMultipleMatchingCommitments(Table table)
        {
            SetupContexLearners(table);

            var provider = StepDefinitionsContext.GetDefaultProvider();
            SetupReferenceData();

            var employer = StepDefinitionsContext.ReferenceDataContext.Employers[0];
            
            //duplicate the commitments
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
               IlrBuilder.Defaults.StandardCode,
               IlrBuilder.Defaults.FrameworkCode,
               IlrBuilder.Defaults.ProgrammeType,
               IlrBuilder.Defaults.PathwayCode,
               1, "1", EnvironmentVariables);
            }

            var startDate = StepDefinitionsContext.GetIlrStartDate().NextCensusDate();
            SubmitIlr(provider.Ukprn, provider.Learners,
                startDate.GetAcademicYear(),
                startDate.NextCensusDate(),
                new ProcessService(new TestLogger()),
                provider.EarnedByPeriod);
        }


        private void SubmitIlrDataWithParameters(Table table, 
                                                decimal? negotiatedCost = null,
                                                DateTime? learningDeliveryStartDate = null)
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
               learningDeliveryStartDate.HasValue ? learningDeliveryStartDate.Value : learner.LearningDelivery.StartDate,
               learner.LearningDelivery.PlannedEndDate,
               negotiatedCost.HasValue ? negotiatedCost.Value : learner.LearningDelivery.AgreedPrice,
               IlrBuilder.Defaults.StandardCode,
               IlrBuilder.Defaults.FrameworkCode,
               IlrBuilder.Defaults.ProgrammeType,
               IlrBuilder.Defaults.PathwayCode,
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
