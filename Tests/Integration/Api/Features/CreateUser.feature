Feature: CreateUser

Scenario: Create a user successfully
	When the CreateUser request is called
	Then the status code is 201
	And the response header "location" contains link to get the user

Scenario: Create a user when null or empty name
	When the CreateUser request is called with "" name
	Then the status code is 400
	And the response errors "Name" is equal to "This field is mandatory."

Scenario: Create a user when null or empty password
	When the CreateUser request is called with "" password
	Then the status code is 400
	And the response errors "Password" is equal to "This field is mandatory."

Scenario: Create a user when user already exist
	Given the user already exist
	When the CreateUser request is called
	Then the status code is 409
	And the response detail is equal to "This user already exist."