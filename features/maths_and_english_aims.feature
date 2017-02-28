Feature: Provider earnings and payments where apprenticeship requires English or maths at level 2.

@MathsAndEnglishNonDas
Scenario: Maths and English payments for a non-das learner finishing on time, funding agreed within band maximum, planned duration is same as programme (assumes both start and finish at same time)
    When an ILR file is submitted with the following data:
        | ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | aim type         | aim cost | framework code | programme type | pathway code |
        | learner a | 16-18 programme only non-DAS | 15000        | 06/08/2017 | 08/08/2018       |                 | continuing        | programme        |          | 403            | 2              | 1            |
        | learner a | 16-18 programme only non-DAS |              | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | maths or english | 471      | 403            | 2              | 1            |
    Then the provider earnings and payments break down as follows:
        | Type                                    | 08/17   | 09/17   | 10/17   | 11/17   | 12/17   | ... | 07/18   | 08/18  | 09/18 |
        | Provider Earned Total                   | 1039.25 | 1039.25 | 1039.25 | 2039.25 | 1039.25 | ... | 1039.25 | 1000   | 0     |
        | Provider Earned from SFA                | 939.25  | 939.25  | 939.25  | 1939.25 | 939.25  | ... | 939.25  | 1000   | 0     |
        | Provider Earned from Employer           | 100     | 100     | 100     | 100     | 100     | ... | 100     | 100    | 0     |
        | Provider Paid by SFA                    | 0       | 939.25  | 939.25  | 939.25  | 1939.25 | ... | 939.25  | 939.25 | 1000  |
        | Payment due from Employer               | 0       | 100     | 100     | 100     | 100     | ... | 100     | 100    | 0     |
        | Levy account debited                    | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0      | 0     |
        | SFA Levy employer budget                | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0      | 0     |
        | SFA Levy co-funding budget              | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0      | 0     |
        | SFA non-Levy co-funding budget          | 900     | 900     | 900     | 900     | 900     | ... | 900     | 0      | 0     |
        | SFA non-Levy additional payments budget | 39.25   | 39.25   | 39.25   | 1039.25 | 39.25   | ... | 39.25   | 1000   | 0     |
    And the transaction types for the payments are:
        | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
        | On-program                     | 900   | 900   | 900   | 900   | ... | 900   | 0     |
        | Completion                     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Balancing                      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Employer 16-18 incentive       | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Provider 16-18 incentive       | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | English and maths on programme | 39.25 | 39.25 | 39.25 | 39.25 | ... | 39.25 | 0     |
        | English and maths Balancing    | 0     | 0     | 0     | 0     | ... | 0     | 0     |

