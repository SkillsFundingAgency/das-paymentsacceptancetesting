Feature: Business definitions

Scenario Outline: Learner type
    When the learner is age <Age>
    And the contract type is <DAS contract status>
    And English is <English component>
    And maths is <Maths component>
    And learning support is <Has learning support>
    And incentive payments due are <Incentive payments>
    And no ILR validation errors
    Then the learner is <learner type>

    Examples:
    | Age | DAS contract status | English component | Maths component | Has learning support | Incentive payments | learner type       |
    | 30  | DAS                 | no                | no              | does not exist       | none               | programme only DAS |
    | 30  | non-DAS             | no                | no              | does not exist       | none               | programme non-DAS  |

Scenario Outline: Calculating census months
    Given the census date is the last date of the month
    When the start date is <start date>
    And the planned end date is <planned end date>
    Then the number of census months is <census months>

    Examples:
    | start date | planned end date | census months |
    | 01/09/2017 | 08/09/2018       | 12            |
    | 01/09/2017 | 29/09/2018       | 12            |
    | 01/09/2017 | 30/09/2018       | 13            |

Scenario Outline: Earnings distribution
    When the planned course duration covers <census months> months
    And an agreed price of 15000
    Then the monthly earnings is <monthly earnings>
    And the completion payment is <completion payment>

    Examples:
    | census months | monthly earnings | completion payment |
    | 12            | 1000             | 3000               |
    | 6             | 2000             | 3000               |