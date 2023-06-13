Feature: GetUser

Scenario: Get a user successfully
	Given the user already exist
	When the GetUser request is called
	Then the status code is 200
	And the response is equal to the user

Scenario: Get a user when user does not exist
	When the GetUser request is called
	Then the status code is 404