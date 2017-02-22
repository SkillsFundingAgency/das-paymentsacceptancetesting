Feature: Provider earnings and payments where a learner changes employers

    Background:
        Given the apprenticeship funding band maximum is 17000

    Scenario: Earnings and payments for a DAS learner, levy available, and there is a change to the Negotiated Cost which happens at the end of the month
        Given The learner is programme only DAS
        And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type | ILR employment start date |
            | ABC      | DAS  | 01/08/2017                |
            | XYZ      | DAS  | 01/11/2017                |
        And the following commitments exist on 03/12/2017:
            | Employer | commitment Id | version Id | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | ABC      | 1             | 1          | learner a | 01/08/2017 | 31/08/2018 | 15000        | active    | 01/08/2017     | 31/10/2017   |
            | ABC      | 1             | 2          | learner a | 01/08/2017 | 31/08/2018 | 15000        | cancelled | 01/11/2017     |              |
            | XYZ      | 2             | 1          | learner a | 01/11/2017 | 31/08/2018 | 5625         | active    | 01/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | learner a | 01/08/2017 | 04/08/2018       |                 | continuing        | 12000                | 01/08/2017                          | 3000                   | 01/08/2017                            | 5000                    | 01/11/2017                             | 625                       | 01/11/2017                               |
        Then the data lock status of the ILR in 03/12/2017 is:
            | Type                | 08/17 - 10/17 | 11/17 onwards |
            | Matching commitment | ABC           | XYZ           |
        And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from ABC   | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ   | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA       |       | 1000  | 1000  | 1000  | 500   |
            | Payment due from ABC       | 0     | 0     | 0     | 0     | 0     |
            | Payment due from XYZ       | 0     | 0     | 0     | 0     | 0     |
            | ABC Levy account debited   | 0     | 1000  | 1000  | 1000  | 0     |
            | XYZ Levy account debited   | 0     | 0     | 0     | 0     | 500   |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 500   | 500   |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     |


    Scenario: Earnings and payments for a DAS learner, levy available, and there is a change to the Negotiated Cost which happens mid month
        Given The learner is programme only DAS
        And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type | ILR employment start date |
            | ABC      | DAS  | 04/08/2017                |
            | XYZ      | DAS  | 10/11/2017                |
        And the following commitments exist on 03/12/2017:
            | Employer | commitment Id | version Id | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | ABC      | 1             | 1          | learner a | 01/08/2017 | 31/08/2018 | 15000        | active    | 01/08/2017     | 31/10/2017   |
            | ABC      | 1             | 2          | learner a | 01/08/2017 | 31/08/2018 | 15000        | cancelled | 01/11/2017     |              |
            | XYZ      | 2             | 1          | learner a | 01/11/2017 | 31/08/2018 | 5625         | active    | 01/11/2017     |              |
        
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | learner a | 04/08/2017 | 04/08/2018       |                 | continuing        | 12000                | 04/08/2017                          | 3000                   | 04/08/2017                            | 5000                    | 10/11/2017                             | 625                       | 10/11/2017                               |
        Then the data lock status of the ILR in 03/12/2017 is:
            | Type                | 08/17 - 10/17 | 11/17 onwards |
            | Matching commitment | ABC           | XYZ           |
        And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from ABC   | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ   | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA       |       | 1000  | 1000  | 1000  | 500   |
            | Payment due from ABC       | 0     | 0     | 0     | 0     | 0     |
            | Payment due from XYZ       | 0     | 0     | 0     | 0     | 0     |
            | ABC Levy account debited   | 0     | 1000  | 1000  | 1000  | 0     |
            | XYZ Levy account debited   | 0     | 0     | 0     | 0     | 500   |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 500   | 500   |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     |


    Scenario: Earnings and payments for a DAS learner, levy available, and there is a change to the Negotiated Cost earlier than expected
        Given The learner is programme only DAS
        And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type | ILR employment start date |
            | ABC      | DAS  | 04/08/2017                |
            | XYZ      | DAS  | 10/11/2017                |
        And the following commitments exist on 03/12/2017:
            | Employer | commitment Id | version Id | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | ABC      | 1             | 1          | learner a | 01/08/2017 | 31/08/2018 | 15000        | active    | 01/08/2017     | 31/10/2017   |
            | ABC      | 1             | 2          | learner a | 01/08/2017 | 31/08/2018 | 15000        | cancelled | 01/11/2017     |              |
            | XYZ      | 2             | 1          | learner a | 01/11/2017 | 31/08/2018 | 5625         | active    | 01/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | learner a | 04/08/2017 | 04/08/2018       |                 | continuing        | 12000                | 04/08/2017                          | 3000                   | 04/08/2017                            | 5000                    | 25/10/2017                             | 625                       | 25/10/2017                               |
        Then the data lock status of the ILR in 03/12/2017 is:
            | Type                | 08/17 - 09/17 | 10/17 onwards |
            | Matching commitment | ABC           |               |
        And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 450   | 450   | 450   |
            | Provider Earned from SFA   | 1000  | 1000  | 450   | 450   | 450   |
            | Provider Earned from ABC   | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ   | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA       |       | 1000  | 1000  | 0     | 0     |
            | Payment due from ABC       | 0     | 0     | 0     | 0     | 0     |
            | Payment due from XYZ       | 0     | 0     | 0     | 0     | 0     |
            | ABC Levy account debited   | 0     | 1000  | 1000  | 0     | 0     |
            | XYZ Levy account debited   | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy employer budget   | 1000  | 1000  | 0     | 0     | 0     |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     |


    Scenario: Earnings and payments for a DAS learner, levy available, where a learner switches from DAS to Non Das employer at the end of month
        Given The learner is programme only DAS
        And the ABC has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type    | ILR employment start date |
            | ABC      | DAS     | 03/08/2017                |
            | XYZ      | Non DAS | 03/11/2017                |
        And the following commitments exist on 03/12/2017:
            | Employer | commitment Id | version Id | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | ABC      | 1             | 1          | learner a | 03/08/2017 | 04/08/2018 | 15000        | active    | 03/08/2017     | 02/11/2017   |
            | ABC      | 1             | 2          | learner a | 03/08/2017 | 04/08/2018 | 15000        | withdrawn | 03/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | learner a | 03/08/2017 | 04/08/2018       |                 | continuing        | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            | 4500                    | 03/11/2017                             | 1125                      | 03/11/2017                               |
        And the Contract type in the ILR is:
            | contract type | date from  | date to    |
            | DAS           | 03/08/2017 | 02/11/2017 |
            | Non DAS       | 03/11/2017 | 04/08/2018 |
        Then the data lock status of the ILR in 03/12/2017 is:
            | Type                | 08/17 - 10/17 | 11/17 onwards |
            | Matching commitment | ABC           |               |
        And the provider earnings and payments break down as follows:
            | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total          | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from SFA       | 1000  | 1000  | 1000  | 450   | 450   |
            | Provider Earned from ABC       | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ       | 0     | 0     | 0     | 50    | 50    |
            | Provider Paid by SFA           | 0     | 1000  | 1000  | 1000  | 450   |
            | Payment due from ABC           | 0     | 0     | 0     | 0     | 0     |
            | Payment due from XYZ           | 0     | 0     | 0     | 0     | 50    |
            | ABC Levy account debited       | 0     | 1000  | 1000  | 1000  | 0     |
            | SFA Levy employer budget       | 1000  | 1000  | 1000  | 0     | 0     |
            | SFA Levy co-funding budget     | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget | 0     | 0     | 0     | 450   | 450   |


