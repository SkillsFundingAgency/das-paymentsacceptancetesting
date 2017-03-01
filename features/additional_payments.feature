Feature: 16 to 18 learner incentives, framework uplifts, level 2 english or maths payments
  
   Scenario: Payment for a 16-18 DAS learner, levy available, incentives earned
    
    Given levy balance > agreed price for all months
    And the following commitments exist:
          | ULN       | start date | end date   | agreed price | status |
          | learner a | 01/08/2017 | 01/08/2018 | 15000        | active |

    When an ILR file is submitted with the following data:
          | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status |
          | learner a | 16-18 programme only DAS | 15000        | 06/08/2017 | 08/08/2018       |                 | continuing        |
      
    Then the provider earnings and payments break down as follows:
          | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
          | Provider Earned Total               | 1000  | 1000  | 1000  | 2000  | 1000  | ... | 1000  | 0     |
          | Provider Paid by SFA                | 0     | 1000  | 1000  | 1000  | 2000  | ... | 1000  | 1000  |
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


Scenario: Payment for a 16-18 DAS learner, levy available, incentives not paid as data lock fails
    
    Given levy balance > agreed price for all months
    And the following commitments exist:
        | commitment number | ULN       | start date | end date   | agreed price | status |
        | 1                 | learner a | 01/09/2017 | 01/09/2018 | 15000        | active |
    
     When an ILR file is submitted with the following data:
        | ULN       | learner type             | agreed price | start date | planned end date | actual end date | completion status |
        | learner a | 16-18 programme only DAS | 15000        | 28/08/2017 | 29/08/2018       |                 | continuing        |
      
    Then the data lock status will be as follows:
        | type                | 08/17 onwards |   
        | matching commitment |               |   
      
    And the provider earnings and payments break down as follows:
        | Type                                | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
        | Provider Earned Total               | 1000  | 1000  | 1000  | 2000  | 1000  | ... | 1000  | 0     |
        | Provider Paid by SFA                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Levy account debited                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | SFA Levy employer budget            | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | SFA Levy co-funding budget          | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | SFA Levy additional payments budget | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
      

Scenario:582-AC1-Learner finishes on time, earns on-programme and completion payments. Assumes 12 month apprenticeship and learner completes after 10 months.

	When an ILR file is submitted with the following data:
		| ULN    | learner type                 | agreed price | start date | planned end date | actual end date | completion status | Framework Code | Programme Type | Pathway Code |
		| 123456 | 16-18 programme only non-DAS | 9000         | 06/08/2017 | 09/08/2018       | 10/08/2018      | Completed         | 403            | 2              | 1            |

	Then the provider earnings and payments break down as follows:
		| Type                                    | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 06/18 | 07/18 | 08/18 | 09/18 |
		| Provider Earned Total                   | 720   | 720   | 720   | 1720  | 720   | ... | 720   | 720   | 3160  | 0     |
		| Provider Earned from SFA                | 660   | 660   | 660   | 1660  | 660   | ... | 660   | 660   | 2980  | 0     |
		| Provider Earned from Employer           | 60    | 60    | 60    | 60    | 60    | ... | 60    | 60    | 180   | 0     |
		| Provider Paid by SFA                    | 0     | 660   | 660   | 660   | 1660  | ... | 660   | 660   | 660   | 2980  |
		| Payment due from Employer               | 0     | 60    | 60    | 60    | 60    | ... | 60    | 60    | 60    | 180   |
		| Levy account debited                    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
		| SFA Levy employer budget                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
		| SFA Levy co-funding budget              | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
		| SFA non Levy co-funding budget          | 540   | 540   | 540   | 540   | 540   | ... | 540   | 540   | 1620  | 0     |
		| SFA non-Levy additional payments budget | 120   | 120   | 120   | 1120  | 120   | ... | 120   | 120   | 1360  | 0     |

    And the transaction types for the payments are:
		| Payment type                 | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 06/18 | 07/18 | 08/18 | 09/18 |
		| On-program                   | 0     | 540   | 540   | 540   | 540   | ... | 540   | 540   | 540   | 0     |
		| Completion                   | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 1620  |
		| Balancing                    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
		| Employer 16-18 incentive     | 0     | 0     | 0     | 0     | 500   | ... | 0     | 0     | 0     | 500   |
		| Provider 16-18 incentive     | 0     | 0     | 0     | 0     | 500   | ... | 0     | 0     | 0     | 500   |
		| Framework uplift on-program  | 0     | 120   | 120   | 120   | 120   | ... | 120   | 120   | 120   | 0     |
		| Framework uplift completion  | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 360   |
		| Framework uplift balancing   | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
		| Provider disadvantage uplift | 0     | 0     | 0     | 0     | 0     | ..  | 0     | 0     | 0     | 0     |

		
