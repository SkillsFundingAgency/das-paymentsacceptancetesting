using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.ExecutionManagers;
using SFA.DAS.Payments.AcceptanceTests.ReferenceDataModels;
using TechTalk.SpecFlow;
using SFA.DAS.Payments.AcceptanceTests.TableParsers;
using SFA.DAS.Payments.AcceptanceTests.Assertions;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class EmployerAccountSteps
    {
        public EmployerAccountSteps(EmployerAccountContext employerAccountContext,
                                    EarningsAndPaymentsContext earningsAndPaymentsContext,
                                    SubmissionContext submissionContext)
        {
            EmployerAccountContext = employerAccountContext;
            EarningsAndPaymentsContext = earningsAndPaymentsContext;
            SubmissionContext = submissionContext;

        }
        public EmployerAccountContext EmployerAccountContext { get; }

        public EarningsAndPaymentsContext EarningsAndPaymentsContext { get; }

        public SubmissionContext SubmissionContext { get; set; }

        [Given("levy balance > agreed price for all months")]
        public void GivenUnnamedEmployersLevyBalanceIsMoreThanPrice()
        {
            GivenNamedEmployersLevyBalanceIsMoreThanPrice(Defaults.EmployerAccountId.ToString());
        }

        [Given("the employer (.*) has a levy balance > agreed price for all months")]
        public void GivenNamedEmployersLevyBalanceIsMoreThanPrice(string employerNumber)
        {
            int id;
            if (!int.TryParse(employerNumber, out id))
            {
                throw new ArgumentException($"Employer number '{employerNumber}' is not a valid number");
            }

            AddOrUpdateEmployerAccount(id, int.MaxValue);
        }

        [Given("levy balance = 0 for all months")]
        public void GivenLevyBalanceIsZero()
        {
            AddOrUpdateEmployerAccount(Defaults.EmployerAccountId, 0m);
        }

        [Given("the employer's levy balance is:")]
        public void GivenUnnamedEmployersLevyBalanceIsDifferentPerMonth(Table employerBalancesTable)
        {
            GivenNamedEmployersLevyBalanceIsDifferentPerMonth(Defaults.EmployerAccountId.ToString(), employerBalancesTable);
        }

        [Given("the employer (.*) has a levy balance of:")]
        public void GivenNamedEmployersLevyBalanceIsDifferentPerMonth(string employerNumber, Table employerBalancesTable)
        {
            int id;
            if (!int.TryParse(employerNumber, out id))
            {
                throw new ArgumentException($"Employer number '{employerNumber}' is not a valid number");
            }
            var periodBalances = LevyBalanceTableParser.ParseLevyAccountBalanceTable(employerBalancesTable,id);
            AddOrUpdateEmployerAccount(id, 0m, periodBalances);
        }

        [Given("the learner changes employers")]
        public void GivenTheLearnerChangesEmployers(Table employmentDates)
        {
            foreach (var row in employmentDates.Rows)
            {
                if (!row[0].StartsWith("employer"))
                {
                    continue;
                }

                var employerAccountId = int.Parse(row[0].Substring("employer ".Length));
                var isLevyPayer = row[1].Equals("DAS", StringComparison.CurrentCultureIgnoreCase);

                var account = EmployerAccountContext.EmployerAccounts.SingleOrDefault(a => a.Id == employerAccountId);
                if (account == null)
                {
                    account = AddOrUpdateEmployerAccount(employerAccountId, 0, null, isLevyPayer);
                }
                account.IsLevyPayer = isLevyPayer;
            }
        }

       

        [Then(@"the net effect on employer's levy balance after each period end is:")]
        public void ThenTheEmployerSLevyBalanceIs(Table table)
        {
           
            var periodBalances = LevyBalanceTableParser.ParseLevyAccountBalanceTable(table,Defaults.EmployerAccountId);

            var breakdown = new  EarningsAndPaymentsBreakdown
            {
                EmployerLevyTransactions = periodBalances
            };

            EarningsAndPaymentsContext.OverallEarningsAndPayments.Add(breakdown);
            PaymentsAndEarningsAssertions.AssertPaymentsAndEarningsResults(EarningsAndPaymentsContext, SubmissionContext, EmployerAccountContext);
        }


        private EmployerAccountReferenceData AddOrUpdateEmployerAccount(int id, decimal balance, List<EmployerAccountPeriodValue> periodBalances = null, bool isLevyPayer = true)
        {
            var account = EmployerAccountContext.EmployerAccounts.SingleOrDefault(a => a.Id == id);
            if (account == null)
            {
                account = new EmployerAccountReferenceData
                {
                    Id = id
                };
                EmployerAccountContext.EmployerAccounts.Add(account);
            }

            account.Balance = balance;
            account.PeriodBalances = periodBalances ?? new List<EmployerAccountPeriodValue>();
            account.IsLevyPayer = isLevyPayer;

            EmployerAccountManager.AddOrUpdateAccount(account);

            return account;
        }
    }
}
