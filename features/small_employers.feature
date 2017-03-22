
Feature: 1 learner aged 16-18, non-DAS, employed with a small employer at start, is fully funded for on programme and completion payments
@SmallEmployerNonDas
Scenario:AC1-Payment for a 16-18 non-DAS learner, small employer at start
	Given the apprenticeship funding band maximum is 9000
    When an ILR file is submitted with the following data:
        | ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status   | Employment Status Applies | Employer Id | Small Employer |
        | learner a | 16-18 programme only non-DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment  | 05/08/2017                | 12345678    | SEM1           |
	And the employment status in the ILR is:
        | Employer    | Employment Status      | Employment Status Applies | Small Employer |
        | employer 1  | in paid employment     | 05/08/2017                | SEM1           |
    Then the provider earnings and payments break down as follows:
        | Type                                    | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
        | Provider Earned Total                   | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
        | Provider Earned from SFA                | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
        | Provider Earned from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | Provider Paid by SFA                    | 0     | 620   | 620   | 620   | 1620  | ... | 620   | 620   | 2860  |
        | Payment due from Employer               | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | Levy account debited                    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA Levy employer budget                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA Levy co-funding budget              | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA non-Levy co-funding budget          | 500   | 500   | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
        | SFA non-Levy additional payments budget | 120   | 120   | 120   | 1120  | 120   | ... | 120   | 1360  | 0     |

    And the transaction types for the payments are:
        | Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
        | On-program                   | 500   | 500   | 500   | 500   | ... | 500   | 0     |
        | Completion                   | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
        | Balancing                    | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Employer 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Provider 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Framework uplift on-program  | 120   | 120   | 120   | 120   | ... | 120   | 0     |
        | Framework uplift completion  | 0     | 0     | 0     | 0     | ... | 0     | 360   |
        | Framework uplift balancing   | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Provider disadvantage uplift | 0     | 0     | 0     | 0     | ..  | 0     | 0     |

@SmallEmployerNonDas
Scenario:AC2- 1 learner aged 19-24, non-DAS, with an Education Health Care (EHC) plan, In paid employment with a small employer at start, is fully funded for on programme and completion payments
#Note: EHC plans are flagged on the ILR through Eligibility for Enhanced Funding (EEF) code = 2*
	Given the apprenticeship funding band maximum is 9000
    When an ILR file is submitted with the following data:
        | ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer | LearnDelFAM |
        | learner a | 19-24 programme only non-DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    | SEM1           | EEF2        |
	And the employment status in the ILR is:
        | Employer    | Employment Status      | Employment Status Applies | Small Employer |
        | employer 1  | in paid employment     | 05/08/2017                | SEM1           |
	Then the provider earnings and payments break down as follows:
        | Type                                    | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
        | Provider Earned Total                   | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
        | Provider Earned from SFA                | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
        | Provider Earned from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | Provider Paid by SFA                    | 0     | 620   | 620   | 620   | 1620  | ... | 620   | 620   | 2860  |
        | Payment due from Employer               | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | Levy account debited                    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA Levy employer budget                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA Levy co-funding budget              | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA non-Levy co-funding budget          | 500   | 500   | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
        | SFA non-Levy additional payments budget | 120   | 120   | 120   | 1120  | 120   | ... | 120   | 1360  | 0     |

    And the transaction types for the payments are:
        | Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
        | On-program                   | 500   | 500   | 500   | 500   | ... | 500   | 0     |
        | Completion                   | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
        | Balancing                    | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Employer 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Provider 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Framework uplift on-program  | 120   | 120   | 120   | 120   | ... | 120   | 0     |
        | Framework uplift completion  | 0     | 0     | 0     | 0     | ... | 0     | 360   |
        | Framework uplift balancing   | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Provider disadvantage uplift | 0     | 0     | 0     | 0     | ..  | 0     | 0     |
@SmallEmployerNonDas
Scenario:AC3- 1 learner aged 19-24, non-DAS, is a care leaver, In paid employment with a small employer at start, is fully funded for on programme and completion payments

