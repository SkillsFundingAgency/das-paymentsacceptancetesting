@Refunds
Feature: Provider earnings and payments where learner refund payments are due
Scenario:673-AC01 DAS learner, levy available, provider retrospectively notifies a withdrawal and previously-paid monthly instalments need to be refunded.

  Given The learner is programme only DAS
    And the apprenticeship funding band maximum is 17000
    And levy balance > agreed price for all months
	And the following commitments exist:
		| commitment Id | version Id | ULN       | start date | end date   | status | agreed price | effective from | effective to |
		| 1             | 1          | learner a | 01/08/2017 | 01/08/2018 | active | 11250        | 01/08/2017     |              |

	And the following earnings and payments have been made to the provider for learner a:
	    | Type                          | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
        | Provider Earned Total         | 750   | 750   | 750   | 750   | 750   | 0     |       
        | Provider Earned from SFA      | 750   | 750   | 750   | 750   | 750   | 0     |       
        | Provider Earned from Employer | 0     | 0     | 0     | 0     | 0     | 0     |       
        | Provider Paid by SFA          | 0     | 750   | 750   | 750   | 750   | 750   |        
        | Payment due from Employer     | 0     | 0     | 0     | 0     | 0     | 0     |       
        | Levy account debited          | 0     | 750   | 750   | 750   | 750   | 750   |         
        | SFA Levy employer budget      | 750   | 750   | 750   | 750   | 750   | 0     |        
        | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | 0     | 0     |       
    When an ILR file is submitted for the first time on 10/01/18 with the following data:
        | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | 
        | learner a | 04/08/2017 | 20/08/2018       | 12/11/2017      | withdrawn         | 9000                 | 04/08/2017                          | 2250                   | 04/08/2017                            | 
	Then the provider earnings and payments break down as follows:
        | Type                          | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 | 02/18 |
        | Provider Earned Total         | 750   | 750   | 750   | 0     | 0     | 0     | 0     |
        | Provider Earned from SFA      | 750   | 750   | 750   | 0     | 0     | 0     | 0     |
        | Provider Earned from Employer | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
        | Provider Paid by SFA          | 0     | 750   | 750   | 750   | 750   | 750   | 0     |
        | Refund taken by SFA           | 0     | 0     | 0     | 0     | 0     | 0     | -1500 |
        | Payment due from Employer     | 0     | 0     | 0     | 0     | 0     | 0     | 0     |
        | Levy account debited          | 0     | 750   | 750   | 750   | 750   | 750   | 0     |
        | Levy account credited         | 0     | 0     | 0     | 0     | 0     | 0     | 1500  |
        | SFA Levy employer budget      | 750   | 750   | 750   | 750   | 750   | 0     | 0     |
        | SFA Levy co-funding budget    | 0     | 0     | 0     | 0     | 0     | 0     | 0     |


