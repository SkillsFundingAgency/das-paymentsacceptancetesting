@ChangeInCircumstances
Feature: Provider earnings and payments where learner changes apprenticeship standard or there is a change to the negotiated price at the end of a month, remaining with the same employer and provider

    Background:
        Given The learner is programme only DAS
        And the apprenticeship funding band maximum is 17000
        And levy balance > agreed price for all months

    Scenario: Earnings and payments for a DAS learner, levy available, where the apprenticeship standard changes and data lock is passed in both instances
        Given the following commitments exist on 03/12/2017:
            | commitment Id | version Id | ULN       | standard code | start date | end date   | agreed price | effective from | effective to |
            | 1             | 1-001      | learner a | 51            | 01/08/2017 | 01/08/2018 | 15000        | 01/08/2017     | 31/10/2017   |
            | 1             | 1-002      | learner a | 52            | 01/08/2017 | 01/08/2018 | 5625         | 03/11/2017     |              |
        When an ILR file is submitted with the following data:
            | ULN       | standard code | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date |
            | learner a | 51            | 03/08/2017 | 01/08/2018       | 31/10/2017      | withdrawn         | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            |
            | learner a | 52            | 03/11/2017 | 01/08/2018       |                 | continuing        | 4500                 | 03/11/2017                          | 1125                   | 03/11/2017                            |
        Then the data lock status of the ILR in 03/12/2017 is:
            | Payment type | 08/17               | 09/17               | 10/17               | 11/17               | 12/17               |
            | On-program   | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-002 | commitment 1 v1-002 |
        And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | 1000  | 500   |
            | Levy account debited       | 0     | 1000  | 1000  | 1000  | 500   |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 500   | 500   |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     |


    Scenario: ILR changes before second Commitment starts (i.e. there is only one existing Commitment in place)
        Given the following commitments exist on 03/12/2017:
            | commitment Id | version Id | ULN       | standard code | start date | end date   | agreed price |
            | 1             |  1-001   | learner a | 51            | 03/08/2017 | 04/08/2018 | 15000        |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | standard code | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date |
            | learner a | 51            | 03/08/2017 | 01/08/2018       | 31/10/2017      | withdrawn         | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            |
            | learner a | 52            | 03/11/2017 | 01/08/2018       |                 | continuing        | 4500                 | 03/11/2017                          | 1125                   | 03/11/2017                            |
        Then the data lock status of the ILR in 03/12/2017 is:
            | Payment type | 08/17               | 09/17               | 10/17               | 11/17 | 12/17 |
            | On-program   | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 |       |       |
        And the provider earnings and payments break down as follows:
            | Type                          | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Earned from SFA      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Paid by SFA          | 0     | 1000  | 1000  | 1000  | 0     |
            | Levy account debited          | 0     | 1000  | 1000  | 1000  | 0     |
            | SFA Levy employer budget      | 1000  | 1000  | 1000  | 0     | 0     |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | 0     |


    Scenario: New Commitment which is not reflected in the updated ILR submission (i.e. new Commitment but no corresponding change in the ILR).
        Given the following commitments exist on 03/12/2017:
            | commitment Id | version Id | ULN       | standard code | start date | end date   | agreed price | effective from | effective to |
            | 1             | 1-001      | learner a | 51            | 03/08/2017 | 04/08/2018 | 15000        | 03/08/2017     | 31/10/2017   |
            | 1             | 1-002      | learner a | 52            | 03/08/2017 | 04/08/2018 | 5625         | 01/11/2017     |              |
		When an ILR file is submitted with the following data:
            | ULN       | standard code | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date |
            | learner a | 51            | 03/08/2017 | 04/08/2018       |                 | continuing        | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            |
        Then the data lock status of the ILR in 03/12/2017 is:
            | Payment type | 08/17               | 09/17               | 10/17               | 11/17 | 12/17 | 01/18 |
            | On-program   | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 |       |       |       |
        And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 1000  | 1000  | 1000  |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | 1000  | 1000  | 1000  |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | 1000  | 0     | 0     |
            | Levy account debited       | 0     | 1000  | 1000  | 1000  | 0     | 0     |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 0     | 0     | 0     |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     | 0     |


    Scenario: Apprentice goes on a planned break midway through the learning episode and this is notified through the ILR
        Given the following commitments exist on 03/12/2017:
            | commitment Id | version Id | ULN       | start date | end date   | status | agreed price | effective from | effective to |
            | 1             | 1-001      | learner a | 01/09/2017 | 08/09/2018 | active | 15000        | 01/09/2017     | 31/10/2017   |
            | 1             | 1-002      | learner a | 01/09/2017 | 08/09/2018 | paused | 15000        | 01/11/2017     | 02/01/2018   |
            | 1             | 1-003      | learner a | 01/09/2017 | 08/09/2018 | active | 15000        | 03/01/2018     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date |
            | learner a | 01/09/2017 | 08/09/2018       | 31/10/2017      | planned break     | 12000                | 01/09/2017                          | 3000                   | 01/09/2017                            |
            | learner a | 03/01/2018 | 08/11/2018       |                 | continuing        | 12000                | 03/01/2018                          | 3000                   | 03/01/2018                            |
        Then the provider earnings and payments break down as follows:
            | Type                     | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 | 02/18 | ... | 10/18 | 11/18 |
            | Provider Earned from SFA | 1000  | 1000  | 0     | 0     | 1000  | 1000  | ... | 1000  | 0     |
            | Provider Paid by SFA     | 0     | 1000  | 1000  | 0     | 0     | 1000  | ... | 1000  | 1000  |
            | Levy account debited     | 0     | 1000  | 1000  | 0     | 0     | 1000  | ... | 1000  | 1000  |
            | SFA Levy employer budget | 1000  | 1000  | 0     | 0     | 1000  | 1000  | ... | 1000  | 0     |


    Scenario: Earnings and payments for a DAS learner, levy available, and there is a change to the Negotiated Cost which happens at the end of the month
        Given the following commitments exist:
            | commitment Id | version Id | ULN       | start date | end date   | status | agreed price | effective from | effective to |
            | 1             | 1-001      | learner a | 01/08/2017 | 31/08/2018 | active | 15000        | 01/08/2017     | 31/10/2017   |
            | 1             | 1-002      | learner a | 01/08/2017 | 31/08/2018 | active | 9375         | 01/11/2017     |              |
        When an ILR file is submitted with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price 1 | Total training price 1 effective date | Total assessment price 1 | Total assessment price 1 effective date | Total training price 2 | Total training price 2 effective date | Total assessment price 2 | Total assessment price 2 effective date |
            | learner a | 01/08/2017 | 04/08/2018       |                 | continuing        | 12000                  | 01/08/2017                            | 3000                     | 01/08/2017                              | 7500                   | 01/11/2017                            | 1875                     | 01/11/2017                              |
        Then the data lock status will be as follows:
            | Payment type | 08/17               | 09/17               | 10/17               | 11/17               | 12/17               | ... | 07/18               |
            | On-program   | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-002 | commitment 1 v1-002 | ... | commitment 1 v1-002 | 
        And the provider earnings and payments break down as follows:
            | Type                          | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | 500   | 500   | ... | 500   | 0     |
            | Provider Earned from SFA      | 1000  | 1000  | 1000  | 500   | 500   | ... | 500   | 0     |
            | Provider Earned from Employer | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Provider Paid by SFA          | 0     | 1000  | 1000  | 1000  | 500   | ... | 500   | 500   |
            | Payment due from Employer     | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Levy account debited          | 0     | 1000  | 1000  | 1000  | 500   | ... | 500   | 500   |
            | SFA Levy employer budget      | 1000  | 1000  | 1000  | 500   | 500   | ... | 500   | 0     |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |


    Scenario: Earnings and payments for a DAS learner, levy available, and there is a change to the Negotiated Cost which happens in the middle of the month
        Given the following commitments exist:
            | commitment Id | version Id | ULN       | start date | end date   | status | agreed price | effective from | effective to |
            | 1             | 1-001      | learner a | 01/08/2017 | 31/08/2018 | active | 15000        | 01/08/2017     | 31/10/2017   |
            | 1             | 1-002      | learner a | 01/08/2017 | 31/08/2018 | active | 9375         | 01/11/2017     |              |
        When an ILR file is submitted with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price 1 | Total training price 1 effective date | Total assessment price 1 | Total assessment price 1 effective date | Total training price 2 | Total training price 2 effective date | Total assessment price 2 | Total assessment price 2 effective date |
            | learner a | 01/08/2017 | 04/08/2018       |                 | continuing        | 12000                  | 01/08/2017                            | 3000                     | 01/08/2017                              | 7500                   | 10/11/2017                            | 1875                     | 10/11/2017                              |
        Then the data lock status will be as follows:
            | Payment type | 08/17               | 09/17               | 10/17               | 11/17               | 12/17               | ... | 07/18               |
            | On-program   | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-002 | commitment 1 v1-002 | ... | commitment 1 v1-002 | 
        And the provider earnings and payments break down as follows:
            | Type                          | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | 500   | 500   | ... | 500   | 0     |
            | Provider Earned from SFA      | 1000  | 1000  | 1000  | 500   | 500   | ... | 500   | 0     |
            | Provider Earned from Employer | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Provider Paid by SFA          | 0     | 1000  | 1000  | 1000  | 500   | ... | 500   | 500   |
            | Payment due from Employer     | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Levy account debited          | 0     | 1000  | 1000  | 1000  | 500   | ... | 500   | 500   |
            | SFA Levy employer budget      | 1000  | 1000  | 1000  | 500   | 500   | ... | 500   | 0     |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |


    Scenario: Earnings and payments for a DAS learner, levy available, and there is a change to the Negotiated Cost which happens in the middle of the month and the ILR starts earlier
        Given the following commitments exist:
            | commitment Id | version Id | ULN       | start date | end date   | status | agreed price | effective from | effective to |
            | 1             | 1-001      | learner a | 01/08/2017 | 31/08/2018 | active | 15000        | 01/08/2017     | 31/10/2017   |
            | 1             | 1-002      | learner a | 01/08/2017 | 31/08/2018 | active | 9375         | 01/11/2017     |              |
        When an ILR file is submitted with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price 1 | Total training price 1 effective date | Total assessment price 1 | Total assessment price 1 effective date | Total training price 2 | Total training price 2 effective date | Total assessment price 2 | Total assessment price 2 effective date |
            | learner a | 01/08/2017 | 04/08/2018       |                 | continuing        | 12000                  | 01/08/2017                            | 3000                     | 01/08/2017                              | 7500                   | 24/10/2017                            | 1875                     | 24/10/2017                              |
        Then the data lock status will be as follows:
            | Payment type | 08/17               | 09/17               | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
            | On-program   | commitment 1 v1-001 | commitment 1 v1-001 |       |       |       | ... |       |       |
        And the provider earnings and payments break down as follows:
            | Type                          | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
            | Provider Earned Total         | 1000  | 1000  | 550   | 550   | 550   | ... | 550   | 0     |
            | Provider Earned from SFA      | 1000  | 1000  | 0     | 0     | 0     | ... | 0     | 0     |
            | Provider Earned from Employer | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Provider Paid by SFA          | 0     | 1000  | 1000  | 0     | 0     | ... | 0     | 0     |
            | Payment due from Employer     | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Levy account debited          | 0     | 1000  | 1000  | 0     | 0     | ... | 0     | 0     |
            | SFA Levy employer budget      | 1000  | 1000  | 0     | 0     | 0     | ... | 0     | 0     |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |


    Scenario: Earnings and payments for a DAS learner, levy available, and there is a change to the Negotiated Cost and the ILR is not updated
        Given the following commitments exist:
            | commitment Id | version Id | ULN       | start date | end date   | status | agreed price | effective from | effective to |
            | 1             | 1-001      | learner a | 01/08/2017 | 31/08/2018 | active | 15000        | 01/08/2017     | 31/10/2017   |
            | 1             | 1-002      | learner a | 01/08/2017 | 31/08/2018 | active | 9375         | 01/11/2017     |              |
        When an ILR file is submitted with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | Total training price 1 | Total training price 1 effective date | Total assessment price 1 | Total assessment price 1 effective date |
            | learner a | 01/08/2017 | 04/08/2018       |                 | continuing        | 12000                  | 01/08/2017                            | 3000                     | 01/08/2017                              |
        Then the data lock status will be as follows:
            | Payment type | 08/17               | 09/17               | 10/17               | 11/17 | 12/17 | ... | 07/18 | 08/18 |
            | On-program   | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 |       |       | ... |       |       |
        And the provider earnings and payments break down as follows:
            | Type                          | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
            | Provider Earned from SFA      | 1000  | 1000  | 1000  | 0     | 0     | ... | 0     | 0     |
            | Provider Earned from Employer | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Provider Paid by SFA          | 0     | 1000  | 1000  | 1000  | 0     | ... | 0     | 0     |
            | Payment due from Employer     | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Levy account debited          | 0     | 1000  | 1000  | 1000  | 0     | ... | 0     | 0     |
            | SFA Levy employer budget      | 1000  | 1000  | 1000  | 0     | 0     | ... | 0     | 0     |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |

