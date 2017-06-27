@DataLockErrors
Feature: Datalock produces correct errors when ILR does not match commitment

Scenario: DLOCK07 - When no matching record found in an employer digital account for for the agreed price then datalock DLOCK_07 will be produced
    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   | status | effective from |
        | 73            | 73-125     | Provider a | learner a | 450            | 2              | 1            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
    When an ILR file is submitted with the following data:  
        | Provider   | ULN       | framework code | programme type | pathway code | start date | planned end date | completion status | Total training price | Total training price effective date |
        | Provider a | learner a | 450            | 2              | 1            | 01/05/2017 | 08/08/2018       | continuing        | 10010                | 01/05/2017                          |
    
    Then the following data lock event is returned:
        | Price Episode identifier  | Apprenticeship Id | ULN       | ILR Start Date | ILR Training Price |
        | 2-450-1-01/05/2017        | 73                | learner a | 01/05/2017     | 10010              |
    And the data lock event has the following errors:    
        | Price Episode identifier  | Error code | Error Description										                                    |
        | 2-450-1-01/05/2017        | DLOCK_07   | No matching record found in the employer digital account for the negotiated cost of training	|
    And the data lock event has the following periods    
        | Price Episode identifier | Period   | Payable Flag | Transaction Type |
        | 2-450-1-01/05/2017       | 1617-R10 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R11 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R12 | false        | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | framework code | programme type | pathway code | Negotiated Price | Effective Date |
        | 2-450-1-01/05/2017       | 73-125             | 01/05/2017 | 450            | 2              | 1            | 10000            | 01/05/2017     |



Scenario: DLOCK09 - When no matching record found in an employer digital account for for the start date then datalock DLOCK_09 will be produced
    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | standard code | agreed price | start date | end date   | status | effective from |
        | 73            | 73-125     | Provider a | learner a | 23            | 10000        | 01/06/2017 | 01/05/2018 | active | 01/06/2017     |
        
    When an ILR file is submitted with the following data:  
        | Provider   | ULN       | standard code | start date | planned end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date |
        | Provider a | learner a | 23            | 01/05/2017 | 08/08/2018       | continuing        | 9000                 | 01/05/2017                          | 1000                   | 01/05/2017                            |
    
    Then the following data lock event is returned:
        | Price Episode identifier  | Apprenticeship Id | ULN       | ILR Start Date | ILR Training Price | ILR End point assessment price  | 
        | 25-23-01/05/2017          | 73                | learner a | 01/05/2017     | 9000               | 1000                            |
    And the data lock event has the following errors:    
        | Price Episode identifier  | Error code | Error Description										                                                             |
        | 25-23-01/05/2017          | DLOCK_09   | The start date for this negotiated price is before the corresponding price start date in the employer digital account |
    And the data lock event has the following periods    
        | Price Episode identifier | Period   | Payable Flag | Transaction Type |
        | 25-23-01/05/2017         | 1617-R11 | false        | Learning         |
        | 25-23-01/05/2017         | 1617-R12 | false        | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | standard code | Negotiated Price | Effective Date |
        | 25-23-01/05/2017         | 73-125             | 01/06/2017 | 23            | 10000            | 01/06/2017     |



Scenario: DLOCK03 - When no matching record found in an employer digital account for for the standard code then datalock DLOCK_03 will be produced
    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | standard code | agreed price | start date | end date   | status | effective from |
        | 73            | 73-125     | Provider a | learner a | 21            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
    When an ILR file is submitted with the following data:  
        | Provider   | ULN       | standard code | start date | planned end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date |
        | Provider a | learner a | 23            | 01/05/2017 | 08/08/2018       | continuing        | 9000                 | 01/05/2017                          | 1000                   | 01/05/2017                            |
    
    Then the following data lock event is returned:
        | Price Episode identifier  | Apprenticeship Id | ULN       | ILR Start Date | ILR Training Price | ILR End point assessment price  | 
        | 25-23-01/05/2017          | 73                | learner a | 01/05/2017     | 9000               | 1000                            |
    And the data lock event has the following errors:    
        | Price Episode identifier  | Error code | Error Description										                      |
        | 25-23-01/05/2017          | DLOCK_03   | No matching record found in the employer digital account for the standard code |
    And the data lock event has the following periods    
        | Price Episode identifier | Period   | Payable Flag | Transaction Type |
        | 25-23-01/05/2017         | 1617-R10 | false        | Learning         |
        | 25-23-01/05/2017         | 1617-R11 | false        | Learning         |
        | 25-23-01/05/2017         | 1617-R12 | false        | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | standard code | Negotiated Price | Effective Date |
        | 25-23-01/05/2017         | 73-125             | 01/05/2017 | 21            | 10000            | 01/05/2017     |


