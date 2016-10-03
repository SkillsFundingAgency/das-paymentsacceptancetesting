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
* The latest versions of the DAS payment components (Download artifacts from VSO builds); extracted to C:\temp\PaymentsAT\components

You will then need to run:
* The AT scripts in [src/Deploy](src/Deploy/)
* Run the test stack scripts

N.B. files are names *.transient.* for scripts to run on transient db and *.deds.* for scripts to run on deds