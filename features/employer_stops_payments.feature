@EmployerStopsPayments
Feature: Employer stops payments on a commitment

    Scenario: Commitment payments are stopped midway through the learning episode
        Given levy balance > agreed price for all months
        And the following commitments exist:
            | commitment Id | version Id | ULN       | start date | end date   | status | agreed price | effective from | effective to |
            | 1             | 1          | learner a | 01/09/2017 | 08/09/2018 | active | 15000        | 01/09/2017     | 31/10/2017   |
            | 1             | 2          | learner a | 01/09/2017 | 08/09/2018 | paused | 15000        | 01/11/2017     |              |
        When an ILR file is submitted every month with the following data:
            | ULN       | agreed price | learner type       | start date | planned end date | completion status |
            | learner a | 15000        | programme only DAS | 01/09/2017 | 08/09/2018       | continuing        |
        Then the provider earnings and payments break down as follows:
            | Type                          | 09/17 | 10/17 | 11/17 | 12/17 | ... | 03/18 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | 1000  | ... | 1000  |
            | Provider Earned from SFA      | 1000  | 1000  | 0     | 0     | ... | 0     |
            | Provider Earned from Employer | 0     | 0     | 0     | 0     | ... | 0     |
            | Provider Paid by SFA          | 0     | 1000  | 1000  | 0     | ... | 0     |
            | Payment due from Employer     | 0     | 0     | 0     | 0     | ... | 0     |
            | Levy account debited          | 0     | 1000  | 1000  | 0     | ... | 0     |
            | SFA Levy employer budget      | 1000  | 1000  | 0     | 0     | ... | 0     |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | ... | 0     |
            | SFA non-Levy co-funding budget| 0     | 0     | 0     | 0     | ... | 0     |

            
    Scenario: The provider submits the first ILR file after the commitment payments have been stopped
        Given levy balance > agreed price for all months
        And the following commitments exist:
            | commitment Id | version Id | ULN       | start date | end date   | status | agreed price | effective from | effective to |
            | 1             | 1          | learner a | 01/09/2017 | 08/09/2018 | active | 15000        | 01/09/2017     | 31/08/2017   |
            | 1             | 2          | learner a | 01/09/2017 | 08/09/2018 | paused | 15000        | 01/09/2017     |              |
        When an ILR file is submitted for the first time on 28/12/17 with the following data:
            | ULN       | agreed price | learner type       | start date | planned end date | completion status |
            | learner a | 15000        | programme only DAS | 01/09/2017 | 08/09/2018       | continuing        |
        Then the provider earnings and payments break down as follows:
            | Type                          | 09/17 | 10/17 | 11/17 | 12/17 | ... | 03/18 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | 1000  | ... | 1000  |
            | Provider Earned from SFA      | 0     | 0     | 0     | 0     | ... | 0     |
            | Provider Earned from Employer | 0     | 0     | 0     | 0     | ... | 0     |
            | Provider Paid by SFA          | 0     | 0     | 0     | 0     | ... | 0     |
            | Payment due from Employer     | 0     | 0     | 0     | 0     | ... | 0     |
            | Levy account debited          | 0     | 0     | 0     | 0     | ... | 0     |
            | SFA Levy employer budget      | 0     | 0     | 0     | 0     | ... | 0     |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | ... | 0     |
            | SFA non-Levy co-funding budget| 0     | 0     | 0     | 0     | ... | 0     |


Scenario:700_AC01 DAS learner, payments are stopped as the employer has never paid levy
 
        Given the employer is not a levy payer
		And the following commitments exist:
            | commitment Id | ULN       | priority | start date | end date   | agreed price | 
            | 1             | learner a | 1        | 01/08/2017 | 01/08/2018 | 15000        | 
		When an ILR file is submitted with the following data:
			| learner type       | agreed price | start date | planned end date | actual end date | completion status |
			| programme only DAS | 15000        | 05/08/2017 | 20/08/2018       |                 | continuing        |
		Then the data lock status will be as follows:
			| Payment type                   | 08/17           | 09/17           | 10/17           | 11/17           | 12/17           |
			| On-program                     |                 |                 |                 |                 |                 |
			| Completion                     |                 |                 |                 |                 |                 |
			| Employer 16-18 incentive       |                 |                 |                 |                 |                 |
			| Provider 16-18 incentive       |                 |                 |                 |                 |                 |
			| Provider learning support      |                 |                 |                 |                 |                 |
			| English and maths on programme |                 |                 |                 |                 |                 |
			| English and maths Balancing    |                 |                 |                 |                 |                 |     

		And the provider earnings and payments break down as follows:
            | Type                       | 08/17 | 09/17 | 10/17 | 
            | Provider Earned Total      | 1000  | 1000  | 1000  | 
            | Provider Earned from SFA   | 0     | 0     | 0     | 
            | Provider Paid by SFA       | 0     | 0     | 0     | 
            | Levy account debited       | 0     | 0     | 0     | 
            | SFA Levy employer budget   | 0     | 0     | 0     | 
            | SFA Levy co-funding budget | 0     | 0     | 0     | 
            



					

