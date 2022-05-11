Feature: Calculator

1. Navigate to calculator
2. Select Country: UK
3. Select Rate: 20%
4. Input value into `Price without VAT` (Net) input: `10.00`
5. Check value of `Value-Added Tax` (VAT): `2.00`
6. Check value of `Price incl. VAT` (Gross): `12.00`


@Calculator

Background: United Kingdom VAT20
	//Given Navigate to VAT calculator
	Given Selected the United Kingdom country
	Given Selected the rate of 20%


Scenario: Basic
	Given the net price is 10.00
	Then the vat sum should be 2.00
	And the price should be 12.00

Scenario: Basic 2

	Given the net price is 9.99
	Then the vat sum should be 2.00
	And the price should be 11.99

Scenario: Precision
	Given the net price is 9.999
	Then the vat sum should be 2.00
	And the price should be 12.00

Scenario: Negative Value
	Given the net price is -10
	Then the vat sum should be -2.00
	And the price should be -12.00

Scenario: Text Value
	Given the net price is test
	Then the vat sum should not be a number
	And the price should not be a number