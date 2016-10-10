using System;
using System.Collections.Generic;
using IlrGenerator;
using ProviderPayments.TestStack.Core.Domain;

namespace SFA.DAS.Payments.AcceptanceTests.Builders
{
    public class IlrLearningDeliveryBuilder : IlrLearnerBuilder
    {
        internal readonly LearningDelivery Delivery;

        internal IlrLearningDeliveryBuilder(IlrLearnerBuilder parentBuilder)
            : base(parentBuilder.Submission, parentBuilder.Learner)
        {
            Delivery = new LearningDelivery
            {
                StandardCode = Defaults.StandardCode,
                FrameworkCode = Defaults.FrameworkCode,
                ProgrammeType = Defaults.ProgrammeType,
                PathwayCode = Defaults.PathwayCode,
                ActualStartDate = Defaults.ActualStartDate,
                PlannedEndDate = Defaults.PlannedEndDate,
                ActualEndDate = Defaults.ActualEndDate,
                AgreedPrice = Defaults.AgreedPrice,
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
            Delivery.AgreedPrice = agreedPrice;
            return this;
        }

        public IlrLearningDeliveryBuilder WithActFamCodeValue(short actFamCodeValue)
        {
            Delivery.ActFamCodeValue = actFamCodeValue;
            return this;
        }
    }
}