Feature: Provider payments

Background:
	Given The learner is normal DAS
	And the agreed price is 15000
	And the apprenticeship funding band maximum is 17000
	And levy balance > agreed price

Scenario: Payments for a DAS learner, levy available, learner finishes on time
	Given the following earnings:
	  | Type                       | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 | 
	  | Provider Earned            | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     | 
	When the ILR period closes for each period
	Then the provider payments break down as follows:
	  | Type                       | 09/17 | 10/17 | 11/17 | ... | 08/18 | 09/18 | 10/18 | 
	  | Provider Paid              | 0     | 1000  | 1000  | ... | 1000  | 1000  | 3000  | 
	  | Levy account debited       | 0     | 1000  | 1000  | ... | 1000  | 1000  | 3000  | 
	  | SFA Levy budget            | 1000  | 1000  | 1000  | ... | 1000  | 3000  | 0     |
	  | SFA Levy co-funding budget | 0     | 0     | 0     | ... | 0     | 0     | 0     |