Scenario: DLOCK04 - When no matching record found in an employer digital account for for the framework code then datalock DLOCK_04 will be produced
    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   | status | effective from |
        | 73            | 73-125        | Provider a | learner a | 451            | 2              | 1            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
    When an ILR file is submitted with the following data:  
        | Provider   | ULN       | framework code | programme type | pathway code | start date | planned end date | completion status | Total training price | Total training price effective date |
        | Provider a | learner a | 450            | 2              | 1            | 01/05/2017 | 08/08/2018       | continuing        | 10000                | 01/05/2017                          |
    
    Then the following data lock event is returned:
        | Price Episode identifier  | Apprenticeship Id | ULN       | ILR Start Date | ILR Training Price | 
        | 2-450-1-01/05/2017        | 73                | learner a | 01/05/2017     | 10000              |
    And the data lock event has the following errors:    
        | Price Episode identifier  | Error code | Error Description										                       |
        | 2-450-1-01/05/2017        | DLOCK_04   | No matching record found in the employer digital account for the framework code |
    And the data lock event has the following periods    
        | Price Episode identifier | Period   | Payable Flag | Transaction Type |
        | 2-450-1-01/05/2017       | 1617-R10 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R11 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R12 | false        | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | framework code | programme type | pathway code | Negotiated Price | Effective Date |
        | 2-450-1-01/05/2017       | 73-125             | 01/05/2017 | 451            | 2              | 1            | 10000            | 01/05/2017     |


Scenario: DLOCK05 - When no matching record found in an employer digital account for for the programme type then datalock DLOCK_05 will be produced
    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   | status | effective from |
        | 73            | 73-125     | Provider a | learner a | 450            | 3              | 1            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
    When an ILR file is submitted with the following data:  
        | Provider   | ULN       | framework code | programme type | pathway code | start date | planned end date | completion status | Total training price | Total training price effective date |
        | Provider a | learner a | 450            | 2              | 1            | 01/05/2017 | 08/08/2018       | continuing        | 10000                | 01/05/2017                          |
    
    Then the following data lock event is returned:
        | Price Episode identifier  | Apprenticeship Id | ULN       | ILR Start Date | ILR Training Price | 
        | 2-450-1-01/05/2017        | 73                | learner a | 01/05/2017     | 10000              |
    And the data lock event has the following errors:    
        | Price Episode identifier  | Error code | Error Description										                       |
        | 2-450-1-01/05/2017        | DLOCK_05   | No matching record found in the employer digital account for the programme type |
    And the data lock event has the following periods    
        | Price Episode identifier | Period   | Payable Flag | Transaction Type |
        | 2-450-1-01/05/2017       | 1617-R10 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R11 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R12 | false        | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | framework code | programme type | pathway code | Negotiated Price | Effective Date |
        | 2-450-1-01/05/2017       | 73-125             | 01/05/2017 | 450            | 3              | 1            | 10000            | 01/05/2017     |