Scenario:582-AC2-Learner finishes on time, Price is less than Funding Band Maximum of £9,000
		
	When an ILR file is submitted with the following data:
		| ULN    | learner type                 | agreed price | start date | planned end date | actual end date | completion status | Framework Code | Programme Type | Pathway Code |
		| 123455 | 16-18 programme only non-DAS | 8250         | 06/08/2017 | 09/08/2018       | 10/08/2018      | Completed         | 403            | 2              | 1            |

	Then the provider earnings and payments break down as follows:
		| Type                                    | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 06/18 | 07/18 | 08/18 | 09/18 |
		| Provider Earned Total                   | 670   | 670   | 670   | 1670  | 670   | ... | 670   | 670   | 3010  | 0     |
		| Provider Earned from SFA                | 615   | 615   | 615   | 1615  | 615   | ... | 615   | 615   | 3845  | 0     |
		| Provider Earned from Employer           | 55    | 55    | 55    | 55    | 55    | ... | 55    | 55    | 165   | 0     |
		| Provider Paid by SFA                    | 0     | 615   | 615   | 615   | 1615  | ... | 615   | 615   | 615   | 2845  |
		| Payment due from Employer               | 0     | 55    | 55    | 55    | 55    | ... | 55    | 55    | 55    | 165   |
		| Levy account debited                    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
		| SFA Levy employer budget                | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
		| SFA Levy co-funding budget              | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
		| SFA non Levy co-funding budget          | 495   | 495   | 495   | 495   | 495   | ... | 495   | 495   | 1485  | 0     |
		| SFA non-Levy additional payments budget | 120   | 120   | 120   | 1120  | 120   | ... | 120   | 120   | 1360  | 0     |

	  And the transaction types for the payments are:
		| Payment type                 | 08/17 | 09/17 | 10/17 | 11/17 | 12/17 | ... | 06/18 | 07/18 | 08/18 | 09/18 |
		| On-program                   | 0     | 495   | 495   | 495   | 495   | ... | 495   | 495   | 495   | 0     |
		| Completion                   | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 1485  |
		| Balancing                    | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
		| Employer 16-18 incentive     | 0     | 0     | 0     | 0     | 500   | ... | 0     | 0     | 0     | 500   |
		| Provider 16-18 incentive     | 0     | 0     | 0     | 0     | 500   | ... | 0     | 0     | 0     | 500   |
		| Framework uplift on-program  | 0     | 120   | 120   | 120   | 120   | ... | 120   | 120   | 120   | 0     |
		| Framework uplift completion  | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 360   |
		| Framework uplift balancing   | 0     | 0     | 0     | 0     | 0     | ... | 0     | 0     | 0     | 0     |
		| Provider disadvantage uplift | 0     | 0     | 0     | 0     | 0     | ..  | 0     | 0     | 0     | 0     |