#changes in the middle of month

    Scenario: Earnings and payments for a DAS learner, levy available, where the apprenticeship standard changes middle of month and data lock is passed in both instances
        Given the following commitments exist:
            | commitment Id | version Id | ULN       | standard code | start date | end date   | agreed price | status | effective from | effective to |
            | 1             | 1-001      | learner a | 51            | 01/08/2017 | 01/08/2018 | 15000        | active | 01/08/2017     | 10/11/2017   |
            | 1             | 1-002      | learner a | 52            | 01/08/2017 | 01/08/2018 | 5625         | active | 11/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | standard code | start date | planned end date | actual end date | completion status | Total training price 1 | Total training price 1 effective date | Total assessment price 1 | Total assessment price 1 effective date |
            | learner a | 51            | 03/08/2017 | 04/08/2018       | 10/11/2017      | withdrawn         | 12000                  | 03/08/2017                            | 3000                     | 03/08/2017                              |
            | learner a | 52            | 11/11/2017 | 04/08/2018       |                 | continuing        | 4500                   | 11/11/2017                            | 1125                     | 11/11/2017                              | 
        
        Then the data lock status will be as follows:
            | Payment type | 08/17               | 09/17               | 10/17               | 11/17               | 12/17               |
            | On-program   | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-002 | commitment 1 v1-002 |
   
        And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | 1000  | 500   |
            | Levy account debited       | 0     | 1000  | 1000  | 1000  | 500   |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 500   | 500   |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     |

