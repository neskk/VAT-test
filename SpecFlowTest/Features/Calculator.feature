@Calculator
Feature: Calculator

Scenario Outline: net price
  Given I select the <country> country
  Given I select the rate of <rate>%
  When the net price is <net>
  Then the vat sum should be <vatsum>
  And the price should be <price>

  Examples:
    | country          | rate | net   | vatsum | price |
    | United Kingdom   |   20 | 10.00 |   2.00 | 12.00 |
    | United Kingdom   |   20 |  9.99 |   2.00 | 11.99 |
    | United Kingdom   |    5 | 10.00 |   0.50 | 10.50 |
    | United Kingdom   |    5 |  9.99 |   0.50 | 10.49 |
    | Portugal         |   23 | 10.00 |   2.30 | 12.30 |
    | Portugal         |   23 |  9.99 |   2.30 | 12.29 |
    | Portugal         |   13 | 10.00 |   1.30 | 11.30 |
    | Portugal         |   13 |  9.99 |   1.30 | 11.29 |
    | Portugal         |    6 | 10.00 |   0.60 | 10.60 |
    | Portugal         |    6 |  9.99 |   0.60 | 10.59 |

Scenario Outline: vat sum
  Given I select the <country> country
  Given I select the rate of <rate>%
  When the vat sum is <vatsum>
  Then the net price should be <net>
  And the price should be <price>

  Examples:
    | country          | rate | net   | vatsum | price |
    | United Kingdom   |   20 | 10.00 |   2.00 | 12.00 |
    | United Kingdom   |    5 | 10.00 |   0.50 | 10.50 |
    | Portugal         |   23 | 10.00 |   2.30 | 12.30 |
    | Portugal         |   13 | 10.00 |   1.30 | 11.30 |
    | Portugal         |    6 | 10.00 |   0.60 | 10.60 |

Scenario Outline: price
  Given I select the <country> country
  Given I select the rate of <rate>%
  When the price is <price>
  Then the net price should be <net>
  And the vat sum should be <vatsum>

  Examples:
    | country          | rate | net   | vatsum | price |
    | United Kingdom   |   20 | 10.00 |   2.00 | 12.00 |
    | United Kingdom   |   20 |  9.99 |   2.00 | 11.99 |
    | United Kingdom   |    5 | 10.00 |   0.50 | 10.50 |
    | United Kingdom   |    5 |  9.99 |   0.50 | 10.49 |
    | Portugal         |   23 | 10.00 |   2.30 | 12.30 |
    | Portugal         |   23 |  9.99 |   2.30 | 12.29 |
    | Portugal         |   13 | 10.00 |   1.30 | 11.30 |
    | Portugal         |   13 |  9.99 |   1.30 | 11.29 |
    | Portugal         |    6 | 10.00 |   0.60 | 10.60 |
    | Portugal         |    6 |  9.99 |   0.60 | 10.59 |

Scenario: Precision
  Given I select the United Kingdom country
  Given I select the rate of 20%
  When the net price is 9.999
  Then the vat sum should be 2.00
  And the price should be 12.00

Scenario: Negative Value
  Given I select the United Kingdom country
  Given I select the rate of 20%
  When the net price is -10
  Then the vat sum should be -2.00
  And the price should be -12.00

Scenario: Text Value
  Given I select the United Kingdom country
  Given I select the rate of 20%
  When the net price is test
  Then the vat sum should not be a number
  And the price should not be a number