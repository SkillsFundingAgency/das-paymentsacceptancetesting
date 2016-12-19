
Feature: 2 learners, paid in priority order

Background:
       
Given Two learners are programme only DAS 
    And the apprenticeship funding band maximum for each learner is 17000
        
Scenario: Earnings and payments for two DAS learners, levy is spent in priority order and available for both learners
     
Given the employer's levy balance is:
		| 08/17 | 09/17 | 10/17 | 11/17 | ...  | 08/18 | 09/18 |
		| 2000  | 2000  | 2000  | 2000  | 2000 | 2000  | 2000  |

And the following commitments exist on 03/12/2017:
    | priority | ULN | Price effective date | planned end date | Agreed Price |
    | 1        | 123 | 01/08/2017           | 28/08/2018       | 15000        |
    | 2        | 456 | 01/08/2017           | 28/08/2018       | 15000        |
            
When an ILR file is submitted on 03/12/2017 with the following data:
    | ULN   | start date | planned end date | actual end date | completion status | Total training price | Total assessment price | 
    | 123   | 01/08/2017 | 28/08/2018       |                 | continuing        | 12000                | 3000                   |
    | 456   | 01/08/2017 | 28/08/2018       |                 | continuing        | 12000                | 3000                   |                                                                    
               			          
Then the provider earnings and payments break down for ULN 123 as follows:
            | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | ... | 07/18 | 08/18 |
            | Provider Earned Total          | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
            | Provider Earned from SFA       | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
            | Provider Earned from Employer  | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Provider Paid by SFA           | 0     | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
            | Payment due from Employer      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Levy account debited           | 0     | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
            | SFA Levy employer budget       | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
            | SFA Levy co-funded budget      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | SFA non-Levy co-funding budget | 0     | 0     | 0     | 0     | ... | 0     | 0     |

And the provider earnings and payments break down for ULN 456 as follows:
            | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | ... | 07/18 | 08/18 |
            | Provider Earned Total          | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
            | Provider Earned from SFA       | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
            | Provider Earned from Employer  | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Provider Paid by SFA           | 0     | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
            | Payment due from Employer      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Levy account debited           | 0     | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
            | SFA Levy employer budget       | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
            | SFA Levy co-funded budget      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | SFA non-Levy co-funding budget | 0     | 0     | 0     | 0     | ... | 0     | 0     |            
            
And the provider earnings and payments break down as follows:
            | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | ... | 07/18 | 08/18 |
            | Provider Earned Total          | 2000  | 2000  | 2000  | 2000  | ... | 2000  | 0     |
            | Provider Earned from SFA       | 2000  | 2000  | 2000  | 2000  | ... | 2000  | 0     |
            | Provider Earned from Employer  | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Provider Paid by SFA           | 0     | 2000  | 2000  | 2000  | ... | 2000  | 2000  |
            | Payment due from Employer      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | Levy account debited           | 0     | 2000  | 2000  | 2000  | ... | 2000  | 2000  |
            | SFA Levy employer budget       | 2000  | 2000  | 2000  | 2000  | ... | 2000  | 0     |
            | SFA Levy co-funded budget      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
            | SFA non-Levy co-funding budget | 0     | 0     | 0     | 0     | ... | 0     | 0     |  
