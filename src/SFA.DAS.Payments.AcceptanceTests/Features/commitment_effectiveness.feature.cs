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
    [NUnit.Framework.DescriptionAttribute("Commitment effective dates apply correctly in data collections processing")]
    public partial class CommitmentEffectiveDatesApplyCorrectlyInDataCollectionsProcessingFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "commitment_effectiveness.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-GB"), "Commitment effective dates apply correctly in data collections processing", null, ProgrammingLanguage.CSharp, ((string[])(null)));
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
        testRunner.Given("the apprenticeship funding band maximum for each learner is 17000", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 5
        testRunner.And("levy balance > agreed price for all months", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change in price month 2")]
        public virtual void ChangeInPriceMonth2()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change in price month 2", ((string[])(null)));
#line 7
 this.ScenarioSetup(scenarioInfo);
#line 3
    this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table258 = new TechTalk.SpecFlow.Table(new string[] {
                        "commitment Id",
                        "version Id",
                        "Employer",
                        "Provider",
                        "ULN",
                        "start date",
                        "end date",
                        "agreed price",
                        "effective from",
                        "effective to"});
            table258.AddRow(new string[] {
                        "1",
                        "1",
                        "employer 1",
                        "provider a",
                        "learner a",
                        "01/05/2017",
                        "01/05/2018",
                        "7500",
                        "01/05/2017",
                        "31/05/2017"});
            table258.AddRow(new string[] {
                        "1",
                        "2",
                        "employer 1",
                        "provider a",
                        "learner a",
                        "01/01/2017",
                        "01/05/2018",
                        "15000",
                        "01/06/2017",
                        ""});
#line 8
  testRunner.Given("the following commitments exist:", ((string)(null)), table258, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table259 = new TechTalk.SpecFlow.Table(new string[] {
                        "ULN",
                        "learner type",
                        "start date",
                        "planned end date",
                        "completion status",
                        "Total training price 1",
                        "Total training price 1 effective date",
                        "Total training price 2",
                        "Total training price 2 effective date"});
            table259.AddRow(new string[] {
                        "learner a",
                        "programme only DAS",
                        "12/05/2017",
                        "20/05/2018",
                        "continuing",
                        "7500",
                        "01/05/2017",
                        "15000",
                        "01/06/2017"});
#line 13
  testRunner.When("an ILR file is submitted with the following data:", ((string)(null)), table259, "When ");
#line hidden
            TechTalk.SpecFlow.Table table260 = new TechTalk.SpecFlow.Table(new string[] {
                        "Payment type",
                        "05/17",
                        "06/17",
                        "07/17"});
            table260.AddRow(new string[] {
                        "On-program",
                        "commitment 1 v1",
                        "commitment 1 v2",
                        "commitment 1 v2"});
#line 17
  testRunner.Then("the data lock status will be as follows:", ((string)(null)), table260, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table261 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "05/17",
                        "06/17",
                        "07/17"});
            table261.AddRow(new string[] {
                        "Provider Earned Total",
                        "500",
                        "1045.45",
                        "1045.45"});
            table261.AddRow(new string[] {
                        "Provider Earned from SFA",
                        "500",
                        "1045.45",
                        "1045.45"});
            table261.AddRow(new string[] {
                        "Provider Paid by SFA",
                        "0",
                        "500",
                        "1045.45"});
            table261.AddRow(new string[] {
                        "Levy account debited",
                        "0",
                        "500",
                        "1045.45"});
            table261.AddRow(new string[] {
                        "SFA Levy employer budget",
                        "500",
                        "1045.45",
                        "1045.45"});
            table261.AddRow(new string[] {
                        "SFA Levy co-funding budget",
                        "0",
                        "0",
                        "0"});
#line 20
        testRunner.And("the provider earnings and payments break down as follows:", ((string)(null)), table261, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change in price month 2 with priority change after")]
        public virtual void ChangeInPriceMonth2WithPriorityChangeAfter()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change in price month 2 with priority change after", ((string[])(null)));
#line 31
 this.ScenarioSetup(scenarioInfo);
#line 3
    this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table262 = new TechTalk.SpecFlow.Table(new string[] {
                        "commitment Id",
                        "version Id",
                        "priority",
                        "Employer",
                        "Provider",
                        "ULN",
                        "start date",
                        "end date",
                        "agreed price",
                        "effective from",
                        "effective to"});
            table262.AddRow(new string[] {
                        "1",
                        "1",
                        "1",
                        "employer 1",
                        "provider a",
                        "learner a",
                        "01/05/2017",
                        "01/05/2018",
                        "7500",
                        "01/05/2017",
                        "31/05/2017"});
            table262.AddRow(new string[] {
                        "1",
                        "2",
                        "1",
                        "employer 1",
                        "provider a",
                        "learner a",
                        "01/01/2017",
                        "01/05/2018",
                        "15000",
                        "01/06/2017",
                        "13/07/2017"});
            table262.AddRow(new string[] {
                        "1",
                        "3",
                        "2",
                        "employer 1",
                        "provider a",
                        "learner a",
                        "01/01/2017",
                        "01/05/2018",
                        "15000",
                        "14/07/2017",
                        ""});
#line 32
  testRunner.Given("the following commitments exist:", ((string)(null)), table262, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table263 = new TechTalk.SpecFlow.Table(new string[] {
                        "ULN",
                        "learner type",
                        "start date",
                        "planned end date",
                        "completion status",
                        "Total training price 1",
                        "Total training price 1 effective date",
                        "Total training price 2",
                        "Total training price 2 effective date"});
            table263.AddRow(new string[] {
                        "learner a",
                        "programme only DAS",
                        "12/05/2017",
                        "20/05/2018",
                        "continuing",
                        "7500",
                        "01/05/2017",
                        "15000",
                        "01/06/2017"});
#line 38
  testRunner.When("an ILR file is submitted with the following data:", ((string)(null)), table263, "When ");
#line hidden
            TechTalk.SpecFlow.Table table264 = new TechTalk.SpecFlow.Table(new string[] {
                        "Payment type",
                        "05/17",
                        "06/17",
                        "07/17"});
            table264.AddRow(new string[] {
                        "On-program",
                        "commitment 1 v1",
                        "commitment 1 v2",
                        "commitment 1 v3"});
#line 42
  testRunner.Then("the data lock status will be as follows:", ((string)(null)), table264, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table265 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "05/17",
                        "06/17",
                        "07/17"});
            table265.AddRow(new string[] {
                        "Provider Earned Total",
                        "500",
                        "1045.45",
                        "1045.45"});
            table265.AddRow(new string[] {
                        "Provider Earned from SFA",
                        "500",
                        "1045.45",
                        "1045.45"});
            table265.AddRow(new string[] {
                        "Provider Paid by SFA",
                        "0",
                        "500",
                        "1045.45"});
            table265.AddRow(new string[] {
                        "Levy account debited",
                        "0",
                        "500",
                        "1045.45"});
            table265.AddRow(new string[] {
                        "SFA Levy employer budget",
                        "500",
                        "1045.45",
                        "1045.45"});
            table265.AddRow(new string[] {
                        "SFA Levy co-funding budget",
                        "0",
                        "0",
                        "0"});
#line 45
        testRunner.Then("the provider earnings and payments break down as follows:", ((string)(null)), table265, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
