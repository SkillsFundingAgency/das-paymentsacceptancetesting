Feature: Datalock validation fails for different reasons

Scenario: When no matching record found in an employer digital account for the UKPRN then datalock DLOCK_01 will be produced

    Given the following commitment exists for an apprentice:

        | UKPRN   | ULN  | standard code | agreed price | start date |
        | 9999999 | 1234 | 1             | 10000        | 01/05/2017 |
      
    When and ILR is submitted with the following data:
        
        | UKPRN   | ULN  | standard code | agreed price | start date |
        | 1000000 | 1234 | 1             | 10000        | 01/05/2017 |
      
    Then a datalock error DLOCK_01 is produced

Scenario: When no matching record found in an employer digital account for the ULN then datalock DLOCK_02 will be produced

    Given the following commitment exists for an apprentice:

        | UKPRN   | ULN  | standard code | agreed price | start date |
        | 9999999 | 1234 | 1             | 10000        | 01/05/2017 |
      
    When and ILR is submitted with the following data:
        
        | UKPRN   | ULN  | standard code | agreed price | start date |
        | 9999999 | 3456 | 1             | 10000        | 01/05/2017 |
      
    Then a datalock error DLOCK_02 is produced

Scenario: When no matching record found in an employer digital account for the standard code then datalock DLOCK_03 will be produced

    Given the following commitment exists for an apprentice:

        | UKPRN   | ULN  | standard code | agreed price | start date |
        | 9999999 | 1234 | 1             | 10000        | 01/05/2017 |
      
    When and ILR is submitted with the following data:
        
        | UKPRN   | ULN  | standard code | agreed price | start date |
        | 9999999 | 1234 | 2             | 10000        | 01/05/2017 |
      
    Then a datalock error DLOCK_03 is produced


	
Scenario: When no matching record found in an employer digital account for the framework code then datalock DLOCK_04 will be produced

    Given the following commitment exists for an apprentice:

        | UKPRN   | ULN  | framework code | programme type | pathway code | agreed price | start date |
        | 9999999 | 1234 | 450            | 2              | 1            | 10000        | 01/05/2017 |
      
    When and ILR is submitted with the following data:
        
        | UKPRN   | ULN  | framework code | programme type | pathway code | agreed price | start date |
        | 9999999 | 1234 | 451            | 2              | 1            | 10000        | 01/05/2017 |
		
    Then a datalock error DLOCK_04 is produced


	Scenario: When no matching record found in an employer digital account for the programme type then datalock DLOCK_05 will be produced

    Given the following commitment exists for an apprentice:

        | UKPRN   | ULN  | framework code | programme type | pathway code | agreed price | start date |
        | 9999999 | 1234 | 450            | 2              | 1            | 10000        | 01/05/2017 |
      
    When and ILR is submitted with the following data:
        
        | UKPRN   | ULN  | framework code | programme type | pathway code | agreed price | start date |
        | 9999999 | 1234 | 450            | 3              | 1            | 10000        | 01/05/2017 |
      
    Then a datalock error DLOCK_05 is produced

	Scenario: When no matching record found in an employer digital account for the pathway code then datalock DLOCK_06 will be produced

    Given the following commitment exists for an apprentice:

        | UKPRN   | ULN  | framework code | programme type | pathway code | agreed price | start date |
        | 9999999 | 1234 | 450            | 2              | 1            | 10000        | 01/05/2017 |
      
    When and ILR is submitted with the following data:
        
        | UKPRN   | ULN  | framework code | programme type | pathway code | agreed price | start date |
        | 9999999 | 1234 | 450            | 2              | 2            | 10000        | 01/05/2017 |
      
    Then a datalock error DLOCK_06 is produced
  

Scenario: When no matching record found in an employer digital account for for the agreed price then datalock DLOCK_07 will be produced

    Given the following commitment exists for an apprentice:

        | UKPRN   | ULN  | framework code | programme type | pathway code | agreed price | start date |
        | 9999999 | 1234 | 450            | 2              | 1            | 10000        | 01/05/2017 |
      
    When and ILR is submitted with the following data:
        
        | UKPRN   | ULN  | framework code | programme type | pathway code | agreed price | start date |
        | 9999999 | 1234 | 450            | 2              | 1            | 10001        | 01/05/2017 |
      
    Then a datalock error DLOCK_07 is produced


	
Scenario: When there is more than one matching commitment in the employer digital account then datalock DLOCK_08 will be produced

    Given the following commitments exist for an apprentice:

        | UKPRN   | ULN  | framework code | programme type | pathway code | agreed price | start date |
        | 9999999 | 1234 | 450            | 2              | 1            | 10000        | 01/05/2017 |
        | 9999999 | 1234 | 450            | 2              | 1            | 10000        | 01/05/2017 |

    When and ILR is submitted with the following data:
        
        | UKPRN   | ULN  | framework code | programme type | pathway code | agreed price | start date |
        | 9999999 | 1234 | 450            | 2              | 1            | 10000        | 01/05/2017 |
      
    Then a datalock error DLOCK_08 is produced

	

Scenario: When the start month recorded in the employer digital account is after the start month in the ILR then datalock DLOCK_09 will be produced

    Given the following commitment exists for an apprentice:

        | UKPRN   | ULN  | framework code | programme type | pathway code | agreed price | start date |
        | 9999999 | 1234 | 450            | 2              | 1            | 10000        | 01/05/2017 |
      
    When and ILR is submitted with the following data:
        
        | UKPRN   | ULN  | framework code | programme type | pathway code | agreed price | start date |
        | 9999999 | 1234 | 450            | 2              | 1            | 10000        | 30/04/2017 |
      
    Then a datalock error DLOCK_09 is produced
  