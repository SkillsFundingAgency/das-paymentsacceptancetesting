using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.DataHelpers;
using SFA.DAS.Payments.AcceptanceTests.Entities;
using SFA.DAS.Payments.AcceptanceTests.Enums;
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
        public void GivenTwoLearnersAreProgrammeOnlyDas()
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
            for (var rowIndex = 0; rowIndex < table.RowCount; rowIndex++)
            {
                var employerName = table.Rows[rowIndex]["Employer"];
                var type = table.Rows[rowIndex]["Type"] == "DAS" ? LearnerType.ProgrammeOnlyDas : LearnerType.ProgrammeOnlyNonDas;

                if (!ReferenceDataContext.Employers.Any(x => x.Name.Equals(employerName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    var employer = new Employer
                    {
                        Name = employerName,
                        AccountId = long.Parse(IdentifierGenerator.GenerateIdentifier(8, false)),
                        LearnersType = type,
                        MonthlyAccountBalance = new Dictionary<string, decimal>()
                    };

                    ReferenceDataContext.AddEmployer(employer);
                }
                else
                {
                    ReferenceDataContext.SetEmployerLearnersType(employerName, type);
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
            var commitments = new List<Commitment>();

            for (var rowIndex = 0; rowIndex < table.RowCount; rowIndex++)
            {
                var row = table.Rows[rowIndex];

                var frameworkCode = table.Header.Contains("framework code")
                    ? int.Parse(row["framework code"])
                    : IlrBuilder.Defaults.FrameworkCode;
                var programmeType = table.Header.Contains("programme type")
                    ? int.Parse(row["programme type"])
                    : IlrBuilder.Defaults.ProgrammeType;
                var pathwayCode = table.Header.Contains("pathway code")
                    ? int.Parse(row["pathway code"])
                    : IlrBuilder.Defaults.PathwayCode;

                var standardCode = table.Header.Contains("standard code")
                    ? int.Parse(row["standard code"])
                    : IlrBuilder.Defaults.StandardCode;

                if (frameworkCode > 0 && programmeType > 0 && pathwayCode > 0)
                {
                    standardCode = 0;
                }

                var commitment = new Commitment
                {
                    Id = row.ContainsKey("commitment Id")
                        ? long.Parse(row["commitment Id"])
                        : long.Parse(IdentifierGenerator.GenerateIdentifier(6, false)),
                    VersionId = row.ContainsKey("version Id")
                        ? long.Parse(row["version Id"])
                        : 1,
                    Priority = row.ContainsKey("priority")
                        ? int.Parse(row["priority"])
                        : 1,
                    Learner = row["ULN"],
                    Employer = row.ContainsKey("Employer")
                        ? row["Employer"]
                        : "employer",
                    Provider = row.ContainsKey("Provider")
                        ? row["Provider"]
                        : "provider",
                    Status = row.ContainsKey("status")
                        ? GetStatus(row["status"])
                        : CommitmentPaymentStatus.Active,
                    StartDate = row.ContainsKey("start date")
                        ? DateTime.Parse(row["start date"])
                        : (DateTime?)null,
                    EndDate = row.ContainsKey("end date")
                        ? DateTime.Parse(row["end date"])
                        : (DateTime?)null,
                    AgreedPrice = row.ContainsKey("agreed price")
                        ? decimal.Parse(row["agreed price"])
                        : (decimal?)null,

                    EffectiveFrom = row.ContainsKey("effective from")
                        ? DateTime.Parse(row["effective from"])
                        : (DateTime?)null,
                    EffectiveTo = row.ContainsKey("effective to") && !string.IsNullOrWhiteSpace(row["effective to"])
                        ? DateTime.Parse(row["effective to"])
                        : (DateTime?)null,
                    StandardCode = standardCode ,
                    FrameworkCode = frameworkCode,
                    PathwayCode = pathwayCode,
                    ProgrammeType = programmeType

            };

                commitments.Add(commitment);
            }

            ReferenceDataContext.Commitments = commitments.ToArray();
        }
    }
}
