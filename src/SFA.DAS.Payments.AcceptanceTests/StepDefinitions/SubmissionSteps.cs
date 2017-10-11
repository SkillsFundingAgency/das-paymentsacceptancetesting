using System;
using SFA.DAS.Payments.AcceptanceTests.Contexts;
using SFA.DAS.Payments.AcceptanceTests.TableParsers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class SubmissionSteps
    {
        public SubmissionSteps(CommitmentsContext commitmentsContext, SubmissionContext submissionsContext, LookupContext lookupContext)
        {
            CommitmentsContext = commitmentsContext;
            SubmissionsContext = submissionsContext;
            LookupContext = lookupContext;
        }
        public SubmissionContext SubmissionsContext { get; }
        public CommitmentsContext CommitmentsContext { get; }
        public LookupContext LookupContext { get; }


        [When("an ILR file is submitted with the following data:")]
        [When(@"an ILR file is submitted every month with the following data:")] //Duplicate?
        public void WhenAnIlrFileIsSubmitted(Table ilrDetails)
        {
            IlrTableParser.ParseIlrTableIntoContext(SubmissionsContext, ilrDetails);
        }

        [When("the providers submit the following ILR files:")] //Duplicate?
        public void WhenMultipleIlrFilesAreSubmitted(Table ilrDetails)
        {
            IlrTableParser.ParseIlrTableIntoContext(SubmissionsContext, ilrDetails);
        }

        [When(@"an ILR file is submitted on (.*) with the following data:")] // what is the purpose of the dates?
        public void WhenIlrSubmittedOnSpecificDate(string specSumissionDate, Table ilrDetails)
        {
            IlrTableParser.ParseIlrTableIntoContext(SubmissionsContext, ilrDetails);
        }

        [When("an ILR file is submitted for the first time on (.*) with the following data:")]
        public void WhenIlrFirstSubmittedOnSpecificDate(string specSumissionDate, Table ilrDetails)
        {
            IlrTableParser.ParseIlrTableIntoContext(SubmissionsContext, ilrDetails);

            if (!DateTime.TryParse(specSumissionDate, out var firstSubmissionDate))
            {
                throw new ArgumentException($"{specSumissionDate} is not a valid date");
            }
            SubmissionsContext.FirstSubmissionDate = firstSubmissionDate;
        }

        [When("the Contract type in the ILR is:")]
        public void WhenTheContractTypeInTheIlrIs(Table contractTypes)
        {
            ContractTypeTableParser.ParseContractTypesIntoContext(SubmissionsContext, contractTypes);
        }

        [When("the employment status in the ILR is:")]
        public void WhenTheEmploymentStatusInTheIlrIs(Table employmentStatus)
        {
            EmploymentStatusTableParser.ParseEmploymentStatusIntoContext(SubmissionsContext, employmentStatus);
        }

        [When(@"the learning support status of the ILR is:")]
        public void WhenTheLearningSupportStatusOfTheIlrIs(Table learningSupportStatus)
        {
            LearningSupportTableParser.ParseLearningSupportIntoContext(SubmissionsContext, learningSupportStatus);
        }

       

    }
}