Scenario: ILR changes standard in the middle of the month, but no corresponding change to the commitment is confirmed
        Given the following commitments exist:
            | commitment Id | version Id | ULN       | standard code | start date | end date   | agreed price | status | effective from | effective to |
            | 1             | 1-001      | learner a | 51            | 01/08/2017 | 01/08/2018 | 15000        | active | 01/08/2017     |              |
        When an ILR file is submitted with the following data:
            | ULN       | standard code | start date | planned end date | actual end date | completion status | Total training price 1 | Total training price 1 effective date | Total assessment price 1 | Total assessment price 1 effective date |
            | learner a | 51            | 03/08/2017 | 04/08/2018       | 10/11/2017      | withdrawn         | 12000                  | 03/08/2017                            | 3000                     | 03/08/2017                              |
            | learner a | 52            | 11/11/2017 | 04/08/2018       |                 | continuing        | 4500                   | 11/11/2017                            | 1125                     | 11/11/2017                              | 
        
        Then the data lock status will be as follows:
            | Payment type | 08/17               | 09/17               | 10/17               | 11/17 | 12/17 | 01/18 |
            | On-program   | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 |       |       |       |
   
        And the provider earnings and payments break down as follows: 
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   | 500   |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | 1000  | 0     | 0     |
            | Levy account debited       | 0     | 1000  | 1000  | 1000  | 0     | 0     |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 0     | 0     | 0     |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     | 0     |
            
