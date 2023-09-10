Feature: User

Scenario: Get a user successfully
	Given the user already exist
	When the get user request is called
	Then the status code is 200
	And the response is equal to the user

Scenario: Get a user when user does not exist
	When the get user request is called
	Then the status code is 404
	And the response detail is equal to "This user doesn't exist."

Scenario: Create a user successfully
	When the create user request is called
	Then the status code is 201

Scenario: Create a user when null or empty name
	When the create user request is called with "" name
	Then the status code is 400
	And the response errors "Name" is equal to "This field is mandatory."

Scenario: Create a user when null or empty password
	When the create user request is called with "" password
	Then the status code is 400
	And the response errors "Password" is equal to "This field is mandatory."

Scenario: Create a user when user already exist
	Given the user already exist
	When the create user request is called
	Then the status code is 409
	And the response detail is equal to "This user already exist."

Scenario: Update a user successfully
	Given the user already exist
	When the update user request is called
	Then the status code is 204

Scenario: Update a user when null or empty name
	When the update user request is called with "" name
	Then the status code is 400
	And the response errors "Name" is equal to "This field is mandatory."

Scenario: Update a user when null or empty password
	When the update user request is called with "" password
	Then the status code is 400
	And the response errors "Password" is equal to "This field is mandatory."

Scenario: Update a user when user does not exist
	When the update user request is called
	Then the status code is 404
	And the response detail is equal to "This user doesn't exist."

Scenario: Delete a user successfully
	Given the user already exist
	When the delete user request is called
	Then the status code is 204

Scenario: Delete a user when user does not exist
	When the delete user request is called
	Then the status code is 404
	And the response detail is equal to "This user doesn't exist."