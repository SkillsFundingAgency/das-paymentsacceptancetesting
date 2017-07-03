
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

			
Scenario:808_AC1- DAS learner, is made redundant within the last 6 months of planned learning - receives full government funding for the rest of the programme, a provider incentive, but no employer incentive
 
	    Given The learner is programme only DAS
        And the apprenticeship funding band maximum is 9000
        And levy balance > agreed price for all months
       # And the learner is made redundant less than 6 months before the planned end date
	    And the following commitments exist:
            | commitment Id | version Id | ULN       | framework code | programme type | pathway code | start date | end date   | agreed price | status    | effective from | effective to |
            | 1             | 1          | learner a | 403            | 2              | 1            | 01/08/2017 | 01/08/2018 | 6125         | Active    | 01/08/2017     |              |
                   
	    When an ILR file is submitted with the following data:
	    	| ULN       | learner type             | aim type         | framework code | programme type | pathway code | agreed price | aim rate | start date | planned end date | actual end date | completion status |
	    	| learner a | 16-18 programme only DAS | programme        | 403            | 2              | 1            | 6125         |          | 06/08/2017 | 20/10/2018       | 08/08/2018      | completed         |
	    	| learner a | 16-18 programme only DAS | maths or english | 403            | 2              | 1            |              | 471      | 06/08/2017 | 06/10/2018       | 06/10/2018      | completed         |
            
        And the Contract type in the ILR is:
            | contract type | date from  | date to    |
            | DAS           | 06/08/2017 | 20/04/2018 |        
            | Non-DAS       | 21/04/2018 |            |
        
        And the employment status in the ILR is:
            | Employer   | Employment Status      | Employment Status Applies |
            | employer 1 | in paid employment     | 05/08/2017                |
            |            | not in paid employment | 21/04/2018                |
            
          And the learning support status of the ILR is:
            | Learning support code | date from  | date to    |
            | 1                     | 06/08/2017 | 20/10/2018 |
              
            
        Then the provider earnings and payments break down as follows:
	    	| Type                                    | 08/17  | 09/17  | 10/17  | 11/17   | 12/17   | ... | 03/18  | 04/18  | 05/18  | ... | 08/18     | 09/18   | 10/18  | 11/18 |
	    	| Provider Earned Total                   | 636.50 | 636.50 | 636.50 | 1636.50 | 636.50  | ... | 636.50 | 636.50 | 636.50 | ... | 3174.36   | 183.64  | 0      | 0     |
	    	| Provider Earned from SFA                | 636.50 | 636.50 | 636.50 | 1636.50 | 636.50  | ... | 636.50 | 636.50 | 636.50 | ... | 3174.36   | 183.64  | 0      | 0     |
	    	| Provider Earned from Employer           | 0      | 0      | 0      | 0       | 0       | ... | 0      | 0      | 0      | ... | 0         | 0       | 0      | 0     |
	    	| Provider Paid by SFA                    | 0      | 636.50 | 636.50 | 636.50  | 1636.50 | ... | 636.50 | 636.50 | 636.50 | ... | 636.50    | 3174.36 | 183.64 | 0     |
	    	| Payment due from Employer               | 0      | 0      | 0      | 0       | 0       | ... | 0      | 0      | 0      | ... | 0         | 0       | 0      | 0     |
	    	| Levy account debited                    | 0      | 350    | 350    | 350     | 350     | ... | 350    | 350    | 0      | ... | 0         | 0       | 0      | 0     |
	    	| SFA Levy employer budget                | 350    | 350    | 350    | 350     | 350     | ... | 350    | 0      | 0      | ... | 0         | 0       | 0      | 0     |
	    	| SFA Levy co-funding budget              | 0      | 0      | 0      | 0       | 0       | ... | 0      | 0      | 0      | ... | 0         | 0       | 0      | 0     |
	    	| SFA non-Levy co-funding budget          | 0      | 0      | 0      | 0       | 0       | ... | 0      | 350    | 350    | ... | 1925      | 0       | 0      | 0     |
	    	| SFA Levy additional payments budget     | 286.50 | 286.50 | 286.50 | 1286.50 | 286.50  | ... | 286.50 | 0      | 0      | ... | 0         | 0       | 0      | 0     |
	    	| SFA non-Levy additional payments budget | 0      | 0      | 0      | 0       | 0       | ... | 0      | 286.50 | 286.50 | ... | 1249.3571 | 183.64  | 0      | 0     |  
        
        And the transaction types for the payments are:
	    	| Payment type                   | 09/17  | 10/17  | 11/17  | 12/17  | ... | 02/18  | 03/18  | 04/18  | ... | 09/18     | 10/18 | 11/18 |
	    	| On-program                     | 350    | 350    | 350    | 350    | ... | 350    | 350    | 350    | ... | 0         | 0     | 0     |
	    	| Completion                     | 0      | 0      | 0      | 0      | ... | 0      | 0      | 0      | ... | 1225      | 0     | 0     |
	    	| Balancing                      | 0      | 0      | 0      | 0      | ... | 0      | 0      | 0      | ... | 700       | 0     | 0     |
	    	| English and maths on programme | 33.64  | 33.64  | 33.64  | 33.64  | ... | 33.64  | 33.64  | 33.64  | ... | 33.64     | 33.64 | 0     |
	    	| English and maths Balancing    | 0      | 0      | 0      | 0      | ... | 0      | 0      | 0      | ... | 0         | 0     | 0     |
	    	| Employer 16-18 incentive       | 0      | 0      | 0      | 500    | ... | 0      | 0      | 0      | ... | 0         | 0     | 0     |
	    	| Provider 16-18 incentive       | 0      | 0      | 0      | 500    | ... | 0      | 0      | 0      | ... | 500       | 0     | 0     |
	    	| Framework uplift on-program    | 102.86 | 102.86 | 102.86 | 102.86 | ... | 102.86 | 102.86 | 102.86 | ... | 0         | 0     | 0     |
	    	| Framework uplift completion    | 0      | 0      | 0      | 0      | ... | 0      | 0      | 0      | ... | 360       | 0     | 0     |
	    	| Framework uplift balancing     | 0      | 0      | 0      | 0      | ... | 0      | 0      | 0      | ... | 205.71429 | 0     | 0     |
	    	| Provider disadvantage uplift   | 0      | 0      | 0      | 0      | ... | 0      | 0      | 0      | ... | 0         | 0     | 0     |
	    	| Provider learning support      | 150    | 150    | 150    | 150    | ... | 150    | 150    | 150    | ... | 150       | 150   | 0     |   


