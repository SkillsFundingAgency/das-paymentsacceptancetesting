Feature: The ILR is submitted late

    When a provider submits an ILR several months after learning has started (but before the academic year boundary), the earnings calculation is updated retrospectively and the provider gets paid for the preceding months.

    Background:
        Given the apprenticeship funding band maximum for each learner is 17000
        And levy balance > agreed price for all months

    Scenario: ILR submitted late for a DAS learner, levy available, learner finishes on time
		Given the following commitments exist:
            | ULN       | priority | start date | end date   | agreed price |
            | learner a | 1        | 01/09/2017 | 08/09/2018 | 15000        |
        When an ILR file is submitted for the first time on 28/12/17 with the following data:
            | learner type       | agreed price | start date | planned end date | completion status |
            | programme only DAS | 15000        | 01/09/2017 | 08/09/2018       | continuing        |
        Then the provider earnings and payments break down as follows:
            | Type                       | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 | 02/18 | ... |
            | Provider Earned Total      | 1000  | 1000  | 1000  | 1000  | 1000  | 1000  | ... |
            | Provider Earned from SFA   | 1000  | 1000  | 1000  | 1000  | 1000  | 1000  | ... |
            | Provider Paid by SFA       | 0     | 0     | 0     | 0     | 4000  | 1000  | ... |
            | Levy account debited       | 0     | 0     | 0     | 0     | 4000  | 1000  | ... |
            | SFA Levy employer budget   | 1000  | 1000  | 1000  | 1000  | 1000  | 1000  | ... |
            | SFA Levy co-funding budget | 0     | 0     | 0     | 0     | 0     | 0     | ... |
