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
Given No matching record found in the employer digital account 
When an ILR file is submitted with the following data where standard code does not match: 
    | learner type       | agreed price | start date | planned end date | actual end date | completion status | standard code |
    | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/08/2018      | completed         | 99999         |
Then a datalock error DLOCK_03 is produced


Scenario: No matching record found in the employer digital account for the framework code then datalock DLOCK_04 will be produced
Given No matching record found in the employer digital account 
When  an ILR file is submitted with the following data where framework code does not match: 
    | learner type       | agreed price | start date | planned end date | actual end date | completion status | standard code | framework code |
    | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/08/2018      | completed         | 0             | 99999          |
Then a datalock error DLOCK_04 is produced


Scenario: No matching record found in the employer digital account for the programme type then datalock DLOCK_05 will be produced
Given No matching record found in the employer digital account 
When an ILR file is submitted with the following data where programme type does not match: 
    | learner type       | agreed price | start date | planned end date | actual end date | completion status | standard code | programme type |
    | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/08/2018      | completed         | 0             | 99999          |
Then a datalock error DLOCK_05 is produced


Scenario: No matching record found in the employer digital account for the pathway code then datalock DLOCK_06 will be produced
Given No matching record found in the employer digital account 
When an ILR file is submitted with the following data where pathway code does not match: 
    | learner type       | agreed price | start date | planned end date | actual end date | completion status | standard code | pathway code |
    | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/08/2018      | completed         | 0             | 99999        |
Then a datalock error DLOCK_06 is produced

Scenario: No matching record found in the employer digital account for the negotiated cost then datalock DLOCK_07 will be produced
Given No matching record found in the employer digital account 
When an ILR file is submitted with the following data where negotiated cost does not match:
    | learner type       | agreed price | start date | planned end date | actual end date | completion status |
    | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/08/2018      | completed         |
Then a datalock error DLOCK_07 is produced


Scenario: The start month recorded in the employer digital account is after the ILR learning delivery start month then datalock DLOCK_08 will be produced
Given No matching record found in the employer digital account 
When an ILR file is submitted with the following data where there are multiple matching commitments:
    | learner type       | agreed price | start date | planned end date | actual end date | completion status |
    | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/08/2018      | completed         |
Then a datalock error DLOCK_08 is produced

Scenario: The start month recorded in the employer digital account is after the ILR learning delivery start month then datalock DLOCK_09 will be produced
Given No matching record found in the employer digital account 
When an ILR file is submitted with the following data where learning delivery start does not match:
    | learner type       | agreed price | start date | planned end date | actual end date | completion status |
    | programme only DAS | 15000        | 02/09/2017 | 08/09/2018       | 08/08/2018      | completed         |
Then a datalock error DLOCK_09 is produced

