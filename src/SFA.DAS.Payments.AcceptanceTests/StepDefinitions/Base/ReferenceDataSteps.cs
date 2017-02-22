using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers.Entities;
using SFA.DAS.Payments.AcceptanceTests.Entities;
using SFA.DAS.Payments.AcceptanceTests.Enums;
using SFA.DAS.Payments.AcceptanceTests.ExecutionEnvironment;
using TechTalk.SpecFlow;
using IlrBuilder = SFA.DAS.Payments.AcceptanceTests.Builders.IlrBuilder;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions.Base
{
    [Binding]
    public class ReferenceDataSteps
    {
        public ReferenceDataSteps(ReferenceDataContext referenceDataContext)
        {
            ReferenceDataContext = referenceDataContext;
        }

        public ReferenceDataContext ReferenceDataContext { get; set; }

        [Given(@"The learner is programme only DAS")]
        public void GivenTheLearnerIsProgrammeOnlyDas()
        {
            ReferenceDataContext.LearnerType = LearnerType.ProgrammeOnlyDas;
        }

        [Given(@"The learner is programme only non-DAS")]
        public void GivenTheLearnerIsProgrammeOnlyNonDas()
        {
            ReferenceDataContext.LearnerType = LearnerType.ProgrammeOnlyNonDas;
        }

        [Given(@"Two learners are programme only DAS")]
        public void GivenTwoLearnersAreProgrammeOnlyDAS()
        {
            ReferenceDataContext.LearnerType = LearnerType.ProgrammeOnlyDas;
        }


        [Given(@"the agreed price is (.*)")]
        public void GivenTheAgreedPriceIs(decimal agreedPrice)
        {
            ReferenceDataContext.AgreedPrice = agreedPrice;
        }

        [Given(@"the apprenticeship funding band maximum for each learner is (.*)")]
        public void GivenTheApprenticeshipFundingBandMaximumForEachLearnerIs(int fundingMaximum)
        {
            ReferenceDataContext.FundingMaximum = fundingMaximum;
        }

        [Given(@"the apprenticeship funding band maximum is (.*)")]
        public void GivenTheApprenticeshipFundingBandMaximumIs(int fundingMaximum)
        {
            ReferenceDataContext.FundingMaximum = fundingMaximum;
        }

     
        [Given(@"levy balance = (.*) for all months")]
        public void GivenLevyBalanceAgreedPrice(int levyBalance)
        {
            ReferenceDataContext.SetDefaultEmployer(new Dictionary<string, decimal> { { "All", levyBalance } });
        }

        [Given(@"levy balance > agreed price for all months")]
        public void GivenLevyBalanceAgreedPrice()
        {
            ReferenceDataContext.SetDefaultEmployer(new Dictionary<string, decimal> { { "All", int.MaxValue } });
        }

        [Given(@"the employer's levy balance is:")]
        public void GivenTheMonthlyLevyBalanceIs(Table table)
        {
            var monthlyAccountBalance = new Dictionary<string, decimal>();

            for (var colIndex = 0; colIndex < table.Header.Count; colIndex++)
            {
                var period = table.Header.ElementAt(colIndex);
              
                var balance = decimal.Parse(table.Rows[0][period]);

                monthlyAccountBalance.Add(period, balance);
            }

            ReferenceDataContext.SetDefaultEmployer(monthlyAccountBalance);
        }

        [Given(@"the following commitments exist:")]
        public void GivenTheFollowingCommitments(Table table)
        {
            BuildContextCommitments(table);
        }

        [Given(@"the (.*) has a levy balance > agreed price for all months")]
        public void GivenTheEmployerHasALevyBalanceGreaterThanAgreedPrice(string employerName)
        {
            var employer = new Employer
            {
                Name = employerName,
                AccountId = long.Parse(IdentifierGenerator.GenerateIdentifier(8, false)),
                LearnersType = LearnerType.ProgrammeOnlyDas,
                MonthlyAccountBalance = new Dictionary<string, decimal> { { "All", int.MaxValue } }
            };

            ReferenceDataContext.AddEmployer(employer);
        }

        [Given(@"the (.*) has a levy balance of:")]
        public void GivenTheEmployerHasALevyBalanceOf(string employerName, Table table)
        {
            var monthlyAccountBalance = new Dictionary<string, decimal>();

            for (var colIndex = 0; colIndex < table.Header.Count; colIndex++)
            {
                var period = table.Header.ElementAt(colIndex);
                var balance = decimal.Parse(table.Rows[0][period]);

                monthlyAccountBalance.Add(period, balance);
            }

            var employer = new Employer
            {
                Name = employerName,
                AccountId = long.Parse(IdentifierGenerator.GenerateIdentifier(8, false)),
                LearnersType = LearnerType.ProgrammeOnlyDas,
                MonthlyAccountBalance = monthlyAccountBalance
            };

            ReferenceDataContext.AddEmployer(employer);
        }

        [Given(@"the following commitments exist on (.*):")]
        public void WhenTheFollowingCommitmentsExistOnADate(string date, Table table)
        {
            BuildContextCommitments(table);
        }

        [Given(@"the learner changes employers")]
        public void WhenALearnerChangesFromOneDasEmployerToAnotherDasEmployer(Table table)
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            foreach (var row in table.Rows)
            {
                foreach (var key in row.Keys)
                {
                    var entity = new SpecFlowEntity
                    {
                        Name = "employer",
                        Field = key,
                        Type = "column"
                    };

                    SpecFlowEntitiesDataHelper.AddEntityRow(entity, environmentVariables);
                }
            }

        }

        private CommitmentPaymentStatus GetStatus(string status)
        {
            CommitmentPaymentStatus paymentStatus;

            if (status.Equals("withdrawn",StringComparison.InvariantCultureIgnoreCase))
            {
                return CommitmentPaymentStatus.Cancelled;
            }

            if (Enum.TryParse(status, true, out paymentStatus))
            {
                return paymentStatus;
            }

            throw new ArgumentException($"Invalid commitment status value: {status}");
        }

        private void BuildContextCommitments(Table table)
        {
            var environmentVariables = EnvironmentVariablesFactory.GetEnvironmentVariables();

            foreach (var row in table.Rows)
            {
                foreach (var key in row.Keys)
                {
                    var entity = new SpecFlowEntity
                    {
                        Name = "commitment",
                        Field = key,
                        Type = "column"
                    };

                    SpecFlowEntitiesDataHelper.AddEntityRow(entity, environmentVariables);
                }
            }
        }
    }
}
