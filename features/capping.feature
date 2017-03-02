Feature: Provider earnings and payments for a learner with a negotiated price above the funding band cap

Scenario: 640-AC01-Payment for a DAS learner with a negotiated price above the funding band cap
    Given levy balance > agreed price for all months
    And the apprenticeship funding band maximum is 15000
    And the following commitments exist:
        | commitment Id | version Id | provider   | ULN       | start date | end date   | agreed price | standard code | status | effective from | effective to |
        | 1             | 1          | provider a | learner a | 01/08/2017 | 01/08/2018 | 18000        | 50            | active | 01/08/2017     |              |
    When an ILR file is submitted with the following data:
        | provider   | ULN       | learner type       | agreed price | start date | planned end date | actual end date | completion status | standard code |
        | provider a | learner a | programme only DAS | 18000        | 06/08/2017 | 08/08/2018       |                 | continuing        | 50            |
    Then the following capping will apply to the price episodes:
        | provider   | price episode | negotiated price | funding cap | previous funding paid | price above cap | effective price for SFA payments |
        | provider a | 08/17 - 08/18 | 18000            | 15000       | 0                     | 3000            | 15000                            |
    And the provider earnings and payments break down as follows:
        | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
        | Provider Earned Total               | 1000  | 1000  | 1000  | 1000  | 1000  |
        | Provider Earned from SFA            | 1000  | 1000  | 1000  | 1000  | 1000  |
        | Provider Earned from Employer       | 0     | 0     | 0     | 0     | 0     |
        | Provider Paid by SFA                | 0     | 1000  | 1000  | 1000  | 1000  |
        | Payment due from Employer           | 0     | 0     | 0     | 0     | 0     |
        | Levy account debited                | 0     | 1000  | 1000  | 1000  | 1000  |
        | SFA Levy employer budget            | 1000  | 1000  | 1000  | 1000  | 1000  |
        | SFA Levy co-funding budget          | 0     | 0     | 0     | 0     | 0     |
        | SFA Levy additional payments budget | 0     | 0     | 0     | 0     | 0     |
        | SFA non-Levy co-funding budget      | 0     | 0     | 0     | 0     | 0     |
    And the transaction types for the payments are:
        | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 |
        | On-program                     | 1000  | 1000  | 1000  | 1000  |
        | Completion                     | 0     | 0     | 0     | 0     |
        | Balancing                      | 0     | 0     | 0     | 0     |
        | Employer 16-18 incentive       | 0     | 0     | 0     | 0     |
        | Provider 16-18 incentive       | 0     | 0     | 0     | 0     |