Scenario:884_AC01 DAS learner, no incentive payments made when commitment is withdrawan before threshold date
# Start date is August 15
# Threshold date is November 13
 
        Given levy balance > agreed price for all months
		And the following commitments exist:
            | commitment Id | version Id | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | 1             | 1-001      | learner a | 15/08/2017 | 01/08/2018 | 15000        | active    | 15/08/2017     | 01/11/2017   |
            | 1             | 1-002      | learner a | 01/11/2017 | 01/08/2018 | 15000        | cancelled | 01/11/2017     | 01/08/2018   |
       
		 When an ILR file is submitted with the following data:
            | learner type             | agreed price | start date | planned end date | actual end date | completion status |
            | 16-18 programme only DAS | 15000        | 15/08/2017 | 20/08/2018       |                 | continuing        |
      
		Then the provider earnings and payments break down as follows:
            | Type                          | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | 2000  | 1000  |
            | Provider Earned from SFA      | 1000  | 1000  | 1000  | 2000  | 1000  |
            | Provider Earned from Employer | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA          | 0     | 1000  | 1000  | 1000  | 0     |
            | Payment due from Employer     | 0     | 0     | 0     | 0     | 0     |
            | Levy account debited          | 0     | 1000  | 1000  | 1000  | 0     |
            | SFA Levy employer budget      | 1000  | 1000  | 1000  | 0     | 0     |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | 0     |
     
		And the transaction types for the payments for provider a are:
            | Payment type             | 09/17 | 10/17 | 11/17 | 12/17 |
            | On-program               | 1000  | 1000  | 1000  | 0     |
            | Employer 16-18 incentive | 0     | 0     | 0     | 0     |
            | Provider 16-18 incentive | 0     | 0     | 0     | 0     |
			
			

Scenario:884_AC02 DAS learner, commitment cancelled after threshold date but before census date, paid for incentive but not for on-prog 
 
        Given levy balance > agreed price for all months
		And the following commitments exist:
            | commitment Id | version Id | ULN       | start date | end date   | agreed price | status    | effective from | effective to |
            | 1             | 1-001      | learner a | 15/08/2017 | 01/08/2018 | 15000        | active    | 15/08/2017     | 20/11/2017   |
            | 1             | 1-002      | learner a | 15/08/2017 | 01/08/2018 | 15000        | cancelled | 21/11/2017     | 01/08/2018   |
            
		When an ILR file is submitted with the following data:
            | learner type             | agreed price | start date | planned end date | actual end date | completion status |
            | 16-18 programme only DAS | 15000        | 15/08/2017 | 20/08/2018       |                 | continuing        |
      
		Then the provider earnings and payments break down as follows:
            | Type                          | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 |
            | Provider Earned Total         | 1000  | 1000  | 1000  | 2000  | 1000  |
            | Provider Earned from SFA      | 1000  | 1000  | 1000  | 2000  | 1000  |
            | Provider Earned from Employer | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA          | 0     | 1000  | 1000  | 1000  | 1000  |
            | Payment due from Employer     | 0     | 0     | 0     | 0     | 0     |
            | Levy account debited          | 0     | 1000  | 1000  | 1000  | 0     |
            | SFA Levy employer budget      | 1000  | 1000  | 1000  | 0     | 0     |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | 0     |

		And the transaction types for the payments for provider a are:
            | Payment type             | 09/17 | 10/17 | 11/17 | 12/17 |
            | On-program               | 1000  | 1000  | 1000  | 0     |
            | Employer 16-18 incentive | 0     | 0     | 0     | 500   |
            | Provider 16-18 incentive | 0     | 0     | 0     | 500   |


