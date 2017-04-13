Feature: Datalock produces correct errors when ILR does not match commitment

Scenario: When no matching record found in an employer digital account for for the agreed price then datalock DLOCK_07 will be produced
    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   | status | effective from |
        | 73            | 125        | Provider a | learner a | 450            | 2              | 1            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
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
        | 2-450-1-01/05/2017       | 1617-R10 | true         | Learning         |
        | 2-450-1-01/05/2017       | 1617-R11 | true         | Learning         |
        | 2-450-1-01/05/2017       | 1617-R12 | true         | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | framework code | programme type | pathway code | Negotiated Price | Effective Date |
        | 2-450-1-01/05/2017       | 125                | 01/05/2017 | 450            | 2              | 1            | 10000            | 01/05/2017     |



Scenario: When no matching record found in an employer digital account for for the start date then datalock DLOCK_09 will be produced
    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | standard code  | agreed price | start date | end date   | status | effective from |
        | 73            | 125        | Provider a | learner a | 23             | 10000        | 01/06/2017 | 01/05/2018 | active | 01/06/2017     |
        
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
        | 25-23-01/05/2017         | 1617-R11 | true         | Learning         |
        | 25-23-01/05/2017         | 1617-R12 | true         | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | standard code | Negotiated Price | Effective Date |
        | 25-23-01/05/2017         | 125                | 01/06/2017 | 23            | 10000            | 01/06/2017     |



Scenario: When no matching record found in an employer digital account for for the standard code then datalock DLOCK_03 will be produced
    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | standard code  | agreed price | start date | end date   | status | effective from |
        | 73            | 125        | Provider a | learner a | 21             | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
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
        | 25-23-01/05/2017         | 1617-R10 | true         | Learning         |
        | 25-23-01/05/2017         | 1617-R11 | true         | Learning         |
        | 25-23-01/05/2017         | 1617-R12 | true         | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | standard code | Negotiated Price | Effective Date |
        | 25-23-01/05/2017         | 125                | 01/05/2017 | 21            | 10000            | 01/05/2017     |


Scenario: When no matching record found in an employer digital account for for the framework code then datalock DLOCK_04 will be produced
    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | framework code | programme type | pathway code | agreed price | start date | end date   | status | effective from |
        | 73            | 125        | Provider a | learner a | 451            | 2              | 1            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
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
        | 2-450-1-01/05/2017       | 1617-R10 | true         | Learning         |
        | 2-450-1-01/05/2017       | 1617-R11 | true         | Learning         |
        | 2-450-1-01/05/2017       | 1617-R12 | true         | Learning         |
    And the data lock event used the following commitments   
        | Price Episode identifier | Apprentice Version | Start Date | framework code | programme type | pathway code | Negotiated Price | Effective Date |
        | 2-450-1-01/05/2017       | 125                | 01/05/2017 | 451            | 2              | 1            | 10000            | 01/05/2017     |