@ChangeInCircumstances
Feature: chanegs in aim sequence or ULN

@AimSequenceOrUlnChange
Scenario:822-AC01- Levy apprentice, provider changes aim sequence numbers in ILR after payments have already occurred

        Given The learner is programme only DAS
        And levy balance > agreed price for all months
        And the apprenticeship funding band maximum is 9000

        And the following commitments exist:
			| commitment Id | version Id | ULN       | start date | end date   | framework code | programme type | pathway code | agreed price | status    | effective from | effective to |
			| 1             | 1          | learner a | 01/05/2017 | 01/05/2018 | 403            | 2              | 1            | 9000         | Active    | 01/05/2017     |              |
        
		And following learning has been recorded for previous payments:
			| ULN       | start date | aim sequence number | framework code | programme type | pathway code | completion status |
			| learner a | 06/05/2017 | 1                   | 403            | 2              | 1            | continuing        |
  
		And the following earnings and payments have been made to the provider A for learner a:
			| Type                                | 05/17 | 06/17 | 07/17 | 08/17 |
			| Provider Earned Total               | 600   | 600   | 0     | 0     |
			| Provider Earned from SFA            | 600   | 600   | 0     | 0     |
			| Provider Earned from Employer       | 0     | 0     | 0     | 0     |
			| Provider Paid by SFA                | 0     | 600   | 600   | 0     |
			| Payment due from Employer           | 0     | 0     | 0     | 0     |
			| Levy account debited                | 0     | 600   | 600   | 0     |
			| SFA Levy employer budget            | 600   | 600   | 0     | 0     |
			| SFA Levy co-funding budget          | 0     | 0     | 0     | 0     |
			| SFA Levy additional payments budget | 0     | 0     | 0     | 0     |
			| SFA non-Levy co-funding budget      | 0     | 0     | 0     | 0     | 
        
        When an ILR file is submitted for the first time on 31/07/17 with the following data:
			| ULN       | learner type       | agreed price | start date | planned end date | actual end date | completion status | aim type         | aim sequence number | aim rate | framework code | programme type | pathway code |
			| learner a | programme only DAS | 9000         | 06/05/2017 | 20/05/2018       |                 | continuing        | programme        | 2                   |          | 403            | 2              | 1            |
			| learner a | programme only DAS |              | 06/05/2017 | 20/05/2018       |                 | continuing        | maths or english | 1                   | 471      | 403            | 2              | 1            |
  
        Then the provider earnings and payments break down as follows:
			| Type                                    | 05/17  | 06/17  | 07/17  | 08/17  |
			| Provider Earned Total                   | 639.25 | 639.25 | 639.25 | 639.25 |
			| Provider Earned from SFA                | 639.25 | 639.25 | 639.25 | 0      |
			| Provider Earned from Employer           | 0      | 0      | 0      | 0      |
			| Provider Paid by SFA                    | 0      | 600    | 600    | 717.75 |
			| Refund taken by SFA                     | 0      | 0      | 0      | 0      |
			| Payment due from Employer               | 0      | 0      | 0      | 0      |
			| Refund due to employer                  | 0      | 0      | 0      | 0      |
			| Levy account debited                    | 0      | 600    | 600    | 600    |
			| Levy account credited                   | 0      | 0      | 0      | 0      |
			| SFA Levy employer budget                | 600    | 600    | 600    | 600    |
			| SFA Levy co-funding budget              | 0      | 0      | 0      | 0      |
			| SFA Levy additional payments budget     | 39.25  | 39.25  | 39.25  | 39.25  |
			| SFA non-Levy co-funding budget          | 0      | 0      | 0      | 0      |
			| SFA non-Levy additional payments budget | 0      | 0      | 0      | 0      |        
        

@AimSequenceOrUlnChange
 Scenario:822-AC02 Non-Levy apprentice, provider changes aim sequence numbers in ILR after payments have already occurred

        Given the apprenticeship funding band maximum is 9000
 		And following learning has been recorded for previous payments:
			| ULN       | start date | aim sequence number | framework code | programme type | pathway code | completion status |
			| learner a | 06/05/2017 | 1                   | 403            | 2              | 1            | continuing        |
  
		And the following earnings and payments have been made to the provider A for learner a:
			| Type                                    | 05/17 | 06/17 | 07/17 | 08/17 |
			| Provider Earned Total                   | 600   | 600   | 0     | 0     |
			| Provider Earned from SFA                | 540   | 540   | 0     | 0     |
			| Provider Earned from Employer           | 60    | 60    | 0     | 0     |
			| Provider Paid by SFA                    | 0     | 540   | 540   | 0     |
			| Payment due from Employer               | 0     | 60    | 60    | 0     |
			| Levy account debited                    | 0     | 0     | 0     | 0     |
			| SFA Levy employer budget                | 0     | 0     | 0     | 0     |
			| SFA Levy co-funding budget              | 0     | 0     | 0     | 0     |
			| SFA Levy additional payments budget     | 0     | 0     | 0     | 0     |
			| SFA non-Levy co-funding budget          | 540   | 540   | 0     | 0     |
			| SFA non-Levy additional payments budget | 0     | 0     | 0     | 0     |         
        
        When an ILR file is submitted for the first time on 31/07/17 with the following data:
			| ULN       | learner type           | agreed price | start date | planned end date | actual end date | completion status | aim type         | aim sequence number | aim rate | framework code | programme type | pathway code |
			| learner a | programme only non-DAS | 9000         | 06/05/2017 | 20/05/2018       |                 | continuing        | programme        | 2                   |          | 403            | 2              | 1            |
			| learner a | programme only non-DAS |              | 06/05/2017 | 20/05/2018       |                 | continuing        | maths or english | 1                   | 471      | 403            | 2              | 1            |
  	
        Then the provider earnings and payments break down as follows:
			| Type                                    | 05/17  | 06/17  | 07/17  | 08/17  |
			| Provider Earned Total                   | 639.25 | 639.25 | 639.25 | 639.25 |
			| Provider Earned from SFA                | 579.25 | 579.25 | 579.25 | 0      |
			| Provider Earned from Employer           | 60     | 60     | 60     | 0      |
			| Provider Paid by SFA                    | 0      | 540    | 540    | 657.75 |
			| Refund taken by SFA                     | 0      | 0      | 0      | 0      |
			| Payment due from Employer               | 0      | 60     | 60     | 60     |
			| Refund due to employer                  | 0      | 0      | 0      | 0      |
			| Levy account debited                    | 0      | 0      | 0      | 0      |
			| Levy account credited                   | 0      | 0      | 0      | 0      |
			| SFA Levy employer budget                | 0      | 0      | 0      | 0      |
			| SFA Levy co-funding budget              | 0      | 0      | 0      | 0      |
			| SFA Levy additional payments budget     | 0      | 0      | 0      | 0      |
			| SFA non-Levy co-funding budget          | 540    | 540    | 540    | 540    |
			| SFA non-Levy additional payments budget | 39.25  | 39.25  | 39.25  | 39.25  | 