Scenario:884_AC03 DAS learner, commitment cancelled from begining and a new commitment is created from start with a lower price, it should reconcile price difference and pay correct incentives
# Start date: 15 Aug
# Threshold date: 13 Nov

        Given levy balance > agreed price for all months
		And the following commitments exist:
            | commitment Id | version Id | ULN       | start date | end date   | standard code | agreed price | status    | effective from | effective to |
            | 1             | 1-001      | learner a | 15/08/2017 | 01/08/2018 | 50            | 15000        | cancelled | 15/08/2017     |              |
            | 2             | 2-001      | learner a | 15/08/2017 | 01/08/2018 | 50            | 1            | active    | 15/08/2017     |              |
       
	    And following learning has been recorded for previous payments:
			| ULN       | start date | aim sequence number | standard code | completion status | agreed price |
			| learner a | 15/08/2017 | 1                   | 50            | continuing        | 15000        |
  
		And the following earnings and payments have been made to the provider A for learner a:
			| Type                                | 08/17 | 09/17 | 10/17 |
			| Provider Earned Total               | 1000  | 1000  | 0     |
			| Provider Earned from SFA            | 1000  | 1000  | 00    |
			| Provider Earned from Employer       | 0     | 0     | 0     |
			| Provider Paid by SFA                | 0     | 1000  | 1000  |
			| Payment due from Employer           | 0     | 0     | 0     |
			| Levy account debited                | 0     | 1000  | 1000  |
			| SFA Levy employer budget            | 1000  | 1000  | 00    |
			| SFA Levy co-funding budget          | 0     | 0     | 0     |
			| SFA Levy additional payments budget | 0     | 0     | 0     |
			| SFA non-Levy co-funding budget      | 0     | 0     | 0     |
			      
		When an ILR file is submitted for the first time on 30/10/17 with the following data:
            | learner type             | agreed price | standard code | start date | planned end date | actual end date | completion status |
            | 16-18 programme only DAS | 1            | 50            | 15/08/2017 | 20/08/2018       |                 | continuing        |
         
		 
		Then the data lock status will be as follows:
			| Payment type                   | 08/17               | 09/17               | 10/17               | 11/17               | 12/17               |
			| On-program                     | commitment 1 v1-001 | commitment 1 v1-001 | commitment 2 v2-001 | commitment 2 v2-001 | commitment 2 v2-001 |
			| Completion                     |                     |                     |                     |                     |                     |
			| Employer 16-18 incentive       |                     |                     |                     | commitment 2 v2-001 |                     |
			| Provider 16-18 incentive       |                     |                     |                     | commitment 2 v2-001 |                     |
			| Provider learning support      |                     |                     |                     |                     |                     |
			| English and maths on programme |                     |                     |                     |                     |                     |
			| English and maths Balancing    |                     |                     |                     |                     |                     |     

		And the provider earnings and payments break down as follows:
            | Type                          | 08/17 | 09/17 | 10/17 | 11/17    | 12/17   |
            | Provider Earned Total         | 0.07  | 0.07  | 0.07  | 1000.07  | 0.07    |
            | Provider Earned from SFA      | 0.07  | 0.07  | 0.07  | 1000.07  | 0.07    |
            | Provider Earned from Employer | 0     | 0     | 0     | 0        | 0       |
            | Provider Paid by SFA          | 0     | 1000  | 1000  | 0.07     | 1000.07 |
            | Refund taken by SFA           | 0     | 0     | 0     | -1999.87 | 0       |
            | Payment due from Employer     | 0     | 0     | 0     | 0        | 0       |
            | Levy account debited          | 0     | 1000  | 1000  | 0.07     | 0.07    |
            | Levy account credited         | 0     | 0     | 0     | 1999.87  | 0       |
            | SFA Levy employer budget      | 0.07  | 0.07  | 0.07  | 0.07     | 0.07    |
            | SFA Levy co-funding budget    | 0     | 0     | 0     | 0        | 0       |

		And the transaction types for the payments for provider a are:
            | Payment type             | 09/17 | 10/17 | 11/17 | 12/17 |
            | On-program               | 1000  | 1000  | 1000  | 0     |
            | Employer 16-18 incentive | 0     | 0     | 0     | 500   |
            | Provider 16-18 incentive | 0     | 0     | 0     | 500   |