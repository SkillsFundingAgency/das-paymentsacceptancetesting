# DAS Payments - Acceptance Tests

This repository represents the specifications for the SFA - DAS payment system; including automation to test the specifications against all the components (from other repositories) that comprise the system.

Documentation can be viewed at [here](https://skillsfundingagency.github.io/das-paymentsacceptancetesting/)

## Structure

* The [features](features/) folder contains the [Gherkin](https://github.com/cucumber/cucumber/wiki/Gherkin) specifications
* The [src](src/) folder contains the [SpecFlow](http://www.specflow.org/) solution to automate the specifications

## Running locally

In order to run the tests locally you will need:

* SQL Server 2008 onwards
* A database named DasPaymentsAT_Transient to act as the DCFS transient database
* A database named DasPaymentsAT_Deds to act as the DCFS DEDS database
* Download the latest component from each of these builds on VSTS and extract to: C:\temp\PaymentsAT\components
    * [Das-CollectionsEarnings-OPA-Calculator](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/index?context=allDefinitions&path=%5CDAS-Payments&definitionId=398&_a=completed)
    * [Das-Payments-Reference-Accounts](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/index?context=allDefinitions&path=%5CDAS-Payments&definitionId=160&_a=completed)
    * [Das-Payments-Reference-Commitments](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/index?context=allDefinitions&path=%5CDAS-Payments&definitionId=161&_a=completed)
    * [Das-PaymentEventsApi](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/index?context=allDefinitions&path=%5CDAS-Payments&definitionId=197&_a=completed)
    * [Das-ProviderEvents-Components](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/index?context=allDefinitions&path=%5CDAS-Payments&definitionId=298&_a=completed)
    * [Das-ProviderPayments-Calculator](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/index?context=allDefinitions&path=%5CDAS-Payments&definitionId=133&_a=completed)
    * [Das-CollectionEarnings-Datalock](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/index?context=allDefinitions&path=%5CDAS-Payments&definitionId=126&_a=completed)

You will then need to run:
* The AT scripts in [src/Deploy](src/Deploy/) 
    * files are names *.transient.* for scripts to run on transient db and *.deds.* for scripts to run on deds
* Run the tests

