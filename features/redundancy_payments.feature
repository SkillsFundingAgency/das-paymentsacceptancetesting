
@Redundancy
Feature: Provider earnings and payments where learner is made redundant during learning

Scenario:806_AC1- DAS learner, is made redundant within the last 6 months of planned learning - receives full government funding for the rest of the programme 

        Given The learner is programme only DAS
        And the apprenticeship funding band maximum is 15000
        And levy balance > agreed price for all months
        #And the learner is made redundant less than 6 months before the planned end date
            
        And the following commitments exist:
            | commitment Id | version Id | Employer        | Provider   | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | 1             | 1          | employer 1      | provider a | learner a | 01/08/2017 | 01/08/2018 | 15000        | Active    | 01/08/2017     |              |
            
        When an ILR file is submitted with the following data:
            | ULN       | learner type       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | 
            | learner a | programme only DAS | 03/08/2017 | 20/08/2018       |                 | continuing        | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            | 
        
        And the Contract type in the ILR is:
            | contract type | date from  | date to    |
            | DAS           | 03/08/2017 | 20/02/2018 |        
            | Non-DAS       | 21/02/2018 |            |
        
        
        And the employment status in the ILR is:
            | Employer   | Employment Status      | Employment Status Applies |
            | employer 1 | in paid employment     | 02/08/2017                |
            |            | not in paid employment | 21/02/2018                |
              
        Then the provider earnings and payments break down as follows:
            | Type                           | 08/17 | 09/17 | 10/17 | ... | 01/18 | 02/18 | 03/18 | 04/18 | 05/18 |
            | Provider Earned Total          | 1000  | 1000  | 1000  | ... | 1000  | 1000  | 1000  | 1000  | 1000  |
            | Provider Earned from SFA       | 1000  | 1000  | 1000  | ... | 1000  | 1000  | 1000  | 1000  | 1000  |
            | Provider Earned from Employer  | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA           | 0     | 1000  | 1000  | ... | 1000  | 1000  | 1000  | 1000  | 1000  |
            | Refund taken by SFA            | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Payment due from Employer      | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Refund due to employer         | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Levy account debited           | 0     | 1000  | 1000  | ... | 1000  | 1000  | 0     | 0     | 0     |
            | Levy account credited          | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy employer budget       | 1000  | 1000  | 1000  | ... | 1000  | 0     | 0     | 0     | 0     |
            | SFA Levy co-funding budget     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget | 0     | 0     | 0     | ... | 0     | 1000  | 1000  | 1000  | 1000  |

@Redundancy			       
Scenario:806_AC2- DAS learner, is made redundant outside of the last 6 months of planned learning - receives full government funding for the next 12 weeks only 

        Given The learner is programme only DAS
        And the apprenticeship funding band maximum is 15000
        And levy balance > agreed price for all months
        #And the learner is made redundant less than 6 months before the planned end date
            
        And the following commitments exist:
            | commitment Id | version Id | Employer        | Provider   | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | 1             | 1          | employer 1      | provider a | learner a | 01/08/2017 | 01/08/2018 | 15000        | Active    | 01/08/2017     |              |
            
        When an ILR file is submitted with the following data:
            | ULN       | learner type       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | 
            | learner a | programme only DAS | 03/08/2017 | 20/08/2018       |                 | continuing        | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            | 
        
        And the Contract type in the ILR is:
            | contract type | date from  | date to    |
            | DAS           | 03/08/2017 | 19/02/2018 |        
            | Non-DAS       | 20/02/2018 |            |
        
        And the employment status in the ILR is:
            | Employer   | Employment Status      | Employment Status Applies |
            | employer 1 | in paid employment     | 02/08/2017                |
            |            | not in paid employment | 20/02/2018                |
              
        Then the provider earnings and payments break down as follows:
            | Type                           | 08/17 | 09/17 | 10/17 | ... | 01/18 | 02/18 | 03/18 | 04/18 | 05/18 |
            | Provider Earned Total          | 1000  | 1000  | 1000  | ... | 1000  | 1000  | 1000  | 1000  | 0     | 
            | Provider Earned from SFA       | 1000  | 1000  | 1000  | ... | 1000  | 1000  | 1000  | 1000  | 0     |  
            | Provider Earned from Employer  | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA           | 0     | 1000  | 1000  | ... | 1000  | 1000  | 1000  | 1000  | 1000  |
            | Refund taken by SFA            | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Payment due from Employer      | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Refund due to employer         | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Levy account debited           | 0     | 1000  | 1000  | ... | 1000  | 1000  | 0     | 0     | 0     |
            | Levy account credited          | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy employer budget       | 1000  | 1000  | 1000  | ... | 1000  | 0     | 0     | 0     | 0     |
            | SFA Levy co-funding budget     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget | 0     | 0     | 0     | ... | 0     | 1000  | 1000  | 1000  | 0     |