Scenario: DLOCK06 - When no matching record found in an employer digital account for for the pathway code then datalock DLOCK_06 will be produced
    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   | status | effective from |
        | 73            | 73-125        | Provider a | learner a | 450            | 2              | 6            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
    When an ILR file is submitted with the following data:  
        | Provider   | ULN       | framework code | programme type | pathway code | start date | planned end date | completion status | Total training price | Total training price effective date |
        | Provider a | learner a | 450            | 2              | 1            | 01/05/2017 | 08/08/2018       | continuing        | 10000                | 01/05/2017                          |
    
    Then the following data lock event is returned:
        | Price Episode identifier  | Apprenticeship Id | ULN       | ILR Start Date | ILR Training Price | 
        | 2-450-1-01/05/2017        | 73                | learner a | 01/05/2017     | 10000              |
    And the data lock event has the following errors:    
        | Price Episode identifier  | Error code | Error Description										                       |
        | 2-450-1-01/05/2017        | DLOCK_06   | No matching record found in the employer digital account for the pathway code   |
    And the data lock event has the following periods    
        | Price Episode identifier | Period   | Payable Flag | Transaction Type |
        | 2-450-1-01/05/2017       | 1617-R10 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R11 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R12 | false        | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | framework code | programme type | pathway code | Negotiated Price | Effective Date |
        | 2-450-1-01/05/2017       | 73-125             | 01/05/2017 | 450            | 2              | 6            | 10000            | 01/05/2017     |




Scenario: DLOCK04 + DLOCK05 - When no matching record found in an employer digital account for for the framework code and programme type then datalock DLOCK_04 and DLOCK05 will be produced
    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   | status | effective from |
        | 73            | 73-125     | Provider a | learner a | 451            | 3              | 1            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
    When an ILR file is submitted with the following data:  
        | Provider   | ULN       | framework code | programme type | pathway code | start date | planned end date | completion status | Total training price | Total training price effective date |
        | Provider a | learner a | 450            | 2              | 1            | 01/05/2017 | 08/08/2018       | continuing        | 10000                | 01/05/2017                          |
    
    Then the following data lock event is returned:
        | Price Episode identifier  | Apprenticeship Id | ULN       | ILR Start Date | ILR Training Price | 
        | 2-450-1-01/05/2017        | 73                | learner a | 01/05/2017     | 10000              |
    And the data lock event has the following errors:    
        | Price Episode identifier  | Error code | Error Description										                       |
        | 2-450-1-01/05/2017        | DLOCK_04   | No matching record found in the employer digital account for the framework code |
        | 2-450-1-01/05/2017        | DLOCK_05   | No matching record found in the employer digital account for the programme type |
    And the data lock event has the following periods    
        | Price Episode identifier | Period   | Payable Flag | Transaction Type |
        | 2-450-1-01/05/2017       | 1617-R10 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R11 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R12 | false        | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | framework code | programme type | pathway code | Negotiated Price | Effective Date |
        | 2-450-1-01/05/2017       | 73-125             | 01/05/2017 | 451            | 3              | 1            | 10000            | 01/05/2017     |




Scenario: DLOCK04 + DLOCK05 + DLOCK06 - When no matching record found in an employer digital account for for the framework code, programme type and pathway code then datalock DLOCK_04, DLOCK05 and DLOCK06 will be produced
    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   | status | effective from |
        | 73            | 73-125     | Provider a | learner a | 451            | 3              | 6            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
    When an ILR file is submitted with the following data:  
        | Provider   | ULN       | framework code | programme type | pathway code | start date | planned end date | completion status | Total training price | Total training price effective date |
        | Provider a | learner a | 450            | 2              | 1            | 01/05/2017 | 08/08/2018       | continuing        | 10000                | 01/05/2017                          |
    
    Then the following data lock event is returned:
        | Price Episode identifier  | Apprenticeship Id | ULN       | ILR Start Date | ILR Training Price | 
        | 2-450-1-01/05/2017        | 73                | learner a | 01/05/2017     | 10000              |
    And the data lock event has the following errors:    
        | Price Episode identifier  | Error code | Error Description										                       |
        | 2-450-1-01/05/2017        | DLOCK_04   | No matching record found in the employer digital account for the framework code |
        | 2-450-1-01/05/2017        | DLOCK_05   | No matching record found in the employer digital account for the programme type |
        | 2-450-1-01/05/2017        | DLOCK_06   | No matching record found in the employer digital account for the pathway code   |
    And the data lock event has the following periods    
        | Price Episode identifier | Period   | Payable Flag | Transaction Type |
        | 2-450-1-01/05/2017       | 1617-R10 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R11 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R12 | false        | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | framework code | programme type | pathway code | Negotiated Price | Effective Date |
        | 2-450-1-01/05/2017       | 73-125             | 01/05/2017 | 451            | 3              | 6            | 10000            | 01/05/2017     |


