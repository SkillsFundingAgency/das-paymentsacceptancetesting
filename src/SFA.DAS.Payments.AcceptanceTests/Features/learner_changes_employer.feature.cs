﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SFA.DAS.Payments.AcceptanceTests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Provider earnings and payments where a learner changes employers")]
    public partial class ProviderEarningsAndPaymentsWhereALearnerChangesEmployersFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "learner_changes_employer.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-GB"), "Provider earnings and payments where a learner changes employers", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 3
    #line 4
        testRunner.Given("the apprenticeship funding band maximum is 17000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Earnings and payments for a DAS learner, levy available, and there is a change to" +
            " the Negotiated Cost which happens at the end of the month")]
        public virtual void EarningsAndPaymentsForADASLearnerLevyAvailableAndThereIsAChangeToTheNegotiatedCostWhichHappensAtTheEndOfTheMonth()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Earnings and payments for a DAS learner, levy available, and there is a change to" +
                    " the Negotiated Cost which happens at the end of the month", ((string[])(null)));
#line 6
    this.ScenarioSetup(scenarioInfo);
#line 3
    this.FeatureBackground();
#line 7
        testRunner.Given("The learner is programme only DAS", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
        testRunner.And("the ABC has a levy balance > agreed price for all months", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 9
        testRunner.And("the XYZ has a levy balance > agreed price for all months", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table60 = new TechTalk.SpecFlow.Table(new string[] {
                        "Employer",
                        "Type",
                        "ILR employment start date"});
            table60.AddRow(new string[] {
                        "ABC",
                        "DAS",
                        "01/08/2017"});
            table60.AddRow(new string[] {
                        "XYZ",
                        "DAS",
                        "01/11/2017"});
#line 10
        testRunner.And("the learner changes employers", ((string)(null)), table60, "And ");
#line hidden
            TechTalk.SpecFlow.Table table61 = new TechTalk.SpecFlow.Table(new string[] {
                        "Employer",
                        "ULN",
                        "price effective date",
                        "planned end date",
                        "actual end date",
                        "agreed price"});
            table61.AddRow(new string[] {
                        "ABC",
                        "learner a",
                        "01/08/2017",
                        "31/08/2018",
                        "31/10/2017",
                        "15000"});
            table61.AddRow(new string[] {
                        "XYZ",
                        "learner a",
                        "01/11/2017",
                        "31/08/2018",
                        "",
                        "5625"});
#line 14
        testRunner.And("the following commitments exist on 03/12/2017:", ((string)(null)), table61, "And ");
#line hidden
            TechTalk.SpecFlow.Table table62 = new TechTalk.SpecFlow.Table(new string[] {
                        "ULN",
                        "start date",
                        "planned end date",
                        "actual end date",
                        "completion status",
                        "Total training price",
                        "Total training price effective date",
                        "Total assessment price",
                        "Total assessment price effective date",
                        "Residual training price",
                        "Residual training price effective date",
                        "Residual assessment price",
                        "Residual assessment price effective date"});
            table62.AddRow(new string[] {
                        "learner a",
                        "01/08/2017",
                        "04/08/2018",
                        "",
                        "continuing",
                        "12000",
                        "01/08/2017",
                        "3000",
                        "01/08/2017",
                        "5000",
                        "01/11/2017",
                        "625",
                        "01/11/2017"});
#line 18
        testRunner.When("an ILR file is submitted on 03/12/2017 with the following data:", ((string)(null)), table62, "When ");
#line hidden
            TechTalk.SpecFlow.Table table63 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "08/17 - 10/17",
                        "11/17 onwards"});
            table63.AddRow(new string[] {
                        "Matching commitment",
                        "ABC",
                        "XYZ"});
#line 21
        testRunner.Then("the data lock status of the ILR in 03/12/2017 is:", ((string)(null)), table63, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table64 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "08/17",
                        "09/17",
                        "10/17",
                        "11/17",
                        "12/17"});
            table64.AddRow(new string[] {
                        "Provider Earned Total",
                        "1000",
                        "1000",
                        "1000",
                        "500",
                        "500"});
            table64.AddRow(new string[] {
                        "Provider Earned from SFA",
                        "1000",
                        "1000",
                        "1000",
                        "500",
                        "500"});
            table64.AddRow(new string[] {
                        "Provider Earned from ABC",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table64.AddRow(new string[] {
                        "Provider Earned from XYZ",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table64.AddRow(new string[] {
                        "Provider Paid by SFA",
                        "",
                        "1000",
                        "1000",
                        "1000",
                        "500"});
            table64.AddRow(new string[] {
                        "Payment due from ABC",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table64.AddRow(new string[] {
                        "Payment due from XYZ",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table64.AddRow(new string[] {
                        "ABC Levy account debited",
                        "0",
                        "1000",
                        "1000",
                        "1000",
                        "0"});
            table64.AddRow(new string[] {
                        "XYZ Levy account debited",
                        "0",
                        "0",
                        "0",
                        "0",
                        "500"});
            table64.AddRow(new string[] {
                        "SFA Levy employer budget",
                        "1000",
                        "1000",
                        "1000",
                        "500",
                        "500"});
            table64.AddRow(new string[] {
                        "SFA Levy co-funding budget",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
#line 24
        testRunner.And("the provider earnings and payments break down as follows:", ((string)(null)), table64, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Earnings and payments for a DAS learner, levy available, and there is a change to" +
            " the Negotiated Cost which happens mid month")]
        public virtual void EarningsAndPaymentsForADASLearnerLevyAvailableAndThereIsAChangeToTheNegotiatedCostWhichHappensMidMonth()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Earnings and payments for a DAS learner, levy available, and there is a change to" +
                    " the Negotiated Cost which happens mid month", ((string[])(null)));
#line 39
    this.ScenarioSetup(scenarioInfo);
#line 3
    this.FeatureBackground();
#line 40
        testRunner.Given("The learner is programme only DAS", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 41
        testRunner.And("the ABC has a levy balance > agreed price for all months", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
        testRunner.And("the XYZ has a levy balance > agreed price for all months", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table65 = new TechTalk.SpecFlow.Table(new string[] {
                        "Employer",
                        "Type",
                        "ILR employment start date"});
            table65.AddRow(new string[] {
                        "ABC",
                        "DAS",
                        "04/08/2017"});
            table65.AddRow(new string[] {
                        "XYZ",
                        "DAS",
                        "10/11/2017"});
#line 43
        testRunner.And("the learner changes employers", ((string)(null)), table65, "And ");
#line hidden
            TechTalk.SpecFlow.Table table66 = new TechTalk.SpecFlow.Table(new string[] {
                        "Employer",
                        "ULN",
                        "price effective date",
                        "planned end date",
                        "actual end date",
                        "agreed price"});
            table66.AddRow(new string[] {
                        "ABC",
                        "learner a",
                        "01/08/2017",
                        "31/08/2018",
                        "31/10/2017",
                        "15000"});
            table66.AddRow(new string[] {
                        "XYZ",
                        "learner a",
                        "01/11/2017",
                        "31/08/2018",
                        "",
                        "5625"});
#line 47
        testRunner.And("the following commitments exist on 03/12/2017:", ((string)(null)), table66, "And ");
#line hidden
            TechTalk.SpecFlow.Table table67 = new TechTalk.SpecFlow.Table(new string[] {
                        "ULN",
                        "start date",
                        "planned end date",
                        "actual end date",
                        "completion status",
                        "Total training price",
                        "Total training price effective date",
                        "Total assessment price",
                        "Total assessment price effective date",
                        "Residual training price",
                        "Residual training price effective date",
                        "Residual assessment price",
                        "Residual assessment price effective date"});
            table67.AddRow(new string[] {
                        "learner a",
                        "04/08/2017",
                        "04/08/2018",
                        "",
                        "continuing",
                        "12000",
                        "04/08/2017",
                        "3000",
                        "04/08/2017",
                        "5000",
                        "10/11/2017",
                        "625",
                        "10/11/2017"});
#line 51
        testRunner.When("an ILR file is submitted on 03/12/2017 with the following data:", ((string)(null)), table67, "When ");
#line hidden
            TechTalk.SpecFlow.Table table68 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "08/17 - 10/17",
                        "11/17 onwards"});
            table68.AddRow(new string[] {
                        "Matching commitment",
                        "ABC",
                        "XYZ"});
#line 54
        testRunner.Then("the data lock status of the ILR in 03/12/2017 is:", ((string)(null)), table68, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table69 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "08/17",
                        "09/17",
                        "10/17",
                        "11/17",
                        "12/17"});
            table69.AddRow(new string[] {
                        "Provider Earned Total",
                        "1000",
                        "1000",
                        "1000",
                        "500",
                        "500"});
            table69.AddRow(new string[] {
                        "Provider Earned from SFA",
                        "1000",
                        "1000",
                        "1000",
                        "500",
                        "500"});
            table69.AddRow(new string[] {
                        "Provider Earned from ABC",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table69.AddRow(new string[] {
                        "Provider Earned from XYZ",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table69.AddRow(new string[] {
                        "Provider Paid by SFA",
                        "",
                        "1000",
                        "1000",
                        "1000",
                        "500"});
            table69.AddRow(new string[] {
                        "Payment due from ABC",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table69.AddRow(new string[] {
                        "Payment due from XYZ",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table69.AddRow(new string[] {
                        "ABC Levy account debited",
                        "0",
                        "1000",
                        "1000",
                        "1000",
                        "0"});
            table69.AddRow(new string[] {
                        "XYZ Levy account debited",
                        "0",
                        "0",
                        "0",
                        "0",
                        "500"});
            table69.AddRow(new string[] {
                        "SFA Levy employer budget",
                        "1000",
                        "1000",
                        "1000",
                        "500",
                        "500"});
            table69.AddRow(new string[] {
                        "SFA Levy co-funding budget",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
#line 57
        testRunner.And("the provider earnings and payments break down as follows:", ((string)(null)), table69, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Earnings and payments for a DAS learner, levy available, and there is a change to" +
            " the Negotiated Cost earlier than expected")]
        public virtual void EarningsAndPaymentsForADASLearnerLevyAvailableAndThereIsAChangeToTheNegotiatedCostEarlierThanExpected()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Earnings and payments for a DAS learner, levy available, and there is a change to" +
                    " the Negotiated Cost earlier than expected", ((string[])(null)));
#line 72
    this.ScenarioSetup(scenarioInfo);
#line 3
    this.FeatureBackground();
#line 73
        testRunner.Given("The learner is programme only DAS", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 74
        testRunner.And("the ABC has a levy balance > agreed price for all months", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 75
        testRunner.And("the XYZ has a levy balance > agreed price for all months", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table70 = new TechTalk.SpecFlow.Table(new string[] {
                        "Employer",
                        "Type",
                        "ILR employment start date"});
            table70.AddRow(new string[] {
                        "ABC",
                        "DAS",
                        "04/08/2017"});
            table70.AddRow(new string[] {
                        "XYZ",
                        "DAS",
                        "10/11/2017"});
#line 76
        testRunner.And("the learner changes employers", ((string)(null)), table70, "And ");
#line hidden
            TechTalk.SpecFlow.Table table71 = new TechTalk.SpecFlow.Table(new string[] {
                        "Employer",
                        "ULN",
                        "price effective date",
                        "planned end date",
                        "actual end date",
                        "agreed price"});
            table71.AddRow(new string[] {
                        "ABC",
                        "learner a",
                        "01/08/2017",
                        "31/08/2018",
                        "31/10/2017",
                        "15000"});
            table71.AddRow(new string[] {
                        "XYZ",
                        "learner a",
                        "01/11/2017",
                        "31/08/2018",
                        "",
                        "5625"});
#line 80
        testRunner.And("the following commitments exist on 03/12/2017:", ((string)(null)), table71, "And ");
#line hidden
            TechTalk.SpecFlow.Table table72 = new TechTalk.SpecFlow.Table(new string[] {
                        "ULN",
                        "start date",
                        "planned end date",
                        "actual end date",
                        "completion status",
                        "Total training price",
                        "Total training price effective date",
                        "Total assessment price",
                        "Total assessment price effective date",
                        "Residual training price",
                        "Residual training price effective date",
                        "Residual assessment price",
                        "Residual assessment price effective date"});
            table72.AddRow(new string[] {
                        "learner a",
                        "04/08/2017",
                        "04/08/2018",
                        "",
                        "continuing",
                        "12000",
                        "04/08/2017",
                        "3000",
                        "04/08/2017",
                        "5000",
                        "25/10/2017",
                        "625",
                        "25/10/2017"});
#line 84
        testRunner.When("an ILR file is submitted on 03/12/2017 with the following data:", ((string)(null)), table72, "When ");
#line hidden
            TechTalk.SpecFlow.Table table73 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "08/17 - 09/17",
                        "10/17 onwards"});
            table73.AddRow(new string[] {
                        "Matching commitment",
                        "ABC",
                        ""});
#line 87
        testRunner.Then("the data lock status of the ILR in 03/12/2017 is:", ((string)(null)), table73, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table74 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "08/17",
                        "09/17",
                        "10/17",
                        "11/17",
                        "12/17"});
            table74.AddRow(new string[] {
                        "Provider Earned Total",
                        "1000",
                        "1000",
                        "450",
                        "450",
                        "450"});
            table74.AddRow(new string[] {
                        "Provider Earned from SFA",
                        "1000",
                        "1000",
                        "450",
                        "450",
                        "450"});
            table74.AddRow(new string[] {
                        "Provider Earned from ABC",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table74.AddRow(new string[] {
                        "Provider Earned from XYZ",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table74.AddRow(new string[] {
                        "Provider Paid by SFA",
                        "",
                        "1000",
                        "1000",
                        "0",
                        "0"});
            table74.AddRow(new string[] {
                        "Payment due from ABC",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table74.AddRow(new string[] {
                        "Payment due from XYZ",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table74.AddRow(new string[] {
                        "ABC Levy account debited",
                        "0",
                        "1000",
                        "1000",
                        "0",
                        "0"});
            table74.AddRow(new string[] {
                        "XYZ Levy account debited",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table74.AddRow(new string[] {
                        "SFA Levy employer budget",
                        "1000",
                        "1000",
                        "0",
                        "0",
                        "0"});
            table74.AddRow(new string[] {
                        "SFA Levy co-funding budget",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
#line 90
        testRunner.And("the provider earnings and payments break down as follows:", ((string)(null)), table74, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Earnings and payments for a DAS learner, levy available, where a learner switches" +
            " from DAS to Non Das employer at the end of month")]
        public virtual void EarningsAndPaymentsForADASLearnerLevyAvailableWhereALearnerSwitchesFromDASToNonDasEmployerAtTheEndOfMonth()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Earnings and payments for a DAS learner, levy available, where a learner switches" +
                    " from DAS to Non Das employer at the end of month", ((string[])(null)));
#line 108
    this.ScenarioSetup(scenarioInfo);
#line 3
    this.FeatureBackground();
#line 109
        testRunner.Given("The learner is programme only DAS", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 110
        testRunner.And("the ABC has a levy balance > agreed price for all months", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table75 = new TechTalk.SpecFlow.Table(new string[] {
                        "Employer",
                        "Type",
                        "ILR employment start date"});
            table75.AddRow(new string[] {
                        "ABC",
                        "DAS",
                        "03/08/2017"});
            table75.AddRow(new string[] {
                        "XYZ",
                        "Non DAS",
                        "03/11/2017"});
#line 112
  testRunner.And("the learner changes employers", ((string)(null)), table75, "And ");
#line hidden
            TechTalk.SpecFlow.Table table76 = new TechTalk.SpecFlow.Table(new string[] {
                        "Employer",
                        "ULN",
                        "price effective date",
                        "planned end date",
                        "actual end date",
                        "agreed price"});
            table76.AddRow(new string[] {
                        "ABC",
                        "learner a",
                        "03/08/2017",
                        "04/08/2018",
                        "02/11/2017",
                        "15000"});
#line 116
  testRunner.And("the following commitments exist on 03/12/2017:", ((string)(null)), table76, "And ");
#line hidden
            TechTalk.SpecFlow.Table table77 = new TechTalk.SpecFlow.Table(new string[] {
                        "ULN",
                        "start date",
                        "planned end date",
                        "actual end date",
                        "completion status",
                        "Total training price",
                        "Total training price effective date",
                        "Total assessment price",
                        "Total assessment price effective date",
                        "Residual training price",
                        "Residual training price effective date",
                        "Residual assessment price",
                        "Residual assessment price effective date"});
            table77.AddRow(new string[] {
                        "learner a",
                        "03/08/2017",
                        "04/08/2018",
                        "",
                        "continuing",
                        "12000",
                        "03/08/2017",
                        "3000",
                        "03/08/2017",
                        "4500",
                        "03/11/2017",
                        "1125",
                        "03/11/2017"});
#line 120
        testRunner.When("an ILR file is submitted on 03/12/2017 with the following data:", ((string)(null)), table77, "When ");
#line hidden
            TechTalk.SpecFlow.Table table78 = new TechTalk.SpecFlow.Table(new string[] {
                        "contract type",
                        "date from",
                        "date to"});
            table78.AddRow(new string[] {
                        "DAS",
                        "03/08/2017",
                        "02/11/2017"});
            table78.AddRow(new string[] {
                        "Non DAS",
                        "03/11/2017",
                        "04/08/2018"});
#line 124
      testRunner.And("the Contract type in the ILR is:", ((string)(null)), table78, "And ");
#line hidden
            TechTalk.SpecFlow.Table table79 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "08/17 - 10/17",
                        "11/17 onwards"});
            table79.AddRow(new string[] {
                        "Matching commitment",
                        "ABC",
                        ""});
#line 129
     testRunner.Then("the data lock status of the ILR in 03/12/2017 is:", ((string)(null)), table79, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table80 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "08/17",
                        "09/17",
                        "10/17",
                        "11/17",
                        "12/17"});
            table80.AddRow(new string[] {
                        "Provider Earned Total",
                        "1000",
                        "1000",
                        "1000",
                        "500",
                        "500"});
            table80.AddRow(new string[] {
                        "Provider Earned from SFA",
                        "1000",
                        "1000",
                        "1000",
                        "500",
                        "450"});
            table80.AddRow(new string[] {
                        "Provider Earned from ABC",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table80.AddRow(new string[] {
                        "Provider Earned from XYZ",
                        "0",
                        "0",
                        "0",
                        "0",
                        "50"});
            table80.AddRow(new string[] {
                        "Provider Paid by SFA",
                        "",
                        "1000",
                        "1000",
                        "1000",
                        "0"});
            table80.AddRow(new string[] {
                        "Payment due from ABC",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table80.AddRow(new string[] {
                        "Payment due from XYZ",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
            table80.AddRow(new string[] {
                        "ABC Levy account debited",
                        "0",
                        "1000",
                        "1000",
                        "1000",
                        "0"});
            table80.AddRow(new string[] {
                        "SFA Levy employer budget",
                        "1000",
                        "1000",
                        "1000",
                        "500",
                        "0"});
            table80.AddRow(new string[] {
                        "SFA Levy co-funding budget",
                        "0",
                        "0",
                        "0",
                        "0",
                        "0"});
#line 132
        testRunner.And("the provider earnings and payments break down as follows:", ((string)(null)), table80, "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
