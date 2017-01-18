Feature: 1 learner aged 16-18, levy available, earns incentives
  
   Scenario: Payment for a 16-18 DAS learner, levy available, incentives earned
    
	Given levy balance > agreed price for all months
    And the following commitments exist:
		  | ULN       | price effective date  | planned end date   | agreed price | status   |
		  | learner a | 01/08/2017            | 01/08/2018         | 15000        | active   |

	When an ILR file is submitted with the following data:
		  | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status |
		  | learner a | 16-18 programme only DAS | 15000        | 06/08/2017 | 08/08/2018       |                 | continuing        |
      
    Then the provider earnings and payments break down as follows:
		  | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		  | Provider Earned                     | 1000  | 1000  | 1000  | 2000  | 1000  | ... | 1000  | 0     |
		  | Provider Paid                       | 0     | 1000  | 1000  | 1000  | 2000  | ... | 1000  | 1000  |
		  | Levy account debited                | 0     | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
		  | SFA Levy employer budget            | 1000  | 1000  | 1000  | 1000  | 1000  | ... | 0     | 0     |
		  | SFA Levy co-funding budget          | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | SFA Levy additional payments budget | 0     | 0     | 0     | 1000  | 0     | ... | 1000  | 0     |

    And the transaction types for the payments are:
		  | Payment type             | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		  | On-program               | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
		  | Completion               | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | Balancing                | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		  | Employer 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		  | Provider 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |
   