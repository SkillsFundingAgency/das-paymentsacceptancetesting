
Feature: 1 learner aged 16-18, non-DAS, employed with a small employer at start, is fully funded for on programme and completion payments
@SmallEmployerNonDas
Scenario: Payment for a 16-18 non-DAS learner, small employer at start
	When an ILR file is submitted with the following data:
		| ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status | Employment Status Applies | Employer Id | Small Employer |
		| learner a | 16-18 programme only non-DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment          | 05/08/2017                | 12345678    | SEM1           |
    Then the provider earnings and payments break down as follows:
	    | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
	    | Provider Earned Total               | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from SFA            | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from Employer       | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Provider Paid by SFA                | 0     | 500   | 500   | 500   | 1500  | ... | 500   | 500   | 2500  |
	    | Payment due from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Levy account debited                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy employer budget            | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy co-funding budget          | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA non-Levy co-funding budget      | 500   | 500   | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
	    | SFA Levy additional payments budget | 0     | 0     | 0     | 1000  | 0     | ... | 0     | 1000  | 0     |
	And the transaction types for the payments are:
		| Payment type             | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		| On-program               | 500   | 500   | 500   | 500   | ... | 500   | 0     |
		| Completion               | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
		| Balancing                | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Employer 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Provider 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |
@SmallEmployerNonDas
Scenario: 1 learner aged 19-24, non-DAS, with an Education Health Care (EHC) plan, In paid employment with a small employer at start, is fully funded for on programme and completion payments
#Note: EHC plans are flagged on the ILR through Eligibility for Enhanced Funding (EEF) code = 2*
	When an ILR file is submitted with the following data:
		| ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status | Employment Status Applies | Employer Id | Small Employer | LearnDelFAM |
		| learner a | 19-24 programme only non-DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment          | 05/08/2017                | 12345678    | SEM1           | EEF2        |
   Then the provider earnings and payments break down as follows:
	    | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
	    | Provider Earned Total               | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from SFA            | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from Employer       | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Provider Paid by SFA                | 0     | 500   | 500   | 500   | 1500  | ... | 500   | 500   | 2500  |
	    | Payment due from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Levy account debited                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy employer budget            | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy co-funding budget          | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA non-Levy co-funding budget      | 500   | 500   | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
	    | SFA Levy additional payments budget | 0     | 0     | 0     | 1000  | 0     | ... | 0     | 1000  | 0     |
	And the transaction types for the payments are:
		| Payment type             | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		| On-program               | 500   | 500   | 500   | 500   | ... | 500   | 0     |
		| Completion               | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
		| Balancing                | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Employer 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Provider 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |
@SmallEmployerNonDas
Scenario: 1 learner aged 19-24, non-DAS, is a care leaver, In paid employment with a small employer at start, is fully funded for on programme and completion payments

#Note: care leavers are flagged on the ILR through EEF code = 4*

	When an ILR file is submitted with the following data:
		| ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status | Employment Status Applies | Employer Id | Small Employer | LearnDelFAM |
		| learner a | 19-24 programme only non-DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment          | 05/08/2017                | 12345678    | SEM1           | EEF4        |

	Then the provider earnings and payments break down as follows:
	    | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
	    | Provider Earned Total               | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from SFA            | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from Employer       | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Provider Paid by SFA                | 0     | 500   | 500   | 500   | 1500  | ... | 500   | 500   | 2500  |
	    | Payment due from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Levy account debited                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy employer budget            | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy co-funding budget          | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA non-Levy co-funding budget      | 500   | 500   | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
	    | SFA Levy additional payments budget | 0     | 0     | 0     | 1000  | 0     | ... | 0     | 1000  | 0     |
	And the transaction types for the payments are:
		| Payment type             | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		| On-program               | 500   | 500   | 500   | 500   | ... | 500   | 0     |
		| Completion               | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
		| Balancing                | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Employer 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Provider 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |
@SmallEmployerNonDas		
Scenario: 1 learner aged 19-24, non-DAS, employed with a small employer at start, is co-funded for on programme and completion payments (this apprentice does not have a Education Health Care plan and is not a care leaver)

	When an ILR file is submitted with the following data:
		| ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status | Employment Status Applies | Employer Id | Small Employer | LearnDelFAM |
		| learner a | 19-24 programme only non-DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment          | 05/08/2017                | 12345678    | SEM1           |             |

	Then the provider earnings and payments break down as follows:
		| Type                           | 08/17 | 09/17 | 10/17 | ... | 08/18 | 09/18 |
		| Provider Earned Total          | 500   | 500   | 500   | ... | 1500  | 0     |
		| Provider Earned from SFA       | 450   | 450   | 450   | ... | 1350  | 0     |
		| Provider Earned from Employer  | 50    | 50    | 50    | ... | 150   | 0     |
		| Provider Paid by SFA           | 0     | 450   | 450   | ... | 450   | 1350  |
		| Payment due from Employer      | 0     | 50    | 50    | ... | 50    | 150   |
		| Levy account debited           | 0     | 0     | 0     | ... | 0     | 0     |
		| SFA Levy employer budget       | 0     | 0     | 0     | ... | 0     | 0     |
		| SFA Levy co-funding budget     | 0     | 0     | 0     | ... | 0     | 0     |
		| SFA non-Levy co-funding budget | 450   | 450   | 450   | ... | 1350  | 0     |
    
	And the transaction types for the payments are:
		| Payment type | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 |
		| On-program   | 450   | 450   | 450   | ... | 450   | 0     |
		| Completion   | 0     | 0     | 0     | ... | 0     | 1350  |
		| Balancing    | 0     | 0     | 0     | ... | 0     | 0     |
