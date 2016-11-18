namespace SFA.DAS.Payments.AcceptanceTests.Contexts
{
    public class Commitment
    {
        public long Id { get; set; }
        public int Priority { get; set; }
        public string Learner { get; set; }
        public string Employer { get; set; }
        public string Provider { get; set; }
    }
}