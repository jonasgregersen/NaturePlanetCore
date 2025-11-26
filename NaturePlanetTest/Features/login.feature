Feature: Login

@login
Scenario: Succesful login test
    Given i enter the username "olehansen@gmail.com" and password "123456"
    When i enter the login button
    Then i login succesfully