@SmallEmployerNonDas
 Scenario: Payment for a 16-18 non-DAS learner, employer is not small
    
	When an ILR file is submitted with the following data:
		| ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer |
		| learner a | 16-18 programme only non-DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    |                |
    Then the provider earnings and payments break down as follows:
	    | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
	    | Provider Earned Total               | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from SFA            | 450   | 450   | 450   | 1450  | 450   | ... | 450   | 2350  | 0     |
	    | Provider Earned from Employer       | 50    | 50    | 50    | 50    | 50    | ... | 50    | 150   | 0     |
	    | Provider Paid by SFA                | 0     | 450   | 450   | 450   | 1450  | ... | 450   | 450   | 2350  |
	    | Payment due from Employer           | 0     | 50    | 50    | 50    | 50    | ... | 50    | 50    | 150   |
	    | Levy account debited                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy employer budget            | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy co-funding budget          | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA non-Levy co-funding budget      | 450   | 450   | 450   | 450   | 450   | ... | 450   | 1350  | 0     |
	    | SFA Levy additional payments budget | 0     | 0     | 0     | 1000  | 0     | ... | 0     | 1000  | 0     |
    And the transaction types for the payments are:
		| Payment type             | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		| On-program               | 450   | 450   | 450   | 450   | ... | 450   | 0     |
		| Completion               | 0     | 0     | 0     | 0     | ... | 0     | 1350  |
		| Balancing                | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Employer 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Provider 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |

#DAS small employers
@SmallEmployerDas
 Scenario: Payment for a 16-18 DAS learner, small employer at start
    Given levy balance > agreed price for all months
    And the following commitments exist:
	    | ULN       | framework code | programme type | pathway code | agreed price | start date |
	    | learner a | 403            | 2              | 1            | 7500         | 06/08/2017 |
	When an ILR file is submitted with the following data:
		| ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer |
		| learner a | 16-18 programme only DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    | SEM1           |
	Then the provider earnings and payments break down as follows:
	    | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
	    | Provider Earned Total               | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from SFA            | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from Employer       | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Provider Paid by SFA                | 0     | 500   | 500   | 500   | 1500  | ... | 500   | 500   | 2500  |
	    | Payment due from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Levy account debited                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy employer budget            | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy co-funding budget          | 500   | 500   | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
	    | SFA non-Levy co-funding budget      | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy additional payments budget | 0     | 0     | 0     | 1000  | 0     | ... | 0     | 1000  | 0     |
	And the transaction types for the payments are:
		| Payment type             | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		| On-program               | 500   | 500   | 500   | 500   | ... | 500   | 0     |
		| Completion               | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
		| Balancing                | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Employer 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Provider 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |

@SmallEmployerDas	
 Scenario: Scenario: 1 learner aged 19-24, DAS, with an Education Health Care (EHC) plan, employed with a small employer at start, is fully funded for on programme and completion payments
    Given levy balance > agreed price for all months
    And the following commitments exist:
	    | ULN       | framework code | programme type | pathway code | agreed price | start date |
	    | learner a | 403            | 2              | 1            | 7500         | 06/08/2017 |
	When an ILR file is submitted with the following data:
		| ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer | LearnDelFAM |
		| learner a | 19-24 programme only DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    | SEM1           | EEF2        |
   Then the provider earnings and payments break down as follows:
	    | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
	    | Provider Earned Total               | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from SFA            | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from Employer       | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Provider Paid by SFA                | 0     | 500   | 500   | 500   | 1500  | ... | 500   | 500   | 2500  |
	    | Payment due from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Levy account debited                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy employer budget            | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy co-funding budget          | 500   | 500   | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
	    | SFA non-Levy co-funding budget      | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy additional payments budget | 0     | 0     | 0     | 1000  | 0     | ... | 0     | 1000  | 0     |
	And the transaction types for the payments are:
		| Payment type             | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		| On-program               | 500   | 500   | 500   | 500   | ... | 500   | 0     |
		| Completion               | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
		| Balancing                | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Employer 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Provider 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |
	
