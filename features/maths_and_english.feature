Feature:  Maths and English

#Background information: 
#
#Providers are paid £471 per aim. this is funded from outside the total price and is flat-profiled across the planned number of months in learning for that aim. There is no N+1, there is no money held back for completion. 
#If the learner has english and maths needs above level 2 (apprenticeship requires higher) this is funded within the total price.
#It is permissible for a Level 2 apprentice to fail level 2 English & Maths, retake the learning, but complete the programme because they have already met the policy.

Scenario: DPP-597a Non DAS Learner, taking single level 2 aim, fails, retakes beyond programme end, completes to time

Given the apprenticeship funding band maximum is 15000
	
	When an ILR file is submitted with the following data:
		  | ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | restart indicator | aim type         |
		  | learner a | 19-24 programme only non-DAS | 15000        | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | NO                | programme        |
		  | learner a | 19-24 programme only non-DAS | 471          | 06/08/2017 | 08/06/2018       | 08/05/2018      | withdrawn         | NO                | maths or english |
		  | learner a | 19-24 programme only non-DAS | 471          | 09/06/2018 | 08/06/2019       | 08/06/2019      | continuing        | YES               | maths or english |
		  

    Then the provider earnings and payments break down as follows:
		  | Type                                    | 08/17   | 09/17   | 10/17   | ... | 04/18   | 05/18  | 06/18   | 07/18   | 08/18   | 09/18   | ... | 05/19 | 06/19 |
		  | Provider Earned Total                   | 1047.10 | 1047.10 | 1047.10 | ... | 1047.10 | 1000   | 1039.25 | 1039.25 | 3039.25 | 39.25   | ... | 39.25 | 0     |
		  | Provider Paid by SFA                    | 0       | 947.10  | 947.10  | ... | 947.10  | 947.10 | 900     | 939.25  | 939.25  | 2739.25 | ... | 39.25 | 39.25 |
		  | Payment due from Employer               | 0       | 100     | 100     | ... | 100     | 100    | 100     | 100     | 100     | 300     | ... | 0     | 0     |
		  | Levy account debited                    | 0       | 0       | 0       | ... | 0       | 0      | 0       | 0       | 0       | 0       | ... | 0     | 0     |
		  | SFA Levy employer budget                | 0       | 0       | 0       | ... | 0       | 0      | 0       | 0       | 0       | 0       | ... | 0     | 0     |
		  | SFA levy co-funding budget              | 0       | 0       | 0       | ... | 0       | 0      | 0       | 0       | 0       | 0       | ... | 0     | 0     |
		  | SFA non-levy co-funding budget          | 900     | 900     | 900     | ... | 900     | 900    | 900     | 900     | 2700    | 0       | ... | 0     | 0     |
		  | SFA non-Levy additional payments budget | 47.10   | 47.10   | 47.10   | ... | 47.10   | 0      | 39.25   | 39.25   | 39.25   | 39.25   | ... | 39.25 | 0     |
		  | SFA levy additional payments budget     | 0       | 0       | 0       | ... | 0       | 0      | 0       | 0       | 0       | 0       | ... | 0     | 0     |
		  
    And the transaction types for the payments are:
		  | Payment type                   | 09/17 | 10/17 | ... | 04/18 | 05/18 | 06/18 | 07/18 | 08/18 | 09/18 | ... | 05/19 | 06/19 |
		  | On-program                     | 900   | 900   | ... | 900   | 900   | 900   | 900   | 900   | 0     | ... | 0     | 0     |
		  | Completion                     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | 2700  | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths on programme | 47.10 | 47.10 | ... | 47.10 | 47.10 | 0     | 39.25 | 39.25 | 39.25 | ... | 39.25 | 39.25 |
		  | English and maths Balancing    | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |





Scenario: DPP-597b DAS Learner, taking single level 2 aim, fails, retakes beyond programme end, completes to time
	Given levy balance > agreed price for all months

	And the following commitments exist:
		  | ULN       | start date  	      | end date           | agreed price | status   |
		  | learner a | 06/08/2017            | 08/08/2018         | 15000        | active   |

	When an ILR file is submitted with the following data:
		  | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | restart indicator | aim type         |
		  | learner a | 19-24 programme only DAS | 15000        | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | NO                | programme        |
		  | learner a | 19-24 programme only DAS | 471          | 06/08/2017 | 08/06/2018       | 08/05/2018      | withdrawn         | NO                | maths or english |
		  | learner a | 19-24 programme only DAS | 471          | 09/06/2018 | 08/06/2019       | 08/06/2019      | continuing        | YES               | maths or english |
	
	Then the provider earnings and payments break down as follows:
		  | Type                                    | 08/17   | 09/17   | 10/17   | ... | 04/18   | 05/18   | 06/18   | 07/18   | 08/18   | 09/18   | ... | 05/19 | 06/19 |
		  | Provider Earned Total                   | 1047.10 | 1047.10 | 1047.10 | ... | 1047.10 | 1000    | 1039.25 | 1039.25 | 3039.25 | 39.25   | ... | 39.25 | 0     |
		  | Provider Paid by SFA                    | 0       | 1047.10 | 1047.10 | ... | 1047.10 | 1047.10 | 1000    | 1039.25 | 1039.25 | 3039.25 | ... | 39.25 | 39.25 |
		  | Payment due from Employer               | 0       | 0       | 0       | ... | 0       | 0       | 0       | 0       | 0       | 0       | ... | 0     | 0     |
		  | Levy account debited                    | 0       | 1000    | 1000    | ... | 1000    | 1000    | 1000    | 1000    | 1000    | 3000    | ... | 0     | 0     |
		  | SFA Levy employer budget                | 1000    | 1000    | 1000    | ... | 1000    | 1000    | 1000    | 1000    | 3000    | 0       | ... | 0     | 0     |
		  | SFA levy co-funding budget              | 0       | 0       | 0       | ... | 0       | 0       | 0       | 0       | 0       | 0       | ... | 0     | 0     |
		  | SFA non-levy co-funding budget          | 0       | 0       | 0       | ... | 0       | 0       | 0       | 0       | 0       | 0       | ... | 0     | 0     |
		  | SFA non-Levy additional payments budget | 0       | 0       | 0       | ... | 0       | 0       | 0       | 0       | 0       | 0       | ... | 0     | 0     |
		  | SFA levy additional payments budget     | 47.10   | 47.10   | 47.10   | ... | 47.10   | 0       | 39.25   | 39.25   | 39.25   | 39.25   | ... | 39.25 | 0     |
		 
	And the transaction types for the payments are:
		  | Payment type                   | 08/17 | 09/17 | 10/17 | ... | 05/18 | 06/18 | 07/18 | 08/18 | 09/18 | ... | 05/19 | 06/19 |
		  | On-program                     | 0     | 1000  | 1000  | ... | 1000  | 1000  | 1000  | 1000  | 0     | ... | 0     | 0     |
		  | Completion                     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 3000  | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths on programme | 0     | 47.10 | 47.10 | ... | 47.10 | 0     | 39.25 | 39.25 | 39.25 | ... | 39.25 | 39.25 |
		  | English and maths Balancing    | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  



