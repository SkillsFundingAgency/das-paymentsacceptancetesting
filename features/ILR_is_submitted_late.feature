Feature: The ILR is submitted late

    When a provider submits an ILR several months after learning has started (but before the academic year boundary), the earnings calculation is updated retrospectively and the provider gets paid for the preceding months.

    Background:
        Given the apprenticeship funding band maximum for each learner is 17000
        And levy balance > agreed price for all months

    Scenario: A DAS learner, levy available, learner finishes on time
        When an ILR file is submitted in 12/2017 with the following data:
            | learner type       | start date | planned end date | completion status |
            | programme only DAS | 01/09/2017 | 08/09/2018       | continuing        |
        Then the provider earnings and payments break down as follows:
            | Type                       | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 | 02/18 | ... |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 1000  | 1000  | 1000  | ... |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | 1000  | 1000  | 1000  | ... |
            | Provider Paid by SFA       | 0     | 0     | 0     | 0     | 4000  | 1000  | ... |
            | Levy account debited       | 0     | 0     | 0     | 0     | 4000  | 1000  | ... |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 1000  | 1000  | 1000  | ... |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 1000  | 1000  | ... |
