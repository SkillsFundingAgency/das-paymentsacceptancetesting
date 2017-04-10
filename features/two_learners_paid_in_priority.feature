@LearnersPaidInPriority
Feature: 2 learners, paid in priority order

    Background:
        Given Two learners are programme only DAS 
        And the apprenticeship funding band maximum for each learner is 17000

    Scenario: Earnings and payments for two DAS learners, levy is spent in priority order and available for both learners
        Given the employer's levy balance is:
                | 08/17 | 09/17 | 10/17 | 11/17 | ...  | 08/18 | 09/18 |
                | 2000  | 2000  | 2000  | 2000  | 2000 | 2000  | 2000  |
        And the following commitments exist on 03/12/2017:
                | priority | ULN | start date | end date   | agreed price |
                | 1        | 123 | 01/08/2017 | 28/08/2018 | 15000        |
                | 2        | 456 | 01/08/2017 | 28/08/2018 | 15000        |
        When an ILR file is submitted on 03/12/2017 with the following data:
                | ULN | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date |
                | 123 | 01/08/2017 | 28/08/2018       |                 | continuing        | 12000                | 01/08/2017                          | 3000                   | 01/08/2017                            |
                | 456 | 01/08/2017 | 28/08/2018       |                 | continuing        | 12000                | 01/08/2017                          | 3000                   | 01/08/2017                            |
        Then the provider earnings and payments break down for ULN 123 as follows:
                | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | ... | 07/18 | 08/18 |
                | Provider Earned Total          | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | Provider Earned from SFA       | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | Provider Earned from Employer  | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | Provider Paid by SFA           | 0     | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
                | Payment due from Employer      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | Levy account debited           | 0     | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
                | SFA Levy employer budget       | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | SFA Levy co-funding budget     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | SFA non-Levy co-funding budget | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        And the provider earnings and payments break down for ULN 456 as follows:
                | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | ... | 07/18 | 08/18 |
                | Provider Earned Total          | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | Provider Earned from SFA       | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | Provider Earned from Employer  | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | Provider Paid by SFA           | 0     | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
                | Payment due from Employer      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | Levy account debited           | 0     | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
                | SFA Levy employer budget       | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | SFA Levy co-funding budget     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | SFA non-Levy co-funding budget | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        And the provider earnings and payments break down as follows:
                | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | ... | 07/18 | 08/18 |
                | Provider Earned Total          | 2000  | 2000  | 2000  | 2000  | ... | 2000  | 0     |
                | Provider Earned from SFA       | 2000  | 2000  | 2000  | 2000  | ... | 2000  | 0     |
                | Provider Earned from Employer  | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | Provider Paid by SFA           | 0     | 2000  | 2000  | 2000  | ... | 2000  | 2000  |
                | Payment due from Employer      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | Levy account debited           | 0     | 2000  | 2000  | 2000  | ... | 2000  | 2000  |
                | SFA Levy employer budget       | 2000  | 2000  | 2000  | 2000  | ... | 2000  | 0     |
                | SFA Levy co-funding budget     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | SFA non-Levy co-funding budget | 0     | 0     | 0     | 0     | ... | 0     | 0     |


    Scenario: Earnings and payments for two DAS learners, levy is spent in priority order and is available for one learner only
        Given the employer's levy balance is:
                | 08/17 | 09/17 | 10/17 | 11/17 | ...  | 08/18 | 09/18 |
                | 1000  | 1000  | 1000  | 1000  | 1000 | 1000  | 1000  |
        And the following commitments exist on 03/12/2017:
                | priority | ULN | start date | end date   | agreed price |
                | 1        | 123 | 01/08/2017 | 28/08/2018 | 15000        |
                | 2        | 456 | 01/08/2017 | 28/08/2018 | 15000        |
        When an ILR file is submitted on 03/12/2017 with the following data:
                | ULN | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date |
                | 123 | 01/08/2017 | 28/08/2018       |                 | continuing        | 12000                | 01/08/2017                          | 3000                   | 01/08/2017                            |
                | 456 | 01/08/2017 | 28/08/2018       |                 | continuing        | 12000                | 01/08/2017                          | 3000                   | 01/08/2017                            |
        Then the provider earnings and payments break down for ULN 123 as follows:
                | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | ... | 07/18 | 08/18 |
                | Provider Earned Total          | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | Provider Earned from SFA       | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | Provider Earned from Employer  | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | Provider Paid by SFA           | 0     | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
                | Payment due from Employer      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | Levy account debited           | 0     | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
                | SFA Levy employer budget       | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | SFA Levy co-funding budget     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | SFA non-Levy co-funding budget | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        And the provider earnings and payments break down for ULN 456 as follows:
                | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | ... | 07/18 | 08/18 |
                | Provider Earned Total          | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | Provider Earned from SFA       | 900   | 900   | 900   | 900   | ... | 900   | 0     |
                | Provider Earned from Employer  | 100   | 100   | 100   | 100   | ... | 100   | 0     |
                | Provider Paid by SFA           | 0     | 900   | 900   | 900   | ... | 900   | 900   |
                | Payment due from Employer      | 0     | 100   | 100   | 100   | ... | 100   | 100   |
                | Levy account debited           | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | SFA Levy employer budget       | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | SFA Levy co-funding budget     | 900   | 900   | 900   | 900   | ... | 900   | 0     |
                | SFA non-Levy co-funding budget | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        And the provider earnings and payments break down as follows:
                | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | ... | 07/18 | 08/18 |
                | Provider Earned Total          | 2000  | 2000  | 2000  | 2000  | ... | 2000  | 0     |
                | Provider Earned from SFA       | 1900  | 1900  | 1900  | 1900  | ... | 1900  | 0     |
                | Provider Earned from Employer  | 100   | 100   | 100   | 100   | ... | 100   | 0     |
                | Provider Paid by SFA           | 0     | 1900  | 1900  | 1900  | ... | 1900  | 1900  |
                | Payment due from Employer      | 0     | 100   | 100   | 100   | ... | 100   | 100   |
                | Levy account debited           | 0     | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
                | SFA Levy employer budget       | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | SFA Levy co-funding budget     | 900   | 900   | 900   | 900   | ... | 900   | 0     |
                | SFA non-Levy co-funding budget | 0     | 0     | 0     | 0     | ... | 0     | 0     |


    Scenario:  Earnings and payments for two DAS learners, levy is spent in priority order and there is enough levy to fund one and a half learners
        Given the employer's levy balance is:
                | 08/17 | 09/17 | 10/17 | 11/17 | ...  | 08/18 | 09/18 |
                | 1500  | 1500  | 1500  | 1500  | 1500 | 1500  | 1500  |
        And the following commitments exist on 03/12/2017:
                | priority | ULN | start date | end date   | agreed price |
                | 1        | 123 | 01/08/2017 | 28/08/2018 | 15000        |
                | 2        | 456 | 01/08/2017 | 28/08/2018 | 15000        |
        When an ILR file is submitted on 03/12/2017 with the following data:
                | ULN | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date |
                | 123 | 01/08/2017 | 28/08/2018       |                 | continuing        | 12000                | 01/08/2017                          | 3000                   | 01/08/2017                            |
                | 456 | 01/08/2017 | 28/08/2018       |                 | continuing        | 12000                | 01/08/2017                          | 3000                   | 01/08/2017                            |
        Then the provider earnings and payments break down for ULN 123 as follows:
                | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | ... | 07/18 | 08/18 |
                | Provider Earned Total          | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | Provider Earned from SFA       | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | Provider Earned from Employer  | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | Provider Paid by SFA           | 0     | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
                | Payment due from Employer      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | Levy account debited           | 0     | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
                | SFA Levy employer budget       | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | SFA Levy co-funding budget     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
                | SFA non-Levy co-funding budget | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        And the provider earnings and payments break down for ULN 456 as follows:
                | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | ... | 07/18 | 08/18 |
                | Provider Earned Total          | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
                | Provider Earned from SFA       | 950   | 950   | 950   | 950   | ... | 950   | 0     |
                | Provider Earned from Employer  | 50    | 50    | 50    | 50    | ... | 50    | 0     |
                | Provider Paid by SFA           | 0     | 950   | 950   | 950   | ... | 950   | 950   |
                | Payment due from Employer      | 0     | 50    | 50    | 50    | ... | 50    | 50    |
                | Levy account debited           | 0     | 500   | 500   | 500   | ... | 500   | 500   |
                | SFA Levy employer budget       | 500   | 500   | 500   | 500   | ... | 500   | 0     |
                | SFA Levy co-funding budget     | 450   | 450   | 450   | 450   | ... | 450   | 0     |
                | SFA non-Levy co-funding budget | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        And the provider earnings and payments break down as follows:
                | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | ... | 07/18 | 08/18 |
                | Provider Earned Total          | 2000  | 2000  | 2000  | 2000  | ... | 2000  | 0     |
                | Provider Earned from SFA       | 1950  | 1950  | 1950  | 1950  | ... | 1950  | 0     |
                | Provider Earned from Employer  | 50    | 50    | 50    | 50    | ... | 50    | 0     |
                | Provider Paid by SFA           | 0     | 1950  | 1950  | 1950  | ... | 1950  | 1950  |
                | Payment due from Employer      | 0     | 50    | 50    | 50    | ... | 50    | 50    |
                | Levy account debited           | 0     | 1500  | 1500  | 1500  | ... | 1500  | 1500  |
                | SFA Levy employer budget       | 1500  | 1500  | 1500  | 1500  | ... | 1500  | 0     |
                | SFA Levy co-funding budget     | 450   | 450   | 450   | 450   | ... | 450   | 0     |
                | SFA non-Levy co-funding budget | 0     | 0     | 0     | 0     | ... | 0     | 0     |