#change of employer mid month


    Scenario:  Earnings and payments for a DAS learner, levy available, commitment entered for a new employer in the middle of the month, and there is a change to the employer and negotiated cost in the middle of a month in the ILR
        Given The learner is programme only DAS
        And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type | ILR employment start date |
            | ABC      | DAS  | 01/08/2017                |
            | XYZ      | DAS  | 15/11/2017                |
        And the following commitments exist on 03/12/2017:
            | Employer | commitment Id | version Id | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | ABC      | 1             | 1          | learner a | 01/08/2017 | 28/08/2018 | 15000        | active    | 01/08/2017     | 14/11/2017   |
            | ABC      | 1             | 2          | learner a | 01/08/2017 | 28/08/2018 | 15000        | withdrawn | 15/11/2017     |              |
            | XYZ      | 2             | 1          | learner a | 15/11/2017 | 28/08/2018 | 5625         | active    | 15/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | learner a | 01/08/2017 | 28/08/2018       |                 | continuing        | 12000                | 01/08/2017                          | 3000                   | 01/08/2017                            | 5000                    | 15/11/2017                             | 625                       | 15/11/2017                               |
        Then the data lock status of the ILR in 03/12/2017 is:
            | Type                | 08/17 - 10/17 | 11/17 onwards |
            | Matching commitment | ABC           | XYZ           |
        And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from ABC   | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ   | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA       |       | 1000  | 1000  | 1000  | 500   |
            | Payment due from ABC       | 0     | 0     | 0     | 0     | 0     |
            | Payment due from XYZ       | 0     | 0     | 0     | 0     | 0     |
            | ABC Levy account debited   | 0     | 1000  | 1000  | 1000  | 0     |
            | XYZ Levy account debited   | 0     | 0     | 0     | 0     | 500   |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 500   | 500   |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     |


    Scenario:  Earnings and payments for a DAS learner, levy available, commitment entered for a new employer in the middle of the month with gap, and there is a change to the employer and negotiated cost in the middle of a month in the ILR
        Given The learner is programme only DAS
        And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type | ILR employment start date |
            | ABC      | DAS  | 01/08/2017                |
            | XYZ      | DAS  | 15/11/2017                |
        And the following commitments exist on 03/12/2017:
            | Employer | commitment Id | version Id | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | ABC      | 1             | 1          | learner a | 01/08/2017 | 28/08/2018 | 15000        | active    | 01/08/2017     | 14/11/2017   |
            | ABC      | 1             | 2          | learner a | 01/08/2017 | 28/08/2018 | 15000        | withdrawn | 15/11/2017     |              |
            | XYZ      | 2             | 1          | learner a | 15/11/2017 | 28/08/2018 | 5625         | active    | 15/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | learner a | 01/08/2017 | 28/08/2018       |                 | continuing        | 12000                | 01/08/2017                          | 3000                   | 01/08/2017                            | 5000                    | 25/11/2017                             | 625                       | 25/11/2017                               |
        Then the data lock status of the ILR in 03/12/2017 is:
            | Type                | 08/17 - 10/17 | 11/17 onwards |
            | Matching commitment | ABC           | XYZ           |
        And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from ABC   | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ   | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA       |       | 1000  | 1000  | 1000  | 500   |
            | Payment due from ABC       | 0     | 0     | 0     | 0     | 0     |
            | Payment due from XYZ       | 0     | 0     | 0     | 0     | 0     |
            | ABC Levy account debited   | 0     | 1000  | 1000  | 1000  | 0     |
            | XYZ Levy account debited   | 0     | 0     | 0     | 0     | 500   |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 500   | 500   |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     |


    Scenario:  Earnings and payments for a DAS learner, levy available, commitment entered for a new employer in the middle of the month and ILR file is submitted before new price episode, and there is a change to the employer and negotiated cost in the middle of a month in the ILR
        Given The learner is programme only DAS
        And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type | ILR employment start date |
            | ABC      | DAS  | 01/08/2017                |
            | XYZ      | DAS  | 15/11/2017                |
        And the following commitments exist on 03/12/2017:
            | Employer | commitment Id | version Id | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | ABC      | 1             | 1          | learner a | 01/08/2017 | 28/08/2018 | 15000        | active    | 01/08/2017     | 14/11/2017   |
            | ABC      | 1             | 2          | learner a | 01/08/2017 | 28/08/2018 | 15000        | withdrawn | 15/11/2017     |              |
            | XYZ      | 2             | 1          | learner a | 15/11/2017 | 28/08/2018 | 5625         | active    | 15/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | learner a | 01/08/2017 | 28/08/2018       |                 | continuing        | 12000                | 01/08/2017                          | 3000                   | 01/08/2017                            | 5000                    | 05/11/2017                             | 625                       | 25/11/2017                               |
        Then the data lock status of the ILR in 03/12/2017 is:
            | Type                | 08/17 - 10/17 | 11/17 onwards |
            | Matching commitment | ABC           |               |
        And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from ABC   | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ   | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | 1000  | 0     |
            | Payment due from ABC       | 0     | 0     | 0     | 0     | 0     |
            | Payment due from XYZ       | 0     | 0     | 0     | 0     | 0     |
            | ABC Levy account debited   | 0     | 1000  | 1000  | 1000  | 0     |
            | XYZ Levy account debited   | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 0     | 0     |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     |


    Scenario: Apprentice changes from a non-DAS to DAS employer, levy is available for the DAS employer
        Given The learner is programme only DAS
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type    | ILR employment start date |
            | ABC      | Non DAS | 06/08/2017                |
            | XYZ      | DAS     | 01/04/2018                |
        And the following commitments exist on 03/04/2017:
            | Employer | commitment Id | version Id | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | XYZ      | 1             | 1          | learner a | 01/04/2018 | 01/08/2018 | 3500         | active    | 01/04/2018     |              |
        When an ILR file is submitted with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | learner a | 06/08/2017 | 08/08/2018       |                 | continuing        | 5000                 | 06/08/2017                          | 1000                   | 06/08/2017                            | 2500                    | 01/04/2018                             | 1000                      | 01/04/2018                               |
        And the Contract type in the ILR is:
            | contract type | date from  | date to    |
            | Non DAS       | 06/08/2017 | 31/03/2018 |
            | DAS           | 01/04/2018 | 08/08/2018 |
        Then the data lock status will be as follows:
            | type                | 08/17 - 03/18 | 04/18 onwards |  
            | matching commitment |               | XYZ           |
        And the provider earnings and payments break down as follows:
            | Type                           | 08/17 | 09/17 | 10/17 | ... | 03/18 | 04/18 | 05/18 | 06/18 | 07/18 | 08/18 |
            | Provider Earned Total          | 400   | 400   | 400   | ... | 400   | 700   | 700   | 700   | 700   | 0     |
            | Provider Earned from SFA       | 360   | 360   | 360   | ... | 360   | 700   | 700   | 700   | 700   | 0     |
            | Provider Earned from ABC       | 40    | 40    | 40    | ... | 40    | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ       | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA           | 0     | 360   | 360   | ... | 360   | 360   | 700   | 700   | 700   | 700   |
            | Payment due from ABC           | 0     | 40    | 40    | ... | 40    | 40    | 0     | 0     | 0     | 0     |
            | Payment due from XYZ           | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | 0     |
            | XYZ Levy account debited       | 0     | 0     | 0     | ... | 0     | 0     | 700   | 700   | 700   | 700   |
            | SFA Levy employer budget       | 0     | 0     | 0     | ... | 0     | 700   | 700   | 700   | 700   | 0     |
            | SFA Levy co-funding budget     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget | 360   | 360   | 360   | ... | 360   | 0     | 0     | 0     | 0     | 0     |


    Scenario: Earnings and payments for a DAS learner, levy available, where a total cost changes during the programme and ILR is submitted late
        Given The learner is programme only DAS 
        And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type | ILR employment start date |
            | ABC      | DAS  | 03/08/2017                |
            | XYZ      | DAS  | 03/11/2017                |
        And the following commitments exist on 03/12/2017:
            | Employer | commitment Id | version Id | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | ABC      | 1             | 1          | learner a | 01/08/2017 | 31/08/2018 | 15000        | active    | 01/08/2017     | 31/10/2017   |
            | ABC      | 1             | 2          | learner a | 01/08/2017 | 31/08/2018 | 15000        | cancelled | 01/11/2017     |              |
            | XYZ      | 2             | 1          | learner a | 01/11/2017 | 31/08/2018 | 5625         | active    | 01/11/2017     |              |
        When an ILR file is submitted for the first time on 28/11/17 with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | learner a | 03/08/2017 | 04/08/2018       |                 | continuing        | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            | 5000                    | 03/11/2017                             | 625                       | 03/11/2017                               |
        Then the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from ABC   | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ   | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA       | 0     | 0     | 0     | 0     | 3500  |
            | Payment due from ABC       | 0     | 0     | 0     | 0     | 0     |
            | Payment due from XYZ       | 0     | 0     | 0     | 0     | 0     |
            | ABC Levy account debited   | 0     | 0     | 0     | 0     | 3000  |
            | XYZ Levy account debited   | 0     | 0     | 0     | 0     | 500   |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 500   | 500   |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     |


            
    Scenario: Earnings and payments for a DAS learner, levy available, and they have a break in learning at the end of a month and return at the start of a later month with a different employer
    
        Given the apprenticeship funding band maximum is 17000
        Given The learner is programme only DAS
        And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type | ILR employment start date |
            | ABC      | DAS  | 01/08/2017                |
            | XYZ      | DAS  | 01/01/2018                |
        And the following commitments exist on 03/12/2017:
            | Employer | commitment Id | version Id | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | ABC      | 1             | 1          | learner a | 01/08/2017 | 31/08/2018 | 15000        | active    | 01/08/2017     | 31/10/2017   |
            | ABC      | 1             | 2          | learner a | 01/08/2017 | 31/08/2018 | 15000        | cancelled | 01/11/2017     |              |
            | XYZ      | 2             | 1          | learner a | 01/01/2018 | 31/10/2018 | 5625         | active    | 01/01/2018     |              |
        When an ILR file is submitted with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | learner a | 01/08/2017 | 04/08/2018       | 31/10/2017      | withdrawn         | 12000                | 01/08/2017                          | 3000                   | 01/08/2017                            |                         |                                        |                           |                                          |
            | learner a | 01/01/2018 | 04/10/2018       |                 | continuing        |                      |                                     |                        |                                       | 5000                    | 01/01/2018                             | 625                       | 01/01/2018                               |
           
        Then the data lock status of the ILR in 03/12/2017 is:
            | Type                | 08/17 - 10/17 | 01/18 onwards |
            | Matching commitment | ABC           | XYZ           |
        And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 | 02/18 | 03/18 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 0     | 0     | 500   | 500   | 500   |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | 0     | 0     | 500   | 500   | 500   |
            | Provider Earned from ABC   | 0     | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ   | 0     | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA       |       | 1000  | 1000  | 1000  | 0     | 0     | 500   | 500   |
            | Payment due from ABC       | 0     | 0     | 0     | 0     | 0     | 0     | 0     | 0     | 
            | Payment due from XYZ       | 0     | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
            | ABC Levy account debited   | 0     | 1000  | 1000  | 1000  | 0     | 0     | 0     | 0     |
            | XYZ Levy account debited   | 0     | 0     | 0     | 0     | 0     | 0     | 500   | 500   |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 0     | 0     | 500   | 500   | 500   |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     | 0     | 0     | 0     |

            
    Scenario: Earnings and payments for a DAS learner, levy available, and they have a break in learning in the middle of a month and return in the middle of a later month with a different employer

        Given The learner is programme only DAS
        And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type | ILR employment start date |
            | ABC      | DAS  | 01/08/2017                |
            | XYZ      | DAS  | 01/01/2018                |
        And the following commitments exist on 03/12/2017:
            | Employer | commitment Id | version Id | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | ABC      | 1             | 1          | learner a | 01/08/2017 | 31/08/2018 | 15000        | active    | 01/08/2017     | 31/10/2017   |
            | ABC      | 1             | 2          | learner a | 01/08/2017 | 31/08/2018 | 15000        | cancelled | 01/11/2017     |              |
            | XYZ      | 2             | 1          | learner a | 01/01/2018 | 31/10/2018 | 5625         | active    | 01/01/2018     |              |
        When an ILR file is submitted with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | learner a | 03/08/2017 | 04/08/2018       | 18/11/2017      | withdrawn         | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            |                         |                                        |                           |                                          |
            | learner a | 11/01/2018 | 04/10/2018       |                 | continuing        |                      |                                     |                        |                                       | 5000                    | 11/01/2018                             | 625                       | 11/01/2018                               |
         
        Then the data lock status of the ILR in 03/12/2017 is:
            | Type                | 08/17 - 10/17 | 01/18 onwards |
            | Matching commitment | ABC           | XYZ           |
        And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 | 02/18 | 03/18 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 0     | 0     | 500   | 500   | 500   |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | 0     | 0     | 500   | 500   | 500   |
            | Provider Earned from ABC   | 0     | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ   | 0     | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | 1000  | 0     | 0     | 500   | 500   |
            | Payment due from ABC       | 0     | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
            | Payment due from XYZ       | 0     | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
            | ABC Levy account debited   | 0     | 1000  | 1000  | 1000  | 0     | 0     | 0     | 0     |
            | XYZ Levy account debited   | 0     | 0     | 0     | 0     | 0     | 0     | 500   | 500   |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 0     | 0     | 500   | 500   | 500   |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     | 0     | 0     | 0     |

