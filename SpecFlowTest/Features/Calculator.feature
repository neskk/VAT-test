Feature: Calculator

1. Navigate to calculator
2. Select Country: UK
3. Select Rate: 20%
4. Input value into `Price without VAT` (Net) input: `10.00`
5. Check value of `Value-Added Tax` (VAT): `2.00`
6. Check value of `Price incl. VAT` (Gross): `12.00`


Background: United Kingdom VAT20
  #Given I navigate to VAT calculator
  Given I select the United Kingdom country
  Given I select the rate of 20%

@Calculator
Scenario Outline: net price
  When the net price is <net>
  Then the vat sum should be <vatsum>
  And the price should be <price>

  Examples:
    | net   | vatsum | price |
    | 10.00 |   2.00 | 12.00 |
    |  9.99 |   2.00 | 11.99 |

Scenario Outline: vat sum
  When the vat sum is <vatsum>
  Then the net price should be <net>
  And the price should be <price>

  Examples:
    | net   | vatsum | price |
    | 10.00 |   2.00 | 12.00 |
    |  9.99 |   2.00 | 11.99 |

Scenario Outline: price
  When the price is <price>
  Then the net price should be <net>
  And the vat sum should be <vatsum>

  Examples:
    | net   | vatsum | price |
    | 10.00 |   2.00 | 12.00 |
    |  9.99 |   2.00 | 11.99 |

Scenario: Precision
  When the net price is 9.999
  Then the vat sum should be 2.00
  And the price should be 12.00

Scenario: Negative Value
  When the net price is -10
  Then the vat sum should be -2.00
  And the price should be -12.00

Scenario: Text Value
  When the net price is test
  Then the vat sum should not be a number
  And the price should not be a number