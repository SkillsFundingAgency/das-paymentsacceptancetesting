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



Scenario Outline: Payment type breakdown
Given the account has a balance of <balance>
When payment of <due amount> is due
Then a levy payment of <levy amount> is made
And a government payment of <government amount> is made
And a employer payment of <employer amount> is expected
Examples:
| balance | due amount | levy amount | government amount | employer amount |
| 99999   | 1000       | 1000        | 0                 | 0               |
| 500     | 1000       | 500         | 450               | 50              |
| 0       | 1000       | 0           | 900               | 100             |