#Note: care leavers are flagged on the ILR through EEF code = 4*
	Given the apprenticeship funding band maximum is 9000
    When an ILR file is submitted with the following data:
        | ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer | LearnDelFAM |
        | learner a | 19-24 programme only non-DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    | SEM1           | EEF4        |
	And the employment status in the ILR is:
        | Employer    | Employment Status      | Employment Status Applies | Small Employer |
        | employer 1  | in paid employment     | 05/08/2017                | SEM1           |
    Then the provider earnings and payments break down as follows:
        | Type                                    | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
        | Provider Earned Total                   | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
        | Provider Earned from SFA                | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
        | Provider Earned from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | Provider Paid by SFA                    | 0     | 620   | 620   | 620   | 1620  | ... | 620   | 620   | 2860  |
        | Payment due from Employer               | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | Levy account debited                    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA Levy employer budget                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA Levy co-funding budget              | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA non-Levy co-funding budget          | 500   | 500   | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
        | SFA non-Levy additional payments budget | 120   | 120   | 120   | 1120  | 120   | ... | 120   | 1360  | 0     |

    And the transaction types for the payments are:
        | Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
        | On-program                   | 500   | 500   | 500   | 500   | ... | 500   | 0     |
        | Completion                   | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
        | Balancing                    | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Employer 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Provider 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Framework uplift on-program  | 120   | 120   | 120   | 120   | ... | 120   | 0     |
        | Framework uplift completion  | 0     | 0     | 0     | 0     | ... | 0     | 360   |
        | Framework uplift balancing   | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Provider disadvantage uplift | 0     | 0     | 0     | 0     | ..  | 0     | 0     |
@SmallEmployerNonDas        
Scenario:AC4- 1 learner aged 19-24, non-DAS, employed with a small employer at start, is co-funded for on programme and completion payments (this apprentice does not have a Education Health Care plan and is not a care leaver)

    When an ILR file is submitted with the following data:
        | ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer | LearnDelFAM |
        | learner a | 19-24 programme only non-DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    | SEM1           |             |
	And the employment status in the ILR is:
        | Employer    | Employment Status      | Employment Status Applies | Small Employer |
        | employer 1  | in paid employment     | 06/08/2017                | SEM1           |
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
 Scenario:AC5- Payment for a 16-18 non-DAS learner, employer is not small
    Given the apprenticeship funding band maximum is 9000
    When an ILR file is submitted with the following data:
        | ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer |
        | learner a | 16-18 programme only non-DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    |                |
    And the employment status in the ILR is:
        | Employer    | Employment Status      | Employment Status Applies | Small Employer |
        | employer 1  | in paid employment     | 05/08/2017                |                |
	Then the provider earnings and payments break down as follows:
        | Type                                    | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
        | Provider Earned Total                   | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
        | Provider Earned from SFA                | 570   | 570   | 570   | 1570  | 570   | ... | 570   | 2710  | 0     |
        | Provider Earned from Employer           | 50    | 50    | 50    | 50    | 50    | ... | 50    | 150   | 0     |
        | Provider Paid by SFA                    | 0     | 570   | 570   | 570   | 1570  | ... | 570   | 570   | 2710  |
        | Payment due from Employer               | 0     | 50    | 50    | 50    | 50    | ... | 50    | 50    | 150   |
        | Levy account debited                    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA Levy employer budget                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA Levy co-funding budget              | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA non-Levy co-funding budget          | 450   | 450   | 450   | 450   | 450   | ... | 450   | 1350  | 0     |
        | SFA non-Levy additional payments budget | 120   | 120   | 120   | 1120  | 120   | ... | 120   | 1360  | 0     |
    And the transaction types for the payments are:
        | Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
        | On-program                   | 450   | 450   | 450   | 450   | ... | 450   | 0     |
        | Completion                   | 0     | 0     | 0     | 0     | ... | 0     | 1350  |
        | Balancing                    | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Employer 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Provider 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Framework uplift on-program  | 120   | 120   | 120   | 120   | ... | 120   | 0     |
        | Framework uplift completion  | 0     | 0     | 0     | 0     | ... | 0     | 360   |
        | Framework uplift balancing   | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Provider disadvantage uplift | 0     | 0     | 0     | 0     | ..  | 0     | 0     |