Scenario:808-AC2- DAS learner, is made redundant outside the last 6 months of planned learning - receives full government funding for the next 12 weeks only
 
	    Given The learner is programme only DAS
        And the apprenticeship funding band maximum is 9000
        And levy balance > agreed price for all months
        #And the learner is made redundant less than 6 months before the planned end date
        
	    And the following commitments exist:
            | commitment Id | version Id | ULN       | framework code | programme type | pathway code | start date | end date   | agreed price | status    | effective from | effective to |
            | 1             | 1          | learner a | 403            | 2              | 1            | 01/08/2017 | 01/08/2018 | 6125         | Active    | 01/08/2017     |              |
                   
	    When an ILR file is submitted with the following data:
	    	| ULN       | learner type             | aim type         | framework code | programme type | pathway code | agreed price | aim rate | start date | planned end date | actual end date | completion status |
	    	| learner a | 16-18 programme only DAS | programme        | 403            | 2              | 1            | 6125         |          | 06/08/2017 | 20/10/2018       | 08/08/2018      | completed         |
	    	| learner a | 16-18 programme only DAS | maths or english | 403            | 2              | 1            |              | 471      | 06/08/2017 | 06/10/2018       | 06/10/2018      | completed         |
            
        And the Contract type in the ILR is:
            | contract type | date from  | date to    |
            | DAS           | 06/08/2017 | 20/04/2018 |        
            | Non-DAS       | 21/04/2018 |            |
        
        And the employment status in the ILR is:
            | Employer   | Employment Status      | Employment Status Applies |
            | employer 1 | in paid employment     | 05/08/2017                |
            |            | not in paid employment | 21/04/2018                |
            
          And the learning support status of the ILR is:
            | Learning support code | date from  | date to    |
            | 1                     | 06/08/2017 | 20/10/2018 |
              
            
	    Then the provider earnings and payments break down as follows:
	    	| Type                                    | 08/17  | 09/17  | 10/17  | 11/17   | 12/17   | ... | 03/18  | 04/18  | 05/18  | 06/18  | 07/18  | 08/18  | 09/18  | 10/18  | 11/18 |
	    	| Provider Earned Total                   | 636.50 | 636.50 | 636.50 | 1636.50 | 636.50  | ... | 636.50 | 636.50 | 636.50 | 636.50 | 183.64 | 183.64 | 183.64 | 0      | 0     |
	    	| Provider Earned from SFA                | 636.50 | 636.50 | 636.50 | 1636.50 | 636.50  | ... | 636.50 | 636.50 | 636.50 | 636.50 | 0      | 0      | 0      | 0      | 0     |
	    	| Provider Earned from Employer           | 0      | 0      | 0      | 0       | 0       | ... | 0      | 0      | 0      | 0      | 0      | 0      | 0      | 0      | 0     |
	    	| Provider Paid by SFA                    | 0      | 636.50 | 636.50 | 636.50  | 1636.50 | ... | 636.50 | 636.50 | 636.50 | 636.50 | 636.50 | 183.64 | 183.64 | 183.64 | 0     |
	    	| Payment due from Employer               | 0      | 0      | 0      | 0       | 0       | ... | 0      | 0      | 0      | 0      | 0      | 0      | 0      | 0      | 0     |
	    	| Levy account debited                    | 0      | 350    | 350    | 350     | 350     | ... | 350    | 350    | 0      | 0      | 0      | 0      | 0      | 0      | 0     |
	    	| SFA Levy employer budget                | 350    | 350    | 350    | 350     | 350     | ... | 350    | 0      | 0      | 0      | 0      | 0      | 0      | 0      | 0     |
	    	| SFA Levy co-funding budget              | 0      | 0      | 0      | 0       | 0       | ... | 0      | 0      | 0      | 0      | 0      | 0      | 0      | 0      | 0     |
	    	| SFA non-Levy co-funding budget          | 0      | 0      | 0      | 0       | 0       | ... | 0      | 350    | 350    | 350    | 0      | 0      | 0      | 0      | 0     |
	    	| SFA Levy additional payments budget     | 286.50 | 286.50 | 286.50 | 1286.50 | 286.50  | ... | 286.50 | 0      | 0      | 0      | 0      | 0      | 0      | 0      | 0     |
	    	| SFA non-Levy additional payments budget | 0      | 0      | 0      | 0       | 0       | ... | 0      | 286.50 | 286.50 | 286.50 | 183.64 | 183.64 | 183.64 | 0      | 0     |
        
        And the transaction types for the payments are:
	    	| Payment type                   | 09/17  | 10/17  | 11/17  | 12/17  | ... | 02/18  | 03/18  | 04/18  | 05/18  | 06/18  | 07/18  | 08/18 |
	    	| On-program                     | 350    | 350    | 350    | 350    | ... | 350    | 350    | 350    | 350    | 350    | 350    | 0     |
	    	| Completion                     | 0      | 0      | 0      | 0      | ... | 0      | 0      | 0      | 0      | 0      | 0      | 0     |
	    	| Balancing                      | 0      | 0      | 0      | 0      | ... | 0      | 0      | 0      | 0      | 0      | 0      | 0     |
	    	| English and maths on programme | 33.64  | 33.64  | 33.64  | 33.64  | ... | 33.64  | 33.64  | 33.64  | 33.64  | 33.64  | 33.64  | 33.64 |
	    	| English and maths Balancing    | 0      | 0      | 0      | 0      | ... | 0      | 0      | 0      | 0      | 0      | 0      | 0     |
	    	| Employer 16-18 incentive       | 0      | 0      | 0      | 500    | ... | 0      | 0      | 0      | 0      | 0      | 0      | 0     |
	    	| Provider 16-18 incentive       | 0      | 0      | 0      | 500    | ... | 0      | 0      | 0      | 0      | 0      | 0      | 0     |
	    	| Framework uplift on-program    | 102.86 | 102.86 | 102.86 | 102.86 | ... | 102.86 | 102.86 | 102.86 | 102.86 | 102.86 | 102.86 | 0     |
	    	| Framework uplift completion    | 0      | 0      | 0      | 0      | ... | 0      | 0      | 0      | 0      | 0      | 0      | 0     |
	    	| Framework uplift balancing     | 0      | 0      | 0      | 0      | ... | 0      | 0      | 0      | 0      | 0      | 0      | 0     |
	    	| Provider disadvantage uplift   | 0      | 0      | 0      | 0      | ... | 0      | 0      | 0      | 0      | 0      | 0      | 0     |
	    	| Provider learning support      | 150    | 150    | 150    | 150    | ... | 150    | 150    | 150    | 150    | 150    | 150    | 150   |  
                                                                                   
            