Scenario:673-AC02 Non-DAS learner, levy available, provider retrospectively notifies a withdrawal and previously-paid monthly instalments need to be refunded.
	Given  the apprenticeship funding band maximum is 17000
	And the following earnings and payments have been made to the provider for learner a:
        | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
        | Provider Earned Total          | 750   | 750   | 750   | 750   | 750   | 0     |       
        | Provider Earned from SFA       | 675   | 675   | 675   | 675   | 675   | 0     |       
        | Provider Earned from Employer  | 75    | 75    | 75    | 75    | 75    | 0     |       
        | Provider Paid by SFA           | 0     | 675   | 675   | 675   | 675   | 675   |        
        | Payment due from Employer      | 0     | 75    | 75    | 75    | 75    | 75    |       
        | Levy account debited           | 0     | 0     | 0     | 0     | 0     | 0     |         
        | SFA Levy employer budget       | 0     | 0     | 0     | 0     | 0     | 0     |        
        | SFA Levy co-funding budget     | 0     | 0     | 0     | 0     | 0     | 0     |       
        | SFA non-Levy co-funding budget | 675   | 675   | 675   | 675   | 675   | 0     |
        
    When an ILR file is submitted for the first time on 28/02/18 with the following data:
        
        | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | 
        | learner a | 04/08/2017 | 20/08/2018       | 12/11/2017      | withdrawn         | 9000                 | 04/08/2017                          | 2250                   | 04/08/2017                            | 
	Then the provider earnings and payments break down as follows:
        | Type                           | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
        | Provider Earned Total          | 750   | 750   | 750   | 0     | 0     | 0     |
        | Provider Earned from SFA       | 675   | 675   | 675   | 0     | 0     | 0     |
        | Provider Earned from Employer  | 75    | 75    | 75    | 0     | 0     | 0     |
        | Provider Paid by SFA           | 0     | 675   | 675   | 675   | 675   | 675   | 
        | Refund taken by SFA            | 0     | 0     | 0     | 0     | -675  | -675  |
        | Payment due from Employer      | 0     | 75    | 75    | 75    | 0     | 0     |
        | Levy account debited           | 0     | 0     | 0     | 0     | 0     | 0     |
        | Levy account credited          | 0     | 0     | 0     | 0     | 0     | 0     |
        | SFA Levy employer budget       | 0     | 0     | 0     | 0     | 0     | 0     |
        | SFA Levy co-funding budget     | 0     | 0     | 0     | 0     | 0     | 0     |
        | SFA non-Levy co-funding budget | 675   | 675   | 675   | 0     | 0     | 0     |
		
Scenario:673-AC03 DAS learner, insufficient levy available to cover full payments, provider retrospectively notifies a withdrawal and previously-paid monthly instalments need to be refunded.	Given The learner is programme only DAS
    Given The learner is programme only DAS
	And the apprenticeship funding band maximum is 17000
    And levy balance > agreed price for all months
	And the following commitments exist:
		  | commitment Id | version Id | ULN       | start date | end date   | status | agreed price | effective from | effective to |
		  | 1             | 1          | learner a | 01/08/2017 | 01/08/2018 | active | 11250        | 01/08/2017     |              |

	And the following earnings and payments have been made to the provider for learner a:

		| Type                          | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
		| Provider Earned Total         | 750   | 750   | 750   | 750   | 750   | 0     |       
		| Provider Earned from SFA      | 725   | 725   | 725   | 725   | 725   | 0     |       
		| Provider Earned from Employer | 25    | 25    | 25    | 25    | 25    | 0     |       
		| Provider Paid by SFA          | 0     | 725   | 725   | 725   | 725   | 725   |        
		| Payment due from Employer     | 0     | 25    | 25    | 25    | 25    | 25    |       
		| Levy account debited          | 0     | 500   | 500   | 500   | 500   | 500   |         
		| SFA Levy employer budget      | 500   | 500   | 500   | 500   | 500   | 0     |        
		| SFA Levy co-funding budget    | 225   | 225   | 225   | 225   | 225   | 0     |       
    When an ILR file is submitted for the first time on 28/02/18 with the following data:
        | ULN       | start date | planned end date | actual end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date | 
        | learner a | 04/08/2017 | 20/08/2018       | 12/11/2017      | withdrawn         | 9000                 | 04/08/2017                          | 2250                   | 04/08/2017                            | 
	Then the provider earnings and payments break down as follows:
        | Type                          | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | 01/18 |
        | Provider Earned Total         | 750   | 750   | 750   | 0     | 0     | 0     |
        | Provider Earned from SFA      | 725   | 725   | 725   | 0     | 0     | 0     |
        | Provider Earned from Employer | 25    | 25    | 25    | 0     | 0     | 0     |
        | Provider Paid by SFA          | 0     | 725   | 725   | 725   | 725   | 725   |
        | Refund taken by SFA           | 0     | 0     | 0     | 0     | -725  | -725  |
        | Payment due from Employer     | 0     | 25    | 25    | 25    | 0     | 0     |
        | Levy account debited          | 0     | 500   | 500   | 500   | 500   | 500   |
        | Levy account credited         | 0     | 0     | 0     | 0     | 500   | 500   |
        | SFA Levy employer budget      | 500   | 500   | 500   | 0     | 0     | 0     |
        | SFA Levy co-funding budget    | 225   | 225   | 225   | 0     | 0     | 0     |