Scenario: Earnings and payments for a DAS learner, levy available, and they have a break in learning in the middle of a month and return in the middle of a later month with a different employer - before the second commitment is in place

        Given The learner is programme only DAS
        And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type | ILR employment start date |
            | ABC      | DAS  | 01/08/2017                |
            | XYZ      | DAS  | 01/01/2018                |
        And the following commitments exist on 03/12/2017:
            | Employer | commitment Id | version Id | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | ABC      | 1             | 1          | learner a | 01/08/2017 | 31/08/2018 | 15000        | active    | 01/08/2017     | 31/10/2017   |
            | ABC      | 1             | 2          | learner a | 01/08/2017 | 31/08/2018 | 15000        | cancelled | 01/11/2017     |              |
            | XYZ      | 2             | 1          | learner a | 01/01/2018 | 31/10/2018 | 5625         | active    | 01/01/2018     |              |
        When an ILR file is submitted with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | learner a | 03/08/2017 | 04/08/2018       | 18/11/2017      | withdrawn         | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            |                         |                                        |                           |                                          |
            | learner a | 21/12/2017 | 04/09/2018       |                 | continuing        |                      |                                     |                        |                                       | 5000                    | 21/12/2017                             | 625                       | 21/12/2017                               |
           
        Then the data lock status of the ILR in 03/12/2017 is:
            | Type                | 08/17 - 10/17 | 12/17 onwards |
            | Matching commitment | ABC           |               |
        And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 | 02/18 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 0     | 500   | 500   | 500   |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | 0     | 0     | 0     | 0     |
            | Provider Earned from ABC   | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ   | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | 1000  | 0     | 0     | 0     |
            | Payment due from ABC       | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
            | Payment due from XYZ       | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
            | ABC Levy account debited   | 0     | 1000  | 1000  | 1000  | 0     | 0     | 0     |
            | XYZ Levy account debited   | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 0     | 0     | 0     | 0     |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     | 0     | 0     |



