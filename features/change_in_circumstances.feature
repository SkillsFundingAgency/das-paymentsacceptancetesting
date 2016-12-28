Feature: Provider earnings and payments where learner changes apprenticeship standard

Background:
Given The learner is programme only DAS
And the apprenticeship funding band maximum is 17000
And levy balance > agreed price for all months

Scenario: Earnings and payments for a DAS learner, levy available, where the apprenticeship standard changes and data lock is passed in both instances
 
Given the following commitments exist on 03/12/2017:
      | commitment Id | ULN       | standard code | price effective date | planned end date | actual end date | agreed price |
      | 1             | learner a | 51            | 01/08/2017           | 01/08/2018       | 31/10/2017      | 15000        |
      | 2             | learner a | 52            | 03/11/2017           | 01/08/2018       |                 | 5625         |

When an ILR file is submitted on 03/12/2017 with the following data:
		| ULN       | standard code | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date |
		| learner a | 51            | 03/08/2017 | 01/08/2018       | 31/10/2017      | transferred       | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            |
		| learner a | 52            | 03/11/2017 | 01/08/2018       |                 | continuing        | 4500                 | 03/11/2017                          | 1125                   | 03/11/2017                            |

Then the data lock status of the ILR in 03/12/2017 is:
        | Type                | 08/17 - 10/17 | 11/17 onwards |
        | Matching commitment | 1             | 2             |
		        
And the provider earnings and payments break down as follows:
        | Type                          | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
        | Provider Earned Total         | 1000  | 1000  | 1000  | 500   | 500   |
        | Provider Earned from SFA      | 1000  | 1000  | 1000  | 500   | 500   |
        | Provider Paid by SFA          | 0     | 1000  | 1000  | 1000  | 500   |
        | Employer Levy account debited | 0     | 1000  | 1000  | 1000  | 500   |
        | SFA Levy employer budget      | 1000  | 1000  | 1000  | 500   | 0     |
        | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | 0     |


		
Scenario: ILR changes before second Commitment starts (i.e. there is only one existing Commitment in place)
 
Given the following commitments exist on 03/12/2017:
      | commitment Id | ULN       | standard code | price effective date | planned end date | actual end date | agreed price |
      | 1             | learner a | 51            | 03/08/2017           | 04/08/2018       | 31/10/2017      | 15000        |
    
When an ILR file is submitted on 03/12/2017 with the following data:
		| ULN       | standard code | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date |
		| learner a | 51            | 03/08/2017 | 01/08/2018       | 31/10/2017      | transferred       | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            |
		| learner a | 52            | 03/11/2017 | 01/08/2018       |                 | continuing        | 4500                 | 03/11/2017                          | 1125                   | 03/11/2017                            |

Then the data lock status of the ILR in 03/12/2017 is:
        | Type                | 08/17 - 10/17 | 11/17 onwards |
        | Matching commitment | 1             | FAIL          |
		        
And the provider earnings and payments break down as follows:
        | Type                          | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
        | Provider Earned Total         | 1000  | 1000  | 1000  | 500   | 500   |
        | Provider Earned from SFA      | 1000  | 1000  | 1000  | 500   | 500   |
        | Provider Paid by SFA          | 0     | 1000  | 1000  | 1000  | 0     |
        | Employer Levy account debited | 0     | 1000  | 1000  | 1000  | 0     |
        | SFA Levy employer budget      | 1000  | 1000  | 1000  | 0     | 0     |
        | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | 0     |


		