#Background information: 
#
#Providers are paid £471 per aim. this is funded from outside the total price and is flat-profiled across the planned number of months in learning for that aim. There is no N+1, there is no money held back for completion. 
#If the learner has english and maths needs above level 2 (apprenticeship requires higher) this is funded within the total price.
#It is permissible for a Level 2 apprentice to fail level 2 English & Maths, retake the learning, but complete the programme because they have already met the policy.

Scenario: DPP-644a Non-DAS Learner taking single Level 1 aim, progressing to and completing single Level 2 aim, completes to time 

Given the apprenticeship funding band maximum is 15000

When an ILR file is submitted with the following data:
		  | ULN       | learner type                 | standard code | agreed price | start date | planned end date | actual end date | completion status | aim type         |
		  | learner a | 19-24 programme only non-DAS | 76            | 15000        | 06/08/2017 | 08/08/2019       |                 | continuing        | programme        |
		  | learner a | 19-24 programme only non-DAS | 76            | 471          | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | maths or english |
		  | learner a | 19-24 programme only non-DAS | 76            | 471          | 09/08/2018 | 08/08/2019       | 08/08/2019      | completed         | maths or english |
		  
	
Then the provider earnings and payments break down as follows:

		  | Type                                    | 08/17  | 09/17  | 10/17  | 11/17  | 12/17  | ... | 08/18  | 09/18  | ... | 07/19  | 08/19  |
		  | Provider Earned Total                   | 539.25 | 539.25 | 539.25 | 539.25 | 539.25 | ... | 539.25 | 539.25 | ... | 539.25 | 0      |
		  | Provider Paid by SFA                    | 0      | 489.25 | 489.25 | 489.25 | 489.25 | ... | 489.25 | 489.25 | ... | 489.25 | 489.25 |
		  | Payment due from Employer               | 0      | 50     | 50     | 50     | 50     | ... | 50     | 50     | ... | 50     | 50     |
		  | Levy account debited                    | 0      | 0      | 0      | 0      | 0      | ... | 0      | 0      | ... | 0      | 0      |
		  | SFA Levy employer budget                | 0      | 0      | 0      | 0      | 0      | ... | 0      | 0      | ... | 0      | 0      |
		  | SFA levy co-funding budget              | 0      | 0      | 0      | 0      | 0      | ... | 0      | 0      | ... | 0      | 0      |
		  | SFA non-Levy co-funding budget          | 450    | 450    | 450    | 450    | 450    | ... | 450    | 450    | ... | 450    | 0      |
		  | SFA non-Levy additional payments budget | 39.25  | 39.25  | 39.25  | 39.25  | 39.25  | ... | 39.25  | 39.25  | ... | 39.25  | 0      |
		  | SFA levy additional payments budget     | 0      | 0      | 0      | 0      | 0      | ... | 0      | 0      | ... | 0      | 0      |
		  
And the transaction types for the payments are:
		  | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 | ... | 07/19 | 08/19 |
		  | On-program                     | 450   | 450   | 450   | 450   | ... | 450   | 450   | ... | 450   | 450   |
		  | Completion                     | 0     | 0     | 0     | 0     | ... | 0     | 0     | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | 0     | ... | 0     | 0     | ... | 0     | 0     |
		  | English and maths on programme | 39.25 | 39.25 | 39.25 | 39.25 | ... | 39.25 | 39.25 | ... | 39.25 | 39.25 |
		  | English and maths Balancing    | 0     | 0     | 0     | 0     | ... | 0     | 0     | ... | 0     | 0     |  	  
	


Scenario: DPP-644b DAS Learner taking single Level 1 aim, progressing to and completing single Level 2 aim, completes to time 

Given levy balance > agreed price for all months
		
And the following commitments exist:
		  | ULN       | start date | end date   | agreed price | status |
		  | learner a | 06/08/2017 | 08/08/2019 | 15000        | active |

When an ILR file is submitted with the following data:
		  | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | aim type         |
		  | learner a | 19-24 programme only DAS | 15000        | 06/08/2017 | 08/08/2019       |                 | continuing        | programme        |
		  | learner a | 19-24 programme only DAS | 471          | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | maths or english |
		  | learner a | 19-24 programme only DAS | 471          | 09/08/2018 | 08/08/2019       | 08/08/2019      | completed         | maths or english |
		  
	
