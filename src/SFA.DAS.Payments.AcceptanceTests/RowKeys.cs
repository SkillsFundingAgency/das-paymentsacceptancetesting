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

        public const string OnProgramPayment = "On-program";
        public const string CompletionPayment = "Completion";
        public const string BalancingPayment = "Balancing";

        public const string DataLockMatchingCommitment = "Matching commitment";
    }
}
