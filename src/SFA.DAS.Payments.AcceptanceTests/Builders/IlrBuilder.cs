using IlrGenerator;

namespace SFA.DAS.Payments.AcceptanceTests.Builders
{
    public class IlrBuilder
    {
        internal readonly IlrSubmission Submission;

        static IlrBuilder()
        {
            Defaults = new IlrSubmissionDefaults();
        }
        private IlrBuilder()
        {
            Submission = new IlrSubmission
            {
                Ukprn = Defaults.Ukprn,
                Learners = new Learner[0]
            };
        }

        protected IlrBuilder(IlrSubmission submission)
        {
            Submission = submission;
        }

        public static IlrSubmissionDefaults Defaults { get; }

        public static IlrBuilder CreateAIlrSubmission()
        {
            return new IlrBuilder();
        }

        public IlrBuilder WithUkprn(long ukprn)
        {
            Submission.Ukprn = ukprn;
            return this;
        }
        public IlrLearnerBuilder WithALearner()
        {
            return new IlrLearnerBuilder(this);
        }

        public IlrLearnersBuilder WithMultipleLearners()
        {
            return new IlrLearnersBuilder(this);
        }

        public static implicit operator IlrSubmission(IlrBuilder builder)
        {
            return builder.Submission;
        }
    }
}