Then the provider earnings and payments break down as follows:
		  | Type                                    | 08/17  | 09/17  | 10/17  | 11/17  | 12/17  | ... | 08/18  | 09/18  | ... | 07/19  | 08/19  |
		  | Provider Earned Total                   | 539.25 | 539.25 | 539.25 | 539.25 | 539.25 | ... | 539.25 | 539.25 | ... | 539.25 | 0      |
		  | Provider Paid by SFA                    | 0      | 539.25 | 539.25 | 539.25 | 539.25 | ... | 539.25 | 539.25 | ... | 539.25 | 539.25 |
		  | Payment due from Employer               | 0      | 0      | 0      | 0      | 0      | ... | 0      | 0      | ... | 0      | 0      |
		  | Levy account debited                    | 0      | 500    | 500    | 500    | 500    | ... | 500    | 500    | ... | 500    | 500    |
		  | SFA Levy employer budget                | 500    | 500    | 500    | 500    | 500    | ... | 500    | 500    | ... | 500    | 0      |
		  | SFA Levy co-funding budget              | 0      | 0      | 0      | 0      | 0      | ... | 0      | 0      | ... | 0      | 0      |
		  | SFA non-levy co-funding budget          | 0      | 0      | 0      | 0      | 0      | ... | 0      | 0      | ... | 0      | 0      |
		  | SFA non-Levy additional payments budget | 0      | 0      | 0      | 0      | 0      | ... | 0      | 0      | ... | 0      | 0      |
		  | SFA levy additional payments budget     | 39.25  | 39.25  | 39.25  | 39.25  | 39.25  | ... | 39.25  | 39.25  | ... | 39.25  | 0      |
		  

    And the transaction types for the payments are:
		  | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 | ... | 07/19 | 08/19 |
		  | On-program                     | 500   | 500   | 500   | 500   | ... | 500   | 500   | ... | 500   | 500   |
		  | Completion                     | 0     | 0     | 0     | 0     | ... | 0     | 0     | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | 0     | ... | 0     | 0     | ... | 0     | 0     |
		  | English and maths on programme | 39.25 | 39.25 | 39.25 | 39.25 | ... | 39.25 | 39.25 | ... | 39.25 | 39.25 |
		  | English and maths Balancing    | 0     | 0     | 0     | 0     | ... | 0     | 0     | ... | 0     | 0     |






#DPP-645 Learner taking single level 3 aim, completes to time, no funding generated     
#Feature: Provider earnings and payments where apprenticeship requires english or maths above level 2 - completes on time.

Scenario: DPP-645 A Payment for a non-DAS learner, funding agreed within band maximum, planned duration is same as programme (assumes both start and finish at same time)

Given the apprenticeship funding band maximum is 15000
When an ILR file is submitted with the following data:
		  | ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | aim type         |
		  | learner a | 19-24 programme only non-DAS | 15000        | 06/08/2017 | 08/08/2018       |                 | continuing        | programme        |
		  | learner a | 19-24 programme only non-DAS | 0            | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | maths or english |
		  
      
Then the provider earnings and payments break down as follows:

		  | Type                                    | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
		  | Provider Earned Total                   | 1000  | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
		  | Provider Paid by SFA                    | 0     | 900   | 900   | 900   | 900   | ... | 900   | 900   |
		  | Payment due from Employer               | 0     | 100   | 100   | 100   | 100   | ... | 100   | 100   |
		  | Levy account debited                    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA Levy employer budget                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA levy co-funding budget              | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA non-levy co-funding budget          | 900   | 900   | 900   | 900   | 900   | ... | 900   | 0     |
		  | SFA non-Levy additional payments budget | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA levy additional payments budget     | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  
And the transaction types for the payments are:

		  | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
		  | On-program                     | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
		  | Completion                     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths on programme | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths balancing    | 0     | 0     | 0     | 0     | ... | 0     | 0     |	  
		  




Scenario: DPP-645 B Payment for a DAS learner, funding agreed within band maximum, planned duration is same as programme (assumes both start and finish at same time)
			  
    Given levy balance > agreed price for all months
		
    And the following commitments exist:
		  | ULN       | start date | end date   | agreed price | status |
		  | learner a | 06/08/2017 | 08/08/2018 | 15000        | active |

    When an ILR file is submitted with the following data:
		  | ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | aim type         |
		  | learner a | 19-24 programme only non-DAS | 15000        | 06/08/2017 | 08/08/2018       |                 | continuing        | programme        |
		  | learner a | 19-24 programme only non-DAS | 0            | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | maths or english |
		  
    Then the provider earnings and payments break down as follows:
		  | Type                                    | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
		  | Provider Earned Total                   | 1000  | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
		  | Provider Paid by SFA                    | 0     | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
		  | Payment due from Employer               | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | Levy account debited                    | 0     | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
		  | SFA Levy employer budget                | 1000  | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
		  | SFA levy co-funding budget              | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA non-levy co-funding budget          | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA non-Levy additional payments budget | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA levy additional payments budget     | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  
    And the transaction types for the payments are:
		  | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
		  | On-program                     | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
		  | Completion                     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths on programme | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths Balancing    | 0     | 0     | 0     | 0     | ... | 0     | 0     |		
		  
          



#DPP-646 Learner taking single level 2 aim, prior funding adjustment, completes to time
#Feature: Provider earnings and payments where apprenticeship requires english or maths at level 2 with funding adjustment - COMPLETES ON TIME

Scenario: DPP-646 A Payment for a non-DAS learner, funding agreed within band maximum, planned duration is same as programme (assumes both start and finish at same time)
	
#	Providers are paid £471 per aim. this is funded from outside the total price and is flat-profiled across the planned number of months in learning for that aim. There is no N+1, there is no money held back for completion. 
#	If the learner has english and maths needs above level 2 (apprenticeship requires higher) this is funded within the total price.
	
#	Tech Guide 102. If an adjustment is required due to prior learning, you must record data in the ‘Funding adjustment for prior learning’ field on the ILR. 

	When an ILR file is submitted with the following data:
		  | ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | funding adjustment for prior learning | other funding adjustment | aim type         |
		  | learner a | 19-24 programme only non-DAS | 15000        | 06/08/2017 | 08/08/2018       |                 | continuing        | n/a                                   | n/a                      | programme        |
		  | learner a | 19-24 programme only non-DAS | 471          | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 75%                                   | n/a                      | maths or english |
		  
#    The English or maths aim is submitted with the same start and planned end date
      
    Then the provider earnings and payments break down as follows:
		  | Type                                    | 08/17   | 09/17   | 10/17   | 11/17   | 12/17   | ... | 07/18   | 08/18  |
		  | Provider Earned Total                   | 1029.44 | 1029.44 | 1029.44 | 1029.44 | 1029.44 | ... | 1029.44 | 0      |
		  | Provider Paid by SFA                    | 0       | 929.44  | 929.44  | 929.44  | 929.44  | ... | 929.44  | 929.44 |
		  | Payment due from Employer               | 0       | 100     | 100     | 100     | 100     | ... | 100     | 100    |
		  | Levy account debited                    | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0      |
		  | SFA levy co-funding budget              | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0      |
		  | SFA Levy employer budget                | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0      |
		  | SFA non-Levy co-funding budget          | 900     | 900     | 900     | 900     | 900     | ... | 900     | 0      |
		  | SFA non-levy additional payments budget | 29.44   | 29.44   | 29.44   | 29.44   | 29.44   | ... | 29.44   | 0      |
		  | SFA levy additional payments budget     | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0      |
		  
    And the transaction types for the payments are:
		  | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
		  | On-program                     | 900   | 900   | 900   | 900   | ... | 900   | 900   |
		  | Completion                     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths on programme | 29.44 | 29.44 | 29.44 | 29.44 | ... | 29.44 | 29.44 |
		  | English and maths Balancing    | 0     | 0     | 0     | 0     | ... | 0     | 0     |		  
		  
	
