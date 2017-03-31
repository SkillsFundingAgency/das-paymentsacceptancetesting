Feature: Provider adjustments (EAS) payments

    Scenario: Payments when current and previous provider adjustments exist
        Given that the previous EAS entries for a provider are as follows:
            | Type                                                                    | 05/17  | 06/17 | year to date total |
            | 16-18 levy additional provider payments: audit adjustments              | 143.52 | 13.59 | 157.11             |
            | 16-18 non-levy additional provider payments: training authorised claims | 17.57  | 11.89 | 29.46              |
            | Adult levy training: authorised claims                                  | 501.02 | 98.14 | 599.16             |
            | Adult non-levy additional employer payments: audit adjustments          | 305.25 | 5.23  | 310.48             |
        When the following EAS entries are submitted:
            | Type                                                                    | 05/17  | 06/17 | 07/17  | year to date total |
            | 16-18 levy additional provider payments: audit adjustments              | 195.17 | 4.98  | 63.42  | 263.57             |
            | 16-18 non-levy additional provider payments: training authorised claims | 17.57  | 2.89  | 2.45   | 22.91              |
            | Adult levy training: authorised claims                                  | 475.34 | 98.14 | 65.49  | 638.97             |
            | Adult levy additional provider payments: audit adjustments              | 0      | 18.65 | 1.63   | 20.28              |
            | Adult non-levy additional employer payments: audit adjustments          | 341.25 | 5.23  | 159.34 | 505.82             |
        Then the following adjustments will be generated:
            | Type                                                                    | 05/17  | 06/17 | 07/17  | payments year to date |
            | 16-18 levy additional provider payments: audit adjustments              | 51.65  | -8.61 | 63.42  | 106.46                |
            | 16-18 non-levy additional provider payments: training authorised claims | 0      | -9.00 | 2.45   | -6.55                 |
            | Adult levy training: authorised claims                                  | -25.68 | 0     | 65.49  | 39.81                 |
            | Adult levy additional provider payments: audit adjustments              | 0      | 18.65 | 1.63   | 20.28                 |
            | Adult non-levy additional employer payments: audit adjustments          | 36.00  | 0     | 159.34 | 195.34                |


    Scenario: Payments when only current provider adjustments exist
        Given that the previous EAS entries for a provider are as follows:
            | Type                                                                    | 05/17 | 06/17 | year to date total |
            | 16-18 levy additional provider payments: audit adjustments              | 0     | 0     | 0                  |
            | 16-18 non-levy additional provider payments: training authorised claims | 0     | 0     | 0                  |
            | Adult levy training: authorised claims                                  | 0     | 0     | 0                  |
            | Adult non-levy additional employer payments: audit adjustments          | 0     | 0     | 0                  |
        When the following EAS entries are submitted:
            | Type                                                                    | 05/17  | 06/17 | 07/17  | year to date total |
            | 16-18 levy additional provider payments: audit adjustments              | 195.17 | 4.98  | 63.42  | 263.57             |
            | 16-18 non-levy additional provider payments: training authorised claims | 17.57  | 2.89  | 2.45   | 22.91              |
            | Adult levy training: authorised claims                                  | 475.34 | 98.14 | 65.49  | 638.97             |
            | Adult levy additional provider payments: audit adjustments              | 0      | 18.65 | 1.63   | 20.28              |
            | Adult non-levy additional employer payments: audit adjustments          | 341.25 | 5.23  | 159.34 | 505.82             |
        Then the following adjustments will be generated:
            | Type                                                                    | 05/17  | 06/17 | 07/17  | year to date total |
            | 16-18 levy additional provider payments: audit adjustments              | 195.17 | 4.98  | 63.42  | 263.57             |
            | 16-18 non-levy additional provider payments: training authorised claims | 17.57  | 2.89  | 2.45   | 22.91              |
            | Adult levy training: authorised claims                                  | 475.34 | 98.14 | 65.49  | 638.97             |
            | Adult levy additional provider payments: audit adjustments              | 0      | 18.65 | 1.63   | 20.28              |
            | Adult non-levy additional employer payments: audit adjustments          | 341.25 | 5.23  | 159.34 | 505.82             |