@SmallEmployerNonDasMultipleEmploymentStatuses
Scenario:AC6- 1 learner aged 16-18, non-DAS. Second employment status record added with same employer id but small employer flag removed. Learner retains small employer funding.
	Given the apprenticeship funding band maximum is 9000
	When an ILR file is submitted with the following data:
		| ULN        | learner type                 | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | agreed price |
		| 1234567891 | 16-18 programme only non-DAS | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | 7500         |
	And the employment status in the ILR is:
        | Employer    | Employment Status      | Employment Status Applies | Small Employer |
        | employer 1  | in paid employment     | 05/08/2017                | SEM1           |
        | employer 1  | in paid employment     | 05/10/2017                |                |
    Then the provider earnings and payments break down as follows:
		| Type                                    | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		| Provider Earned Total                   | 620   | 620   | 620   | 1620  | 620   | ... | 2860  | 0     |
		| Provider Earned from SFA                | 620   | 620   | 620   | 1620  | 620   | ... | 2860  | 0     |
		| Provider Earned from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Provider Paid by SFA                    | 0     | 620   | 620   | 620   | 1620  | ... | 620   | 2860  |
		| Payment due from Employer               | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Levy account debited                    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| SFA Levy employer budget                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| SFA Levy co-funding budget              | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| SFA non-Levy co-funding budget          | 500   | 500   | 500   | 500   | 500   | ... | 1500  | 0     |
		| SFA non-Levy additional payments budget | 120   | 120   | 120   | 1120  | 120   | ... | 1360  | 0     |
 	And the transaction types for the payments are:
		| Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		| On-program                   | 500   | 500   | 500   | 500   | ... | 500   | 0     |
		| Completion                   | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
		| Balancing                    | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Employer 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Provider 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Framework uplift on-program  | 120   | 120   | 120   | 120   | ... | 120   | 0     |
		| Framework uplift completion  | 0     | 0     | 0     | 0     | ... | 0     | 360   |
		| Framework uplift balancing   | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Provider disadvantage uplift | 0     | 0     | 0     | 0     | ..  | 0     | 0     |


#DAS small employers
@SmallEmployerDas
 Scenario:AC7- Payment for a 16-18 DAS learner, small employer at start
    Given levy balance > agreed price for all months
	And the apprenticeship funding band maximum is 9000
    And the following commitments exist:
        | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   |
        | learner a | 403            | 2              | 1            | 7500         | 06/08/2017 | 08/08/2018 |
    When an ILR file is submitted with the following data:
        | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer |
        | learner a | 16-18 programme only DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    | SEM1           |
	And the employment status in the ILR is:
        | Employer    | Employment Status      | Employment Status Applies | Small Employer |
        | employer 1  | in paid employment     | 05/08/2017                | SEM1           |
    Then the provider earnings and payments break down as follows:
        | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
        | Provider Earned Total               | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
        | Provider Earned from SFA            | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
        | Provider Earned from Employer       | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | Provider Paid by SFA                | 0     | 620   | 620   | 620   | 1620  | ... | 620   | 620   | 2860  |
        | Payment due from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | Levy account debited                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA Levy employer budget            | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA Levy co-funding budget          | 500   | 500   | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
        | SFA Levy additional payments budget | 120   | 120   | 120   | 1120  | 120   | ... | 120   | 1360  | 0     |

    And the transaction types for the payments are:
        | Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
        | On-program                   | 500   | 500   | 500   | 500   | ... | 500   | 0     |
        | Completion                   | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
        | Balancing                    | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Employer 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Provider 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Framework uplift on-program  | 120   | 120   | 120   | 120   | ... | 120   | 0     |
        | Framework uplift completion  | 0     | 0     | 0     | 0     | ... | 0     | 360   |
        | Framework uplift balancing   | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Provider disadvantage uplift | 0     | 0     | 0     | 0     | ..  | 0     | 0     |