Scenario: DPP-646 B Payment for a DAS learner, planned duration is same as programme (assumes both start and finish at same time)
	
#	Providers are paid £471 per aim. this is funded from outside the total proce and is flat-profiled across the planned number of 
#   months in learning for that aim. There is no N+1, there is no money held back for completion. 
#	If the learner has english and maths needs above level 2 (apprenticeship requires higher) this is funded within the total price	

#	Tech Guide 102. If an adjustment is required due to prior learning, you must record data in the ‘Funding adjustment for prior learning’ field on the ILR. 
		  
	Given levy balance > agreed price for all months
		
    And the following commitments exist:
		  | ULN       | start date | end date   | agreed price | status |
		  | learner a | 06/08/2017 | 08/08/2018 | 15000        | active |

	When an ILR file is submitted with the following data:
		  | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | funding adjustment for prior learning | other funding adjustment | aim type         |
		  | learner a | 19-24 programme only DAS | 15000        | 06/08/2017 | 08/08/2018       |                 | continuing        | n/a                                   | n/a                      | programme        |
		  | learner a | 19-24 programme only DAS | 471          | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 75%                                   | n/a                      | maths or english |
				  	  
#		  The English or maths aim is submitted with the same start and planned end date
      
    Then the provider earnings and payments break down as follows:
		  | Type                                    | 08/17   | 09/17   | 10/17   | 11/17   | 12/17   | ... | 07/18   | 08/18   |
		  | Provider Earned Total                   | 1029.44 | 1029.44 | 1029.44 | 1029.44 | 1029.44 | ... | 1029.44 | 0       |
		  | Provider Paid by SFA                    | 0       | 1029.44 | 1029.44 | 1029.44 | 1029.44 | ... | 1029.44 | 1029.44 |
		  | Payment due from Employer               | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0       |
		  | Levy account debited                    | 0       | 1000    | 1000    | 1000    | 1000    | ... | 1000    | 1000    |
		  | SFA Levy employer budget                | 1000    | 1000    | 1000    | 1000    | 1000    | ... | 1000    | 0       |
		  | SFA non-Levy co-funding budget          | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0       |
		  | SFA non-levy additional payments budget | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0       |
		  | SFA levy additional payments budget     | 29.44   | 29.44   | 29.44   | 29.44   | 29.44   | ... | 29.44   | 0       |
		  
    And the transaction types for the payments are:
		  | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
		  | On-program                     | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
		  | Completion                     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths on programme | 29.44 | 29.44 | 29.44 | 29.44 | ... | 29.44 | 29.44 |
		  | English and maths Balancing    | 0     | 0     | 0     | 0     | ... | 0     | 0     |	



#DPP-655 Learner taking single level 2 aim, 0 prior funding adjustment, completes to time, no funding generated
#Feature: Provider earnings and payments where apprenticeship requires english or maths at level 2 with prior funding adjustment of ZERO - COMPLETES ON TIME

Scenario: DPP-655 A Payment for a non-DAS learner, planned duration is same as programme (assumes both start and finish at same time)
	
#	Providers are paid £471 per aim. this is funded from outside the total proce and is flat-profiled across the planned number of months in learning for that aim. There is no N+1, there is no money held back for completion. 
#	If the learner has english and maths needs above level 2 (apprenticeship requires higher) this is funded within the total price.
	
#	Tech Guide 102. If an adjustment is required due to prior learning, you must record data in the ‘Funding adjustment for prior learning’ field on the ILR. 

	When an ILR file is submitted with the following data:
		  | ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | funding adjustment for prior learning | other funding adjustment | aim type         |
		  | learner a | 19-24 programme only non-DAS | 15000        | 06/08/2017 | 08/08/2018       |                 | continuing        | n/a                                   | n/a                      | programme        |
		  | learner a | 19-24 programme only non-DAS | 471          | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 0%                                    | n/a                      | maths or english |
		  
#    The English or maths aim is submitted with the same start and planned end date
      
    Then the provider earnings and payments break down as follows:
		  | Type                                    | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
		  | Provider Earned Total                   | 1000  | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
		  | Provider Paid by SFA                    | 0     | 900   | 900   | 900   | 900   | ... | 900   | 900   |
		  | Payment due from Employer               | 0     | 100   | 100   | 100   | 100   | ... | 100   | 100   |
		  | Levy account debited                    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA Levy employer budget                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA levy co-funding budget              | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA non-levy co-funding budget          | 900   | 900   | 900   | 900   | 900   | ... | 900   | 0     |
		  | SFA non-Levy additional payments budget | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA levy additional payments budget     | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  
    And the transaction types for the payments are:
		  | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
		  | On-program                     | 900   | 900   | 900   | 900   | ... | 900   | 900   |
		  | Completion                     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths on programme | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths Balancing    | 0     | 0     | 0     | 0     | ... | 0     | 0     |			  
		  

Scenario: DPP-655 B Payment for a DAS learner, funding agreed within band maximum, planned duration is same as programme (assumes both start and finish at same time)
	
#	Providers are paid £471 per aim. this is funded from outside the total proce and is flat-profiled across the planned number of months in learning for that aim. There is no N+1, there is no money held back for completion. 
#	If the learner has english and maths needs above level 2 (apprenticeship requires higher) this is funded within the total price	

