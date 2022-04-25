# GLOBAL BLUE - HOMEWORK

## Background info
Calkoo introduced a simple online VAT calculator for calculating tax on their website:
`http://www.calkoo.com/?lang=3&page=8`

## User story
As a traveller, I want to calculate the various purchase/tax amounts to verify that my shop 
receipt is correct, and the merchant correctly charged the VAT for my recently bought items.


## ACCEPTANCE CRITERIA
- User must select a country which applies VAT scheme
- User must be able to choose a valid VAT rate for the selected country
- One of the following amounts are supported as an input:
	- Net
	- Gross
	- VAT
- Amounts can be entered with maximum 2 decimal digit precision
- Given all mandatory fields (country, vat rate, one of the amounts) are provided, 
the website will calculate and show the other 2 amounts which were not 
provided originally as an input value
- The API provides an error with meaningful error message, in case of:
	- negative input
	- amount >999.999.999

## TECHNICAL REQUIREMENTS
- Please document test cases (manual) that come to your mind.
- In case you encounter bugs - which one and how would you report them (provide example)
- In case there are open questions regarding requirements/acceptance criteria:
	- Document the questions you would like to ask/clarify with End-users/business/product owner based on the given information.
- Please identify at least two test cases for automation:
	- explain why you would choose specifically those
	- automate them using Cypress, Robot or Selenium (if having experience)
