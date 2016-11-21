Feature: Datalock validation fails for different reasons


Scenario Outline: No matching record found in an employer digital account for the UKPRN then datalock DLOCK_01 will be produced
Given There is no employer data in the committments 
When an ILR file is submitted with the following data for UKPRN 999999:
    | learner type       | agreed price | start date | planned end date | actual end date | completion status |
    | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/08/2018      | completed         |
Then a datalock error DLOCK_01 is produced