Scenario:814-AC1- DAS learner, is made redundant within the last 6 months of planned learning and completes late - receives full government funding for the rest of the programme 

        Given The learner is programme only DAS
        And the apprenticeship funding band maximum is 15000
        And levy balance > agreed price for all months
       # And the learner is made redundant less than 6 months before the planned end date
            
        And the following commitments exist:
            | commitment Id | version Id | Employer        | Provider   | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | 1             | 1          | employer 1      | provider a | learner a | 01/08/2017 | 01/08/2018 | 15000        | Active    | 01/08/2017     |              |
            
        When an ILR file is submitted with the following data:
            | ULN       | learner type       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | 
            | learner a | programme only DAS | 03/08/2017 | 20/08/2018       | 20/09/2018      | completed         | 12000                | 03/08/2017                          | 3000                   | 03/08/2017                            | 
        
        And the Contract type in the ILR is:
            | contract type | date from  | date to    |
            | DAS           | 03/08/2017 | 20/02/2018 |        
            | Non-DAS       | 21/02/2018 | 20/09/2018 |
        
        
        And the employment status in the ILR is:
            | Employer   | Employment Status      | Employment Status Applies |
            | employer 1 | in paid employment     | 02/08/2017                |
            |            | not in paid employment | 21/02/2018                |
              
        Then the provider earnings and payments break down as follows:
            | Type                           | 08/17 | 09/17 | 10/17 | ... | 01/18 | 02/18 | 03/18 | 04/18 | 05/18 | ... | 07/18 | 08/18 | 09/18 | 10/18 |
            | Provider Earned Total          | 1000  | 1000  | 1000  | ... | 1000  | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     | 3000  | 0     |
            | Provider Earned from SFA       | 1000  | 1000  | 1000  | ... | 1000  | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     | 3000  | 0     |
            | Provider Earned from Employer  | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA           | 0     | 1000  | 1000  | ... | 1000  | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 1000  | 0     | 3000  | 
            | Refund taken by SFA            | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 
            | Payment due from Employer      | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 
            | Refund due to employer         | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
            | Levy account debited           | 0     | 1000  | 1000  | ... | 1000  | 1000  | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
            | Levy account credited          | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
            | SFA Levy employer budget       | 1000  | 1000  | 1000  | ... | 1000  | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
            | SFA Levy co-funding budget     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget | 0     | 0     | 0     | ... | 0     | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     | 3000  | 0     |
