Feature: Employer stops payments on a commitment

    Scenario: Commitment payments are stopped midway through the learning episode
        Given levy balance > agreed price for all months
        And the following commitments exist:
            | commitment Id | version Id | ULN       | start date | end date   | status | agreed price | effective from | effective to |
            | 1             | 1          | learner a | 01/09/2017 | 08/09/2018 | active | 15000        | 01/09/2017     | 31/10/2017   |
            | 1             | 2          | learner a | 01/09/2017 | 08/09/2018 | paused | 15000        | 01/11/2017     |              |
        When an ILR file is submitted every month with the following data:
            | ULN       | agreed price | learner type       | start date | planned end date | completion status |
            | learner a | 15000        | programme only DAS | 01/09/2017 | 08/09/2018       | continuing        |
        Then the provider earnings and payments break down as follows:
            | Type                          | 09/17 | 10/17 | 11/17 | 12/17 | ... | 03/18 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | 1000  | ... | 1000  |
            | Provider Earned from SFA      | 1000  | 1000  | 0     | 0     | ... | 0     |
            | Provider Earned from Employer | 0     | 0     | 0     | 0     | ... | 0     |
            | Provider Paid by SFA          | 0     | 1000  | 1000  | 0     | ... | 0     |
            | Payment due from Employer     | 0     | 0     | 0     | 0     | ... | 0     |
            | Levy account debited          | 0     | 1000  | 1000  | 0     | ... | 0     |
            | SFA Levy employer budget      | 1000  | 1000  | 0     | 0     | ... | 0     |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | ... | 0     |
            | SFA non-Levy co-funding budget| 0     | 0     | 0     | 0     | ... | 0     |

            
    Scenario: The provider submits the first ILR file after the commitment payments have been stopped
        Given levy balance > agreed price for all months
        And the following commitments exist:
            | commitment Id | version Id | ULN       | start date | end date   | status | agreed price | effective from | effective to |
            | 1             | 1          | learner a | 01/09/2017 | 08/09/2018 | active | 15000        | 01/09/2017     | 31/08/2017   |
            | 1             | 2          | learner a | 01/09/2017 | 08/09/2018 | paused | 15000        | 01/09/2017     |              |
        When an ILR file is submitted for the first time on 28/12/17 with the following data:
            | ULN       | agreed price | learner type       | start date | planned end date | completion status |
            | learner a | 15000        | programme only DAS | 01/09/2017 | 08/09/2018       | continuing        |
        Then the provider earnings and payments break down as follows:
            | Type                          | 09/17 | 10/17 | 11/17 | 12/17 | ... | 03/18 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | 1000  | ... | 1000  |
            | Provider Earned from SFA      | 0     | 0     | 0     | 0     | ... | 0     |
            | Provider Earned from Employer | 0     | 0     | 0     | 0     | ... | 0     |
            | Provider Paid by SFA          | 0     | 0     | 0     | 0     | ... | 0     |
            | Payment due from Employer     | 0     | 0     | 0     | 0     | ... | 0     |
            | Levy account debited          | 0     | 0     | 0     | 0     | ... | 0     |
            | SFA Levy employer budget      | 0     | 0     | 0     | 0     | ... | 0     |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | ... | 0     |
            | SFA non-Levy co-funding budget| 0     | 0     | 0     | 0     | ... | 0     |