#Learner changes employer - incentives earned
Scenario: 1 learner aged 16-18, levy available, changes employer, earns incentive payment in the transfer month - and the date at which the incentive is earned is before the transfer date 
    
        Given The learner is programme only DAS
        And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type | ILR employment start date |
            | ABC      | DAS  | 06/08/2017                |
            | XYZ      | DAS  | 15/11/2017                |
        And the following commitments exist:
            | Employer | commitment Id | version Id | Provider   | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | ABC      | 1             | 1          | provider a | learner a | 01/08/2017 | 01/08/2018 | 7500         | active    | 01/08/2017     | 14/11/2017   |
            | ABC      | 1             | 2          | provider a | learner a | 01/08/2017 | 01/08/2018 | 7500         | withdrawn | 15/11/2017     |              |
            | XYZ      | 2             | 1          | provider a | learner a | 15/11/2017 | 01/08/2018 | 5625         | active    | 15/11/2017     |              |
            
        When an ILR file is submitted with the following data:
            | Provider   | learner type             | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | provider a | 16-18 programme only DAS | learner a | 06/08/2017 | 08/08/2018       |                 | continuing        | 6000                 | 06/08/2017                          | 1500                   | 06/08/2017                            | 4000                    | 15/11/2017                             | 1625                      | 15/11/2017                               |
            
        Then the data lock status will be as follows:
            | type                | 08/17 - 10/17 | 11/17 onwards |  
            | matching commitment | 1             | 2             |
        
        And the earnings and payments break down for provider a is as follows:
            | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
            | Provider Earned Total               | 500   | 500   | 500   | 1500  | 500   | 500   |
            | Provider Earned from SFA            | 500   | 500   | 500   | 1500  | 500   | 500   |
            | Provider Earned from ABC            | 0     | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ            | 0     | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA                | 0     | 500   | 500   | 500   | 1500  | 500   |
            | Payment due from ABC                | 0     | 0     | 0     | 0     | 0     | 0     |
            | Payment due from XYZ                | 0     | 0     | 0     | 0     | 0     | 0     |
            | ABC Levy account debited            | 0     | 500   | 500   | 500   | 0     | 0     |
            | XYZ Levy account debited            | 0     | 0     | 0     | 0     | 500   | 500   |
            | SFA Levy employer budget            | 500   | 500   | 500   | 500   | 500   | 500   |
            | SFA Levy co-funding budget          | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget      | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy additional payments budget | 0     | 0     | 0     | 1000  | 0     | 0     |
            
        And the transaction types for the payments for provider a are:
            | Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
            | On-program                   | 500   | 500   | 500   | 500   | 500   |
            | Completion                   | 0     | 0     | 0     | 0     | 0     |
            | Balancing                    | 0     | 0     | 0     | 0     | 0     |
            | Employer ABC 16-18 incentive | 0     | 0     | 0     | 500   | 0     |
            | Employer XYZ 16-18 incentive | 0     | 0     | 0     | 0     | 0     |
            | Provider 16-18 incentive     | 0     | 0     | 0     | 500   | 0     |


