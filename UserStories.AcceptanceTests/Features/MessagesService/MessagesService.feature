Feature: MessagesService

Background: Retrieve blacklist database before start tests
    Given The user with username '' and password '' is authorised

@Type:API
Scenario: Obtain a new private message sent to a user
    Given The username 'xaviercr1' receives a private message with the following properties
        | Message                          |
        | Sending to myself a test message |
    Then The status code for sending a private message in the forum is '200'
    When The username 'xaviercr1' obtains its private message list
    And The status code for obtaining the private message list is '200'