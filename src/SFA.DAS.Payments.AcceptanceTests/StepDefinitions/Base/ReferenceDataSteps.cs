using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.Entities;
using SFA.DAS.Payments.AcceptanceTests.Enums;
using TechTalk.SpecFlow;

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

        [Given(@"the agreed price is (.*)")]
        public void GivenTheAgreedPriceIs(decimal agreedPrice)
        {
            ReferenceDataContext.AgreedPrice = agreedPrice;
        }

        [Given(@"the apprenticeship funding band maximum for each learner is (.*)")]
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
            var commitments = new Commitment[table.RowCount];

            for (var rowIndex = 0; rowIndex < table.RowCount; rowIndex++)
            {
                commitments[rowIndex] = new Commitment
                {
                    Id = long.Parse(IdentifierGenerator.GenerateIdentifier(6, false)),
                    Priority = table.Rows[rowIndex].ContainsKey("priority") ? int.Parse(table.Rows[rowIndex]["priority"]) : 1,
                    Learner = table.Rows[rowIndex]["ULN"],
                    Employer = table.Rows[rowIndex].ContainsKey("Employer") ? table.Rows[rowIndex]["Employer"] : "employer",
                    Provider = table.Rows[rowIndex].ContainsKey("Provider") ? table.Rows[rowIndex]["Provider"] : "provider",
                    Status = table.Rows[rowIndex].ContainsKey("status")
                                ? GetStatus(table.Rows[rowIndex]["status"])
                                : CommitmentPaymentStatus.Active ,
                    StopPeriod = table.Rows[rowIndex].ContainsKey("stopped on") ? table.Rows[rowIndex]["stopped on"] : string.Empty
                };
            }

            ReferenceDataContext.Commitments = commitments;
        }

        [Given(@"the (.*) has a levy balance > agreed price for all months")]
        public void GivenTheEmployerHasALevyBalanceGreaterThanAgreedPrice(string employerName)
        {
            var employer = new Employer
            {
                Name = employerName,
                AccountId = long.Parse(IdentifierGenerator.GenerateIdentifier(8, false)),
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
                MonthlyAccountBalance = monthlyAccountBalance
            };

            ReferenceDataContext.AddEmployer(employer);
        }

        private CommitmentPaymentStatus GetStatus(string status)
        {
            CommitmentPaymentStatus paymentStatus;

            if (Enum.TryParse(status, true, out paymentStatus))
            {
                return paymentStatus;
            }

            throw new ArgumentException($"Invalid commitment status value: {status}");
        }
    }
}