Scenario: 1 learner aged 16-18, levy available, changes employer, earns incentive payment in the commitment transfer month - and the employer transfer is recorded on the ILR in a later month
    
        Given The learner is programme only DAS
        And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type | ILR employment start date |
            | ABC      | DAS  | 06/08/2017                |
            | XYZ      | DAS  | 15/12/2017                |
        And the following commitments exist:
            | Employer | commitment Id | version Id | Provider   | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | ABC      | 1             | 1          | provider a | learner a | 01/08/2017 | 01/08/2018 | 7500         | active    | 01/08/2017     | 14/11/2017   |
            | ABC      | 1             | 2          | provider a | learner a | 01/08/2017 | 01/08/2018 | 7500         | withdrawn | 15/11/2017     |              |
            | XYZ      | 2             | 1          | provider a | learner a | 15/11/2017 | 01/08/2018 | 5625         | active    | 15/11/2017     |              |
            
        When an ILR file is submitted with the following data:
            | Provider   | learner type             | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | provider a | 16-18 programme only DAS | learner a | 06/08/2017 | 08/08/2018       |                 | continuing        | 6000                 | 06/08/2017                          | 1500                   | 06/08/2017                            | 4000                    | 15/12/2017                             | 1625                      | 15/12/2017                               |

        Then the data lock status will be as follows:
            | type                | 08/17 - 10/17 | 12/17 onwards |  
            | matching commitment | 1             | 2             |
        
        And the earnings and payments break down for provider a is as follows:
            | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17  | 01/18  |
            | Provider Earned Total               | 500   | 500   | 500   | 1500  | 562.50 | 562.50 |
            | Provider Earned from SFA            | 500   | 500   | 500   | 1000  | 562.50 | 562.50 |
            | Provider Earned from ABC            | 0     | 0     | 0     | 0     | 0      | 0      |
            | Provider Earned from XYZ            | 0     | 0     | 0     | 0     | 0      | 0      |
            | Provider Paid by SFA                | 0     | 500   | 500   | 500   | 1000   | 562.50 |
            | Payment due from ABC                | 0     | 0     | 0     | 0     | 0      | 0      |
            | Payment due from XYZ                | 0     | 0     | 0     | 0     | 0      | 0      |
            | ABC Levy account debited            | 0     | 500   | 500   | 500   | 0      | 0      |
            | XYZ Levy account debited            | 0     | 0     | 0     | 0     | 0      | 562.50 |
            | SFA Levy employer budget            | 500   | 500   | 500   | 0     | 562.50 | 562.50 |
            | SFA Levy co-funding budget          | 0     | 0     | 0     | 0     | 0      | 0      |
            | SFA non-Levy co-funding budget      | 0     | 0     | 0     | 0     | 0      | 0      |
            | SFA Levy additional payments budget | 0     | 0     | 0     | 1000  | 0      | 0      |
            
         And the transaction types for the payments for provider a are:
            | Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18  |
            | On-program                   | 500   | 500   | 500   | 0     | 562.50 |
            | Completion                   | 0     | 0     | 0     | 0     | 0      |
            | Balancing                    | 0     | 0     | 0     | 0     | 0      |
            | Employer ABC 16-18 incentive | 0     | 0     | 0     | 500   | 0      |
            | Employer XYZ 16-18 incentive | 0     | 0     | 0     | 0     | 0      |
            | Provider 16-18 incentive     | 0     | 0     | 0     | 500   | 0      |
          

