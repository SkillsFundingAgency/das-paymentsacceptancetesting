Feature: Amount due is calculated based on previously earned amount


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

Scenario Outline: Earnings distribution
When the planned course duration covers <census months> months
And an agreed price of <agreed price>
Then the monthly earnings is <monthly earnings>
And the completion payment is <completion payment>
Examples:
| agreed price | census months | monthly earnings | completion payment |
| 15000        | 12            | 1000             | 3000               |
| 15000        | 6             | 2000             | 3000               |
| 7500         | 12            | 500              | 1500               |
| 7500         | 6             | 1000             | 1500               |