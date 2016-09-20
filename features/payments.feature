Feature: Provider payments

    For DAS learners, where there is no lag in ILR submission, payments follow these rules:
    - Provider payment follows the month after earnings
    - The levy account is debited in the same month as payment is made
    - Spend against budget is represented against the month in which funding is earned
    - Where a levy account is used for funding, payments are made against the SFA Levy budget

Background:
    Given The learner is normal DAS
    And the agreed price is 15000
    And the apprenticeship funding band maximum is 17000
    And levy balance > agreed price
    
   Scenario: Payments for a DAS learner, levy available, learner finishes on time
    Given the following earnings:
      | Type                  | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 | 
      | Provider Earned       | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     | 
    Then the provider payments break down as follows:
      | Type                  | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 | 
      | Provider Paid         | 0     | 1000  | 1000  | ... | 1000  | 1000  | 3000  | 
      | Levy account debited  | 0     | 1000  | 1000  | ... | 1000  | 1000  | 3000  |
      | SFA Levy budget       | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
      | SFA co-funding budget | 0     | 0     | 0     | ... | 0     | 0     | 0     |