@SmallEmployerDas	
Scenario: 1 learner aged 19-24, DAS, is a care leaver, employed with a small employer at start, is fully funded for on programme and completion payments
	Given levy balance > agreed price for all months
    And the following commitments exist:
	    | ULN       | framework code | programme type | pathway code | agreed price | start date |
	    | learner a | 403            | 2              | 1            | 7500         | 06/08/2017 |
	When an ILR file is submitted with the following data:
		| ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer | LearnDelFAM |
		| learner a | 19-24 programme only DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    | SEM1           | EEF4        |
   Then the provider earnings and payments break down as follows:
	    | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
	    | Provider Earned Total               | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from SFA            | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from Employer       | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Provider Paid by SFA                | 0     | 500   | 500   | 500   | 1500  | ... | 500   | 500   | 2500  |
	    | Payment due from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Levy account debited                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy employer budget            | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy co-funding budget          | 500   | 500   | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
	    | SFA non-Levy co-funding budget      | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy additional payments budget | 0     | 0     | 0     | 1000  | 0     | ... | 0     | 1000  | 0     |
	And the transaction types for the payments are:
		| Payment type             | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		| On-program               | 500   | 500   | 500   | 500   | ... | 500   | 0     |
		| Completion               | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
		| Balancing                | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Employer 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Provider 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |


@SmallEmployerDas		
Scenario: 1 learner aged 19-24, DAS, employed with a small employer at start, is funded using levy for on programme and completion payments (this apprentice does not have a Education Health Care plan and is not a care leaver)

	Given levy balance > agreed price for all months
    And the following commitments exist:
	    | ULN       | framework code | programme type | pathway code | agreed price | start date |
	    | learner a | 403            | 2              | 1            | 7500         | 06/08/2017 |
	When an ILR file is submitted with the following data:
		| ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer | LearnDelFAM |
		| learner a | 19-24 programme only DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    | SEM1           |             |
	Then the provider earnings and payments break down as follows:
	    | Type                           | 08/17 | 09/17 | 10/17 | ... | 07/18 | 08/18 | 09/18 |
	    | Provider Earned Total          | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
	    | Provider Earned from SFA       | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
	    | Provider Earned from Employer  | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Provider Paid by SFA           | 0     | 500   | 500   | ... | 500   | 500   | 1500  |
	    | Payment due from Employer      | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Levy account debited           | 0     | 500   | 500   | ... | 500   | 500   | 1500  |
	    | SFA Levy employer budget       | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
	    | SFA Levy co-funding budget     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA non-Levy co-funding budget | 0     | 0     | 0     | ... | 0     | 0     | 0     |
    And the transaction types for the payments are:
		| Payment type             | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 |
		| On-program               | 500   | 500   | 500   | ... | 500   | 0     |
		| Completion               | 0     | 0     | 0     | ... | 0     | 1500  |
		| Balancing                | 0     | 0     | 0     | ... | 0     | 0     |
		| Employer 16-18 incentive | 0     | 0     | 0     | ... | 0     | 0     |
		| Provider 16-18 incentive | 0     | 0     | 0     | ... | 0     | 0     |
   
  @SmallEmployerDas   
Scenario: Payment for a 16-18 DAS learner, employer is not small
  
	Given levy balance > agreed price for all months
    And the following commitments exist:
	    | ULN       | framework code | programme type | pathway code | agreed price | start date |
	    | learner a | 403            | 2              | 1            | 7500         | 06/08/2017 |
	When an ILR file is submitted with the following data:
		| ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer | LearnDelFAM |
		| learner a | 16-18 programme only DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    |                |             |
 
	Then the provider earnings and payments break down as follows:
	    | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
	    | Provider Earned Total               | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from SFA            | 500   | 500   | 500   | 1500  | 500   | ... | 500   | 2500  | 0     |
	    | Provider Earned from Employer       | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Provider Paid by SFA                | 0     | 500   | 500   | 500   | 1500  | ... | 500   | 500   | 2500  |
	    | Payment due from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Levy account debited                | 0     | 500   | 500   | 500   | 500   | ... | 500   | 500   | 1500  |
	    | SFA Levy employer budget            | 500   | 500   | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
	    | SFA Levy co-funding budget          | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA non-Levy co-funding budget      | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy additional payments budget | 0     | 0     | 0     | 1000  | 0     | ... | 0     | 1000  | 0     |
	And the transaction types for the payments are:
		| Payment type             | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		| On-program               | 500   | 500   | 500   | 500   | ... | 500   | 0     |
		| Completion               | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
		| Balancing                | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Employer 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Provider 16-18 incentive | 0     | 0     | 0     | 500   | ... | 0     | 500   |