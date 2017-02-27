namespace SFA.DAS.Payments.AcceptanceTests
{
    public static class RowKeys
    {
        public const string Earnings = "Provider Earned Total";

        public const string DefaultLevyPayment = "Levy account debited";
        public const string LevyPayment = " Levy account debited";

        public const string DefaultCoFinanceEmployerPayment = "Payment due from Employer";
        public const string CoFinanceEmployerPayment = "Payment due from ";

        public const string CoFinanceGovernmentPaymentForLevyContracts = "SFA Levy co-funding budget";
        public const string CoFinanceGovernmentPaymentForNonLevyContracts = "SFA non-Levy co-funding budget";
        public const string SfaLevyAdditionalPaymentsBudget = "SFA Levy additional payments budget";
        public const string SfaNonLevyAdditionalPaymentsBudget = "SFA non-Levy additional payments budget";


        public const string OnProgramPayment = "On-program";
        public const string CompletionPayment = "Completion";
        public const string BalancingPayment = "Balancing";
        public const string DefaultEmployerIncentive = "Employer {0} 16-18 incentive";
        public const string ProviderIncentive = "Provider 16-18 incentive";


        public const string FrameworkUpliftOnProgramme = "Framework uplift on-program";
        public const string FrameworkUpliftCompletion = "Framework uplift completion";
        public const string FrameworkUpliftBalancing = "Framework uplift balancing";
        public const string ProviderDisadvantageUplift = " Provider disadvantage uplift";


        public const string DataLockMatchingCommitment = "Matching commitment";
        public const string DataLockMatchingPrice = "Matching price";
    }
}
