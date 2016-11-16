Feature: Amount due is calculated based on previously earned amount

    Scenario: Provider has no previous earnings
        Given Provider has previously earned 0 in the period 
        When an earning of 1000 is calculated for the period 
        Then a payment of 1000 is due
          
	Scenario: Provider has previously earned more than  0 
        Given Provider has previously earned 500 in the period 
        When an earning of 1000 is calculated for the period
        Then a payment of 500 is due
          
	Scenario: Provider has earned same amount as amount due
        Given Provider has previously earned 1000 in the period
        When an earning of 1000 is calculated for the period
        Then a payment of 0 is due
      
	Scenario: Provider has not earned any amount and none is due
        Given Provider has previously earned 0 in the period
        When an earning of 0 is calculated for the period
        Then a payment of 0 is due