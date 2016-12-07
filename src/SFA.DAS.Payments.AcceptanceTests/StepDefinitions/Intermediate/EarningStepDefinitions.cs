using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ProviderPayments.TestStack.Core;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment;
using SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Base;
using TechTalk.SpecFlow;
using IlrBuilder = SFA.DAS.Payments.AcceptanceTests.Builders.IlrBuilder;


namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Intermediate
{
    [Binding]
    public class EarningStepDefinitions : BaseStepDefinitions
    {
        public EarningStepDefinitions(StepDefinitionsContext earningAndPaymentsContext)
            :base(earningAndPaymentsContext)
        {

        }

        [Given(@"a provider has previously earned (.*) in period R01")]
        public void GivenProviderHasPreviouslyEarnedInPeriod(decimal previousAmount)
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            StepDefinitionsContext.SetDefaultProvider();

            var provider = StepDefinitionsContext.GetDefaultProvider();
            var learner = StepDefinitionsContext.CreateLearner(15000, new DateTime(2017, 08, 01), new DateTime(2018, 07, 01));

            SetupEarningsData(provider, learner);


            var committment = StepDefinitionsContext.ReferenceDataContext.Commitments.First();
            var account = StepDefinitionsContext.ReferenceDataContext.Employers.First(x => x.Name == committment.Employer);

            //Save the previous earning
            EarningsDataHelper.SaveEarnedAmount(provider.Ukprn,
                                                committment.Id,
                                                account.AccountId,
                                                learner.Uln,
                                                "R01",
                                                08,
                                                2016,
                                                1,
                                                previousAmount, environmentVariables);

        }

        [When(@"an earning of (.*) is calculated for period R01")]
        public void AnEarningIsCalculatedForThePeriod(decimal earnedAmount)
        {

            // Setup reference data
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            //save the periodiosed values
            EarningsDataHelper.SavePeriodisedValuesForUkprn(StepDefinitionsContext.GetDefaultProvider().Ukprn,
                                                            new Dictionary<string, decimal> { { "Period_1", earnedAmount } },
                                                            environmentVariables);

            RunMonthEnd(new DateTime(2016, 09, 01));
        }


       


        [Then(@"a payment of (.*) is due")]
        public void ThenAPaymentIsDue(decimal dueAmount)
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            //Get the due amount 
            var earning = PaymentsDueDataHelper.GetPaymentsDueForPeriod(StepDefinitionsContext.GetDefaultProvider().Ukprn,
                                                                        2016,
                                                                        09,
                                                                        environmentVariables)
                                                                        .FirstOrDefault();

            if (dueAmount != 0)
            {
                Assert.IsNotNull(earning, $"Expected earning for the period but nothing found");
                Assert.AreEqual(dueAmount, earning.AmountDue, $"Expected earning of {dueAmount} for period R01 but found {earning.AmountDue}");
            }
            else
            {
                Assert.IsNull(earning, $"There was no expected earning for the period but earnigs data found");

            }
        }


        #region Earnings Distribution


        [When(@"the actual duration of learning is (.*) months")]
        public void WhenTheActualDurationOfLearningIsMonths(int actualCensusMonths)
        {
            ScenarioContext.Current.Add("actualCensusMonths", actualCensusMonths);
        }

      
       
        [When(@"the planned course duration covers (.*) months")]
        public void WhenThePlannedCourseDurationCoversMonths(int months)
        {
            ScenarioContext.Current.Add("plannedCensusMonths", months);
        }

        [When(@"there is an agreed price of (.*)")]
        public void WhenAnAgreedPriceOf(decimal agreedPrice)
        {
            //get months value
            var plannedCensusMonths = ScenarioContext.Current.Get<int>("plannedCensusMonths");

            StepDefinitionsContext.SetDefaultProvider();

            var provider = StepDefinitionsContext.GetDefaultProvider();

            var startDate = new DateTime(2016,08,15);
            var ilrStartDate = startDate.NextCensusDate();

            var plannedEndDate = startDate.AddMonths(plannedCensusMonths);
            DateTime? actualEndDate = null;

            if (ScenarioContext.Current.ContainsKey("actualCensusMonths"))
            {
                var actualCensusMonths = ScenarioContext.Current.Get<int>("actualCensusMonths");
                int variation;
                if (actualCensusMonths < plannedCensusMonths)
                    variation =  (plannedCensusMonths - actualCensusMonths + 1) *-1;
                else
                    variation = actualCensusMonths - plannedCensusMonths - 1;
                
                actualEndDate = plannedEndDate.AddMonths(variation);
            }

            var learner = StepDefinitionsContext.CreateLearner(agreedPrice, startDate, plannedEndDate,actualEndDate);

            
            // Store spec values in context
            StepDefinitionsContext.AddProviderLearner(provider, learner);


            //set a default employer
            StepDefinitionsContext.ReferenceDataContext.SetDefaultEmployer(
                                                new Dictionary<string, decimal> {
                                                    { "All", int.MaxValue }
                                                });

            // Setup reference data
            SetupReferenceData();

            // Process months
           

            SubmitIlr(provider.Ukprn, provider.Learners,
                ilrStartDate.GetAcademicYear(),
                actualEndDate.HasValue? actualEndDate.Value : plannedEndDate,
                new ProcessService(new TestLogger()),
                provider.EarnedByPeriod);
        }

        [Then(@"the monthly earnings is (.*)")]
        public void ThenTheMonthlyEarningsIs(decimal monthlyEarnings)
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            var learner = StepDefinitionsContext.GetDefaultProvider().Learners.First();
            var output = LearnerDataHelper.GetAELearningDelivery(StepDefinitionsContext.GetDefaultProvider().Ukprn,
                                                                learner.Uln,
                                                                learner.LearningDelivery.StartDate,
                                                                learner.LearningDelivery.PlannedEndDate,
                                                                environmentVariables);


            Assert.IsNotNull(output, $"Expected AE Learning Delivery but nothing found");
            Assert.AreEqual(monthlyEarnings, output.MonthlyInstallment, $"Expected monthly installment of {monthlyEarnings} but found {output.MonthlyInstallment}");


        }

        [Then(@"the completion payment is (.*)")]
        public void ThenTheCompletionPaymentIs(decimal completionPayment)
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            var learner = StepDefinitionsContext.GetDefaultProvider().Learners.First();
            var output = LearnerDataHelper.GetAELearningDelivery(StepDefinitionsContext.GetDefaultProvider().Ukprn,
                                                                learner.Uln,
                                                                learner.LearningDelivery.StartDate,
                                                                learner.LearningDelivery.PlannedEndDate,
                                                                environmentVariables);


            Assert.IsNotNull(output, $"Expected AE Learning Delivery but nothing found");
            Assert.AreEqual(completionPayment, output.CompletionPayment, $"Expected completion payment of {completionPayment} but found {output.CompletionPayment}");


        }

        [Then(@"the balancing payment is (.*)")]
        public void ThenTheBalancingPaymentIs(decimal balancingPayment)
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            var learner = StepDefinitionsContext.GetDefaultProvider().Learners.First();
            var endDate = learner.LearningDelivery.ActualEndDate == null ? learner.LearningDelivery.PlannedEndDate : learner.LearningDelivery.ActualEndDate.Value;
            var periodNumber = endDate.GetPeriodNumber();

            var output = EarningsDataHelper.GetBalancingPaymentForUkprn(StepDefinitionsContext.GetDefaultProvider().Ukprn,
                                                                $"Period_{periodNumber}",
                                                                environmentVariables);


            Assert.IsNotNull(output, $"Expected balancing payment value but nothing found");
            Assert.AreEqual(balancingPayment, output, $"Expected balancing payment of {balancingPayment} but found {output}");

        }


        #endregion
    }
}
