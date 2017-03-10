using SFA.DAS.Payments.AcceptanceTests.Refactoring.Contexts;
using SFA.DAS.Payments.AcceptanceTests.Refactoring.TableParsers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Refactoring.StepDefinitions
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

        [When("an ILR file is submitted for the first time on (.*) with the following data:")]
        [When(@"an ILR file is submitted on (.*) with the following data:")] // what is the purpose of the dates?
        public void WhenIlrSubmittedOnSpecificDate(string specSumissionDate, Table ilrDetails)
        {
            IlrTableParser.ParseIlrTableIntoContext(SubmissionsContext, ilrDetails);
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
    }
}
