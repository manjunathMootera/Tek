Feature: Negative Tests
	In order to make sure that the Web Service returns the expected results
	As a stake holder
	I want to veriy the expected outcomes of the negative scenarios

Scenario: No results for an invalid state abbrevation
	Given I have entered an invalid state abbrevation
	When I execute a Get request for an invalid state abbrevation
	Then the response should send a invalid state message

Scenario: No results for a valid state abbrevation but invalid case
	Given I have entered a valid state abbrevation in an invalid case
	When I execute a Get request for an valid state abbrevation with invalid case
	Then the response should send a invalid state message

Scenario: No results for an invalid state name
	Given I have an invalid state name
	When I execute a Get request
	Then the response will not contain the invalid state

Scenario: No results for a valid state name with special or invalid characters
	Given I have a state name with a hyphen
	When I execute a Get request
	Then the response will not contain the valid state with a hyphen

Scenario: Page not found for a request without a state 
	Given I have a request without a state
	When I execute a Get request without the state
	Then the response will contain a 404 in it