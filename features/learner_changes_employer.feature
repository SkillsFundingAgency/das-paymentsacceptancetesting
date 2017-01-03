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
            | Employer | ULN       | price effective date | planned end date | actual end date | agreed price |
            | ABC      | learner a | 01/08/2017           | 31/08/2018       | 31/10/2017      | 15000        |
            | XYZ      | learner a | 01/11/2017           | 31/08/2018       |                 | 5625         |
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
            | Employer | ULN       | price effective date | planned end date | actual end date | agreed price |
            | ABC      | learner a | 01/08/2017           | 31/08/2018       | 31/10/2017      | 15000        |
            | XYZ      | learner a | 01/11/2017           | 31/08/2018       |                 | 5625         |
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
            | Employer | ULN       | price effective date | planned end date | actual end date | agreed price |
            | ABC      | learner a | 01/08/2017           | 31/08/2018       | 31/10/2017      | 15000        |
            | XYZ      | learner a | 01/11/2017           | 31/08/2018       |                 | 5625         |
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
            | Employer | ULN       | price effective date | planned end date | actual end date | agreed price |
            | ABC      | learner a | 03/08/2017           | 04/08/2018       | 02/11/2017      | 15000        |
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
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | 500   | 450   |
            | Provider Earned from ABC   | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from XYZ   | 0     | 0     | 0     | 0     | 50    |
            | Provider Paid by SFA       |       | 1000  | 1000  | 1000  | 0     |
            | Payment due from ABC       | 0     | 0     | 0     | 0     | 0     |
            | Payment due from XYZ       | 0     | 0     | 0     | 0     | 0     |
            | ABC Levy account debited   | 0     | 1000  | 1000  | 1000  | 0     |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 500   | 0     |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     |


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
            | Employer | ULN       | price effective date | planned end date | actual end date | agreed price |
            | ABC      | learner a | 01/08/2017           | 28/08/2018       | 14/11/2017      | 15000        |
            | XYZ      | learner a | 15/11/2017           | 28/08/2018       |                 | 5625         |
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
            | Employer | ULN       | price effective date | planned end date | actual end date | agreed price |
            | ABC      | learner a | 01/08/2017           | 28/08/2018       | 14/11/2017      | 15000        |
            | XYZ      | learner a | 15/11/2017           | 28/08/2018       |                 | 5625         |
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
            | Employer | ULN       | price effective date | planned end date | actual end date | agreed price |
            | ABC      | learner a | 01/08/2017           | 28/08/2018       | 14/11/2017      | 15000        |
            | XYZ      | learner a | 15/11/2017           | 28/08/2018       |                 | 5625         |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | Residual training price | Residual training price effective date | Residual assessment price | Residual assessment price effective date |
            | learner a | 01/08/2017 | 28/08/2018       |                 | continuing        | 12000                | 01/08/2017                          | 3000                   | 01/08/2017                            | 5000                    | 05/11/2017                             | 625                       | 25/11/2017                               |
        Then the data lock status of the ILR in 03/12/2017 is:
            | Type                | 08/17 - 10/17 | 11/17 onwards |
            | Matching commitment | ABC           |               |
        And a DLOCK_09 error message will be produced
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