# DAS Payments - Acceptance Tests

This repository represents the specifications for the SFA - DAS payment system; including automation to test the specifications against all the components (from other repositories) that comprise the system.

## Structure

* The [features](features/) folder contains the [Gherkin](https://github.com/cucumber/cucumber/wiki/Gherkin) specifications
* The [src](src/) folder contains the [SpecFlow](http://www.specflow.org/) solution to automate the specifications

## Running locally

In order to run the tests locally you will need:

* SQL Server 2008 onwards
* A database to act as the DCFS transient database
* A database to act as the DCFS DEDS database
* The latest versions of the DAS payment components

You will then need to run:
* The AT scripts in [src/Deploy](src/Deploy/)
* Run the test stack scripts
