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
        Given levy balance > agreed price
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
        Given levy balance = 0
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
            | SFA Levy co-funded budget     | 900   | 900   | 900   | ... | 900   | 2700  | 0     |
            | SFA non-Levy co-funding budget| 0     | 0     | 0     | ... | 0     | 0     |       |


    Scenario: 2 DAS learners, only enough levy to cover 1
        Given levy balance = 7500
        And the following commitments:
            | ULN       | priority |
            | learner a | 1        |
            | learner b | 2        |
        When an ILR file is submitted with the following data:
            | ULN       | agreed price | learner type       | start date | actual end date | completion status |
            | learner a | 7500         | programme only DAS | 01/09/2017 | 08/09/2018      | completed         |
            | learner b | 15000        | programme only DAS | 01/09/2017 | 08/09/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                          | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total         | 1500  | 1500  | 1500  | ... | 1500  | 4500  | 0     |
            | Provider Earned from SFA      | 1400  | 1400  | 1400  | ... | 1400  | 4200  | 0     |
            | Provider Earned from Employer | 100   | 100   | 100   | ... | 100   | 300   | 0     |
            | Provider Paid by SFA          | 0     | 1400  | 1400  | ... | 1400  | 1400  | 4200  |
            | Payment due from Employer     | 0     | 100   | 100   | ... | 100   | 100   | 300   |
            | Levy account debited          | 0     | 500   | 500   | ... | 500   | 500   | 1500  |
            | SFA Levy employer budget      | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
            | SFA Levy co-funded budget     | 900   | 900   | 900   | ... | 900   | 2700  | 0     |
            | SFA non-Levy co-funding budget| 0     | 0     | 0     | ... | 0     | 0     |       |


    @ignore
    Scenario: A non-DAS learner, learner finishes on time
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
            | SFA Levy co-funded budget     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget| 900   | 900   | 900   | ... | 900   | 2700  | 0     |
