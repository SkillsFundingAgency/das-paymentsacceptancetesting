@CommitmentsEffectiveDate
Feature: Commitment effective dates apply correctly in data collections processing

    Background:
        Given the apprenticeship funding band maximum for each learner is 17000
        And levy balance > agreed price for all months

	Scenario: Change in price month 2
		Given the following commitments exist:
			| commitment Id | version Id | Employer   | Provider   | ULN       | start date | end date   | agreed price | effective from | effective to |
			| 1             | 1          | employer 1 | provider a | learner a | 01/05/2017 | 01/05/2018 | 7500        | 01/05/2017      | 31/05/2017   |
			| 1             | 2          | employer 1 | provider a | learner a | 01/01/2017 | 01/05/2018 | 15000       | 01/06/2017      |              |

		When an ILR file is submitted with the following data:
			| ULN       | learner type       | start date | planned end date | completion status | Total training price 1 | Total training price 1 effective date | Total training price 2 | Total training price 2 effective date |
			| learner a | programme only DAS | 12/05/2017 | 20/05/2018       | continuing        | 7500                   | 01/05/2017                            | 15000                  | 01/06/2017                            |

		Then the data lock status will be as follows:
            | Payment type | 05/17           | 06/17           | 07/17           |
            | On-program   | commitment 1 v1 | commitment 1 v2 | commitment 1 v2 |
        And the provider earnings and payments break down as follows:
            | Type                          | 05/17 | 06/17   | 07/17   |
            | Provider Earned Total         | 500   | 1045.45 | 1045.45 |
            | Provider Earned from SFA      | 500   | 1045.45 | 1045.45 |
            | Provider Paid by SFA          | 0     | 500     | 1045.45 |
            | Levy account debited          | 0     | 500     | 1045.45 |
            | SFA Levy employer budget      | 500   | 1045.45 | 1045.45 |
            | SFA Levy co-funding budget    | 0     | 0       | 0       |

	

	Scenario: Change in price month 2 with priority change after
		Given the following commitments exist:
			| commitment Id | version Id | priority  | Employer   | Provider   | ULN       | start date | end date   | agreed price | effective from | effective to |
			| 1             | 1          | 1         | employer 1 | provider a | learner a | 01/05/2017 | 01/05/2018 | 7500         | 01/05/2017     | 31/05/2017   |
			| 1             | 2          | 1         | employer 1 | provider a | learner a | 01/05/2017 | 01/05/2018 | 15000        | 01/06/2017     | 13/07/2017   |
			| 1             | 3          | 2         | employer 1 | provider a | learner a | 01/05/2017 | 01/05/2018 | 15000        | 14/07/2017     |              |

		When an ILR file is submitted with the following data:
			| ULN       | learner type       | start date | planned end date | completion status | Total training price 1 | Total training price 1 effective date | Total training price 2 | Total training price 2 effective date |
			| learner a | programme only DAS | 12/05/2017 | 20/05/2018       | continuing        | 7500                   | 01/05/2017                            | 15000                  | 01/06/2017                            |

		Then the data lock status will be as follows:
            | Payment type | 05/17           | 06/17           | 07/17           |
            | On-program   | commitment 1 v1 | commitment 1 v2 | commitment 1 v3 |
        Then the provider earnings and payments break down as follows:
            | Type                          | 05/17 | 06/17   | 07/17   |
            | Provider Earned Total         | 500   | 1045.45 | 1045.45 |
            | Provider Earned from SFA      | 500   | 1045.45 | 1045.45 |
            | Provider Paid by SFA          | 0     | 500     | 1045.45 |
            | Levy account debited          | 0     | 500     | 1045.45 |
            | SFA Levy employer budget      | 500   | 1045.45 | 1045.45 |
            | SFA Levy co-funding budget    | 0     | 0       | 0       |