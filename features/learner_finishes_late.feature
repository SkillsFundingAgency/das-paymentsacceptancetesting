Feature: Provider earnings and payments where learner completes later than planned

    The earnings and payment rules for late completions are the same as for learners finishing on time, except that the completion payment is held back until completion.

    Background:
        Given the apprenticeship funding band maximum for each learner is 17000

    Scenario: A DAS learner, levy available, learner finishes late
        Given levy balance > agreed price for all months
        When an ILR file is submitted with the following data:
            | learner type       | agreed price | start date | planned end date | actual end date | completion status |
            | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/10/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                       | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 | 11/18 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | ... | 1000  | 0     | 3000  | 0     |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | ... | 1000  | 0     | 3000  | 0     |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | ... | 1000  | 1000  | 0     | 3000  |
            | Levy account debited       | 0     | 1000  | 1000  | ... | 1000  | 1000  | 0     | 3000  |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | ... | 1000  | 0     | 3000  | 0     |
            | SFA Levy co-funding budget | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |


    Scenario: A non-DAS learner, learner finishes late
        When an ILR file is submitted with the following data:
            | learner type           | agreed price | start date | planned end date | actual end date | completion status |
            | programme only non-DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/12/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                           | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 | 11/18 | 12/18 | 01/19 |
            | Provider Earned Total          | 1000  | 1000  | 1000  | ... | 1000  | 0     | 0     | 0     | 3000  | 0     |
            | Provider Earned from SFA       | 900   | 900   | 900   | ... | 900   | 0     | 0     | 0     | 2700  | 0     |
            | Provider Earned from Employer  | 100   | 100   | 100   | ... | 100   | 0     | 0     | 0     | 300   | 0     |
            | Provider Paid by SFA           | 0     | 900   | 900   | ... | 900   | 900   | 0     | 0     | 0     | 2700  |
            | Payment due from Employer      | 0     | 100   | 100   | ... | 100   | 100   | 0     | 0     | 0     | 300   |
            | Levy account debited           | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy employer budget       | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy co-funding budget     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget | 900   | 900   | 900   | ... | 900   | 0     | 0     | 0     | 2700  | 0     |
        And the transaction types for the payments are:
            | Payment type             | 10/17 | 11/17 | 12/17 | 01/18 | ... | 09/18 | 10/18 | 11/18 | 12/18 | 01/19 |
            | On-program               | 900   | 900   | 900   | 900   | ... | 900   | 0     | 0     | 0     | 0     |
            | Completion               | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 2700  |
            | Balancing                | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Employer 16-18 incentive | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Provider 16-18 incentive | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |


	Scenario: A non-DAS learner, learner withdraws after planned end date 
    
        When an ILR file is submitted with the following data:
            | agreed price | learner type           | start date | planned end date | actual end date | completion status |
            | 15000        | programme only non-DAS | 01/09/2017 | 08/09/2018       | 08/12/2018      | withdrawn         |
            
        Then the provider earnings and payments break down as follows:
            | Type                          | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 | 11/18 | 12/18 | 01/19 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | ... | 1000  | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from SFA      | 900   | 900   | 900   | ... | 900   | 0     | 0     | 0     | 0     | 0     |
            | Provider Earned from Employer | 100   | 100   | 100   | ... | 100   | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA          | 0     | 900   | 900   | ... | 900   | 900   | 0     | 0     | 0     | 0     |
            | Payment due from Employer     | 0     | 100   | 100   | ... | 100   | 100   | 0     | 0     | 0     | 0     |
            | Levy account debited          | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy employer budget      | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget| 900   | 900   | 900   | ... | 900   | 0     | 0     | 0     | 0     | 0     |
   
		And the transaction types for the payments are:
		    | Payment type             | 10/17 | 11/17 | 12/17 | 01/18 | ... | 09/18 | 10/18 | 11/18 | 12/18 | 01/19 |
		    | On-program               | 900   | 900   | 900   | 900   | ... | 900   | 0     | 0     | 0     | 0     |
		    | Completion               | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
		    | Balancing                | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
		    | Employer 16-18 incentive | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
		    | Provider 16-18 incentive | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |