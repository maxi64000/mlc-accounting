Feature: UserIntegration

Scenario: Submit a user integration successfully
	When the submit user integration request is called
	Then the status code is 201
	And The user integration has been created