#	Tech Guide 102. If an adjustment is required due to prior learning, you must record data in the ‘Funding adjustment for prior learning’ field on the ILR. 
		  
	Given levy balance > agreed price for all months
		
    And the following commitments exist:
		  | ULN       | start date | end date   | agreed price | status |
		  | learner a | 06/08/2017 | 08/08/2018 | 15000        | active |

When an ILR file is submitted with the following data:
		  | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | funding adjustment for prior learning | other funding adjustment | aim type         |
		  | learner a | 19-24 programme only DAS | 15000        | 06/08/2017 | 08/08/2018       |                 | continuing        | n/a                                   | n/a                      | programme        |
		  | learner a | 19-24 programme only DAS | 471          | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 0%                                    | n/a                      | maths or english |  
				  	  
		  
#	The English or maths aim is submitted with the same start and planned end date
      
    Then the provider earnings and payments break down as follows:
		  | Type                                         | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
		  | Provider Earned Total                        | 1000  | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
		  | Provider Paid by SFA                         | 0     | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
		  | Payment due from Employer                    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | Levy account debited                         | 0     | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
		  | SFA Levy employer budget                     | 1000  | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
		  | SFA levy co-funding budget                   | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA non-levy co-funding budget               | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA non-Levy additional payments budget      | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA levy additional payments budget          | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  
    And the transaction types for the payments are:
		  | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
		  | On-program                     | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
		  | Completion                     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths on programme | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths Balancing    | 0     | 0     | 0     | 0     | ... | 0     | 0     |


		
		           
#Feature: Provider earnings and payments where apprenticeship requires english or maths at level 2 with prior funding adjustment after break - COMPLETES ON TIME, RETURNS TO SAME PROVIDER

Scenario: DPP-656 A Payment for a non-DAS learner, funding agreed within band maximum, planned duration is same as programme (assumes both start and finish at same time), other funding adjutsment is 75%, prior funding adjustment is 10%
	
#	Providers are paid £471 per aim. this is funded from outside the total proce and is flat-profiled across the planned number of months in learning for that aim. There is no N+1, there is no money held back for completion. 

#	Tech Guide 102. If an adjustment is required due to prior learning, you must record data in the ‘Funding adjustment for prior learning’ field on the ILR. 

	When an ILR file is submitted with the following data:
		  | ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | funding adjustment for prior learning | other funding adjustment | restart indicator | aim type         |
		  | learner a | 19-24 programme only non-DAS | 15000        | 06/08/2017 | 08/08/2018       | 08/01/2018      | planned break     | n/a                                   | n/a                      | NO                | programme        |
		  | learner a | 19-24 programme only non-DAS | 471          | 06/08/2017 | 08/08/2018       | 08/01/2018      | planned break     | n/a                                   | n/a                      | NO                | maths or english |
		  | learner a | 19-24 programme only non-DAS | 15000        | 06/08/2018 | 08/03/2019       |                 | continuing        | n/a                                   | n/a                      | YES               | programme        |
		  | learner a | 19-24 programme only non-DAS | 471          | 06/08/2018 | 08/03/2019       | 08/03/2019      | completed         | 42%                                   | n/a                      | YES               | maths or english |
		  
#	The English or maths aim is submitted with the same start and planned end date
      
    Then the provider earnings and payments break down as follows:
		  | Type                                    | 08/17   | 09/17   | 10/17   | 11/17   | 12/17   | 01/18  | ... | 08/18   | 09/18   | 10/18   | ... | 02/19   | 03/19  |
		  | Provider Earned Total                   | 1039.25 | 1039.25 | 1039.25 | 1039.25 | 1039.25 | 0      | ... | 1028.26 | 1028.26 | 1028.26 | ... | 1028.26 | 0      |
		  | Provider Paid by SFA                    | 0       | 939.25  | 939.25  | 939.25  | 939.25  | 939.25 | ... | 0       | 928.26  | 928.26  | ... | 928.26  | 928.26 |
		  | Payment due from Employer               | 0       | 100     | 100     | 100     | 100     | 100    | ... | 0       | 100     | 100     | ... | 100     | 100    |
		  | Levy account debited                    | 0       | 0       | 0       | 0       | 0       | 0      | ... | 0       | 0       | 0       | ... | 0       | 0      |
		  | SFA Levy employer budget                | 0       | 0       | 0       | 0       | 0       | 0      | ... | 0       | 0       | 0       | ... | 0       | 0      |
		  | SFA Levy co-funding budget              | 0       | 0       | 0       | 0       | 0       | 0      | ... | 0       | 0       | 0       | ... | 0       | 0      |
		  | SFA non-Levy co-funding budget          | 900     | 900     | 900     | 900     | 900     | 0      | ... | 900     | 900     | 900     | ... | 900     | 0      |
		  | SFA non-levy additional payments budget | 39.25   | 39.25   | 39.25   | 39.25   | 39.25   | 0      | ... | 28.26   | 28.26   | 28.26   | ... | 28.26   | 0      |
		  | SFA levy additional payments budget     | 0       | 0       | 0       | 0       | 0       | 0      | ... | 0       | 0       | 0       | ... | 0       | 0      |
		  
    And the transaction types for the payments are:
		  | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 | ... | 09/18 | 10/18 | 11/18 | ... | 02/19 | 03/19 |
		  | On-program                     | 900   | 900   | 900   | 900   | 900   | ... | 900   | 900   | 900   | ... | 900   | 900   |
		  | Completion                     | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths on programme | 39.25 | 39.25 | 39.25 | 39.25 | 39.25 | ... | 28.26 | 28.26 | 28.26 | ... | 28.26 | 28.26 |
		  | English and maths Balancing    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | ... | 0     | 0     |	




#DPP-656 Learner taking single level 2 aim, takes break in learning and returns with prior funding adjustment, completes to time
		  
Scenario: DPP-656 B Payment for a DAS learner, funding agreed within band maximum, planned duration is same as programme (assumes both start and finish at same time), other funding adjutsment is 75%, prior funding adjustment is 10%
	
#	Providers are paid £471 per aim. this is funded from outside the total proce and is flat-profiled across the planned number of months in learning for that aim. There is no N+1, there is no money held back for completion. 