Scenario: 1 learner aged 16-18, levy available, changes employer, earns incentive payment in the commitment transfer month - and the ILR transfer happens at an earlier point than the commitment  changes 
 
        Given The learner is programme only DAS
        And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer | Type | ILR employment start date |
            | ABC      | DAS  | 06/08/2017                |
            | XYZ      | DAS  | 09/11/2017                |
        And the following commitments exist:
            | Employer | commitment Id | version Id | Provider   | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | ABC      | 1             | 1          | provider a | learner a | 01/08/2017 | 01/08/2018 | 7500         | active    | 01/08/2017     | 14/11/2017   |
            | ABC      | 1             | 2          | provider a | learner a | 01/08/2017 | 01/08/2018 | 7500         | withdrawn | 15/11/2017     |              |
            | XYZ      | 2             | 1          | provider a | learner a | 15/11/2017 | 01/08/2018 | 5625         | active    | 15/11/2017     |              |
       
        When an ILR file is submitted with the following data:
            | Provider   | learner type             | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | provider a | 16-18 programme only DAS | learner a | 06/08/2017 | 08/08/2018       |                 | continuing        | 6000                 | 06/08/2017                          | 1500                   | 06/08/2017                            | 4000                    | 09/11/2017                             | 1625                      | 09/11/2017                               |
    
        Then the data lock status will be as follows:
            | type                | 08/17 - 10/17 | 11/17 onwards |
            | matching commitment | 1             |               |
        
        And the earnings and payments break down for provider a is as follows:
            | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
            | Provider Earned Total               | 500   | 500   | 500   | 1500  | 500   | 500   |
            | Provider Earned from SFA            | 500   | 500   | 500   | 1000  | 0     | 0     |
            | Provider Earned from ABC            | 0     | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ            | 0     | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA                | 0     | 500   | 500   | 500   | 1000  | 0     |
            | Payment due from ABC                | 0     | 0     | 0     | 0     | 0     | 0     |
            | Payment due from XYZ                | 0     | 0     | 0     | 0     | 0     | 0     |
            | ABC Levy account debited            | 0     | 500   | 500   | 500   | 0     | 0     |
            | XYZ Levy account debited            | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy employer budget            | 500   | 500   | 500   | 0     | 0     | 0     |
            | SFA Levy co-funding budget          | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget      | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy additional payments budget | 0     | 0     | 0     | 1000  | 0     | 0     |
            
         And the transaction types for the payments for provider a are:
			| Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
			| On-program                   | 500   | 500   | 500   | 0     | 0     |
			| Completion                   | 0     | 0     | 0     | 0     | 0     |
			| Balancing                    | 0     | 0     | 0     | 0     | 0     |
			| Employer ABC 16-18 incentive | 0     | 0     | 0     | 500   | 0     |
			| Employer XYZ 16-18 incentive | 0     | 0     | 0     | 0     | 0     |
			| Provider 16-18 incentive     | 0     | 0     | 0     | 500   | 0     |


@LearnerChangesEmployerGapInCommitments
#AC1
Scenario:AC1- Provider earnings and payments where learner changes employer and there is a gap between commitments - provider receives payment during the gap as they amend the ACT code and employment status code correctly.
		Given The learner is programme only DAS
        And the apprenticeship funding band maximum is 17000
		And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer    | Type    | ILR employment start date |	
            | ABC         | DAS     | 03/08/2017                |
            | No employer | Non-DAS | 03/10/2017                |
            | XYZ         | DAS     | 03/11/2017                |
        And the following commitments exist:
			| commitment Id | version Id | Employer | Provider   | ULN       | price effective date | planned end date | agreed price | status    | effective from | effective to |
			| 1             | 1          | ABC      | provider a | learner a | 01/08/2017           | 04/08/2018       | 15000        | Active    | 01/08/2017     | 02/10/2017   |
			| 1             | 2          | ABC      | provider a | learner a | 01/08/2017           | 04/08/2018       | 15000        | Withdrawn | 03/10/2017     |              |
			| 2             | 1          | XYZ      | provider a | learner a | 01/11/2017           | 04/08/2018       | 5625         | Active    | 01/11/2017     |              |
        When an ILR file is submitted with the following data:
			| ULN       | Provider   | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
			| learner a | provider a | 03/08/2017 | 04/08/2018       |                 | continuing        | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            | 4500                    | 03/11/2017                             | 1125                      | 03/11/2017                               |
		And the Contract type in the ILR is:
            | contract type | date from  | date to    |
            | DAS           | 03/08/2017 | 02/10/2017 |
            | Non DAS       | 03/10/2017 | 02/11/2017 |
            | DAS           | 03/11/2017 | 04/08/2018 |
        And the employment status in the ILR is:
			| Employer | Employment Status      | Employment Status Applies |
			| ABC      | in paid employment     | 02/08/2017                |
			|          | not in paid employment | 03/10/2017                |
			| XYZ      | in paid employment     | 03/11/2017                |         
     
        Then the data lock status will be as follows:
			| type                | 08/17 - 09/17 | 10/17 | 11/17 onwards |
			| matching commitment | 1             | N/A   | 2             |
        And the provider earnings and payments break down as follows:
            | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total          | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Paid by SFA           | 0     | 1000  | 1000  | 1000  | 500   |
            | ABC Levy account debited       | 0     | 1000  | 1000  | 0     | 0     |
            | XYZ Levy account debited       | 0     | 0     | 0     | 0     | 500   |
            | SFA Levy employer budget       | 1000  | 1000  | 0     | 500   | 500   |
            | SFA Levy co-funding budget     | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget | 0     | 0     | 1000  | 0     | 0     |


			
