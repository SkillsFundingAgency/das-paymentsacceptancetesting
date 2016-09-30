using SFA.DAS.Payments.AcceptanceTests.Contexts;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class ReferenceDataSteps
    {
        public ReferenceDataSteps(ReferenceDataContext referenceDataContext)
        {
            ReferenceDataContext = referenceDataContext;
        }

        public ReferenceDataContext ReferenceDataContext { get; set; }

        [Given(@"The learner is normal DAS")]
        public void GivenTheLearnerIsNormalDas()
        {
            ReferenceDataContext.LearnerType = LearnerType.NormalDas;
        }

        [Given(@"the agreed price is (.*)")]
        public void GivenTheAgreedPriceIs(decimal agreedPrice)
        {
            ReferenceDataContext.AgreedPrice = agreedPrice;
        }

        [Given(@"the apprenticeship funding band maximum is (.*)")]
        public void GivenTheApprenticeshipFundingBandMaximumIs(int fundingMaximum)
        {
            ReferenceDataContext.FundingMaximum = fundingMaximum;
        }

        [Given(@"levy balance > agreed price")]
        public void GivenLevyBalanceAgreedPrice()
        {
            ReferenceDataContext.AccountBalance = int.MaxValue;
        }
    }
}
