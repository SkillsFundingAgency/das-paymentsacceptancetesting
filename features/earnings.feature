Feature: Provider earnings

  The total cost of training for an apprentice is split between:
  - 80% of the total cost split into equal monthly instalments
  - 20% of the total cost held back until completion

Background:
	Given The learner is normal DAS
	And the agreed price is 15000
	And the apprenticeship funding band maximum is 17000
	And levy balance > agreed price

Scenario: Earnings for a DAS learner, levy available, learner finishes on time
	When an ILR file is submitted with the following data:
		| start date | planned end date | actual end date | completion status |
		| 01/09/2017 | 08/09/2018       | 08/09/2018      | completed         |
	Then the provider earnings break down as follows:
		| Type                  | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 | 
		| Provider Earned       | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     | 
