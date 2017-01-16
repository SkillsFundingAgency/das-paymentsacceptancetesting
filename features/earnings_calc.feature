Feature: Amount due is calculated based on previously earned amount

@EarningsDistribution
Scenario Outline: Calculating payments due
Given a provider has previously earned <previous amount> in period R01
When an earning of <earned amount> is calculated for period R01
Then a payment of <due amount> is due
Examples: 
| previous amount | earned amount | due amount |
| 0               | 1000          | 1000       |
| 500             | 1000          | 500        |
| 1000            | 1000          | 0          |
| 0               | 0             | 0          |


@EarningsDistribution
Scenario Outline: Earnings distribution
When the planned course duration covers <census months> months
And there is an agreed price of <agreed price>
Then the monthly earnings is <monthly earnings>
And the completion payment is <completion payment>
Examples:
| agreed price | census months | monthly earnings | completion payment |
| 15000        | 12            | 1000             | 3000               |
| 15000        | 6             | 2000             | 3000               |
| 7500         | 12            | 500              | 1500               |
| 7500         | 6             | 1000             | 1500               |


@EarningsDistribution
Scenario Outline: Earnings distribution when learner finishes early or late
When the planned course duration covers <planned census months> months
And the actual duration of learning is <actual census months in learning> months
And there is an agreed price of <agreed price>
Then the monthly earnings is <monthly earnings>
And the completion payment is <completion payment>
And the balancing payment is <balancing payment>
Examples:
| agreed price | early/late | planned census months | actual census months in learning | monthly earnings | completion payment | balancing payment |
| 15000        | early      | 12                    | 8                                | 1000             | 3000               | 4000              |
| 15000        | early      | 6                     | 5                                | 2000             | 3000               | 2000              |
| 7500         | early      | 12                    | 9                                | 500              | 1500               | 1500              |
| 7500         | early      | 6                     | 4                                | 1000             | 1500               | 2000              |
| 15000        | late       | 12                    | 16                               | 1000             | 3000               | 0                 |
| 15000        | late       | 6                     | 7                                | 2000             | 3000               | 0                 |
| 7500         | late       | 12                    | 15                               | 500              | 1500               | 0                 |
| 7500         | late       | 6                     | 8                                | 1000             | 1500               | 0                 |