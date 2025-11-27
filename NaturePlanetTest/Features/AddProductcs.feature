Feature: AddProductcs

A short summary of the feature

@tag1
Scenario: Create product succesfully
	Given an admin wants to create a product with name "Bean Ball Tiger" and weight "250"
	When admin presses the create product button
	Then the product should exist in the database with name "Bean Ball Tiger"
