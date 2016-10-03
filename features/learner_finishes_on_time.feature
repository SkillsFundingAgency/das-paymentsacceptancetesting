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

    Background:
        Given the agreed price is 15000
        And the apprenticeship funding band maximum is 17000

    Scenario: Earnings and payments for a DAS learner, levy available, learner finishes on time
        Given The learner is programme only DAS
		And levy balance > agreed price
        When an ILR file is submitted with the following data:
            | start date | planned end date | actual end date | completion status |
            | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                     | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total    | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
            | Provider Earned from SFA | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
            | Provider Paid by SFA     | 0     | 1000  | 1000  | ... | 1000  | 1000  | 3000  |
            | Levy account debited     | 0     | 1000  | 1000  | ... | 1000  | 1000  | 3000  |
            | SFA Levy budget          | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
            | SFA co-funding budget    | 0     | 0     | 0     | ... | 0     | 0     | 0     |

            
    Scenario: Earnings for a DAS learner, no levy available, learner finishes on time
        Given The learner is programme only DAS
		And levy balance = 0
        When an ILR file is submitted with the following data:
            | start date | planned end date | actual end date | completion status |
            | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
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
            

	@ignore
    Scenario: Earnings for a non-DAS learner, learner finishes on time
        Given The learner is programme only non-DAS
        When an ILR file is submitted with the following data:
            | start date | planned end date | actual end date | completion status |
            | 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
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
