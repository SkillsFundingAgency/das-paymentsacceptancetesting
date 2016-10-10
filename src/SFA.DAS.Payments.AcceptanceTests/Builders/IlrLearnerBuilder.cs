using System.Collections.Generic;
using IlrGenerator;
using ProviderPayments.TestStack.Core.Domain;

namespace SFA.DAS.Payments.AcceptanceTests.Builders
{
    public class IlrLearnerBuilder : IlrBuilder
    {
        internal readonly Learner Learner;

        internal IlrLearnerBuilder(IlrBuilder parentBuilder)
            : base(parentBuilder.Submission)
        {
            Learner = new Learner
            {
                Uln = Defaults.FirstUln + parentBuilder.Submission.Learners.Length,
                LearningDeliveries = new LearningDelivery[0]
            };
            var learners = new List<Learner>(parentBuilder.Submission.Learners) { Learner };
            parentBuilder.Submission.Learners = learners.ToArray();
        }
        protected IlrLearnerBuilder(IlrSubmission submission, Learner learner) 
            : base(submission)
        {
            Learner = learner;
        }

        public IlrLearnerBuilder WithUln(long uln)
        {
            Learner.Uln = uln;
            return this;
        }
        public IlrLearningDeliveryBuilder WithLearningDelivery()
        {
            return new IlrLearningDeliveryBuilder(this);
        }
    }
}