Scenario: DLOCK11 - When employer is not a levy payer, DLOCK11 will be raised

    Given the employer is not a levy payer
    And the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   | status | effective from |
        | 73            | 73-125     | Provider a | learner a | 450            | 2              | 1            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
    When an ILR file is submitted with the following data:  
        | Provider   | ULN       | framework code | programme type | pathway code | start date | planned end date | completion status | Total training price | Total training price effective date |
        | Provider a | learner a | 450            | 2              | 1            | 01/05/2017 | 08/08/2018       | continuing        | 10000                | 01/05/2017                          |
    
    Then the following data lock event is returned:
        | Price Episode identifier  | Apprenticeship Id | ULN       | ILR Start Date | ILR Training Price | 
        | 2-450-1-01/05/2017        | 73                | learner a | 01/05/2017     | 10000              |
    And the data lock event has the following errors:    
        | Price Episode identifier | Error code | Error Description                          |
        | 2-450-1-01/05/2017       | DLOCK_11   | The employer is not currently a levy payer |
    And the data lock event has the following periods    
        | Price Episode identifier | Period   | Payable Flag | Transaction Type |
        | 2-450-1-01/05/2017       | 1617-R10 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R11 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R12 | false        | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | framework code | programme type | pathway code | Negotiated Price | Effective Date |
        | 2-450-1-01/05/2017       | 73-125             | 01/05/2017 | 450            | 2              | 1            | 10000            | 01/05/2017     |


Scenario: DLOCK01 - When no matching record found in an employer digital account for the UKPRN then datalock DLOCK_01 will be produced

    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   | status | effective from |
        | 73            | 73-125     | Provider b | learner a | 450            | 2              | 1            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
    When an ILR file is submitted with the following data:  
        | Provider   | ULN       | framework code | programme type | pathway code | start date | planned end date | completion status | Total training price | Total training price effective date |
        | Provider a | learner a | 450            | 2              | 1            | 01/05/2017 | 08/08/2018       | continuing        | 10000                | 01/05/2017                          |
    
    Then the following data lock event is returned:
        | Price Episode identifier  | Apprenticeship Id | ULN       | ILR Start Date | ILR Training Price | 
        | 2-450-1-01/05/2017        | 73                | learner a | 01/05/2017     | 10000              |
    And the data lock event has the following errors:    
        | Price Episode identifier  | Error code | Error Description										                                    |
        | 2-450-1-01/05/2017        | DLOCK_01   | No matching record found in an employer digital account for the UKPRN                     	|
    And the data lock event has the following periods    
        | Price Episode identifier | Period   | Payable Flag | Transaction Type |
        | 2-450-1-01/05/2017       | 1617-R10 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R11 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R12 | false        | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | framework code | programme type | pathway code | Negotiated Price | Effective Date |
        | 2-450-1-01/05/2017       | 73-125             | 01/05/2017 | 450            | 2              | 1            | 10000            | 01/05/2017     |


Scenario: DLOCK02 - When no matching record found in an employer digital account for the ULN then datalock DLOCK_02 will be produced

    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   | status | effective from |
        | 73            | 73-125     | Provider a | learner b | 450            | 2              | 1            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
    When an ILR file is submitted with the following data:  
        | Provider   | ULN       | framework code | programme type | pathway code | start date | planned end date | completion status | Total training price | Total training price effective date |
        | Provider a | learner a | 450            | 2              | 1            | 01/05/2017 | 08/08/2018       | continuing        | 10000                | 01/05/2017                          |
    
    Then no data lock event is returned