#	Tech Guide 102. If an adjustment is required due to prior learning, you must record data in the ‘Funding adjustment for prior learning’ field on the ILR. 
		  
	Given levy balance > agreed price for all months
		
    And the following commitments exist:
		  | ULN       | start date | end date   | agreed price | status |
		  | learner a | 06/08/2017 | 08/08/2018 | 15000        | active |

	When an ILR file is submitted with the following data:
		  | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | funding adjustment for prior learning | other funding adjustment | restart indicator | aim type         |
		  | learner a | 19-24 programme only DAS | 15000        | 06/08/2017 | 08/08/2018       | 08/01/2018      | planned break     | n/a                                   | n/a                      | NO                | programme        |
		  | learner a | 19-24 programme only DAS | 471          | 06/08/2017 | 08/08/2018       | 08/01/2018      | planned break     | n/a                                   | n/a                      | NO                | maths or english |
		  | learner a | 19-24 programme only DAS | 15000        | 06/08/2018 | 08/03/2019       |                 | continuing        | n/a                                   | n/a                      | YES               | programme        |
		  | learner a | 19-24 programme only DAS | 471          | 06/08/2018 | 08/03/2019       | 08/03/2019      | completed         | 42%                                   | n/a                      | YES               | maths or english |
		  
#	The English or maths aim is submitted with the same start and planned end date
      
    Then the provider earnings and payments break down as follows:
		  | Type                                    | 08/17   | 09/17   | ... | 12/17   | 01/18   | ...   | 08/18   | 09/18   | 10/18   | ... | 02/19   | 03/19   |
		  | Provider Earned Total                   | 1039.25 | 1039.25 | ... | 1039.25 | 0       | ...   | 1028.26 | 1028.26 | 1028.26 | ... | 1028.26 | 0       |
		  | Provider Paid by SFA                    | 0       | 1039.25 | ... | 1039.25 | 1039.25 | ...   | 0       | 1028.26 | 1028.26 | ... | 1028.26 | 1028.26 |
		  | Payment due from Employer               | 0       | 0       | ... | 0       | 0       | ...   | 0       | 0       | 0       | ... | 0       | 0       |
		  | Levy account debited                    | 0       | 1000    | ... | 1000    | 1000    | ...   | 0       | 1000    | 1000    | ... | 1000    | 1000    |
		  | SFA Levy employer budget                | 1000    | 1000    | ... | 1000    | 0       | ...   | 1000    | 1000    | 1000    | ... | 1000    | 0       |
		  | SFA Levy co-funding budget              | 0       | 0       | ... | 0       | 0       | ...   | 0       | 0       | 0       | ... | 0       | 0       |
		  | SFA non-Levy co-funding budget          | 0       | 0       | ... | 0       | 0       | ...   | 0       | 0       | 0       | ... | 0       | 0       |
		  | SFA non-levy additional payments budget | 0       | 0       | ... | 0       | 0       | ...   | 0       | 0       | 0       | ... | 0       | 0       |
		  | SFA levy additional payments budget     | 39.25   | 39.25   | ... | 39.25   | 0       | ...   | 28.26   | 28.26   | 28.26   | ... | 28.26   | 0       |
		  
    And the transaction types for the payments are:
		  | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 | ... | 09/18 | 10/18 | 11/18 | ... | 02/19 | 03/19 |
		  | On-program                     | 1000  | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
		  | Completion                     | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths on programme | 39.25 | 39.25 | 39.25 | 39.25 | 39.25 | ... | 28.26 | 28.26 | 28.26 | ... | 28.26 | 28.26 |
		  | English and maths Balancing    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | ... | 0     | 0     |	



#DPP-678 Learner taking single level 2 aim, changes provider with prior funding adjustment, completes to time

#Feature: Provider earnings and payments where apprenticeship requires english or maths at level 2 with prior funding adjustment after changing provider - COMPLETES ON TIME

Scenario: DPP-678 A Payment for a non-DAS learner, funding agreed within band maximum, planned duration is same as programme (assumes both start and finish at same time), other funding adjutsment is 75%, prior funding adjustment is 10%
	
#	Providers are paid £471 per aim. this is funded from outside the total proce and is flat-profiled across the planned number of months in learning for that aim. There is no N+1, there is no money held back for completion. 

#	Tech Guide 102. If an adjustment is required due to prior learning, you must record data in the ‘Funding adjustment for prior learning’ field on the ILR. 

	When an ILR file is submitted with the following data:
	| Provider | ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | funding adjustment for prior learning | other funding adjustment | restart indicator | aim type         |
	| A        | learner a | 19-24 programme only non-DAS | 15000        | 06/08/2017 | 08/08/2018       | 08/01/2018      | planned break     | n/a                                   | n/a                      | NO                | programme        |
	| A        | learner a | 19-24 programme only non-DAS | 471          | 06/08/2017 | 08/08/2018       | 08/01/2018      | planned break     | n/a                                   | n/a                      | NO                | maths or english |
	| B        | learner a | 19-24 programme only non-DAS | 15000        | 09/01/2018 | 08/08/2018       |                 | continuing        | n/a                                   | n/a                      | YES               | programme        |
	| B        | learner a | 19-24 programme only non-DAS | 471          | 09/01/2018 | 08/08/2018       | 08/08/2018      | completed         | 70%                                   | n/a                      | YES               | maths or english |
		  
