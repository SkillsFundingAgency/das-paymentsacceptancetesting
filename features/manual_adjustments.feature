@ManualAdjustments
Feature: Manual adjustments as a result of payment anomolies
  
  Scenario:893-AC01 Levy apprentice, deleted learner, transaction added for reversal

		Given The learner is programme only DAS
        And levy balance > agreed price for all months
        And the apprenticeship funding band maximum is 9000

        And the following commitments exist:
			| commitment Id | version Id | ULN       | start date | end date   | framework code | programme type | pathway code | agreed price | status    | effective from | effective to |
			| 1             | 1          | learner a | 01/05/2017 | 01/05/2018 | 403            | 2              | 1            | 9000         | Active    | 01/05/2017     |              |
        
        And following learning has been recorded for previous payments:
            | ULN       | start date | aim sequence number | aim type         | aim reference | framework code | programme type | pathway code | completion status |
            | learner a | 06/05/2017 | 1                   | programme        | ZPROG001      | 403            | 2              | 1            | continuing        |
  
        And the following programme earnings and payments have been made to the provider A for learner a:
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

		And the following payments will be added for reversal:
			| Payment type                 | 05/17 | 06/17 |
			| On-program                   | 600   | 0   |
			| Completion                   | 0     | 0     |
			| Balancing                    | 0     | 0     |
			| Employer 16-18 incentive     | 0     | 0     |
			| Provider 16-18 incentive     | 0     | 0     |
			| Framework uplift on-program  | 0     | 0     |
			| Framework uplift completion  | 0     | 0     |
			| Framework uplift balancing   | 0     | 0     |
			| Provider disadvantage uplift | 0     | 0     |
		
        # ILR is submitted but its for a different learner, behaving like learner is removed from ilr 
        When an ILR file is submitted for the first time on 31/07/17 with the following data:
            | ULN       | learner type       | aim sequence number | aim type         | aim reference | aim rate | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code |
            | learner b | programme only DAS | 1                   | programme        | ZPROG001      |          | 9000         | 06/05/2017 | 20/05/2018       |                 | continuing        | 401            | 2              | 1            |
  
        Then the provider earnings and payments break down as follows:
            | Type                                    | 05/17 | 06/17 | 07/17 | 08/17 | 09/17 | 10/17 |
            | Provider Earned Total                   | 600   | 600   | 600   | 600   | 600   | 600   |
            | Provider Earned from SFA                | 600   | 600   | 600   | 600   | 600   | 600   |
            | Provider Earned from Employer           | 0     | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA                    | 0     | 600   | 600   | 0     | 0     | 0     |
            | Refund taken by SFA                     | 0     | 0     | 0     | -1200 | 0     | 0     |
            | Payment due from Employer               | 0     | 0     | 0     | 0     | 0     | 0     |
            | Refund due to employer                  | 0     | 0     | 0     | 0     | 0     | 0     |
            | Levy account debited                    | 0     | 600   | 600   | 0     | 0     | 0     |
            | Levy account credited                   | 0     | 0     | 0     | 1200  | 0     | 0     |
            | SFA Levy employer budget                | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy co-funding budget              | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy additional payments budget     | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget          | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy additional payments budget | 0     | 0     | 0     | 0     | 0     | 0     |   


	Scenario:893-AC02 non levy apprentice, changes to levy after payments are made in previous months, non levy will be all reversed and new paymenst will be made for levy

		Given The learner is programme only DAS
        And levy balance > agreed price for all months
        And the apprenticeship funding band maximum is 9000

        And the following commitments exist:
			| commitment Id | version Id | ULN       | start date | end date   | framework code | programme type | pathway code | agreed price | status    | effective from | effective to |
			| 1             | 1          | learner a | 01/05/2017 | 01/05/2018 | 403            | 2              | 1            | 9000         | Active    | 01/05/2017     |              |
        
        And following learning has been recorded for previous payments:
            | ULN       | learner type           | start date | aim sequence number | aim type  | aim reference | framework code | programme type | pathway code | completion status |
            | learner a | programme only non-DAS | 06/05/2017 | 1                   | programme | ZPROG001      | 403            | 2              | 1            | continuing        |
  
        And the following programme earnings and payments have been made to the provider A for learner a:
            | Type                                | 05/17 | 06/17 | 07/17 | 08/17 |
            | Provider Earned Total               | 600   | 600   | 0     | 0     |
            | Provider Earned from SFA            | 540   | 540   | 0     | 0     |
            | Provider Earned from Employer       | 60    | 60    | 0     | 0     |
            | Provider Paid by SFA                | 0     | 540   | 540   | 0     |
            | Payment due from Employer           | 0     | 60    | 60    | 0     |
            | Levy account debited                | 0     | 0     | 0     | 0     |
            | SFA Levy employer budget            | 0     | 0     | 0     | 0     |
            | SFA Levy co-funding budget          | 0     | 0     | 0     | 0     |
            | SFA Levy additional payments budget | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget      | 540   | 540   | 0     | 0     | 

		And the following payments will be added for reversal:
			| Payment type                 | 05/17 | 06/17 |
			| On-program                   | 600   | 600   |
			| Completion                   | 0     | 0     |
			| Balancing                    | 0     | 0     |
			| Employer 16-18 incentive     | 0     | 0     |
			| Provider 16-18 incentive     | 0     | 0     |
			| Framework uplift on-program  | 0     | 0     |
			| Framework uplift completion  | 0     | 0     |
			| Framework uplift balancing   | 0     | 0     |
			| Provider disadvantage uplift | 0     | 0     |
		
        When an ILR file is submitted for the first time on 31/07/17 with the following data:
            | ULN       | learner type       | aim sequence number | aim type         | aim reference | aim rate | agreed price | start date | planned end date | actual end date | completion status | framework code | programme type | pathway code |
            | learner a | programme only DAS | 1                   | programme        | ZPROG001      |          | 9000         | 06/05/2017 | 20/05/2018       |                 | continuing        | 403            | 2              | 1            |
  
        Then the provider earnings and payments break down as follows:
            | Type                                    | 05/17 | 06/17 | 07/17 | 08/17 | 09/17 | 10/17 |
            | Provider Earned Total                   | 600   | 600   | 600   | 600   | 600   | 600   |
            | Provider Earned from SFA                | 600   | 600   | 600   | 600   | 600   | 600   |
            | Provider Earned from Employer           | 0     | 0     | 0     | 0     | 0     | 0     |
            | Provider Paid by SFA                    | 0     | 540   | 540   | 0     | 0     | 0     |
            | Refund taken by SFA                     | 0     | 0     | 0     | -1200 | 0     | 0     |
            | Payment due from Employer               | 0     | 60    | 60    | 0     | 0     | 0     |
            | Refund due to employer                  | 0     | 0     | 0     | 0     | 0     | 0     |
            | Levy account debited                    | 0     | 600   | 600   | 0     | 0     | 0     |
            | Levy account credited                   | 0     | 0     | 0     | 1200  | 0     | 0     |
            | SFA Levy employer budget                | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy co-funding budget              | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA Levy additional payments budget     | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy co-funding budget          | 0     | 0     | 0     | 0     | 0     | 0     |
            | SFA non-Levy additional payments budget | 0     | 0     | 0     | 0     | 0     | 0     |   
