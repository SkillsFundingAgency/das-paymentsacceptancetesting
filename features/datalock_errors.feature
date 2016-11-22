Feature: Datalock validation fails for different reasons


Scenario: No matching record found in an employer digital account for the UKPRN then datalock DLOCK_01 will be produced
Given There is no employer data in the committments for UKPRN 999999
When an ILR file is submitted with the following data for UKPRN 999999:
    | learner type       | agreed price | start date | planned end date | actual end date | completion status |
    | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/08/2018      | completed         |
Then a datalock error DLOCK_01 is produced


Scenario: No matching record found in the employer digital account for the ULN then datalock DLOCK_02 will be produced
Given No matching record found in the employer digital account for the ULN 999999
When an ILR file is submitted with the following data for the ULN 999999:
    | learner type       | agreed price | start date | planned end date | actual end date | completion status |
    | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/08/2018      | completed         |
Then a datalock error DLOCK_02 is produced


Scenario: No matching record found in the employer digital account for the standard code then datalock DLOCK_03 will be produced
Given No matching record found in the employer digital account for the standard code 999999
When an ILR file is submitted with the following data for the standard code 999999:
    | learner type       | agreed price | start date | planned end date | actual end date | completion status |
    | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/08/2018      | completed         |
Then a datalock error DLOCK_03 is produced