#	The English or maths aim is submitted with the same start and planned end date
    Then the earnings and payments break down for provider A is as follows:
    	  | Type                                    | 08/17   | 09/17   | 10/17   | 11/17   | 12/17   | 01/18  |
		  | Provider Earned Total                   | 1039.25 | 1039.25 | 1039.25 | 1039.25 | 1039.25 | 0      |
		  | Provider Paid by SFA                    | 0       | 939.25  | 939.25  | 939.25  | 939.25  | 939.25 |
		  | Payment due from Employer               | 0       | 100     | 100     | 100     | 100     | 100    |
		  | Levy account debited                    | 0       | 0       | 0       | 0       | 0       | 0      |
		  | SFA Levy employer budget                | 0       | 0       | 0       | 0       | 0       | 0      |
		  | SFA Levy co-funding budget              | 0       | 0       | 0       | 0       | 0       | 0      |
		  | SFA non-Levy co-funding budget          | 900     | 900     | 900     | 900     | 900     | 0      |
		  | SFA non-levy additional payments budget | 39.25   | 39.25   | 39.25   | 39.25   | 39.25   | 0      |
		  | SFA levy additional payments budget     | 0       | 0       | 0       | 0       | 0       | 0      |
		  
    And the transaction types for the payments for provider A are:
		  | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
		  | On-program                     | 900   | 900   | 900   | 900   | 900   |
		  | Completion                     | 0     | 0     | 0     | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | 0     | 0     |
		  | English and maths on programme | 39.25 | 39.25 | 39.25 | 39.25 | 39.25 |
		  | English and maths Balancing    | 0     | 0     | 0     | 0     | 0     |
		  
    Then the earnings and payments break down for provider B is as follows:
		  | Type                                    | 01/18   | 02/18   | 03/18   | ... | 07/19   | 08/19  |
		  | Provider Earned Total                   | 1047.10 | 1047.10 | 1047.10 | ... | 1047.10 | 0      |
		  | Provider Paid by SFA                    | 0       | 947.10  | 947.10  | ... | 947.10  | 947.10 |
		  | Payment due from Employer               | 0       | 100     | 100     | ... | 100     | 100    |
		  | Levy account debited                    | 0       | 0       | 0       | ... | 0       | 0      |
		  | SFA Levy employer budget                | 0       | 0       | 0       | ... | 0       | 0      |
		  | SFA Levy co-funding budget              | 0       | 0       | 0       | ... | 0       | 0      |
		  | SFA Levy co-funding budget              | 900     | 900     | 900     | ... | 900     | 0      |
		  | SFA non-Levy additional payments budget | 47.10   | 47.10   | 47.10   | ... | 47.10   | 0      |
		  | SFA levy additional payments budget     | 0       | 0       | 0       | ... | 0       | 0      |
		  
    And the transaction types for the payments for provider B are:
		  | Payment type                   | 01/18 | 02/18 | 03/18 | ... | 07/19 | 08/19 |
		  | On-program                     | 900   | 900   | 900   | ... | 900   | 900   |
		  | Completion                     | 0     | 0     | 0     | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths on programme | 47.10 | 47.10 | 47.10 | ... | 47.10 | 47.10 |
		  | English and maths Balancing    | 0     | 0     | 0     | ... | 0     | 0     |		  
		  
	
	
Scenario: DPP-678 B Payment for a DAS learner, funding agreed within band maximum, planned duration is same as programme (assumes both start and finish at same time), other funding adjutsment is 75%, prior funding adjustment is 10%
	
#	Providers are paid £471 per aim. this is funded from outside the total proce and is flat-profiled across the planned number of months in learning for that aim. There is no N+1, there is no money held back for completion. 

#	Tech Guide 102. If an adjustment is required due to prior learning, you must record data in the ‘Funding adjustment for prior learning’ field on the ILR. 
	
	Given levy balance > agreed price for all months
		
    And the following commitments exist:
	| Provider | ULN       | start date | end date   | agreed price | status    |
	| A        | learner a | 06/08/2017 | 08/08/2018 | 15000        | Cancelled |
	| B        | learner a | 09/01/2018 | 08/08/2018 | 15000        | active    |

	When an ILR file is submitted with the following data:
	| Provider | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | funding adjustment for prior learning | other funding adjustment | restart indicator | aim type         |
	| P1       | learner a | 19-24 programme only DAS | 15000        | 06/08/2017 | 08/08/2018       | 08/01/2018      | planned break     | n/a                                   | n/a                      | NO                | programme        |
	| P1       | learner a | 19-24 programme only DAS | 471          | 06/08/2017 | 08/08/2018       | 08/01/2018      | planned break     | n/a                                   | n/a                      | NO                | maths or english |
	| P2       | learner a | 19-24 programme only DAS | 15000        | 09/01/2018 | 08/08/2018       |                 | continuing        | n/a                                   | n/a                      | YES               | programme        |
	| P2       | learner a | 19-24 programme only DAS | 471          | 09/01/2018 | 08/08/2018       | 08/08/2018      | completed         | 70%                                   | n/a                      | YES               | maths or english |
	  
#	The English or maths aim is submitted with the same start and planned end date
      
    Then the earnings and payments break down for provider A is as follows:
		  | Type                                    | 08/17   | 09/17   | 10/17   | 11/17   | 12/17   | 01/18   |
		  | Provider Earned Total                   | 1039.25 | 1039.25 | 1039.25 | 1039.25 | 1039.25 | 0       |
		  | Provider Paid by SFA                    | 0       | 1039.25 | 1039.25 | 1039.25 | 1039.25 | 1039.25 |
		  | Payment due from Employer               | 0       | 0       | 0       | 0       | 0       | 0       |
		  | Levy account debited                    | 0       | 1000    | 1000    | 1000    | 1000    | 1000    |
		  | SFA Levy employer budget                | 1000    | 1000    | 1000    | 1000    | 1000    | 0       |
		  | SFA Levy co-funding budget              | 0       | 0       | 0       | 0       | 0       | 0       |
		  | SFA non-Levy co-funding budget          | 0       | 0       | 0       | 0       | 0       | 0       |
		  | SFA non-levy additional payments budget | 0       | 0       | 0       | 0       | 0       | 0       |
		  | SFA levy additional payments budget     | 39.25   | 39.25   | 39.25   | 39.25   | 39.25   | 0       |
		  
    And the transaction types for the payments for provider A are:
		  | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
		  | On-program                     | 1000  | 1000  | 1000  | 1000  | 1000  |
		  | Completion                     | 0     | 0     | 0     | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | 0     | 0     |
          | English and maths on programme | 39.25 | 39.25 | 39.25 | 39.25 | 39.25 |
		  | English and maths Balancing    | 0     | 0     | 0     | 0     | 0     |


    Then the earnings and payments break down for provider B is as follows:
		  | Type                                    | 01/18   | 02/18   | 03/18   | ... | 07/18   | 08/18   |
		  | Provider Earned Total                   | 1047.10 | 1047.10 | 1047.10 | ... | 1047.10 | 0       |
		  | Provider Paid by SFA                    | 0       | 1047.10 | 1047.10 | ... | 1047.10 | 1047.10 |
		  | Payment due from Employer               | 0       | 0       | 0       | ... | 0       | 0       |
		  | Levy account debited                    | 0       | 1000    | 1000    | ... | 1000    | 1000    |
		  | SFA Levy employer budget                | 1000    | 1000    | 1000    | ... | 1000    | 0       |
		  | SFA Levy co-funding budget              | 0       | 0       | 0       | ... | 0       | 0       |
		  | SFA Levy co-funding budget              | 0       | 0       | 0       | ... | 0       | 0       |
		  | SFA non-Levy additional payments budget | 0       | 0       | 0       | ... | 0       | 0       |
		  | SFA levy additional payments budget     | 47.10   | 47.10   | 47.10   | ... | 47.10   | 0       |
		  
    And the transaction types for the payments for provider B are:
		  | Payment type                   | 01/18 | 02/18 | 03/18 | ... | 07/18 | 08/18 |
		  | On-program                     | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
		  | Completion                     | 0     | 0     | 0     | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | ... | 0     | 0     |
          | English and maths on programme | 47.10 | 47.10 | 47.10 | ... | 47.10 | 47.10 |
		  | English and maths Balancing    | 0     | 0     | 0     | ... | 0     | 0     |	




