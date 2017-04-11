Feature: Datalock produces correct errors when ILR does not match commitment

Scenario: When no matching record found in an employer digital account for for the agreed price then datalock DLOCK_07 will be produced
    Given the following commitments exist:
        | commitment Id | version Id | Provider   | ULN       | standard code  | framework code | programme type | pathway code | agreed price | start date | end date   | status | effective from |
        | 73            | 125        | Provider a | learner a | 1	           | 450            | 2              | 1            | 10000        | 01/05/2017 | 01/05/2018 | active | 01/05/2017     |
        
    When an ILR file is submitted with the following data:  
        | Provider   | ULN       | framework code | programme type | pathway code | start date | planned end date | completion status | Total training price | Total training price effective date | Total assessment price | Total assessment price effective date |
        | Provider a | learner a | 450            | 2              | 1            | 01/05/2017 | 08/08/2018       | continuing        | 9000                 | 01/05/2017                          | 1010                   | 01/05/2017                            |
    
    Then the following data lock event is returned:
        | Price Episode identifier  | Apprenticeship Id | ULN       | ILR Start Date | ILR Training Price | ILR End point assessment price  | 
        | 2-450-1-01/05/2017        | 73                | learner a | 01/05/2017     | 9000               | 1010                            |
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
        | 2-450-1-01/05/2017       | 125                | 01/05/2017 | 450            | 2              | 1            | 10000            | 01/05/2017     |