@LearnerChangesEmployerGapInCommitments
#AC2
Scenario:AC2- Provider earnings and payments where learner changes employer and there is a gap between commitments - provider receives no payment during the gap as they do not change the ACT code or employment status on the ILR
		Given The learner is programme only DAS
        And the apprenticeship funding band maximum is 17000
		And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer    | Type    | ILR employment start date |	
            | ABC         | DAS     | 03/08/2017                |
            | No employer | Non-DAS | 03/10/2017                |
            | XYZ         | DAS     | 03/11/2017                |
        And the following commitments exist:
			| commitment Id | version Id | Employer | Provider   | ULN       | price effective date | planned end date | agreed price | status    | effective from | effective to |
			| 1             | 1          | ABC      | provider a | learner a | 01/08/2017           | 04/08/2018       | 15000        | Active    | 01/08/2017     | 02/10/2017   |
			| 1             | 2          | ABC      | provider a | learner a | 01/08/2017           | 04/08/2018       | 15000        | Withdrawn | 03/10/2017     |              |
			| 2             | 1          | XYZ      | provider a | learner a | 01/11/2017           | 04/08/2018       | 5625         | Active    | 01/11/2017     |              |
        When an ILR file is submitted with the following data:
			| ULN       | Provider   | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
			| learner a | provider a | 03/08/2017 | 04/08/2018       |                 | continuing        | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            | 4500                    | 03/11/2017                             | 1125                      | 03/11/2017                               |
		And the Contract type in the ILR is:
            | contract type | date from  | date to    |
            | DAS           | 03/08/2017 | 04/08/2018 |
           
        And the employment status in the ILR is:
			| Employer | Employment Status      | Employment Status Applies |
			| ABC      | in paid employment     | 02/08/2017                |
	    Then the data lock status will be as follows:
			| type                | 08/17 - 10/17 | 11/17 onwards |
			| matching commitment | 1             | 2             |
		And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | 0     | 500   |
            | ABC Levy account debited   | 0     | 1000  | 1000  | 0     | 0     |
            | XYZ Levy account debited   | 0     | 0     | 0     | 0     | 500   |
            | SFA Levy employer budget   | 1000  | 1000  | 0     | 500   | 500   |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     | 

						
@LearnerChangesEmployerGapInCommitments
#AC3
Scenario:AC3- Provider earnings and payments where learner changes employer and there is a gap between commitments - provider does not receive payment during the gap as they amend the ACT code but do not amend the employment status code correctly.
		Given The learner is programme only DAS
        And the apprenticeship funding band maximum is 17000
		And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer    | Type    | ILR employment start date |	
            | ABC         | DAS     | 03/08/2017                |
            | No employer | Non-DAS | 03/10/2017                |
            | XYZ         | DAS     | 03/11/2017                |
        And the following commitments exist:
			| commitment Id | version Id | Employer | Provider   | ULN       | price effective date | planned end date | agreed price | status    | effective from | effective to |
			| 1             | 1          | ABC      | provider a | learner a | 01/08/2017           | 04/08/2018       | 15000        | Active    | 01/08/2017     | 02/10/2017   |
			| 1             | 2          | ABC      | provider a | learner a | 01/08/2017           | 04/08/2018       | 15000        | Withdrawn | 03/10/2017     |              |
			| 2             | 1          | XYZ      | provider a | learner a | 01/11/2017           | 04/08/2018       | 5625         | Active    | 01/11/2017     |              |
        When an ILR file is submitted with the following data:
			| ULN       | Provider   | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
			| learner a | provider a | 03/08/2017 | 04/08/2018       |                 | continuing        | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            | 4500                    | 03/11/2017                             | 1125                      | 03/11/2017                               |
		And the Contract type in the ILR is:
            | contract type | date from  | date to    |
            | DAS           | 03/08/2017 | 02/10/2017 |
            | Non DAS       | 03/10/2017 | 02/11/2017 |
            | DAS           | 03/11/2017 | 04/08/2018 |
        And the employment status in the ILR is:
			| Employer | Employment Status      | Employment Status Applies |
			| ABC      | in paid employment     | 02/08/2017                |
	     Then the data lock status will be as follows:
			| type                | 08/17 - 09/17 | 10/17 | 11/17 onwards |
			| matching commitment | 1             | N/A   | 2             |
	    And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | 0     | 500   |
            | ABC Levy account debited   | 0     | 1000  | 1000  | 0     | 0     |
            | XYZ Levy account debited   | 0     | 0     | 0     | 0     | 500   |
            | SFA Levy employer budget   | 1000  | 1000  | 0     | 500   | 500   |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     |

			
