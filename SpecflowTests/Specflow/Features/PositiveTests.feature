Feature: PositiveTests
	In order to make sure that the Web Service returns the expected results
	As a stake holder
	I want to veriy the expected outcomes of the positive scenarios

Scenario: The API is up and running
	Given I have access to the US states API
	When I execute a Get request
	Then I get a OK response

Scenario: Verify the count of all the states in the response
	Given I have access to the US states API
	When I execute a Get request
	Then I get a count of 55 records

# The Missouri test is designed to fail on purpose
Scenario Outline: Get results for valid <State> name
	Given I have entered a valid <State> name
	When I execute a Get request
	Then the response should contain the correct <Capital> and <Largest City>
	Examples: 
	| State          | Capital    | Largest City |
	| Alaska         | Juneau     | Anchorage    |
	| Missouri       | St. Louis  | Kansas City  |
	| Ohio           | Columbus   | Columbus     |
	| West Virginia  | Charleston | Charleston   |
	| American Samoa | Pago Pago  |              |

Scenario Outline: Get results for valid <State> abbrevation
	Given I have entered a valid <Abbrevation> of a State
	When I execute a Get request
	Then the response should contain the correct <Capital> and <Largest City>
	Examples: 
	| Abbrevation		| Capital			| Largest City	 |
	| AL				| Montgomery		| Birmingham	 |
	| NM				| Santa Fe			| Albuquerque	 |
	| OK				| Oklahoma City		| Oklahoma City	 |
	| VT				| Montpelier		| Burlington	 |
	| VI				| Charlotte Amalie	| 				 |