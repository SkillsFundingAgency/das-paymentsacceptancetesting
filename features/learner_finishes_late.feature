Feature: Provider earnings and payments where learner completes later than planned and is funded by levy

    The earnings and payment rules for late completions are the same as for learners finishing on time, except that the completion payment is held back until completion.

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
            | Type                       | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 | 11/18 |
            | Provider Earned Total      | 1000  | 1000  | 1000  | ... | 1000  | 0     | 3000  | 0     |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | ... | 1000  | 0     | 3000  | 0     |
            | Provider Paid by SFA       | 0     | 1000  | 1000  | ... | 1000  | 1000  | 0     | 3000  |
            | Levy account debited       | 0     | 1000  | 1000  | ... | 1000  | 1000  | 0     | 3000  |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | ... | 1000  | 0     | 3000  | 0     |
            | SFA Levy co-funding budget | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
