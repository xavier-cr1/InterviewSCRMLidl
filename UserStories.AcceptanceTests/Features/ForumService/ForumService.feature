Feature: ForumService

@Type:API
#Create a new message for the forum with a specific theme. Obtain full forum messages list (addtional meaningful, check the message exists in theme list)
Scenario: Obtain a new message in the forum in its themes list
    Given The forum receives a request for creating a message with the following properties
        | Theme      | Subject                  | Message                                       |
        | Automation | Herokuapp api automation | I'm doing it in .net core, what do you think? |
    Then The status code for creating a message in the forum is '200'
    When The forum receives a request for obtaining the forum messages list by theme ''
    And The status code for getting the forum messages list is '200'
    Then The message with the subject 'Herokuapp api automation' is in the theme 'Automation' list