@SmallEmployerDas   
 Scenario:AC8- Scenario: 1 learner aged 19-24, DAS, with an Education Health Care (EHC) plan, employed with a small employer at start, is fully funded for on programme and completion payments
    Given levy balance > agreed price for all months
	And the apprenticeship funding band maximum is 9000
    And the following commitments exist:
        | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   |
        | learner a | 403            | 2              | 1            | 7500         | 06/08/2017 | 08/08/2018 |
    When an ILR file is submitted with the following data:
        | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer | LearnDelFAM |
        | learner a | 19-24 programme only DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    | SEM1           | EEF2        |
	And the employment status in the ILR is:
        | Employer    | Employment Status      | Employment Status Applies | Small Employer |
        | employer 1  | in paid employment     | 05/08/2017                | SEM1           |
    Then the provider earnings and payments break down as follows:
        | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
        | Provider Earned Total               | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
        | Provider Earned from SFA            | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
        | Provider Earned from Employer       | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | Provider Paid by SFA                | 0     | 620   | 620   | 620   | 1620  | ... | 620   | 620   | 2860  |
        | Payment due from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | Levy account debited                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA Levy employer budget            | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA Levy co-funding budget          | 500   | 500   | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
        | SFA Levy additional payments budget | 120   | 120   | 120   | 1120  | 120   | ... | 120   | 1360  | 0     |

    And the transaction types for the payments are:
        | Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
        | On-program                   | 500   | 500   | 500   | 500   | ... | 500   | 0     |
        | Completion                   | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
        | Balancing                    | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Employer 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Provider 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Framework uplift on-program  | 120   | 120   | 120   | 120   | ... | 120   | 0     |
        | Framework uplift completion  | 0     | 0     | 0     | 0     | ... | 0     | 360   |
        | Framework uplift balancing   | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Provider disadvantage uplift | 0     | 0     | 0     | 0     | ..  | 0     | 0     |
    
@SmallEmployerDas   
Scenario:AC9- 1 learner aged 19-24, DAS, is a care leaver, employed with a small employer at start, is fully funded for on programme and completion payments
    Given levy balance > agreed price for all months
	And the apprenticeship funding band maximum is 9000
    And the following commitments exist:
        | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   |
        | learner a | 403            | 2              | 1            | 7500         | 06/08/2017 | 08/08/2018 |
    When an ILR file is submitted with the following data:
        | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer | LearnDelFAM |
        | learner a | 19-24 programme only DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    | SEM1           | EEF4        |
	And the employment status in the ILR is:
        | Employer    | Employment Status      | Employment Status Applies | Small Employer |
        | employer 1  | in paid employment     | 05/08/2017                | SEM1           |
    Then the provider earnings and payments break down as follows:
        | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
        | Provider Earned Total               | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
        | Provider Earned from SFA            | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
        | Provider Earned from Employer       | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | Provider Paid by SFA                | 0     | 620   | 620   | 620   | 1620  | ... | 620   | 620   | 2860  |
        | Payment due from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | Levy account debited                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA Levy employer budget            | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
        | SFA Levy co-funding budget          | 500   | 500   | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
        | SFA Levy additional payments budget | 120   | 120   | 120   | 1120  | 120   | ... | 120   | 1360  | 0     |

    And the transaction types for the payments are:
        | Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
        | On-program                   | 500   | 500   | 500   | 500   | ... | 500   | 0     |
        | Completion                   | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
        | Balancing                    | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Employer 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Provider 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
        | Framework uplift on-program  | 120   | 120   | 120   | 120   | ... | 120   | 0     |
        | Framework uplift completion  | 0     | 0     | 0     | 0     | ... | 0     | 360   |
        | Framework uplift balancing   | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Provider disadvantage uplift | 0     | 0     | 0     | 0     | ..  | 0     | 0     |