@LearnerChangesEmployerGapInCommitments
#AC4
Scenario: AC4-Provider earnings and payments where learner changes employer and there is a gap between commitments - provider does not receive payment during the gap as they amend the employment status code correctly but do not amend the ACT code.

		Given The learner is programme only DAS
        And the apprenticeship funding band maximum is 17000
		And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer    | Type    | ILR employment start date |	
            | ABC         | DAS     | 03/08/2017                |
            | No employer | Non-DAS | 03/10/2017                |
            | XYZ         | DAS     | 03/11/2017                |
        And the following commitments exist:
			| commitment Id | version Id | Employer | Provider   | ULN       | price effective date | planned end date | agreed price | status    | effective from | effective to |
			| 1             | 1          | ABC      | provider a | learner a | 01/08/2017           | 04/08/2018       | 15000        | Active    | 01/08/2017     | 02/10/2017   |
			| 1             | 2          | ABC      | provider a | learner a | 01/08/2017           | 04/08/2018       | 15000        | Withdrawn | 03/10/2017     |              |
			| 2             | 1          | XYZ      | provider a | learner a | 01/11/2017           | 04/08/2018       | 5625         | Active    | 01/11/2017     |              |
        When an ILR file is submitted with the following data:
			| ULN       | Provider   | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
			| learner a | provider a | 03/08/2017 | 04/08/2018       |                 | continuing        | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            | 4500                    | 03/11/2017                             | 1125                      | 03/11/2017                               |
		And the Contract type in the ILR is:
            | contract type | date from  | date to    |
            | DAS           | 03/08/2017 | 04/08/2018 |
       
         And the employment status in the ILR is:
			| Employer | Employment Status      | Employment Status Applies |
			| ABC      | in paid employment     | 02/08/2017                |
			|          | not in paid employment | 03/10/2017                |
			| XYZ      | in paid employment     | 03/11/2017                |
	     Then the data lock status will be as follows:
			| type                | 08/17 - 10/17 | 11/17 onwards |
			| matching commitment | 1             | 2             |
		And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | 0     | 500   |
            | ABC Levy account debited   | 0     | 1000  | 1000  | 0     | 0     |
            | XYZ Levy account debited   | 0     | 0     | 0     | 0     | 500   |
            | SFA Levy employer budget   | 1000  | 1000  | 0     | 500   | 500   |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     |


			

@LearnerChangesEmployerGapInCommitments
#AC5
Scenario:AC5-Provider earnings and payments where learner changes employer and there is a gap of more than 12 weeks between commitments - provider does not receive more than 12 weeks of payments during the gap.
		Given The learner is programme only DAS
        And the apprenticeship funding band maximum is 17000
		And the ABC has a levy balance > agreed price for all months
        And the XYZ has a levy balance > agreed price for all months
        And the learner changes employers
            | Employer    | Type    | ILR employment start date |	
            | ABC         | DAS     | 03/08/2017                |
            | No employer | Non-DAS | 03/10/2017                |
            | XYZ         | DAS     | 03/03/2018                |
        And the following commitments exist:
			| commitment Id | version Id | Employer | Provider   | ULN       | price effective date | planned end date | agreed price | status    | effective from | effective to |
			| 1             | 1          | ABC      | provider a | learner a | 01/08/2017           | 04/08/2018       | 15000        | Active    | 01/08/2017     | 02/10/2017   |
			| 1             | 2          | ABC      | provider a | learner a | 01/08/2017           | 04/08/2018       | 15000        | Withdrawn | 03/10/2017     |              |
			| 2             | 1          | XYZ      | provider a | learner a | 01/11/2017           | 04/08/2018       | 5625         | Active    | 01/03/2018     |              |
        When an ILR file is submitted with the following data:
			| ULN       | Provider   | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
			| learner a | provider a | 03/08/2017 | 04/08/2018       |                 | continuing        | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            | 4500                    | 03/03/2018                             | 1125                      | 03/03/2018                               |
		And the Contract type in the ILR is:
            | contract type | date from  | date to    |
            | DAS           | 03/08/2017 | 02/10/2017 |
            | Non DAS       | 03/10/2017 | 02/03/2018 |
            | DAS           | 03/03/2018 | 04/08/2018 |
        And the employment status in the ILR is:
			| Employer | Employment Status      | Employment Status Applies |
			| ABC      | in paid employment     | 02/08/2017                |
			|          | not in paid employment | 03/10/2017                |
			| XYZ      | in paid employment     | 03/03/2018                |         
     
        Then the data lock status will be as follows:
			| type                | 08/17 - 09/17 | 10/17 | 11/17 onwards |
			| matching commitment | 1             | N/A   | 2             |
        And the provider earnings and payments break down as follows:
            | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 | 02/18 | 03/18 | 04/18 |
            | Provider Earned Total          | 1000  | 1000  | 1000  | 1000  | 1000  | 0     | 0     | 500   | 500   |
            | Provider Paid by SFA           | 0     | 1000  | 1000  | 1000  | 1000  | 1000  | 0     | 0     | 500   |
            | ABC Levy account debited       | 0     | 1000  | 1000  | 0     | 0     | 0     | 0     | 0     | 0     |
            | XYZ Levy account debited       | 0     | 0     | 0     | 0     | 500   | 0     | 0     | 0     | 500   |
            | SFA Levy employer budget       | 1000  | 1000  | 0     | 0     | 0     | 0     | 0     | 500   | 500   |
            | SFA Levy co-funding budget     | 0     | 0     | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget | 0     | 0     | 1000  | 1000  | 1000  | 0     | 0     | 0     | 0     |
