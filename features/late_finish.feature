Feature: Provider earnings and payments where learner completes later than planned and is funded by levy

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
        Given The learner is programme only DAS
        And the agreed price is 15000
        And the apprenticeship funding band maximum is 17000
        And levy balance > agreed price

    Scenario: Earnings for a DAS learner, levy available, learner finishes late
        When an ILR file is submitted with the following data:
            | start date | planned end date | actual end date | completion status |
            | 01/09/2017 | 08/09/2018       | 08/10/2018      | completed         |
        Then the provider earnings and payments break down as follows:
            | Type                  | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 | 11/18 |
            | Provider Earned       | 1000  | 1000  | 1000  | ... | 1000  | 0     | 3000  | 0     | 
            | Provider Paid         | 0     | 1000  | 1000  | ... | 1000  | 1000  | 0     | 3000  |
            | Levy account debited  | 0     | 1000  | 1000  | ... | 1000  | 1000  | 0     | 3000  |
            | SFA Levy budget       | 1000  | 1000  | 1000  | ... | 1000  | 0     | 3000  | 0     |
            | SFA co-funding budget | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |

            
