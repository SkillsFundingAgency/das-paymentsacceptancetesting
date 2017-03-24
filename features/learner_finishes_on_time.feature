Feature: Provider earnings and payments where learner completes on time and is funded by levy

    For earnings, the total cost of training for an apprentice is split between:
    - 80% of the total cost split into equal monthly instalments
    - 20% of the total cost held back until completion

    For payments, where there is no lag in ILR submission, payments follow these rules:
    - Provider payment follows the month after earnings
    - This is due to the fact that activity relating to earnings is captured for funding purposes on the fourth working day of the next calendar month
    - The levy account is debited in the same month as payment is made (although at different times in the month)
    - Spend against budget is represented against the month in which funding is earned
    - Where a levy account is used for funding, payments are made against the SFA Levy budget
    - Levy funds are used until they run out, and then co-funding is used
    - The order in which learners are funded is derived from the priorities of the commitments

    Background:
        Given the apprenticeship funding band maximum for each learner is 17000

    Scenario: A DAS learner, levy available, learner finishes on time
        Given levy balance > agreed price for all months
		And the following commitments exist:
            | ULN       | priority | start date | end date   | agreed price |
            | learner a | 1        | 01/09/2017 | 08/09/2018 | 15000        |
        When an ILR file is submitted with the following data:
            | learner type       | agreed price | start date | planned end date | actual end date | completion status |
            | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                       | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | ... | 1000  | 1000  | 3000  |
            | Levy account debited       | 0     | 1000  | 1000  | ... | 1000  | 1000  | 3000  |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
            | SFA Levy co-funding budget | 0     | 0     | 0     | ... | 0     | 0     | 0     |


    Scenario: A DAS learner, no levy available, learner finishes on time
        Given levy balance = 0 for all months
		And the following commitments exist:
            | ULN       | priority | start date | end date   | agreed price |
            | learner a | 1        | 01/09/2017 | 08/09/2018 | 15000        |
        When an ILR file is submitted with the following data:
            | learner type       | agreed price | start date | planned end date | actual end date | completion status |
            | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                          | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
            | Provider Earned from SFA      | 900   | 900   | 900   | ... | 900   | 2700  | 0     |
            | Provider Earned from Employer | 100   | 100   | 100   | ... | 100   | 300   | 0     |
            | Provider Paid by SFA          | 0     | 900   | 900   | ... | 900   | 900   | 2700  |
            | Payment due from Employer     | 0     | 100   | 100   | ... | 100   | 100   | 300   |
            | Levy account debited          | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | SFA Levy employer budget      | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | SFA Levy co-funding budget    | 900   | 900   | 900   | ... | 900   | 2700  | 0     |
            | SFA non-Levy co-funding budget| 0     | 0     | 0     | ... | 0     | 0     | 0     |


    Scenario: 2 DAS learners, only enough levy to cover 1
        Given the employer's levy balance is:
            | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 |
            | 0     | 500   | 500   | 500 | 500   | 1500  |
        And the following commitments exist:
            | ULN       | priority | start date | end date   | agreed price |
            | learner a | 1        | 01/09/2017 | 08/09/2018 | 7500         |
            | learner b | 2        | 01/09/2017 | 08/09/2018 | 15000        |
        When an ILR file is submitted with the following data:
            | ULN       | agreed price | learner type       | start date | planned end date | actual end date | completion status |
            | learner a | 7500         | programme only DAS | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
            | learner b | 15000        | programme only DAS | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                          | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total         | 1500  | 1500  | 1500  | ... | 1500  | 4500  | 0     |
            | Provider Earned from SFA      | 1350  | 1400  | 1400  | ... | 1400  | 4200  | 0     |
            | Provider Earned from Employer | 150   | 100   | 100   | ... | 100   | 300   | 0     |
            | Provider Paid by SFA          | 0     | 1350  | 1400  | ... | 1400  | 1400  | 4200  |
            | Payment due from Employer     | 0     | 150   | 100   | ... | 100   | 100   | 300   |
            | Levy account debited          | 0     | 0     | 500   | ... | 500   | 500   | 1500  |
            | SFA Levy employer budget      | 0     | 500   | 500   | ... | 500   | 1500  | 0     |
            | SFA Levy co-funding budget    | 1350  | 900   | 900   | ... | 900   | 2700  | 0     |
            | SFA non-Levy co-funding budget| 0     | 0     | 0     | ... | 0     | 0     | 0     |


    Scenario: A non-DAS learner, learner finishes on time
		Given the following commitments exist:
            | ULN       | priority | start date | end date   | agreed price |
            | learner a | 1        | 01/09/2017 | 08/09/2018 | 15000        |
        When an ILR file is submitted with the following data:
            | agreed price | learner type           | start date | planned end date | actual end date | completion status |
            | 15000        | programme only non-DAS | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                          | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
            | Provider Earned from SFA      | 900   | 900   | 900   | ... | 900   | 2700  | 0     |
            | Provider Earned from Employer | 100   | 100   | 100   | ... | 100   | 300   | 0     |
            | Provider Paid by SFA          | 0     | 900   | 900   | ... | 900   | 900   | 2700  |
            | Payment due from Employer     | 0     | 100   | 100   | ... | 100   | 100   | 300   |
            | Levy account debited          | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | SFA Levy employer budget      | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget| 900   | 900   | 900   | ... | 900   | 2700  | 0     |


	Scenario: 1 DAS Learner, not enough levy to cover full payment
        Given the employer's levy balance is:
            | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 |
            | 500   | 500   | 500   | 500 | 500   | 1500  |
		And the following commitments exist:
            | ULN       | priority | start date | end date   | agreed price |
            | learner a | 1        | 01/09/2017 | 08/09/2018 | 15000        |
        When an ILR file is submitted with the following data:
            | learner type       | agreed price | start date | planned end date | actual end date | completion status |
            | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                          | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
            | Provider Earned from SFA      | 950   | 950   | 950   | ... | 950   | 2850  | 0     |
            | Provider Earned from Employer | 50    | 50    | 50    | ... | 50    | 150   | 0     |
            | Provider Paid by SFA          | 0     | 950   | 950   | ... | 950   | 950   | 2850  |
            | Payment due from Employer     | 0     | 50    | 50    | ... | 50    | 50    | 150   |
            | Levy account debited          | 0     | 500   | 500   | ... | 500   | 500   | 1500  |
            | SFA Levy employer budget      | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
            | SFA Levy co-funding budget    | 450   | 450   | 450   | ... | 450   | 1350  | 0     |
            | SFA non-Levy co-funding budget| 0     | 0     | 0     | ... | 0     | 0     | 0     |

            
    Scenario: 2 learners, 2 employers, 1 provider - enough levy
        Given the employer 1 has a levy balance > agreed price for all months
        And the employer 2 has a levy balance > agreed price for all months
        And the following commitments exist:
            | Employer   | ULN       | priority | agreed price | start date | end date   |
            | employer 1 | learner a | 1        | 7500         | 01/09/2017 | 08/09/2018 |
            | employer 2 | learner b | 1        | 15000        | 01/09/2017 | 08/09/2018 |
        When an ILR file is submitted with the following data:
            | ULN       | agreed price | learner type       | start date | planned end date | actual end date | completion status |
            | learner a | 7500         | programme only DAS | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
            | learner b | 15000        | programme only DAS | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                            | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total           | 1500  | 1500  | 1500  | ... | 1500  | 4500  | 0     |
            | Provider Earned from SFA        | 1500  | 1500  | 1500  | ... | 1500  | 4500  | 0     |
            | Provider Earned from Employer 1 | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | Provider Earned from Employer 2 | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | Provider Paid by SFA            | 0     | 1500  | 1500  | ... | 1500  | 1500  | 4500  |
            | Payment due from Employer 1     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | Payment due from Employer 2     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | employer 1 Levy account debited | 0     | 500   | 500   | ... | 500   | 500   | 1500  |
            | employer 2 Levy account debited | 0     | 1000  | 1000  | ... | 1000  | 1000  | 3000  |
            | SFA Levy employer budget        | 1500  | 1500  | 1500  | ... | 1500  | 4500  | 0     |
            | SFA Levy co-funding budget      | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget  | 0     | 0     | 0     | ... | 0     | 0     | 0     |


    Scenario: 2 learners, 2 employers, 1 provider - not enough levy
        Given the employer 1 has a levy balance of:
            | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 |
            | 0     | 100   | 100   | 100 | 250   | 500   |
        And the employer 2 has a levy balance of:
            | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 |
            | 500   | 500   | 500   | 500 | 500   | 1500  |
        And the following commitments exist:
            | Employer   | ULN       | priority | agreed price | start date | end date   |
            | employer 1 | learner a | 1        | 7500         | 01/09/2017 | 08/09/2018 |
            | employer 2 | learner b | 1        | 15000        | 01/09/2017 | 08/09/2018 |
        When an ILR file is submitted with the following data:
            | ULN       | agreed price | learner type       | start date | planned end date | actual end date | completion status |
            | learner a | 7500         | programme only DAS | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
            | learner b | 15000        | programme only DAS | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                            | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total           | 1500  | 1500  | 1500  | ... | 1500  | 4500  | 0     |
            | Provider Earned from SFA        | 1400  | 1410  | 1410  | ... | 1425  | 4250  | 0     |
            | Provider Earned from Employer 1 | 50    | 40    | 40    | ... | 25    | 100   | 0     |
            | Provider Earned from Employer 2 | 50    | 50    | 50    | ... | 50    | 150   | 0     |
            | Provider Paid by SFA            | 0     | 1400  | 1410  | ... | 1410  | 1425  | 4250  |
            | Payment due from Employer 1     | 0     | 50    | 40    | ... | 40    | 25    | 100   |
            | Payment due from Employer 2     | 0     | 50    | 50    | ... | 50    | 50    | 150   |
            | employer 1 Levy account debited | 0     | 0     | 100   | ... | 100   | 250   | 500   |
            | employer 2 Levy account debited | 0     | 500   | 500   | ... | 500   | 500   | 1500  |
            | SFA Levy employer budget        | 500   | 600   | 600   | ... | 750   | 2000  | 0     |
            | SFA Levy co-funding budget      | 900   | 810   | 810   | ... | 675   | 2250  | 0     |
            | SFA non-Levy co-funding budget  | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            
            
    Scenario: 2 learners, 1 employer, 2 providers - enough levy
        Given the employer 1 has a levy balance > agreed price for all months
        And the following commitments exist:
            | Employer   | Provider   | ULN       | priority | agreed price | start date | end date   |
            | employer 1 | provider A | learner a | 1        | 7500         | 01/09/2017 | 08/09/2018 |
            | employer 1 | provider B | learner b | 2        | 15000        | 01/09/2017 | 08/09/2018 |
        When the providers submit the following ILR files:
            | Provider   | ULN       | agreed price | learner type       | start date | planned end date | actual end date | completion status |
            | provider A | learner a | 7500         | programme only DAS | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
            | provider B | learner b | 15000        | programme only DAS | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
        Then the earnings and payments break down for provider A is as follows:
            | Type                            | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total           | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
            | Provider Earned from SFA        | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
            | Provider Earned from Employer 1 | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | Provider Paid by SFA            | 0     | 500   | 500   | ... | 500   | 500   | 1500  |
            | Payment due from Employer       | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | employer 1 Levy account debited | 0     | 500   | 500   | ... | 500   | 500   | 1500  |
            | SFA Levy employer budget        | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
            | SFA Levy co-funding budget      | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget  | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        And the earnings and payments break down for provider B is as follows:
            | Type                            | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total           | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
            | Provider Earned from SFA        | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
            | Provider Earned from Employer 1 | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | Provider Paid by SFA            | 0     | 1000  | 1000  | ... | 1000  | 1000  | 3000  |
            | Payment due from Employer       | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | employer 1 Levy account debited | 0     | 1000  | 1000  | ... | 1000  | 1000  | 3000  |
            | SFA Levy employer budget        | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
            | SFA Levy co-funding budget      | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget  | 0     | 0     | 0     | ... | 0     | 0     | 0     |

        
    Scenario: 2 learners, 1 employer, 2 providers - not enough levy
        Given the employer 1 has a levy balance of:
            | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 |
            | 750   | 750   | 750   | 750 | 1000  | 1000  |
        And the following commitments exist:
            | Employer   | Provider   | ULN       | priority | agreed price | start date | end date   |
            | employer 1 | provider A | learner a | 1        | 7500         | 01/09/2017 | 08/09/2018 |
            | employer 1 | provider B | learner b | 2        | 15000        | 01/09/2017 | 08/09/2018 |
        When the providers submit the following ILR files:
            | Provider   | ULN       | agreed price | learner type       | start date | planned end date | actual end date | completion status |
            | provider A | learner a | 7500         | programme only DAS | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
            | provider B | learner b | 15000        | programme only DAS | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
        Then the earnings and payments break down for provider A is as follows:
            | Type                            | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total           | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
            | Provider Earned from SFA        | 500   | 500   | 500   | ... | 500   | 1450  | 0     |
            | Provider Earned from Employer 1 | 0     | 0     | 0     | ... | 0     | 50    | 0     |
            | Provider Paid by SFA            | 0     | 500   | 500   | ... | 500   | 500   | 1450  |
            | Payment due from Employer 1     | 0     | 0     | 0     | ... | 0     | 0     | 50    |
            | employer 1 Levy account debited | 0     | 500   | 500   | ... | 500   | 500   | 1000  |
            | SFA Levy employer budget        | 500   | 500   | 500   | ... | 500   | 1000  | 0     |
            | SFA Levy co-funding budget      | 0     | 0     | 0     | ... | 0     | 450   | 0     |
            | SFA non-Levy co-funding budget  | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        And the earnings and payments break down for provider B is as follows:
            | Type                            | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total           | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
            | Provider Earned from SFA        | 925   | 925   | 925   | ... | 950   | 2700  | 0     |
            | Provider Earned from Employer 1 | 75    | 75    | 75    | ... | 50    | 300   | 0     |
            | Provider Paid by SFA            | 0     | 925   | 925   | ... | 925   | 950   | 2700  |
            | Payment due from Employer 1     | 0     | 75    | 75    | ... | 75    | 50    | 300   |
            | employer 1 Levy account debited | 0     | 250   | 250   | ... | 250   | 500   | 0     |
            | SFA Levy employer budget        | 250   | 250   | 250   | ... | 500   | 0     | 0     |
            | SFA Levy co-funding budget      | 675   | 675   | 675   | ... | 450   | 2700  | 0     |
            | SFA non-Levy co-funding budget  | 0     | 0     | 0     | ... | 0     | 0     | 0     |