@SmallEmployerDas       
Scenario:AC10- 1 learner aged 19-24, DAS, employed with a small employer at start, is funded using levy for on programme and completion payments (this apprentice does not have a Education Health Care plan and is not a care leaver)

    Given levy balance > agreed price for all months
    And the following commitments exist:
        | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   |
        | learner a | 403            | 2              | 1            | 7500         | 06/08/2017 | 08/08/2018 |
    When an ILR file is submitted with the following data:
        | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer | LearnDelFAM |
        | learner a | 19-24 programme only DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    | SEM1           |             |
	And the employment status in the ILR is:
        | Employer    | Employment Status      | Employment Status Applies | Small Employer |
        | employer 1  | in paid employment     | 05/08/2017                | SEM1           |
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
        | Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
        | On-program                   | 500   | 500   | 500   | 500   | ... | 500   | 0     |
        | Completion                   | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
        | Balancing                    | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Employer 16-18 incentive     | 0     | 0     | 0     | 0   | ... | 0     | 0   |
        | Provider 16-18 incentive     | 0     | 0     | 0     | 0   | ... | 0     | 0   |
       
   
  @SmallEmployerDas   
Scenario:AC11- Payment for a 16-18 DAS learner, employer is not small
 
    Given levy balance > agreed price for all months
	And the apprenticeship funding band maximum is 9000
    And the following commitments exist:
        | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   |
        | learner a | 403            | 2              | 1            | 7500         | 06/08/2017 | 08/08/2018 |
    When an ILR file is submitted with the following data:
        | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | Employment Status  | Employment Status Applies | Employer Id | Small Employer | LearnDelFAM |
        | learner a | 16-18 programme only DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | In paid employment | 05/08/2017                | 12345678    |                |             |
 
	Then the provider earnings and payments break down as follows:
	    | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 07/18 | 08/18 | 09/18 |
	    | Provider Earned Total               | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
	    | Provider Earned from SFA            | 620   | 620   | 620   | 1620  | 620   | ... | 620   | 2860  | 0     |
	    | Provider Earned from Employer       | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Provider Paid by SFA                | 0     | 620   | 620   | 620   | 1620  | ... | 620   | 620   | 2860  |
	    | Payment due from Employer           | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | Levy account debited                | 0     | 500   | 500   | 500   | 500   | ... | 500   | 500   | 1500  |
	    | SFA Levy employer budget            | 500   | 500   | 500   | 500   | 500   | ... | 500   | 1500  | 0     |
	    | SFA Levy co-funding budget          | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     |
	    | SFA Levy additional payments budget | 120   | 120   | 120   | 1120  | 120   | ... | 120   | 1360  | 0     |

	And the transaction types for the payments are:
		| Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		| On-program                   | 500   | 500   | 500   | 500   | ... | 500   | 0     |
		| Completion                   | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
		| Balancing                    | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Employer 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Provider 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Framework uplift on-program  | 120   | 120   | 120   | 120   | ... | 120   | 0     |
		| Framework uplift completion  | 0     | 0     | 0     | 0     | ... | 0     | 360   |
		| Framework uplift balancing   | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Provider disadvantage uplift | 0     | 0     | 0     | 0     | ..  | 0     | 0     |