@MathsAndEnglishNonDas
Scenario: Maths and English payments for a non-das learner finishing on time, funding agreed within band maximum, planned duration is same as programme (assumes both start and finish at same time)
    When an ILR file is submitted with the following data:
        | ULN       | learner type           | agreed price | start date | planned end date | actual end date | completion status | aim type         | aim rate | framework code | programme type | pathway code |
        | learner a | programme only non-DAS | 15000        | 06/08/2017 | 08/08/2018       |                 | continuing        | programme        |          | 403            | 2              | 1            |
        | learner a | programme only non-DAS |              | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | maths or english | 471      | 403            | 2              | 1            |
    Then the provider earnings and payments break down as follows:
        | Type                                    | 08/17   | 09/17   | 10/17   | 11/17   | 12/17   | ... | 07/18   | 08/18  | 09/18 |
        | Provider Earned Total                   | 1039.25 | 1039.25 | 1039.25 | 1039.25 | 1039.25 | ... | 1039.25 | 0      | 0     |
        | Provider Earned from SFA                | 939.25  | 939.25  | 939.25  | 939.25  | 939.25  | ... | 939.25  | 0      | 0     |
        | Provider Earned from Employer           | 100     | 100     | 100     | 100     | 100     | ... | 100     | 0      | 0     |
        | Provider Paid by SFA                    | 0       | 939.25  | 939.25  | 939.25  | 939.25  | ... | 939.25  | 939.25 | 0     |
        | Payment due from Employer               | 0       | 100     | 100     | 100     | 100     | ... | 100     | 100    | 0     |
        | Levy account debited                    | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0      | 0     |
        | SFA Levy employer budget                | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0      | 0     |
        | SFA Levy co-funding budget              | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0      | 0     |
        | SFA non-Levy co-funding budget          | 900     | 900     | 900     | 900     | 900     | ... | 900     | 0      | 0     |
        | SFA non-Levy additional payments budget | 39.25   | 39.25   | 39.25   | 39.25   | 39.25   | ... | 39.25   | 0      | 0     |
    And the transaction types for the payments are:
        | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
        | On-program                     | 900   | 900   | 900   | 900   | ... | 900   | 0     |
        | Completion                     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Balancing                      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Employer 16-18 incentive       | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Provider 16-18 incentive       | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | English and maths on programme | 39.25 | 39.25 | 39.25 | 39.25 | ... | 39.25 | 0     |
        | English and maths Balancing    | 0     | 0     | 0     | 0     | ... | 0     | 0     |


@MathsAndEnglishDas
Scenario: Maths and English payments for a das learner finishing on time, funding agreed within band maximum, planned duration is same as programme (assumes both start and finish at same time)
    Given levy balance > agreed price for all months
    And the following commitments exist:
        | commitment Id | ULN       | start date | end date   | agreed price | framework code | programme type | pathway code | status | effective from | effective to |
        | 1             | learner a | 01/08/2017 | 01/08/2018 | 15000        | 403            | 2              | 1            | active | 01/08/2017     |              |
    When an ILR file is submitted with the following data:
        | ULN       | learner type       | agreed price | start date | planned end date | actual end date | completion status | aim type         | aim rate | framework code | programme type | pathway code |
        | learner a | programme only DAS | 15000        | 06/08/2017 | 08/08/2018       |                 | continuing        | programme        |          | 403            | 2              | 1            |
        | learner a | programme only DAS |              | 06/08/2017 | 08/08/2018       | 08/08/2018      | completed         | maths or english | 471      | 403            | 2              | 1            |
    Then the provider earnings and payments break down as follows:
        | Type                                | 08/17   | 09/17   | 10/17   | 11/17   | 12/17   | ... | 07/18   | 08/18   | 09/18 |
        | Provider Earned Total               | 1039.25 | 1039.25 | 1039.25 | 1039.25 | 1039.25 | ... | 1039.25 | 0       | 0     |
        | Provider Earned from SFA            | 1039.25 | 1039.25 | 1039.25 | 1039.25 | 1039.25 | ... | 1039.25 | 0       | 0     |
        | Provider Earned from Employer       | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0       | 0     |
        | Provider Paid by SFA                | 0       | 1039.25 | 1039.25 | 1039.25 | 1039.25 | ... | 1039.25 | 1039.25 | 0     |
        | Payment due from Employer           | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0       | 0     |
        | Levy account debited                | 0       | 1000    | 1000    | 1000    | 1000    | ... | 1000    | 1000    | 0     |
        | SFA Levy employer budget            | 1000    | 1000    | 1000    | 1000    | 1000    | ... | 1000    | 0       | 0     |
        | SFA Levy co-funding budget          | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0       | 0     |
        | SFA Levy additional payments budget | 39.25   | 39.25   | 39.25   | 39.25   | 39.25   | ... | 39.25   | 0       | 0     |
        | SFA non-Levy co-funding budget      | 0       | 0       | 0       | 0       | 0       | ... | 0       | 0       | 0     |
    And the transaction types for the payments are:
        | Payment type                   | 09/17 | 10/17 | 11/17 | 12/17 | ... | 08/18 | 09/18 |
        | On-program                     | 1000  | 1000  | 1000  | 1000  | ... | 1000  | 0     |
        | Completion                     | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Balancing                      | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Employer 16-18 incentive       | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | Provider 16-18 incentive       | 0     | 0     | 0     | 0     | ... | 0     | 0     |
        | English and maths on programme | 39.25 | 39.25 | 39.25 | 39.25 | ... | 39.25 | 0     |
        | English and maths Balancing    | 0     | 0     | 0     | 0     | ... | 0     | 0     |