Scenario: Earnings and payments for a DAS learner, levy available, where the apprenticeship standard changes middle of month A commitment is updated to show a change in standard, mid-month, but the ILR does not reflect this
        Given the following commitments exist:
            | commitment Id | version Id | ULN       | standard code | start date | end date   | agreed price | status | effective from | effective to |
            | 1             | 1-001      | learner a | 51            | 01/08/2017 | 01/08/2018 | 15000        | active | 01/08/2017     | 10/11/2017   |
            | 1             | 1-002      | learner a | 52            | 01/08/2017 | 01/08/2018 | 5625         | active | 11/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | standard code | start date | planned end date | actual end date | completion status | Total training price 1 | Total training price 1 effective date | Total assessment price 1 | Total assessment price 1 effective date |
            | learner a | 51            | 03/08/2017 | 04/08/2018       |                 | continuing        | 12000                  | 03/08/2017                            | 3000                     | 03/08/2017                              |
        
        Then the data lock status will be as follows:
            | Payment type | 08/17               | 09/17               | 10/17               | 11/17 | 12/17 | 01/18 |
            | On-program   | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 |       |       |       |
   
        And the provider earnings and payments break down as follows:         
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 1000  | 1000  | 1000  |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | 1000  | 0     | 0     |
            | Levy account debited       | 0     | 1000  | 1000  | 1000  | 0     | 0     |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 0     | 0     | 0     |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     | 0     |

Scenario: Earnings and payments for a DAS learner, levy available, where the apprenticeship standard changes in the middle of the month and the ILR change happens later within the same month
        Given the following commitments exist:
            | commitment Id | version Id | ULN       | standard code | start date | end date   | agreed price | status | effective from | effective to |
            | 1             | 1-001      | learner a | 51            | 01/08/2017 | 01/08/2018 | 15000        | active | 01/08/2017     | 10/11/2017   |
            | 1             | 1-002      | learner a | 52            | 01/08/2017 | 01/08/2018 | 5625         | active | 11/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | standard code | start date | planned end date | actual end date | completion status | Total training price 1 | Total training price 1 effective date | Total assessment price 1 | Total assessment price 1 effective date |
            | learner a | 51            | 03/08/2017 | 04/08/2018       | 18/11/2017      | withdrawn         | 12000                  | 03/08/2017                            | 3000                     | 03/08/2017                              |
            | learner a | 52            | 19/11/2017 | 04/08/2018       |                 | continuing        | 4500                   | 19/11/2017                            | 1125                     | 19/11/2017                              | 
        
        Then the data lock status will be as follows:
            | Payment type | 08/17               | 09/17               | 10/17               | 11/17               | 12/17               |
            | On-program   | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-002 | commitment 1 v1-002 |
   
        And the provider earnings and payments break down as follows:         
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | 1000  | 500   |
            | Levy account debited       | 0     | 1000  | 1000  | 1000  | 500   |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 500   | 500   |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     |

