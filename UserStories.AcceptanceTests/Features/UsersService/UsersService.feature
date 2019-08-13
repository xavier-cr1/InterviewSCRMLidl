Feature: UsersService

@Type:API
#Add user with QA role and obtain full users list (addtional meaningful, check user exists)
Scenario: Obtain a new created user for the forum in users list
    Given The forum receives a request for creating a user with the following properties
        | Name   | Username  | Password    | Role | Email                   |
        | Xavier | xaviercr1 | Xavier1234. | QA   | xaviercasafont@test.com |
    Then The status code for creating a new user is '200'
    When The forum receives a request for obtaining the user list
    And The status code for getting the users list is '200'
    Then The user with the username 'xaviercr1' is in the list