#DPP-679 Learner taking single level 2 aim, other funding adjustment, completes to time
#Feature: Provider earnings and payments where apprenticeship requires english or maths at level 2 with other funding adjustment - COMPLETES ON TIME

Scenario: DPP-679 A Payment for a non-DAS learner, funding agreed within band maximum, planned duration is same as programme (assumes both start and finish at same time), other funding adjutsment is 75%
	
#	Providers are paid £471 per aim. this is funded from outside the total proce and is flat-profiled across the planned number of months in learning for that aim. There is no N+1, there is no money held back for completion. 

	When an ILR file is submitted with the following data:
		  | ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | funding adjustment for prior learning | other funding adjustment | aim type         |
		  | learner a | 19-24 programme only non-DAS | 15000        | 06/08/2017 | 08/08/2018       |                 | continuing        | n/a                                   | n/a                      | programme        |
		  | learner a | 19-24 programme only non-DAS | 471          | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | n/a                                   | 75%                      | maths or english |
		  
#	The English or maths aim is submitted with the same start and planned end date
      
    Then the provider earnings and payments break down as follows:
		  | Type                                    | 08/17   | 09/17   | 10/17   | 11/17   | 12/17   | ... | 07/18   | 08/18  |
		  | Provider Earned Total                   | 1029.44 | 1029.44 | 1029.44 | 1029.44 | 1029.44 | ... | 1029.44 | 0      |
		  | Provider Paid by SFA                    | 0       | 929.44  | 929.44  | 929.44  | 929.44  | ... | 929.44  | 929.44 |
		  | Payment due from Employer               | 0       | 100     | 100     | 100     | 100     | ... | 100     | 100    |
		  | Levy account debited                    | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0      |
		  | SFA levy co-funding budget              | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0      |
		  | SFA Levy employer budget                | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0      |
		  | SFA non-Levy co-funding budget          | 900     | 900     | 900     | 900     | 900     | ... | 900     | 0      |
		  | SFA non-levy additional payments budget | 29.44   | 29.44   | 29.44   | 29.44   | 29.44   | ... | 29.44   | 0      |
		  | SFA levy additional payments budget     | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0      |  
		  
    And the transaction types for the payments are:
		  | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
		  | On-program                     | 900   | 900   | 900   | 900   | ... | 900   | 900   |
		  | Completion                     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | English and maths on programme | 29.44 | 29.44 | 29.44 | 29.44 | ... | 29.44 | 29.44 |
		  | English and maths Balancing    | 0     | 0     | 0     | 0     | ... | 0     | 0     |			  
		  
	
	
Scenario: DPP-679 B Payment for a DAS learner, funding agreed within band maximum, planned duration is same as programme (assumes both start and finish at same time), other funding adjutsment is 75%
	
#	Providers are paid £471 per aim. this is funded from outside the total proce and is flat-profiled across the planned number of months in learning for that aim. There is no N+1, there is no money held back for completion. 
		  
	Given levy balance > agreed price for all months
		
    And the following commitments exist:
		  | ULN       | start date | end date   | agreed price | status |
		  | learner a | 06/08/2017 | 08/08/2018 | 15000        | active |

	When an ILR file is submitted with the following data:
		  | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | funding adjustment for prior learning | other funding adjustment | aim type         |
		  | learner a | 19-24 programme only DAS | 15000        | 06/08/2017 | 08/08/2018       |                 | continuing        | n/a                                   | n/a                      | programme        |
		  | learner a | 19-24 programme only DAS | 471          | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | n/a                                   | 75%                      | maths or english |
		  	  
		  
#	The English or maths aim is submitted with the same start and planned end date
      
    Then the provider earnings and payments break down as follows:
		  | Type                                    | 08/17   | 09/17   | 10/17   | 11/17   | 12/17   | ... | 07/18   | 08/18   |
		  | Provider Earned Total                   | 1029.44 | 1029.44 | 1029.44 | 1029.44 | 1029.44 | ... | 1029.44 | 0       |
		  | Provider Paid by SFA                    | 0       | 1029.44 | 1029.44 | 1029.44 | 1029.44 | ... | 1029.44 | 1029.44 |
		  | Payment due from Employer               | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0       |
		  | Levy account debited                    | 0       | 1000    | 1000    | 1000    | 1000    | ... | 1000    | 1000    |
		  | SFA Levy employer budget                | 1000    | 1000    | 1000    | 1000    | 1000    | ... | 1000    | 0       |
		  | SFA Levy employer budget                | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0       |
		  | SFA non-Levy co-funding budget          | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0       |
		  | SFA non-levy additional payments budget | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0       |
		  | SFA levy additional payments budget     | 29.44   | 29.44   | 29.44   | 29.44   | 29.44   | ... | 29.44   | 0       |
		  
    And the transaction types for the payments are:
		  | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 |
		  | On-program                     | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 1000  |
		  | Completion                     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | Balancing                      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
          | English and maths on programme | 29.44 | 29.44 | 29.44 | 29.44 | ... | 29.44 | 29.44 |
		  | English and maths Balancing    | 0     | 0     | 0     | 0     | ... | 0     | 0     |		
		  