Scenario: Earnings and payments for a DAS learner, levy available, where the apprenticeship standard changes in the middle of the month and the ILR change happens later in the next month
        Given the following commitments exist:
            | commitment Id | version Id | ULN       | standard code | start date | end date   | agreed price | status | effective from | effective to |
            | 1             | 1-001      | learner a | 51            | 01/08/2017 | 01/08/2018 | 15000        | active | 01/08/2017     | 10/11/2017   |
            | 1             | 1-002      | learner a | 52            | 01/08/2017 | 01/08/2018 | 5625         | active | 11/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | standard code | start date | planned end date | actual end date | completion status | Total training price 1 | Total training price 1 effective date | Total assessment price 1 | Total assessment price 1 effective date |
            | learner a | 51            | 03/08/2017 | 04/08/2018       | 04/12/2017      | withdrawn         | 12000                  | 03/08/2017                            | 3000                     | 03/08/2017                              |
            | learner a | 52            | 05/12/2017 | 04/08/2018       |                 | continuing        | 4500                   | 05/12/2017                            | 1125                     | 05/12/2017                              | 
        
        Then the data lock status will be as follows:
            | Payment type | 08/17               | 09/17               | 10/17               | 11/17 | 12/17               | 01/18               |
            | On-program   | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 |       | commitment 1 v1-002 | commitment 1 v1-002 |
   
        And the provider earnings and payments break down as follows:         
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17  | 01/18  |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 1000  | 562.50 | 562.50 |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | 1000  | 0      | 562.50 |
            | Levy account debited       | 0     | 1000  | 1000  | 1000  | 0      | 562.50 |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 0     | 562.50 | 562.50 |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0      | 0      |

Scenario: Earnings and payments for a DAS learner, levy available, where the apprenticeship standard changes in the middle of the month and the ILR change happens later within the same month before commitment starts
        Given the following commitments exist:
            | commitment Id | version Id | ULN       | standard code | start date | end date   | agreed price | status | effective from | effective to |
            | 1             | 1-001      | learner a | 51            | 01/08/2017 | 01/08/2018 | 15000        | active | 01/08/2017     | 10/11/2017   |
            | 1             | 1-002      | learner a | 52            | 01/08/2017 | 01/08/2018 | 5625         | active | 11/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | standard code | start date | planned end date | actual end date | completion status | Total training price 1 | Total training price 1 effective date | Total assessment price 1 | Total assessment price 1 effective date |
            | learner a | 51            | 03/08/2017 | 04/08/2018       | 05/11/2017      | withdrawn         | 12000                  | 03/08/2017                            | 3000                     | 03/08/2017                              |
            | learner a | 52            | 06/11/2017 | 04/08/2018       |                 | continuing        | 4500                   | 06/11/2017                            | 1125                     | 06/11/2017                              | 
        
        Then the data lock status will be as follows:
            | Payment type | 08/17               | 09/17               | 10/17               | 11/17 | 12/17 |
            | On-program   | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 |       |       |
   
        And the provider earnings and payments break down as follows:         
            | Type                       | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 500   | 500   |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | 1000  | 0     |
            | Levy account debited       | 0     | 1000  | 1000  | 1000  | 0     |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 0     | 0     |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     |

