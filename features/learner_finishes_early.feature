Feature: Provider earnings and payments where learner completes earlier than planned

    The earnings and payment rules for early completions are the same as for learners finishing on time, except that the completion payment is earned earlier.

    Background:
        Given the apprenticeship funding band maximum for each learner is 20000

    Scenario: A DAS learner, levy available, learner finishes one month early
        Given levy balance > agreed price for all months
		And the following commitments exist:
            | ULN       | priority | start date | end date   | agreed price |
            | learner a | 1        | 01/09/2017 | 08/09/2018 | 15000        |
        When an ILR file is submitted with the following data:
            | learner type       | agreed price | start date | planned end date | actual end date | completion status |
            | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/08/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                       | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | ... | 4000  | 0     |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | ... | 4000  | 0     |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | ... | 1000  | 4000  |
            | Levy account debited       | 0     | 1000  | 1000  | ... | 1000  | 4000  |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | ... | 4000  | 0     |
            | SFA Levy co-funding budget | 0     | 0     | 0     | ... | 0     | 0     |
        And the transaction types for the payments are:
            | Transaction type | 10/17 | 11/17 | ... | 08/18 | 09/18 |
            | On-program       | 1000  | 1000  | ... | 1000  | 0     |
            | Completion       | 0     | 0     | ... | 0     | 3000  |
            | Balancing        | 0     | 0     | ... | 0     | 1000  |

    Scenario: A DAS learner, levy available, learner finishes two months early
        Given levy balance > agreed price for all months
		And the following commitments exist:
            | ULN       | priority | start date | end date   | agreed price |
            | learner a | 1        | 01/09/2017 | 08/09/2018 | 15000        |
        When an ILR file is submitted with the following data:
            | learner type       | agreed price | start date | planned end date | actual end date | completion status |
            | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/07/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                       | 09/17 | 10/17 | 11/17 | ... | 07/18 | 08/18 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | ... | 5000  | 0     |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | ... | 5000  | 0     |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | ... | 1000  | 5000  |
            | Levy account debited       | 0     | 1000  | 1000  | ... | 1000  | 5000  |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | ... | 5000  | 0     |
            | SFA Levy co-funding budget | 0     | 0     | 0     | ... | 0     | 0     |
        And the transaction types for the payments are:
            | Transaction type | 10/17 | 11/17 | ... | 07/18 | 08/18 |
            | On-program       | 1000  | 1000  | ... | 1000  | 0     |
            | Completion       | 0     | 0     | ... | 0     | 3000  |
            | Balancing        | 0     | 0     | ... | 0     | 2000  |


    Scenario: A non-DAS learner, learner finishes early
        When an ILR file is submitted with the following data:
            | learner type           | agreed price | start date | planned end date | actual end date | completion status |
            | programme only non-DAS | 18750        | 01/09/2017 | 08/12/2018       | 08/09/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                           | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total          | 1000  | 1000  | 1000  | ... | 1000  | 6750  | 0     |
            | Provider Earned from SFA       | 900   | 900   | 900   | ... | 900   | 6075  | 0     |
            | Provider Earned from Employer  | 100   | 100   | 100   | ... | 100   | 675   | 0     |
            | Provider Paid by SFA           | 0     | 900   | 900   | ... | 900   | 900   | 6075  |
            | Payment due from Employer      | 0     | 100   | 100   | ... | 100   | 100   | 675   |
            | Levy account debited           | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | SFA Levy employer budget       | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | SFA Levy co-funding budget     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget | 900   | 900   | 900   | ... | 900   | 6075  | 0     |
        And the transaction types for the payments are:
            | Payment type             | 10/17 | 11/17 | 12/17 | 01/18 | ... | 09/18 | 10/18 |
            | On-program               | 900   | 900   | 900   | 900   | ... | 900   | 0     |
            | Completion               | 0     | 0     | 0     | 0     | ... | 0     | 3375  |
            | Balancing                | 0     | 0     | 0     | 0     | ... | 0     | 2700  |
            | Employer 16-18 incentive | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Provider 16-18 incentive | 0     | 0     | 0     | 0     | ... | 0     | 0     |


    Scenario: A non-DAS learner, learner withdraws after qualifying period
    
        When an ILR file is submitted with the following data:
            | agreed price | learner type           | start date | planned end date | actual end date | completion status |
            | 15000        | programme only non-DAS | 01/09/2017 | 08/09/2018       | 08/01/2018      | withdrawn         |
        Then the provider earnings and payments break down as follows:
            | Type                          | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | 1000  | 0     |
            | Provider Earned from SFA      | 900   | 900   | 900   | 900   | 0     |
            | Provider Earned from Employer | 100   | 100   | 100   | 100   | 0     |
            | Provider Paid by SFA          | 0     | 900   | 900   | 900   | 900   |
            | Payment due from Employer     | 0     | 100   | 100   | 100   | 100   |
            | Levy account debited          | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy employer budget      | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget| 900   | 900   | 900   | 900   | 0     |
        And the transaction types for the payments are:
            | Payment type             | 10/17 | 11/17 | 12/17 | 01/18 | 
            | On-program               | 900   | 900   | 900   | 900   | 
            | Completion               | 0     | 0     | 0     | 0     | 
            | Balancing                | 0     | 0     | 0     | 0     | 
            | Employer 16-18 incentive | 0     | 0     | 0     | 0     | 
            | Provider 16-18 incentive | 0     | 0     | 0     | 0     | 