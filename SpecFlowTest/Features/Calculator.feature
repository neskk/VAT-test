Feature: Calculator

1. Navigate to calculator
2. Select Country: UK
3. Select Rate: 20%
4. Input value into `Price without VAT` (Net) input: `10.00`
5. Check value of `Value-Added Tax` (VAT): `2.00`
6. Check value of `Price incl. VAT` (Gross): `12.00`


Background: United Kingdom VAT20
	# Given I navigate to VAT calculator
	Given I select the United Kingdom country
	Given I select the rate of 20%

@Calculator
Scenario: Basic
	When the net price is 10.00
	Then the vat sum should be 2.00
	And the price should be 12.00

Scenario: Basic 2
	When the net price is 9.99
	Then the vat sum should be 2.00
	And the price should be 11.99

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