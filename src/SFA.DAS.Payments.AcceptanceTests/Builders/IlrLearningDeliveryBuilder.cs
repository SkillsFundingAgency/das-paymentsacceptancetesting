using System;
using System.Collections.Generic;
using IlrGenerator;

namespace SFA.DAS.Payments.AcceptanceTests.Builders
{
    public class IlrLearningDeliveryBuilder : IlrLearnerBuilder
    {
        internal readonly LearningDelivery Delivery;

        internal IlrLearningDeliveryBuilder(IlrLearnerBuilder parentBuilder)
            : base(parentBuilder.Submission, parentBuilder.Learner)
        {
            var tnp1 = Defaults.AgreedPrice * 0.8m;
            var tnp2 = Defaults.AgreedPrice - tnp1;

            Delivery = new LearningDelivery
            {
                StandardCode = Defaults.StandardCode,
                FrameworkCode = Defaults.FrameworkCode,
                ProgrammeType = Defaults.ProgrammeType,
                PathwayCode = Defaults.PathwayCode,
                ActualStartDate = Defaults.ActualStartDate,
                PlannedEndDate = Defaults.PlannedEndDate,
                ActualEndDate = Defaults.ActualEndDate,
                TrainingCost = tnp1,
                EndpointAssesmentCost = tnp2,
                ActFamCodeValue = Defaults.ActFamCodeValue
            };
            var deliveries = new List<LearningDelivery>(parentBuilder.Learner.LearningDeliveries) { Delivery };
            parentBuilder.Learner.LearningDeliveries = deliveries.ToArray();
        }

        public IlrLearningDeliveryBuilder WithStandardCode(long standardCode)
        {
            Delivery.StandardCode = standardCode;
            return this;
        }
        public IlrLearningDeliveryBuilder WithFrameworkCode(int frameworkCode)
        {
            Delivery.FrameworkCode = frameworkCode;
            return this;
        }
        public IlrLearningDeliveryBuilder WithProgrammeType(int programmeType)
        {
            Delivery.ProgrammeType = programmeType;
            return this;
        }
        public IlrLearningDeliveryBuilder WithPathwayCode(int pathwayCode)
        {
            Delivery.PathwayCode = pathwayCode;
            return this;
        }

        public IlrLearningDeliveryBuilder WithActualStartDate(DateTime actualStartDate)
        {
            Delivery.ActualStartDate = actualStartDate;
            return this;
        }
        public IlrLearningDeliveryBuilder WithPlannedEndDate(DateTime plannedEndDate)
        {
            Delivery.PlannedEndDate = plannedEndDate;
            return this;
        }
        public IlrLearningDeliveryBuilder WithActualEndDate(DateTime? actualEndDate)
        {
            Delivery.ActualEndDate = actualEndDate;
            return this;
        }

        public IlrLearningDeliveryBuilder WithAgreedPrice(decimal agreedPrice)
        {
            var tnp1 = agreedPrice * 0.8m;
            var tnp2 = agreedPrice - tnp1;

            Delivery.TrainingCost = tnp1;
            Delivery.EndpointAssesmentCost = tnp2;

            return this;
        }

        public IlrLearningDeliveryBuilder WithActFamCodeValue(short actFamCodeValue)
        {
            Delivery.ActFamCodeValue = actFamCodeValue;
            return this;
        }
    }
}