@SmallEmployerMultipleEmploymentStatus
Scenario:AC12- Payment for a 16-18 non-DAS learner, small employer at start, change to large employer
	Given the apprenticeship funding band maximum is 9000
	When an ILR file is submitted with the following data:
		| ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | 
		| learner a | 16-18 programme only non-DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | 
    And the employment status in the ILR is:
		| Employer    | Employment Status  | Employment Status Applies | Small Employer |
		| employer 1  | in paid employment | 05/08/2017                | SEM1           |
		| employer 2  | in paid employment | 05/10/2017                |                |
	 Then the provider earnings and payments break down as follows:
		| Type                                    | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		| Provider Earned Total                   | 620   | 620   | 620   | 1620  | 620   | ... | 2860  | 0     |
		| Provider Earned from SFA                | 620   | 620   | 570   | 1570  | 570   | ... | 2710  | 0     |
		| Provider Earned from Employer           | 0     | 0     | 50    | 50    | 50    | ... | 150   | 0     |
		| Provider Paid by SFA                    | 0     | 620   | 620   | 570   | 1570  | ... | 570   | 2710  |
		| Payment due from Employer 2             | 0     | 0     | 0     | 50    | 50    | ... | 50    | 150   |
		| Levy account debited                    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| SFA Levy employer budget                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| SFA Levy co-funding budget              | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| SFA non-Levy co-funding budget          | 500   | 500   | 450   | 450   | 450   | ... | 1350  | 0     |
		| SFA non-Levy additional payments budget | 120   | 120   | 120   | 1120  | 120   | ... | 1360  | 0     |
	And the transaction types for the payments are:
		| Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		| On-program                   | 500   | 500   | 450   | 450   | ... | 450   | 0     |
		| Completion                   | 0     | 0     | 0     | 0     | ... | 0     | 1350  |
		| Balancing                    | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Employer 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Provider 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Framework uplift on-program  | 120   | 120   | 120   | 120   | ... | 120   | 0     |
		| Framework uplift completion  | 0     | 0     | 0     | 0     | ... | 0     | 360   |
		| Framework uplift balancing   | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Provider disadvantage uplift | 0     | 0     | 0     | 0     | ..  | 0     | 0     |




@SmallEmployerMultipleEmploymentStatus
Scenario:AC13- Payment for a 16-18 non-DAS learner, large employer at start, change to small employer
	Given the apprenticeship funding band maximum is 9000
	When an ILR file is submitted with the following data:
		| ULN       | learner type                 | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code | 
		| learner a | 16-18 programme only non-DAS | 7500         | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | 403            | 2              | 1            | 
    And the employment status in the ILR is:
		| Employer    | Employment Status  | Employment Status Applies | Small Employer |
		| employer 1  | in paid employment | 05/08/2017                |                |
		| employer 2  | in paid employment | 05/10/2017                | SEM1           |
	 Then the provider earnings and payments break down as follows:
	    | Type                                    | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
	    | Provider Earned Total                   | 620   | 620   | 620   | 1620  | 620   | ... | 2860  | 0     |
	    | Provider Earned from SFA                | 570   | 570   | 620   | 1620  | 620   | ... | 2860  | 0     |
	    | Provider Earned from Employer           | 50    | 50    | 0     | 0     | 0     | ... | 0     | 0     |
	    | Provider Paid by SFA                    | 0     | 570   | 570   | 620   | 1620  | ... | 620   | 2860  |
	    | Payment due from Employer 1             | 0     | 50    | 50    | 0     | 0     | ... | 0     | 0     |
	    | Levy account debited                    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
	    | SFA Levy employer budget                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
	    | SFA Levy co-funding budget              | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
	    | SFA non-Levy co-funding budget          | 450   | 450   | 500   | 500   | 500   | ... | 1500  | 0     |
	    | SFA non-Levy additional payments budget | 120   | 120   | 120   | 1120  | 120   | ... | 1360  | 0     |
	And the transaction types for the payments are:
		| Payment type                 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
		| On-program                   | 450   | 450   | 500   | 500   | ... | 500   | 0     |
		| Completion                   | 0     | 0     | 0     | 0     | ... | 0     | 1500  |
		| Balancing                    | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Employer 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Provider 16-18 incentive     | 0     | 0     | 0     | 500   | ... | 0     | 500   |
		| Framework uplift on-program  | 120   | 120   | 120   | 120   | ... | 120   | 0     |
		| Framework uplift completion  | 0     | 0     | 0     | 0     | ... | 0     | 360   |
		| Framework uplift balancing   | 0     | 0     | 0     | 0     | ... | 0     | 0     |
		| Provider disadvantage uplift | 0     | 0     | 0     | 0     | ..  | 0     | 0     |