@TNP2OrTNP4Change
Scenario:630-AC01  Earnings and payments for a DAS learner, levy available, and the total assessment cost is increased in isolation (no change to total training price) during the programme

        Given The learner is programme only DAS
        And levy balance > agreed price for all months
		And the apprenticeship funding band maximum is 27000
        And the following commitments exist:
			| commitment Id | version Id | ULN       | start date | end date   | standard code | agreed price | status | effective from | effective to |
			| 1             | 1-001      | learner a | 01/08/2017 | 28/08/2018 | 11            | 15500        | active | 01/08/2017     | 14/11/2017   |
			| 1             | 1-002      | learner a | 01/08/2017 | 28/08/2018 | 11            | 16000        | active | 15/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | standard code | Total training price 1 | Total training price 1 effective date | Total assessment price 1 | Total assessment price 1 effective date | Total assessment price 2 | Total assessment price 2 effective date |
            | learner a | 05/08/2017 | 28/08/2018       |                 | continuing        | 11            | 12000                  | 05/08/2017                            | 3500                     | 05/08/2017                              | 4000                     | 15/11/2017                              |
		Then the data lock status will be as follows:
			| Payment type                   | 08/17               | 09/17               | 10/17               | 11/17               | 12/17               | 01/18               |
			| On-program                     | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-002 | commitment 1 v1-002 | commitment 1 v1-002 |
			| Completion                     |                     |                     |                     |                     |                     |                     |
			| Employer 16-18 incentive       |                     |                     |                     |                     |                     |                     |
			| Provider 16-18 incentive       |                     |                     |                     |                     |                     |                     |
			| Provider learning support      |                     |                     |                     |                     |                     |                     |
			| English and maths on programme |                     |                     |                     |                     |                     |                     |
			| English and maths Balancing    |                     |                     |                     |                     |                     |                     | 
        And the provider earnings and payments break down as follows:
            | Type                          | 08/17   | 09/17   | 10/17   | 11/17   | 12/17   | 01/18   |
            | Provider Earned Total         | 1033.33 | 1033.33 | 1033.33 | 1077.78 | 1077.78 | 1077.78 |
            | Provider Earned from SFA      | 1033.33 | 1033.33 | 1033.33 | 1077.78 | 1077.78 | 1077.78 |
            | Provider Earned from Employer | 0       | 0       | 0       | 0       | 0       | 0       |
            | Provider Paid by SFA          | 0       | 1033.33 | 1033.33 | 1033.33 | 1077.78 | 1077.78 |
            | Payment due from employer 1   | 0       | 0       | 0       | 0       | 0       | 0       |
            | Levy account debited          | 0       | 1033.33 | 1033.33 | 1033.33 | 1077.78 | 1077.78 |
            | SFA Levy employer budget      | 1033.33 | 1033.33 | 1033.33 | 1077.78 | 1077.78 | 1077.78 |
            | SFA Levy co-funding budget    | 0       | 0       | 0       | 0       | 0       | 0       |

@TNP2OrTNP4Change
Scenario:630-AC02  Earnings and payments for a DAS learner, levy available, and the total assessment cost is decreased in isolation (no change to total training price) during the programme

        Given The learner is programme only DAS
        And levy balance > agreed price for all months
		And the apprenticeship funding band maximum is 27000
        And the following commitments exist:
			| commitment Id | version Id | ULN       | start date | end date   | standard code | agreed price | status | effective from | effective to |
			| 1             | 1-001      | learner a | 01/08/2017 | 28/08/2018 | 11            | 15500        | active | 01/08/2017     | 14/11/2017   |
			| 1             | 1-002      | learner a | 01/08/2017 | 28/08/2018 | 11            | 14000        | active | 15/11/2017     |              |
		When an ILR file is submitted on 03/12/2017 with the following data:
			| ULN       | start date | planned end date | actual end date | completion status | standard code | Total training price 1 | Total training price 1 effective date | Total assessment price 1 | Total assessment price 1 effective date | Total assessment price 2 | Total assessment price 2 effective date |
			| learner a | 05/08/2017 | 28/08/2018       |                 | continuing        | 11            | 12000                  | 05/08/2017                            | 3500                     | 05/08/2017                              | 2000                     | 15/11/2017                              |
        
		Then the data lock status will be as follows:
			| Payment type                   | 08/17               | 09/17               | 10/17               | 11/17               | 12/17               | 01/18               |
			| On-program                     | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-002 | commitment 1 v1-002 | commitment 1 v1-002 |
			| Completion                     |                     |                     |                     |                     |                     |                     |
			| Employer 16-18 incentive       |                     |                     |                     |                     |                     |                     |
			| Provider 16-18 incentive       |                     |                     |                     |                     |                     |                     |
			| Provider learning support      |                     |                     |                     |                     |                     |                     |
			| English and maths on programme |                     |                     |                     |                     |                     |                     |
			| English and maths Balancing    |                     |                     |                     |                     |                     |                     | 
		And the provider earnings and payments break down as follows:
			| Type                          | 08/17   | 09/17   | 10/17   | 11/17   | 12/17  | 01/18  |
			| Provider Earned Total         | 1033.33 | 1033.33 | 1033.33 | 900.00  | 900.00 | 900.00 |
			| Provider Earned from SFA      | 1033.33 | 1033.33 | 1033.33 | 900.00  | 900.00 | 900.00 |
			| Provider Earned from Employer | 0       | 0       | 0       | 0       | 0      | 0      |
			| Provider Paid by SFA          | 0       | 1033.33 | 1033.33 | 1033.33 | 900.00 | 900.00 |
			| Payment due from Employer     | 0       | 0       | 0       | 0       | 0      | 0      |
			| Levy account debited          | 0       | 1033.33 | 1033.33 | 1033.33 | 900.00 | 900.00 |
			| SFA Levy employer budget      | 1033.33 | 1033.33 | 1033.33 | 900.00  | 900.00 | 900.00 |
			| SFA Levy co-funding budget    | 0       | 0       | 0       | 0       | 0      | 0      |

