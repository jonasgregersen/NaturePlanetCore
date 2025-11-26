Feature: Register user

  Scenario: Successful registration
    Given a user with email "test@example.com" and password "Password123"
    When the user tries to register
    Then the registration should be successful