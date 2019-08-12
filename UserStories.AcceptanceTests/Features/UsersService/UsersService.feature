Feature: UsersService

@Type:API
Scenario: Create a new user
    Given The forum receives a request for creating a user with the following properties
        | Name   | Username  | Password    | Role | Email                   |
        | Xavier | xaviercr1 | Xavier1234. | QA   | xaviercasafont@test.com |
    Then The status code of the users service is '200'

@Type:API
Scenario: Get the user list
    Given The forum receives a request for obtaining the user list
    Then The status code of the users service is '200'