@TNP2OrTNP4Change
Scenario:630-AC03  Earnings and payments for a DAS learner, levy available, and the residual assessment cost is increased in isolation (no change to residual training price) during the programme

		Given The learner is programme only DAS
        And levy balance > agreed price for all months
		And the apprenticeship funding band maximum is 27000
        And the following commitments exist:
		    | commitment Id | version Id | ULN       | start date | end date   | standard code | agreed price | status | effective from | effective to |
		    | 1             | 1-001      | learner a | 01/08/2017 | 28/08/2018 | 11            | 15500        | active | 01/08/2017     | 14/11/2017   |
		    | 1             | 1-002      | learner a | 01/08/2017 | 28/08/2018 | 11            | 16000        | active | 15/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | standard code | Residual training price 1 | Residual training price 1 effective date | Residual assessment price 1 | Residual assessment price 1 effective date | Residual assessment price 2 | Residual assessment price 2 effective date |
            | learner a | 05/08/2017 | 28/08/2018       |                 | continuing        | 11            | 12000                     | 05/08/2017                               | 3500                        | 05/08/2017                                 | 4000                        | 15/11/2017                                 |
  		Then the data lock status will be as follows:
		   | Payment type                   | 08/17               | 09/17               | 10/17               | 11/17               | 12/17               | 01/18               |
		   | On-program                     | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-002 | commitment 1 v1-002 | commitment 1 v1-002 |
		   | Completion                     |                     |                     |                     |                     |                     |                     |
		   | Employer 16-18 incentive       |                     |                     |                     |                     |                     |                     |
		   | Provider 16-18 incentive       |                     |                     |                     |                     |                     |                     |
		   | Provider learning support      |                     |                     |                     |                     |                     |                     |
		   | English and maths on programme |                     |                     |                     |                     |                     |                     |
		   | English and maths Balancing    |                     |                     |                     |                     |                     |                     | 
        And the provider earnings and payments break down as follows:
            | Type                          | 08/17   | 09/17   | 10/17   | 11/17   | 12/17   | 01/18   |
            | Provider Earned Total         | 1033.33 | 1033.33 | 1033.33 | 1422.22 | 1422.22 | 1422.22 |
            | Provider Earned from SFA      | 1033.33 | 1033.33 | 1033.33 | 1422.22 | 1422.22 | 1422.22 |
            | Provider Earned from Employer | 0       | 0       | 0       | 0       | 0       | 0       |
            | Provider Paid by SFA          | 0       | 1033.33 | 1033.33 | 1033.33 | 1422.22 | 1422.22 |
            | Payment due from Employer     | 0       | 0       | 0       | 0       | 0       | 0       |
            | Levy account debited          | 0       | 1033.33 | 1033.33 | 1033.33 | 1422.22 | 1422.22 |
            | SFA Levy employer budget      | 1033.33 | 1033.33 | 1033.33 | 1422.22 | 1422.22 | 1422.22 |
            | SFA Levy co-funding budget    | 0       | 0       | 0       | 0       | 0       | 0       |