Scenario: DLOCK08 - When multiple matching record found in an employer digital account then datalock DLOCK_08 will be produced

    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   | status | effective from |
        | 73            | 73-125     | Provider a | learner a | 450            | 2              | 1            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        | 74            | 74-002     | Provider b | learner a | 450            | 2              | 1            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
    When an ILR file is submitted with the following data:  
        | Provider   | ULN       | framework code | programme type | pathway code | start date | planned end date | completion status | Total training price | Total training price effective date |
        | Provider a | learner a | 450            | 2              | 1            | 01/05/2017 | 08/08/2018       | continuing        | 10000                | 01/05/2017                          |
    
    Then the following data lock event is returned:
        | Price Episode identifier  | Apprenticeship Id | ULN       | ILR Start Date | ILR Training Price | 
        | 2-450-1-01/05/2017        | 73                | learner a | 01/05/2017     | 10000              |
        | 2-450-1-01/05/2017        | 74                | learner a | 01/05/2017     | 10000              |
    And the data lock event has the following errors:    
        | Price Episode identifier  | Error code | Error Description										                                    |
        | 2-450-1-01/05/2017        | DLOCK_08   | Multiple matching records found in the employer digital account                          	|
    And the data lock event has the following periods    
        | Price Episode identifier | Period   | Payable Flag | Transaction Type |
        | 2-450-1-01/05/2017       | 1617-R10 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R11 | false        | Learning         |
        | 2-450-1-01/05/2017       | 1617-R12 | false        | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | framework code | programme type | pathway code | Negotiated Price | Effective Date |
        | 2-450-1-01/05/2017       | 73-125             | 01/05/2017 | 450            | 2              | 1            | 10000            | 01/05/2017     |
        | 2-450-1-01/05/2017       | 74-002             | 01/05/2017 | 450            | 2              | 1            | 10000            | 01/05/2017     |



Scenario: DLOCK07(a) - When price is changed, then effective to is set on previous price episode
    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   | status | effective from | effective to |
        | 73            | 73-125     | Provider a | learner a | 450            | 2              | 1            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     | 30/06/2017   |
        | 73            | 73-200        | Provider a | learner a | 450            | 2              | 1            | 15000        | 01/05/2017 | 01/05/2018 | active | 01/07/2017     |              |
        
    When an ILR file is submitted with the following data:  
        | Provider   | ULN       | framework code | programme type | pathway code | start date | planned end date | completion status | Total training price 1 | Total training price 1 effective date | Total training price 2 | Total training price 2 effective date |
        | Provider a | learner a | 450            | 2              | 1            | 01/05/2017 | 08/08/2018       | continuing        | 10000                  | 01/05/2017                            | 14000                  | 01/07/2017                            |
    
    Then the following data lock event is returned:
        | Price Episode identifier | Apprenticeship Id | ULN       | ILR Start Date | ILR Training Price | ILR Effective from | ILR Effective to |
        | 2-450-1-01/05/2017       | 73                | learner a | 01/05/2017     | 10000              | 01/05/2017         | 30/06/2017       |
        | 2-450-1-01/07/2017       | 73                | learner a | 01/05/2017     | 14000              | 01/07/2017         |                  |
    And the data lock event has the following errors:    
        | Price Episode identifier  | Error code | Error Description										                                    |
        | 2-450-1-01/07/2017        | DLOCK_07   | No matching record found in the employer digital account for the negotiated cost of training	|
    And the data lock event has the following periods    
        | Price Episode identifier | Period   | Payable Flag | Transaction Type |
        | 2-450-1-01/05/2017       | 1617-R10 | true         | Learning         |
        | 2-450-1-01/05/2017       | 1617-R11 | true         | Learning         |
        | 2-450-1-01/07/2017       | 1617-R12 | false        | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | framework code | programme type | pathway code | Negotiated Price | Effective Date |
        | 2-450-1-01/05/2017       | 73-125             | 01/05/2017 | 450            | 2              | 1            | 10000            | 01/05/2017     |
        | 2-450-1-01/07/2017       | 73-200             | 01/05/2017 | 450            | 2              | 1            | 15000            | 01/07/2017     |