@Redundancy
Scenario:807_AC1- Non-DAS learner, is made redundant within the last 6 months of planned learning - receives full government funding for the rest of the programme 

        Given the apprenticeship funding band maximum is 15000
		#the learner is programme only non-DAS
        
        #And the learner is made redundant less than 6 months before the planned end date
            
        When an ILR file is submitted with the following data:
            | ULN       | learner type           | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | 
            | learner a | programme only non-DAS | 03/08/2017 | 20/08/2018       |                 | continuing        | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            | 
        
        And the employment status in the ILR is:
            | Employer   | Employment Status      | Employment Status Applies |
            | employer 1 | in paid employment     | 02/08/2017                |
            |            | not in paid employment | 21/02/2018                |
              
        Then the provider earnings and payments break down as follows:
            | Type                           | 08/17 | 09/17 | 10/17 | ... | 01/18 | 02/18 | 03/18 | 04/18 | 05/18 |
            | Provider Earned Total          | 1000  | 1000  | 1000  | ... | 1000  | 1000  | 1000  | 1000  | 1000  |
            | Provider Earned from SFA       | 900   | 900   | 900   | ... | 900   | 1000  | 1000  | 1000  | 1000  |
            | Provider Earned from Employer  | 100   | 100   | 100   | ... | 100   | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA           | 0     | 900   | 900   | ... | 900   | 900   | 1000  | 1000  | 1000  |
            | Refund taken by SFA            | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Payment due from Employer      | 0     | 100   | 100   | ... | 100   | 100   | 0     | 0     | 0     |
            | Refund due to employer         | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Levy account debited           | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Levy account credited          | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy employer budget       | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy co-funding budget     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget | 900   | 900   | 900   | ... | 900   | 1000  | 1000  | 1000  | 1000  |


			    
Scenario:807_AC2- Non-DAS learner, is made redundant outside of the last 6 months of planned learning - receives full government funding for the next 12 weeks only 

        Given the apprenticeship funding band maximum is 15000
        #And the learner is made redundant less than 6 months before the planned end date
            
        When an ILR file is submitted with the following data:
            | ULN       | learner type           | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | 
            | learner a | programme only non-DAS | 03/08/2017 | 20/08/2018       |                 | continuing        | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            | 
        
        And the employment status in the ILR is:
            | Employer   | Employment Status      | Employment Status Applies |
            | employer 1 | in paid employment     | 02/08/2017                |
            |            | not in paid employment | 19/02/2018                |
              
        Then the provider earnings and payments break down as follows:
            | Type                           | 08/17 | 09/17 | 10/17 | ... | 01/18 | 02/18 | 03/18 | 04/18 | 05/18 |
            | Provider Earned Total          | 1000  | 1000  | 1000  | ... | 1000  | 1000  | 1000  | 1000  | 0     |
            | Provider Earned from SFA       | 900   | 900   | 900   | ... | 900   | 1000  | 1000  | 1000  | 0     |
            | Provider Earned from Employer  | 100   | 100   | 100   | ... | 100   | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA           | 0     | 900   | 900   | ... | 900   | 900   | 1000  | 1000  | 1000  |
            | Refund taken by SFA            | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Payment due from Employer      | 0     | 100   | 100   | ... | 100   | 100   | 0     | 0     | 0     |
            | Refund due to employer         | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Levy account debited           | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | Levy account credited          | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy employer budget       | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy co-funding budget     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget | 900   | 900   | 900   | ... | 900   | 1000  | 1000  | 1000  | 0     |