@TNP2OrTNP4Change
Scenario:630-AC04  Earnings and payments for a DAS learner, levy available, and the residual assessment cost is decreased in isolation (no change to residual training price) during the programme
		Given The learner is programme only DAS
        And levy balance > agreed price for all months
		And the apprenticeship funding band maximum is 27000
        And the following commitments exist:
            | commitment Id | version Id | ULN       | start date | end date   | standard code | agreed price | status | effective from | effective to |
            | 1             | 1-001      | learner a | 01/08/2017 | 28/08/2018 | 11            | 15500        | active | 01/08/2017     | 14/11/2017   |
            | 1             | 1-002      | learner a | 01/08/2017 | 28/08/2018 | 11            | 14000        | active | 15/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | standard code | Residual training price 1 | Residual training price 1 effective date | Residual assessment price 1 | Residual assessment price 1 effective date | Residual assessment price 2 | Residual assessment price 2 effective date |
            | learner a | 05/08/2017 | 28/08/2018       |                 | continuing        | 11            | 12000                     | 05/08/2017                               | 3500                        | 05/08/2017                                 | 2000                        | 15/11/2017                                 |
		Then the data lock status will be as follows:
			| Payment type                   | 08/17               | 09/17               | 10/17               | 11/17               | 12/17               | 01/18               |
			| On-program                     | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-002 | commitment 1 v1-002 | commitment 1 v1-002 |
			| Completion                     |                     |                     |                     |                     |                     |                     |
			| Employer 16-18 incentive       |                     |                     |                     |                     |                     |                     |
			| Provider 16-18 incentive       |                     |                     |                     |                     |                     |                     |
			| Provider learning support      |                     |                     |                     |                     |                     |                     |
			| English and maths on programme |                     |                     |                     |                     |                     |                     |
			| English and maths Balancing    |                     |                     |                     |                     |                     |                     | 
	   And the provider earnings and payments break down as follows:
			| Type                          | 08/17   | 09/17   | 10/17   | 11/17   | 12/17   | 01/18   |
			| Provider Earned Total         | 1033.33 | 1033.33 | 1033.33 | 1244.44 | 1244.44 | 1244.44 |
			| Provider Earned from SFA      | 1033.33 | 1033.33 | 1033.33 | 1244.44 | 1244.44 | 1244.44 |
			| Provider Earned from Employer | 0       | 0       | 0       | 0       | 0       | 0       |
			| Provider Paid by SFA          | 0       | 1033.33 | 1033.33 | 1033.33 | 1244.44 | 1244.44 |
			| Payment due from Employer     | 0       | 0       | 0       | 0       | 0       | 0       |
			| Levy account debited          | 0       | 1033.33 | 1033.33 | 1033.33 | 1244.44 | 1244.44 |
			| SFA Levy employer budget      | 1033.33 | 1033.33 | 1033.33 | 1244.44 | 1244.44 | 1244.44 |
			| SFA Levy co-funding budget    | 0       | 0       | 0       | 0       | 0       | 0       |

@TNP2OrTNP4Change			
 Scenario:630-AC05  Earnings and payments for a DAS learner, levy available, and the total assessment cost is added in isolation (no change to total training price, and no assessment cost initially exists) during the programme

      	Given The learner is programme only DAS
        And levy balance > agreed price for all months
		And the apprenticeship funding band maximum is 27000
        And the following commitments exist:
            | commitment Id | version Id | ULN       | start date | end date   | standard code | agreed price | status | effective from | effective to |
            | 1             | 1-001      | learner a | 01/08/2017 | 28/08/2018 | 11            | 15500        | active | 01/08/2017     | 14/11/2017   |
            | 1             | 1-002      | learner a | 01/08/2017 | 28/08/2018 | 11            | 16000        | active | 15/11/2017     |              |
        When an ILR file is submitted on 03/12/2017 with the following data:
            | ULN       | start date | planned end date | actual end date | completion status | standard code | Total training price 1 | Total training price 1 effective date | Total assessment price 1 | Total assessment price 1 effective date | Total assessment price 2 | Total assessment price 2 effective date |
            | learner a | 05/08/2017 | 28/08/2018       |                 | continuing        | 11            | 15500                  | 05/08/2017                            | 0                        | 05/08/2017                              | 500                      | 15/11/2017                              |
		Then the data lock status will be as follows:
			| Payment type                   | 08/17               | 09/17               | 10/17               | 11/17               | 12/17               | 01/18               |
			| On-program                     | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-001 | commitment 1 v1-002 | commitment 1 v1-002 | commitment 1 v1-002 |
			| Completion                     |                     |                     |                     |                     |                     |                     |
			| Employer 16-18 incentive       |                     |                     |                     |                     |                     |                     |
			| Provider 16-18 incentive       |                     |                     |                     |                     |                     |                     |
			| Provider learning support      |                     |                     |                     |                     |                     |                     |
			| English and maths on programme |                     |                     |                     |                     |                     |                     |
			| English and maths Balancing    |                     |                     |                     |                     |                     |                     | 
        And the provider earnings and payments break down as follows:
            | Type                          | 08/17   | 09/17   | 10/17   | 11/17   | 12/17   | 01/18   |
            | Provider Earned Total         | 1033.33 | 1033.33 | 1033.33 | 1077.78 | 1077.78 | 1077.78 |
            | Provider Earned from SFA      | 1033.33 | 1033.33 | 1033.33 | 1077.78 | 1077.78 | 1077.78 |
            | Provider Earned from Employer | 0       | 0       | 0       | 0       | 0       | 0       |
            | Provider Paid by SFA          | 0       | 1033.33 | 1033.33 | 1033.33 | 1077.78 | 1077.78 |
            | Payment due from Employer     | 0       | 0       | 0       | 0       | 0       | 0       |
            | Levy account debited          | 0       | 1033.33 | 1033.33 | 1033.33 | 1077.78 | 1077.78 |
            | SFA Levy employer budget      | 1033.33 | 1033.33 | 1033.33 | 1077.78 | 1077.78 | 1077.78 |
            | SFA Levy co-funding budget    | 0       | 0       | 0       | 0       | 0       | 0       |

