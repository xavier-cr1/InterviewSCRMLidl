Feature: MessagesService

@Type:API
#Send a private message to users himself, and check if this message exists (obtain this new private message)
Scenario: Obtain a new private message sent to a user
    Given The username 'xaviercr1' receives a private message with the following properties
        | Message                          |
        | Sending to myself a test message |
    Then The status code for sending a private message in the forum is '200'
    When The username 'xaviercr1' and password 'Xavier1234.' obtains its private message list with the new message 'Sending to myself a test message'
    And The status code for obtaining the private message list is '200'